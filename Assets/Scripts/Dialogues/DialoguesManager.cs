using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesManager : MonoBehaviour {
    private List<DataObject> dialogueSequenceTemp;
    private List<List<DataObject>> allDialogues;

    [Header("Monologue_Pensee_Alex")]
    [SerializeField]
    private Text nomInterlocuteur;
    [SerializeField]
    private Text boiteDialogue;

    [Header("Dialogue_Alex_Nat")]
    public GameObject messageBox_Temp;
    public GameObject messagesParent;
    public List<GameObject> messagesList;

    [HideInInspector]
    public bool startDialogue;

    private string stringToDisplay;
    [HideInInspector]
    public float text_speed = 0.02f;
    [HideInInspector]
    public bool textDisplayed;

    public SFXSound talk_sound;

    public static DialoguesManager instance = null;

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

        // Create a
        GameObject currentMsg = Instantiate(messageBox_Temp);
        currentMsg.transform.SetParent(messagesParent.transform);
        currentMsg.transform.position = messageBox_Temp.transform.position;
        currentMsg.transform.localScale = Vector3.one;
        currentMsg.SetActive(true);
        messagesList.Add(currentMsg);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach(GameObject msg in messagesList)
            {
                msg.GetComponent<MoveMessageBox>().targetPosition = msg.transform.position + Vector3.up * 70;
            }
            GameObject currentMsg = Instantiate(messageBox_Temp);
            currentMsg.transform.SetParent(messagesParent.transform);
            currentMsg.transform.position = messageBox_Temp.transform.position;
            currentMsg.transform.localScale = Vector3.one;
            currentMsg.SetActive(true);
            messagesList.Add(currentMsg);

            if (messagesList.Count > 5)
            {
                GameObject msgToDestroy = messagesList[0];
                messagesList.Remove(msgToDestroy);
                Destroy(msgToDestroy);
            }
        }
    }

    public void ClickOnNextDialogue()
    {
        if (startDialogue&& !textDisplayed)
        {
            GameManager.instance.dialoguesSeqFinished = DisplayNextSequenceDialogues();
        }
    }

    public bool DisplayNextSequenceDialogues()
    {
        bool sequenceIsFinished = false;
        if (LoadDialoguesManager.sequenceIndex < allDialogues.Count)
        {
            //Debug.Log("sequenceIndex " + sequenceIndex);
            //Debug.Log("dialogueIndex " + dialogueIndex);
            nomInterlocuteur.text = allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].character;
            StartCoroutine(AnimateText(allDialogues[LoadDialoguesManager.sequenceIndex][LoadDialoguesManager.dialogueIndex].dialogue, 0.02F));
            //boiteDialogue.text = allDialogues[sequenceIndex][dialogueIndex].dialogue;

            if (LoadDialoguesManager.dialogueIndex < allDialogues[LoadDialoguesManager.sequenceIndex].Count-1)
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

    public void SetDialogueBox(string _nomInterlocuteur, string _boiteDialogue)
    {
        if (!textDisplayed && ClickableObjetManager.instance.startPAndClick && !ClickableObjetManager.instance.finishedPAndCStep)
        {
            nomInterlocuteur.text = _nomInterlocuteur;
            //StartCoroutine(AnimateText(_boiteDialogue, 0.02F));
            StartCoroutine(AnimateText(_boiteDialogue, text_speed));

        }
    }
    
    IEnumerator AnimateText(string strComplete,float speed)
    {
        textDisplayed = true;
        int i = 0;
        stringToDisplay = "";
        while (i < strComplete.Length)
        {
            stringToDisplay += strComplete[i++];
            boiteDialogue.text = stringToDisplay;
            talk_sound.PlayTheSound();
            yield return new WaitForSeconds(speed);
        }
        textDisplayed = false;
    }

}
