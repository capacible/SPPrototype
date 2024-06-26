using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

/*
 * 
 * HOW THIS WILL WORK
 * (1) We convert the CSV of speakers to one XML
 *      (1.1) XML format is:
 *              <SpeakerCollection>
 *                  <Speakers>
 *                      <Speaker speakerArchetype="">
 *                          <displayName></displayname>
 *                          
 *                          <!-- the next parts are for the NPC data...>
 *                          
 *                          <isFillerCharacter> bool </isFillerCharacter>
 *                          <speakerTraits>
 *                              <Trait> string </Trait>
 *                          </>
 *                          
 *                      </Speaker>
 *                  </>
 *              </>
 * 
 */



// data container of the NPC to be attached into the NPC object
public class Speaker
{
    public static readonly string CPT_PATH = "XMLs/Dialogue/";
    public static readonly string CPT_FILE_NAME = "_lineCPT";

    public string speakerArchetype;             // archetype of speaker aka speaker tag

    public string displayName;                  // display name of speaker

    public bool isFillerCharacter;
    
    /*
     * The following are not a part of the speaker sheet; this is updated during runtime
     */
    [XmlIgnore]
    public List<string> speakerMemories = new List<string>();

    [XmlIgnore]
    public int? relWithPlayer = 0;                    // value relationship with player -- not the numerical rep of gud/bad/neut

    [XmlIgnore]
    public string speakerId = "";                    // id related to the game object.

    // because speaker traits can be randomized, this is not read during runtime. instead, we add this to the NPC data that
    // will be attached to the gameobject
    [XmlIgnore]
    public int speakerTrait = -1;

    [XmlIgnore]
    public Dictionary<string, double> topics = new Dictionary<string, double>();

    [XmlIgnore]
    public List<int> queriedMemories = new List<int>();

    [XmlIgnore]
    public string spawnLocation;

    [XmlIgnore]
    public int currentRelStatus;

    [XmlIgnore]
    public List<double> currentPosteriors = new List<double>(); // the list form, list of resulting probabilities when running the algo

    [XmlIgnore]
    public Dirichlet[][][] currentDialogueCPT; // cpt to use (prior)
            
    public void InitializeTopics(IdCollection topicColl, double initialVal)
    {
        foreach(string topic in topicColl.allIds)
        {
            topics.Add(topic, initialVal);
        }
    }

    public void LoadSpeakerDefaultCPT()
    {
        currentDialogueCPT = DeserializeCPT<Dirichlet[][][]>(CPT_PATH + speakerArchetype + CPT_FILE_NAME);
    }

    /// <summary>
    /// Prioritizes all topics in topicarr and slightly reduces the relevance of the other priority topics.
    /// </summary>
    /// <param name="topicarr"></param>
    public void PrioritizeTopics(params string[] topicarr)
    {

        foreach (string topic in topics.Keys.ToList())
        {
            if (topics[topic] == DirectorConstants.TOPIC_RELEVANCE_HIGH)
            {
                // reduce the value of said priority topic by a bit.
                topics[topic] -= 0.1;
            }
        }

        // then make the topics in topicarr the only priority
        foreach (string topic in topicarr)
        {
            topics[topic] = (double)DirectorConstants.TOPIC_RELEVANCE_HIGH;

            Debug.Log($"For {speakerArchetype}, we have set the topic {topic} as prio");
        }

    }

    public Speaker Clone()
    {
        Speaker newSpeaker = new Speaker
        {
            speakerId = speakerId,
            speakerArchetype = speakerArchetype,
            relWithPlayer = relWithPlayer,
            speakerMemories = new List<string>(speakerMemories),
            displayName = displayName,
            isFillerCharacter = isFillerCharacter,
            topics = new Dictionary<string, double>(topics),
            currentDialogueCPT = currentDialogueCPT,    // from speaker default yung cpt, cocopy lang
            currentPosteriors = currentPosteriors,
            spawnLocation = SceneUtility.currentScene
        };

        newSpeaker.currentRelStatus = newSpeaker.RelationshipStatus();

        return newSpeaker;
    }

