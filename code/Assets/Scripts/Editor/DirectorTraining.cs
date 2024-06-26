using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Serialization;
using UnityEngine;

/// <summary>
/// Class that learns the probabilities of dialogue based on our linedb
/// </summary>
public class DirectorTraining
{
    [UnityEditor.MenuItem("Tools/Training/Test")]
    public static void Test()
    {
        WetGrassSprinklerRain wgsr = new WetGrassSprinklerRain();

        wgsr.Run();
    }

    public static int RelStrToInt(string rel)
    {
        if (rel == DirectorConstants.REL_STATUS_STRING.GOOD)
        {
            return (int)DirectorConstants.REL_STATUS_NUMS.GOOD;
        }
        else if (rel == DirectorConstants.REL_STATUS_STRING.BAD)
        {
            return (int)DirectorConstants.REL_STATUS_NUMS.BAD;
        }
        else if(rel == DirectorConstants.REL_STATUS_STRING.NEUTRAL)
        {
            return (int)DirectorConstants.REL_STATUS_NUMS.NEUTRAL;
        }

        return (int)DirectorConstants.REL_STATUS_NUMS.NONE;
    }

    [UnityEditor.MenuItem("Tools/Training/Generate algorithms")]
    public static void GetAlgorithms()
    {

        // load events
        Dictionary<int, string> eventsDB = IdCollection.LoadArrayFromPath("Resources/" + Director.EVENTS_XML_PATH + ".xml");
        Dictionary<int, string> traitsDB = IdCollection.LoadArrayFromPath("Resources/" + Director.TRAITS_XML_PATH + ".xml");

        Debug.Log("events " + eventsDB.Count);
        Debug.Log("traits" + traitsDB.Count);

        // initialize the lineDb
        Dictionary<int, DialogueLine> lineDB = DialogueLineCollection.LoadAllFromPath(new string[] {
            "Resources/XMLs/dialogue/dialoguePlayer.xml",
            "Resources/XMLs/dialogue/dialogueJonathan.xml",
            "Resources/XMLs/dialogue/dialogueCassandra.xml",
            "Resources/XMLs/dialogue/dialogueFiller_Custodian.xml",
            "Resources/XMLs/dialogue/dialogueFiller_Assistant.xml"
        });

        Debug.Log("LINE SIZE " + lineDB.Count);

        // initialize the model
        DirectorModel model = new DirectorModel(eventsDB.Count, traitsDB.Count, lineDB.Count, DirectorConstants.MAX_REL_STATUS, new Microsoft.ML.Probabilistic.Algorithms.ExpectationPropagation());

        model.GenerateAlgorithms(
            Director.NumKeyLookUp(DirectorConstants.GAME_IS_ACTIVE, refDict: eventsDB),
            Director.NumKeyLookUp(DirectorConstants.NONE_STR, refDict: traitsDB));
    }

