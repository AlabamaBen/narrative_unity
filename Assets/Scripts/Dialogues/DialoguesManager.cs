using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesManager : MonoBehaviour
{
    private List<DataObject> dialogueSequenceTemp;
    private List<List<DataObject>> allDialogues;

    [HideInInspector]
    public DisplayMonolog displayMonolog;


    [Header("Dialogue_Alex_Nat")]
    public GameObject dialogue_Alex_Nat;
    public GameObject messageBox_Temp_Natyahs;
    public GameObject messageBox_Temp_Alex;
    public GameObject messagesParent;
    private List<GameObject> messagesList;
    private string stringToDisplay; // dynamic string that is displayed
    public float text_speed = 0.02f;
    [Header("SFX sound")]
    public SFXSound talk_sound;

    [HideInInspector]
    public bool startDialogue;
    [HideInInspector]
    public bool textDisplayed; // boolean that indicates if the text has finished typing (for both monolog and dialog)
    
    public static DialoguesManager instance = null;
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
        messagesList = new List<GameObject>();
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
        if (!buttonMonologAntiSpam && displayMonolog.animator.GetBool("openMonolog") && !textDisplayed)
        {
            buttonMonologAntiSpam = true;
            displayMonolog.animator.SetBool("openMonolog", false);

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
        displayMonolog.SetMonolog(thought);
    }

    public bool DisplayNextSequenceMonolog()
    {
        bool sequenceIsFinished = false;
        if (LoadDialoguesManager.sequenceIndex < allDialogues.Count)
        {
            //Debug.Log("sequenceIndex " + sequenceIndex);
            //Debug.Log("dialogueIndex " + dialogueIndex);
            //nomInterlocuteur.text = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].character;
            StartCoroutine(displayMonolog.AnimateTextMonolog(allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].dialogue, 0.02F));
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


    public bool DisplayNextSequenceDialog()
    {
        bool sequenceIsFinished = false;
        if (LoadDialoguesManager.sequenceIndex < allDialogues.Count)
        {
            dialogue_Alex_Nat.SetActive(true);
            //Debug.Log("sequenceIndex " + sequenceIndex);
            //Debug.Log("dialogueIndex " + dialogueIndex);
            string nomInterlocuteur = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].character;
            string dialogue = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].dialogue;
            SlideDialog(nomInterlocuteur, dialogue);
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

    private void SlideDialog(string interlocuteur, string text)
    {
        foreach (GameObject msg in messagesList)
        {
            msg.GetComponent<MoveMessageBox>().targetPosition = msg.transform.position + Vector3.up * 100;
        }

        GameObject currentMsg = null;
        if (interlocuteur.Equals("Natyahs"))
        {
            currentMsg = Instantiate(messageBox_Temp_Natyahs);
            currentMsg.transform.position = messageBox_Temp_Natyahs.transform.position;
        }
        else if (interlocuteur.Equals("Alex"))
        {
            currentMsg = Instantiate(messageBox_Temp_Alex);
            currentMsg.transform.position = messageBox_Temp_Alex.transform.position;
        }
        else
        {
            Debug.Log("Error in character name");
            return;
        }

        currentMsg.transform.SetParent(messagesParent.transform);
        currentMsg.transform.localScale = Vector3.one;
        currentMsg.SetActive(true);
        messagesList.Add(currentMsg);
        StartCoroutine(AnimateTextDialog(currentMsg.GetComponentInChildren<Text>(), text, 0.02F));

        if (messagesList.Count > 5)
        {
            GameObject msgToDestroy = messagesList[0];
            messagesList.Remove(msgToDestroy);
            Destroy(msgToDestroy);
        }
    }

    IEnumerator AnimateTextDialog(Text textBox, string strComplete, float speed)
    {
        textDisplayed = true;
        int i = 0;
        stringToDisplay = "";
        while (i < strComplete.Length)
        {
            stringToDisplay += strComplete[i++];
            textBox.text = stringToDisplay;
            talk_sound.PlayTheSound();
            yield return new WaitForSeconds(speed);
        }
        textDisplayed = false;
    }
}
