using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// contains object data that will be accessed during runtime in-world
public class NPCData : ScriptableObject
{
    public string npcId;
    public string speakerArchetype;
    public Sprite npcSprite;
    public Sprite npcPortrait;
    public bool isFillerCharacter;

    /// <summary>
    /// Generates the npcId of filler characters
    /// </summary>
    public void GenerateId()
    {
        if (isFillerCharacter)
        {
            npcId = speakerArchetype + System.Guid.NewGuid().ToString();
        }

    }
}