    /// <summary>
    /// Gets the conditional probability table values of the dialogue given all possible values. 
    /// We use all the lines in our linedb as our "observations" or sample.
    /// </summary>
    [UnityEditor.MenuItem("Tools/Training/Infer initial Dialogue CPTs")]
    public static void GetDialogueCPT()
    {
        // load events
        Dictionary<int, string> eventsDB = IdCollection.LoadArrayFromPath("Resources/"+Director.EVENTS_XML_PATH+".xml");
        Dictionary<int, string> traitsDB = IdCollection.LoadArrayFromPath("Resources/"+Director.TRAITS_XML_PATH + ".xml");

        Debug.Log("events "+eventsDB.Count);
        Debug.Log("traits" + traitsDB.Count);
        
        // initialize the lineDb
        Dictionary<int, DialogueLine> lineDB = DialogueLineCollection.LoadAllFromPath(new string[] {
            "Resources/XMLs/dialogue/dialoguePlayer.xml",
            "Resources/XMLs/dialogue/dialogueJonathan.xml",
            "Resources/XMLs/dialogue/dialogueCassandra.xml",
            "Resources/XMLs/dialogue/dialogueFiller_Custodian.xml",
            "Resources/XMLs/dialogue/dialogueFiller_Assistant.xml"
        });

        // read the speakers csv
        List<Dictionary<string, string>> speakerData = Editor.ReadCSVFile("Assets/Data/CSV/speakers.csv");

        Debug.Log("LINE SIZE " + lineDB.Count);

        // initialize the model
        DirectorModel model = new DirectorModel(eventsDB.Count, traitsDB.Count, lineDB.Count, DirectorConstants.MAX_REL_STATUS, new Microsoft.ML.Probabilistic.Algorithms.ExpectationPropagation(), "DirectorTraining");

        // data is first set as uniform here.
        DirectorData data = model.UniformDirectorData();

        List<int> lineObservations = new List<int>();
        List<int> eventObservations = new List<int>();
        List<int> traitObservations = new List<int>();
        List<int> relObservations = new List<int>();

        string prev_speaker = null;

        // compiling our observations from lineDb
        foreach (KeyValuePair<int, DialogueLine> dlPair in lineDB)
        {
            /*
             *  here we compile each line and their requirements as samples
             *      The ith element in each list lineObservations, eventObservations, traitObservations, and relObservations
             *      represent the same row within the cpt kung baga
             *      
             *      Basically it looks like this:
             *         | lineObs  | evObs | traitObs | relObs |  => these cols are the list above, passed into param learning
             *       1 |    obs   | obs   |    obs   |  obs   |  => the values (integer form) of the observation
             *       2 |    obs   | obs   |    obs   |  obs   |
             *     ... |    obs   | obs   |    obs   |  obs   |
             */

            // for eazy access
            DialogueLine line = dlPair.Value;
            int lineId = dlPair.Key;

            Debug.Log("line: " + line.dialogue);

            // change current speaker
            string speaker = line.speakerId;
            // access the dictionary of the specific speaker we're at.
            Dictionary<string, string> currentSpeakerData = speakerData.FirstOrDefault(s => s["speakerArchetype"].Contains(speaker));

            // compare the speaker of the previous line and the current line
            // if different sila, this means last line na ng prev speaker and we make inferences for the speaker na.
            if (prev_speaker != null && speaker != prev_speaker)
            {
                // make inferences for the previous speaker
                LearnAndSerialize(lineObservations, eventObservations, traitObservations, relObservations, prev_speaker + "_lineCPT.xml", model, data);

                // reset the data
                lineObservations.Clear();
                eventObservations.Clear();
                traitObservations.Clear();
                relObservations.Clear();

            }

            // we get ALL event requirements of the line, and convert them to their respective key
            List<int> prereqEvents = null;
            if(line.relatedEvents != null && line.relatedEvents.Length > 0)
            {
                prereqEvents = new List<int>();
                // converting each related event to respective key...
                line.relatedEvents.ToList().ForEach(
                    e => prereqEvents.Add(
                        Director.NumKeyLookUp(e, refDict: eventsDB)));

                prereqEvents.RemoveAll(e => e == -1);   // remove the prereq events where e is -1
            }
            else
            {
                Debug.Log("no prerequisites -- line's related events field is NULL");
            }

            /*
             *  Getting trait and relationship info
             */
            List<int> traitPrereqs = new List<int>();
            int[] relPrereqs;
            int relPrereq = RelStrToInt(line.relPrereq);

            // get all traits  IF MAY PREREQS
            if(line.traitPrereq != null && line.traitPrereq.Length > 0)
            {
                //Debug.Log("Getting trait prerequisites");
                line.traitPrereq.ToList().ForEach(
                    t => traitPrereqs.Add(
                        Director.NumKeyLookUp(t, refDict: traitsDB)));

                // remove empty or invalid
                traitPrereqs.RemoveAll(t => t == -1);
            }

            if (traitPrereqs.Count == 0 && new List<string> { "main_cassandra", "main_jonathan", DirectorConstants.PLAYER_STR }.Contains(line.speakerId))
            {
                Debug.Log("char is " + line.speakerId);
                Debug.Log("no traits prerequisites -- uese NONE for main chars");
                traitPrereqs.Add(Director.NumKeyLookUp(DirectorConstants.NONE_STR, refDict: traitsDB));
            }
            else if(traitPrereqs.Count == 0)
            {
                Debug.LogWarning("No trait prerequisite -- will infer on all traits OF THE CHARACTER");
                Debug.Log("traits of character: " + speaker + " is " + currentSpeakerData["speakerTraits"]);

                // add all traits of the speaker and only the traits of the speaker ONLY
                currentSpeakerData["speakerTraits"]
                    .Split('/')
                    .ToList()
                    .ForEach(t => traitPrereqs.Add(Director.NumKeyLookUp(t, refDict: traitsDB)));

                //traitsDB.Keys.ToList().ForEach(t => traitPrereqs.Add(t));
            }


            if (relPrereq == (int)DirectorConstants.REL_STATUS_NUMS.NONE)
            {
                Debug.Log("no required relstatus");
                // consider all possible relationships
                relPrereqs = new int[]
                {
                    (int)DirectorConstants.REL_STATUS_NUMS.GOOD,
                    (int)DirectorConstants.REL_STATUS_NUMS.BAD,
                    (int)DirectorConstants.REL_STATUS_NUMS.NEUTRAL
                };
            }
            else
            {
                relPrereqs = new int[] { relPrereq };
            }

            // we add each possible observation that can make this dialogue line appear
            // note that when the dialogue line has an empty trait or relationship field, this just means that
            // all other traits and relationships are to be considered -- there is no requirement, so any trait/rel goes.
            foreach(int trait in traitPrereqs)
            {
                Debug.Log("trait of line for character " + speaker + " is " + traitsDB[trait]);
                foreach(int rel in relPrereqs)
                {
                    if(prereqEvents != null && prereqEvents.Count > 0)
                    {
                        // for every related event
                        // we observe that the line with id lineId is the effect
                        // and we also observe that to get said lineId, we also have the trait listed in the Dline and relationship.
                        foreach (int ev in prereqEvents)
                        {
                            // testing
                            if(ev >= eventsDB.Count || ev < 0)
                            {
                                Debug.Log("an event is somehow an invalid index. number: "+ev);
                            }
                            lineObservations.Add(lineId);
                            traitObservations.Add(trait);
                            relObservations.Add(rel);
                            eventObservations.Add(ev);
                        }
                    }
                    else
                    {
                        Debug.Log("Here we consider all events.");

                        // events is empty / null observation / gameisactive
                        
                        lineObservations.Add(lineId);
                        traitObservations.Add(trait);
                        relObservations.Add(rel);
                        eventObservations.Add(Director.NumKeyLookUp(DirectorConstants.GAME_IS_ACTIVE, refDict:eventsDB));

                    }
                }
            }

            // set the current speaker to be the prev speaker
            prev_speaker = line.speakerId;
        }

        // for last speaker.
        LearnAndSerialize(lineObservations, eventObservations, traitObservations, relObservations, prev_speaker + "_lineCPT.xml", model, data);
    }

