using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// in charge of showing the dialogue
public class DialogueUi : MonoBehaviour
{
    // ui elements
    public Canvas canvas;
    public Image textBox;
    public Image charPortrait;
    public Text dialogueText;
    public Text charName;
    public Button[] choices;
    public Button nextButton;
    public Animator anim;
    public Sprite playerPortrait;

    // internal elements
    private int maxChoiceCount;         // acquired at initialization from how many buttons we added @ editor.
    private Text[] choiceText;          // used to directly reference the Text component of the choices buttons (para di na mag getComponent)
    private List<DialogueLine> playerChoices;
    private Queue<string> retLine;           // the line returned by the manager/director.

    // list of portraits that current npc will use.
    private List<Sprite> portraitList;
    
    void Awake()
    {
        // subscriptions
        EventHandler.TriggeredDialogue += ShowUi;
        EventHandler.FoundNPCLine += ShowNPCDialogue;
        EventHandler.FoundPlayerLines += ShowPlayerChoices;

        Init();
    }

    private void OnDisable()
    {
        EventHandler.TriggeredDialogue -= ShowUi;
        EventHandler.FoundNPCLine -= ShowNPCDialogue;
        EventHandler.FoundPlayerLines -= ShowPlayerChoices;
    }

    private void Init()
    {
        canvas.worldCamera = Camera.main;

        maxChoiceCount = choices.Length;
        anim.SetBool("isActive", false);

        // acquire the text component of each button and initialize + deact.
        choiceText = new Text[maxChoiceCount];
        playerChoices = new List<DialogueLine>();

        for(int i = 0; i < maxChoiceCount; i++)
        {
            Debug.Log("initialized text of choice: " + i);
            choiceText[i] = choices[i].GetComponentInChildren<Text>();
            choices[i].gameObject.SetActive(false);

            // initialize player choice until max of 3 (exclude leave option)
            if (i < maxChoiceCount)
            {
                playerChoices.Add(new DialogueLine());
            }
        }
    }


    /// <summary>
    /// A makeshift constant dictionary that basically converts our more "verbose" portrait tags in writing
    /// into numerical versions that are more easily used in Unity spritesheets (spritesheet slices are named with _x at the end)
    /// </summary>
    /// <param name="portraitTag"></param>
    /// <returns></returns>
    public static int PortraitNum(string portraitTag)
    {
        switch (portraitTag)
        {
            case "neutral": return 0;
            case "happy": return 1;
            case "sad": return 2;
            case "angry": return 3;
        }

        Debug.LogWarning("No appropriate tag found");
        return 0;
    }

    /// <summary>
    /// Shows the textbox when the dialogue is triggered.
    /// </summary>
    /// <param name="obj"></param>
    public void ShowUi(object[] obj)
    {
        NPCData npc = (NPCData) obj[0];
        charPortrait.sprite = npc.dialoguePortraits[0];

        // copy dialogue portraits from npc into our current list of portraits
        portraitList = new List<Sprite>(npc.dialoguePortraits);

        // animate dialogue box entrance
        anim.SetBool("isActive", true);

    }

    /// <summary>
    // changes the text of the textbox to be NPC dialogue.
    /// </summary>
    /// <param name="data"> includes dialogue line, active npc display name, and (optional) portrait emotion </param>
    public void ShowNPCDialogue(object[] data)
    {
        string npcName = (string)data[0];
        string npcLine = (string)data[1];
        string emote = (string)data[2]; // is simple emption

        retLine = new Queue<string>(npcLine.Split("\n", System.StringSplitOptions.RemoveEmptyEntries));

        Debug.Log("Number of subsequent lines: " + retLine.Count);

        // set values
        charName.text = npcName;
        dialogueText.text = retLine.Dequeue();  // we dequeue (fifo) the topmost line.

        // convert last element of portraitFile name (w/c is the tag) to number and get the sprite that contains
        // that number.
        charPortrait.sprite = portraitList[PortraitNum(emote)];
        
        // test log
        Debug.Log("set sprite to: " + charPortrait.sprite.name + " done");

        // show next
        nextButton.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// display the player choices in buttons
    /// </summary>
    /// <param name="allChoices">a list of choice values</param>
    public void ShowPlayerChoices(List<string> allChoices)
    {
        // set the buttons to be active
        // the last choice is always active though
        choices[maxChoiceCount - 1].gameObject.SetActive(true);

        Debug.Log("count of allchoices:" + allChoices.Count);

        // we change the text of the choice buttons to be the choicces we acquired from the manager.
        for (int i = 0; i < allChoices.Count; i++)
        {
            Debug.Log(i);
            // set the button as active
            choices[i].gameObject.SetActive(true);
            // get the text of ith element of player choices, assign it to the ith button
            choiceText[i].text = allChoices[i];
        }
    }


    /// <summary>
    /// this will get invoked when a player makes a choice or clicks on a choice. each button (max 3) has their correspinding indices in player choices array
    /// </summary>
    /// <param name="index">the value of the button in the form of its index sa array</param>
    public void SelectChoice(int index)
    {
        // hide buttons
        foreach (Button b in choices)
        {
            b.gameObject.SetActive(false);
        }

        // change portrait to npc
        charPortrait.sprite = portraitList[0];

        // when choice is selected, call event handler to trigger onDialogueSelected
        // we pass the index of the button selected w/c is representative of the order of the lines we return.
        EventHandler.Instance.DisplayNPCLine(index);
    }

    /// <summary>
    /// hide unnecessary stuff then call the next dialogue handler.
    /// </summary>
    public void NextButton()
    {
        // As long as may laman pa, we keep dequeueing
        if(retLine.TryDequeue(out string line))
        {
            dialogueText.text = line;
        }
        else if (!Director.isActive && !InkDialogueManager.isActive)
        {
            Debug.Log("Exiting dialogue because neither Director nor InkDManager is active.");

            // if the director isn't active and ink dialogue manager is not active, it's obvious that there's nothing
            // more to say.
            EventHandler.Instance.ConcludeDialogue();
        }
        else
        {
            // clear dialogue text and set new char name
            dialogueText.text = "";
            charName.text = "You";

            // set new portrait
            charPortrait.sprite = playerPortrait;

            // set the next button to be inactive
            nextButton.gameObject.SetActive(false);

            EventHandler.Instance.DisplayPlayerLines();
        }
    }

    public void ConcludeButton()
    {
        // animate out
        anim.SetBool("isActive", false);

        // inactive all buttons and the exit
        foreach (Button b in choices)
        {
            b.gameObject.SetActive(false);
        }

        EventHandler.Instance.ConcludeDialogue();
    }
}
