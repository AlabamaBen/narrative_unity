﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour
{
    private List<DataObject> dialogueSequenceTemp;
    private List<List<DataObject>> allDialogues;
    
    public DisplayMonologue displayMonologue;
    public DisplayDialogue displayDialogue;

    [HideInInspector]
    public bool startDialogue;
    [HideInInspector]
    public bool textDisplayed; // boolean that indicates if the text has finished typing (for both monolog and dialog)
    
    public static SpeechManager instance = null;
    private bool buttonMonologAntiSpam;

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
        dialogueSequenceTemp = LoadDialoguesManager.instance.dialogueSequenceTemp;
        allDialogues = LoadDialoguesManager.instance.allDialogues;
    }

    private void Update()
    {
        // IF "Monolog" animation is playing 
        //if(displayMonolog.animator.GetCurrentAnimatorStateInfo(0).IsName("Monolog")) {}

        // IF "Monolog" animation has finished (but still playing)
        //if (displayMonolog.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !displayMonolog.animator.IsInTransition(0)) {}


        /*
        if (Input.anyKeyDown && !textDisplayed)
        {
            foreach(GameObject msg in messagesList)
            {
                msg.GetComponent<MoveMessageBox>().targetPosition = msg.transform.position + Vector3.up * 100;
            }

            GameObject currentMsg = Instantiate(messageBox_Temp_Natyahs);
            currentMsg.transform.SetParent(messagesParent.transform);
            currentMsg.transform.position = messageBox_Temp_Natyahs.transform.position;
            currentMsg.transform.localScale = Vector3.one;
            currentMsg.SetActive(true);
            //currentMsg.GetComponentInChildren<Text>().text = "Salut";
            messagesList.Add(currentMsg);
            StartCoroutine(AnimateTextDialog(currentMsg.GetComponentInChildren<Text>(), "Salut Ca va ?", 0.02F));

            if (messagesList.Count > 5)
            {
                GameObject msgToDestroy = messagesList[0];
                messagesList.Remove(msgToDestroy);
                Destroy(msgToDestroy);
            }
        }*/
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
        displayMonologue.SetMonolog(thought);
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
            displayDialogue.dialogue_Alex_Nat.SetActive(true);
            //Debug.Log("sequenceIndex " + sequenceIndex);
            //Debug.Log("dialogueIndex " + dialogueIndex);
            string nomInterlocuteur = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].character;
            string dialogue = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].dialogue;
            displayDialogue.SlideDialogue(nomInterlocuteur, dialogue);
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

}