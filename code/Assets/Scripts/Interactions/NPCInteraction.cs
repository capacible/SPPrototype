using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls NPC movement and other parts
// NPCs will be changed into prefabs once database reading at start of game is implemented.
public class NPCInteraction : InteractionBase
{
    public NPCData npc;                 // holds archetype id and other info for instantiating sa Director

    // display name override
    public string npcDisplayName;       // display name

    void Start()
    {

        SetSprite();
        InitializeInteraction();

        if (objId == "")
        {
            Debug.LogError("Id field is empty. Please generate an identifier for this NPC object");
        }
        else
        {
            // upon starting, we check if this NPC object is already in the allspeaker dictionary; ensure that we don't
            // add non-directed npcs here
            if (npc.usesDirector && !Director.SpeakerExists(objId))
            {
                Debug.Log("adding NPC: " + npc.speakerArchetype + " id of: " + objId);
                // we add the npc into the allSpeakers list if it doesnt exist yet
                Director.AddNewSpeaker(npc, objId, npcDisplayName);
            }
        }

        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    /// <summary>
    /// When player interacts with this, we trigger a dialogue.
    /// </summary>
    /// <param name="interactParams"></param>
    public override void HandleInteraction(object[] interactParams)
    {
        string id = interactParams[0].ToString();

        if(id == objId)
        {
            // call dialogue trigger and set item name.
            if(InventoryHandler.CurrentItem()!=null)
                Director.activeHeldItem = InventoryHandler.CurrentItem().itemId;

            EventHandler.Instance.LoadDialogueScene(new object[] { objId, new NPCData[] { npc } });
        }
    }

    /// <summary>
    /// Gets the spriterenderer component of the gameobject of this script and modifies the default sprite to be
    /// the actual sprite.
    /// </summary>
    private void SetSprite()
    {

        if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
        {
            if (npc.worldSprite == null)
            {
                Debug.LogWarning("speakerSprite for " + gameObject.name + " does not exist.");
            }
            else
            {
                // change sprite from npc default to what's attached to the NPC so
                s.sprite = npc.worldSprite;
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found.");
        }
    }
    
    /// <summary>
    /// Generates an id for the NPC object exactly once.
    /// </summary>
    [ContextMenu("Generate Id for NPC")]
    public override void GenerateId()
    {
        if (npc != null)
        {
            objId = "NPCInteraction_";

            if (npc.speakerArchetype == "")
            {
                objId += UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_" + npc.name + "_x";
                return;
            }

            // we have a map/scene name + speaker archetype + object tag
            objId += UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_" + npc.speakerArchetype + "_x";
            return;
        }

        Debug.LogError("No NPC data attached to this controller yet.");
    }
}