    /// <summary>
    /// Deserializes an XML file in a given path
    /// </summary>
    /// <typeparam name="T"> type to deserialize into </typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T DeserializeCPT<T>(string path)
    {
        if (path.Contains("CPT") || path.Contains(".xml"))
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T), new DataContractSerializerSettings { DataContractResolver = new InferDataContractResolver() });

            TextAsset cpt = (TextAsset)Resources.Load(path);

            using (var reader = XmlReader.Create(new StringReader(cpt.text)))
            {
                // deserialize/ read the distribution
                return (T)serializer.ReadObject(reader);
            }
        }

        Debug.LogWarning("Invalid path.");
        return default;
    }

    /// <summary>
    /// Initializes the priors of the model
    /// </summary>
    /// <param name="model"></param>
    public void InitializeSpeakerCPT(DirectorModel model, List<int> globalEvs = null)
    {

        Debug.Log("num of globals on init for floo1 charas (should be 2: " + globalEvs.Count);

        int[] traitarr = new int[globalEvs.Count];
        int[] relarr = new int[globalEvs.Count];

        var rel = RelationshipStatus();
        var trait = speakerTrait;

        /// FOR PLAYER CASE
        if (trait == -1)
        {
            trait = Director.NumKeyLookUp(DirectorConstants.NONE_STR, fromTraits: true);
        }

        if (rel == -1)
        {
            relarr = null;
        }
        /////////////////////

        for (int i = 0; i < globalEvs.Count; i++)
        {
            traitarr[i] = trait;
            if (relarr != null)
            {
                relarr[0] = rel;
            }
        }

        model.UpdateSpeakerDialogueProbs(globalEvs.ToArray(), 
            traitarr, 
            relarr, 
            ref currentPosteriors,
            ref currentDialogueCPT);

        Debug.Log("probabilities updated, average: " + currentPosteriors.Average());
    }

    public void OverrideTraits(NPCData npc)
    {
        Debug.Log("overriding traits... of character "+speakerArchetype+" is filler? "+isFillerCharacter);
        if (isFillerCharacter)
        {
            // randomize a trait.
            // get the speaker traits in
            // lookup id of traits from director.
            // exclude last index which is the NONE str
            speakerTrait = Director.NumKeyLookUp(npc.speakerTraits[Random.Range(0, npc.speakerTraits.Count - 1)], fromTraits: true);
            Debug.Log("npc: " + speakerArchetype + " has the trait id of " + speakerTrait);
        }
        else
        {
            speakerTrait = Director.NumKeyLookUp(DirectorConstants.NONE_STR, fromTraits:true);
        }
    }

    public void OverrideDisplayName(string displayNameOverride)
    {
        if(displayNameOverride != "")
        {
            displayName = displayNameOverride;
        }
    }

    /// <summary>
    /// This returns a numerical version of good/bad/neutral based on thresholds
    /// </summary>
    /// <returns></returns>
    public int RelationshipStatus()
    {
        if(relWithPlayer == null)
        {
            return (int)DirectorConstants.REL_STATUS_NUMS.NONE;
        }

        // good
        if(relWithPlayer >= (int)DirectorConstants.REL_STATUS_NUMS.GOOD_THRESH)
        {
            return (int)DirectorConstants.REL_STATUS_NUMS.GOOD;
        }
        else if(relWithPlayer <= (int) DirectorConstants.REL_STATUS_NUMS.BAD_THRESH)
        {
            return (int)DirectorConstants.REL_STATUS_NUMS.BAD;
        }

        return (int)DirectorConstants.REL_STATUS_NUMS.NEUTRAL;
    }
}

[XmlRoot("SpeakerCollection")]
public class SpeakerCollection
{
    [XmlArray("Speakers"), XmlArrayItem("Speaker")]
    public Speaker[] Speakers;
}