using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour
{
    private List<List<DataObject>> allDialogues;
    
    public DisplayMonologue displayMonologue;
    public DisplayDialogue displayDialogue;

    [HideInInspector]
    public bool startDialogue;
    [HideInInspector]
    public bool textDisplayed; // boolean that indicates if the text has finished typing (for both monolog and dialog)
    
    public static SpeechManager instance = null;
    private bool buttonMonologAntiSpam;

    // IF "Monolog" animation is playing 
    //if(displayMonolog.animator.GetCurrentAnimatorStateInfo(0).IsName("Monolog")) {}

    // IF "Monolog" animation has finished (but still playing)
    //if (displayMonolog.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !displayMonolog.animator.IsInTransition(0)) {}


    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    private void Start()
    {
        startDialogue = false;
        allDialogues = LoadDialoguesManager.instance.allDialogues;
    }
    
    #region Monolog Manager

    public void ClickOnNextMonolog()
    {
        if (!buttonMonologAntiSpam && displayMonologue.animator.GetBool("openMonolog") && !textDisplayed)
        {
            buttonMonologAntiSpam = true;
            displayMonologue.animator.SetBool("openMonolog", false);

            if (startDialogue)
            {
                StartCoroutine(WaitCloseMonologAndDisplayNextSequenceMonolog());
            }
            // TO REDO (Not clean)
            else if (GameManager.step == 23)
            {
                GameManager.instance.dialoguesSeqFinished = false;
                GameManager.instance.blockInput = false;
            }

            Invoke("EnablebuttonMonologAntiSpam", 1f);
        }

    }

    private void EnablebuttonMonologAntiSpam()
    {
        buttonMonologAntiSpam = false;
    }

    private IEnumerator WaitCloseMonologAndDisplayNextSequenceMonolog()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.dialoguesSeqFinished = DisplayNextSequenceMonolog();
    }

    public void DisplayThoughOnObject(string thought)
    {
        displayMonologue.SetMonologPAncC(thought);
    }

    public bool DisplayNextSequenceMonolog()
    {
        bool sequenceIsFinished = false;
        if (LoadDialoguesManager.sequenceIndex < allDialogues.Count)
        {
            //Debug.Log("sequenceIndex " + sequenceIndex);
            //Debug.Log("dialogueIndex " + dialogueIndex);
            //nomInterlocuteur.text = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].character;
            StartCoroutine(displayMonologue.AnimateTextMonolog(allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].dialogue, 0.02F));
            //boiteDialogue.text = allDialogues[sequenceIndex][dialogueIndex].dialogue;

            if (LoadDialoguesManager.dialogueIndex < allDialogues[LoadDialoguesManager.sequenceIndex].Count - 1)
            {
                LoadDialoguesManager.dialogueIndex++;
            }
            else
            {
                LoadDialoguesManager.sequenceIndex++;
                LoadDialoguesManager.dialogueIndex = 0;
                sequenceIsFinished = true;
            }
        }
        else
        {
            Debug.Log("Fin des dialogues");
        }
        return sequenceIsFinished;
    }

    #endregion


    #region Dialogue Manager

    public bool DisplayNextSequenceDialogue()
    {
        bool sequenceIsFinished = false;
        if (LoadDialoguesManager.sequenceIndex < allDialogues.Count)
        {
            string nomInterlocuteur = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].character;
            string dialogue = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].dialogue;
            string spriteName = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].imgName;
            displayDialogue.SlideDialogue(nomInterlocuteur, dialogue, spriteName);
            //boiteDialogue.text = allDialogues[sequenceIndex][dialogueIndex].dialogue;

            if (LoadDialoguesManager.dialogueIndex < allDialogues[LoadDialoguesManager.sequenceIndex].Count - 1)
            {
                LoadDialoguesManager.dialogueIndex++;
            }
            else
            {
                LoadDialoguesManager.sequenceIndex++;
                LoadDialoguesManager.dialogueIndex = 0;
                sequenceIsFinished = true;
            }
        }
        else
        {
            Debug.Log("Fin des dialogues");
        }
        return sequenceIsFinished;
    }

    public void DisplayFirstSequence()
    {
        displayDialogue.ResetImages();
        displayDialogue.dialogue_Alex_Nat.SetActive(true);
        displayDialogue.animator.SetBool("dialogOpened", true);
        Invoke("DisplayNextSequenceDialogue", 1f);
        startDialogue = true;
    }

    public void HideDialog()
    {
        displayDialogue.animator.SetBool("dialogOpened", false);
        displayDialogue.DestroyAllDialogues();
        startDialogue = false;
    }
    #endregion

}