    public static void LearnAndSerialize(List<int> lineObservations, 
        List<int> eventObservations, 
        List<int> traitObservations, 
        List<int> relObservations,
        string fname,
        DirectorModel model,
        DirectorData data)
    {

        string path = "Assets/Resources/XMLs/dialogue/";

        // should have same length
        Debug.Log("LENGTHS OF THE OBSERVATIONS:\n" +
            $"line {lineObservations.Count}\n" +
            $"event {eventObservations.Count}\n" +
            $"trait {traitObservations.Count}\n" +
            $"rel {relObservations.Count}\n");

        // once all lines are finished, we convert them to array and make inferences.
        // all of these must have the SAME length. this is the overall row count of our "table"
        int[] lineArr = lineObservations.ToArray();
        int[] evArr = eventObservations.ToArray();
        int[] traitArr = traitObservations.ToArray();
        int[] relArr = relObservations.ToArray();


        // make inferences here, based on uniform base data.
        model.Learn(lineArr, evArr, traitArr, relArr, data);
        // return the inference as director data
        DirectorData learned = model.DataFromPosteriors();

        SerializeCPT<Dirichlet[][][]>(path, fname, learned.dialogueProb);
    }

    public static void SerializeCPT<T>(string path, string fname, object toSerialize)
    {
        T distribution = (T)toSerialize;

        DataContractSerializer serializer = new DataContractSerializer(typeof(T),
            new DataContractSerializerSettings
            {
                DataContractResolver = new InferDataContractResolver()
            });

        using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(new FileStream(path + fname, FileMode.Create)))
        {
            serializer.WriteObject(writer, distribution);
        }
    }
}
