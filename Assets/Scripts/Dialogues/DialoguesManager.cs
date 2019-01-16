using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesManager : MonoBehaviour {
    [HideInInspector]
    public List<DataObject> dialogueSequenceTemp;
    [HideInInspector]
    public List<List<DataObject>> allDialogues;

    [SerializeField]
    private Text nomInterlocuteur;
    [SerializeField]
    private Text boiteDialogue;

    public static int sequenceIndex;
    public static int dialogueIndex;
    public bool startDialogue;

    private string stringToDisplay;
    public static DialoguesManager instance = null;
    public bool textDisplayed;

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
        // Init Data
        InitCsvParser();
        startDialogue = false;
    }

    private void InitCsvParser()
    {
        allDialogues = new List<List<DataObject>>();

        //Get the path of the Game data folder
        string m_Path = Application.dataPath + "/Resources/test.csv";

        //Output the Game data path to the console
        //Debug.Log("Path : " + m_Path);

        // Care not to open the csv file (in excel or other app) when launching script
        // Check that your file is UTF 8 encoded 
        StreamReader reader = new StreamReader(m_Path);


        string line;

        //Define separator pattern
        Regex CSVParser = new Regex(";"); // (",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        // Skip 1st line
        if ((line = reader.ReadLine()) != null)
        {
            /*
            //Separating columns to array
            string[] rowData = CSVParser.Split(line);

            Debug.Log("Data name");

            foreach (string data in rowData)
            {
                Debug.Log(data);
            }
            Debug.Log("\n");*/
        }

        int compteur = 0;
        dialogueSequenceTemp = new List<DataObject>();

        // Read file until end of file
        while ((line = reader.ReadLine()) != null) // Foreach lines in the document
        {
            //Separating columns to array
            string[] rowData = CSVParser.Split(line);

            DataObject tempObject = new DataObject(rowData[0], rowData[1], rowData[2], rowData[3], rowData[4], rowData[5]);
            if (int.Parse(rowData[0]) == compteur)
            {
                dialogueSequenceTemp.Add(tempObject); // first column is the key name
            }
            else
            {
                allDialogues.Add(dialogueSequenceTemp);
                dialogueSequenceTemp = new List<DataObject>();
                dialogueSequenceTemp.Add(tempObject);
                compteur++;
            }

        }
        allDialogues.Add(dialogueSequenceTemp);

        sequenceIndex = 0;
        dialogueIndex = 0;

        /*
        for(int i= 0; i<allDialogues.Count;i ++)
        {
            Debug.Log("sequence index " + i);
            for (int j = 0; j < allDialogues[i].Count; j++)
            {
                Debug.Log("dialogue index " + j);
                Debug.Log(allDialogues[i][j].dialogue);
            }
        }*/
    }

    public void ClickOnNextDialogue()
    {
        if (startDialogue&& !textDisplayed)
        {
            GameManager.instance.dialoguesSeqFinished = DisplaySequenceDialogues();
        }
    }

    public bool DisplaySequenceDialogues()
    {
        bool sequenceIsFinished = false;
        if (sequenceIndex < allDialogues.Count)
        {
            //Debug.Log("sequenceIndex " + sequenceIndex);
            //Debug.Log("dialogueIndex " + dialogueIndex);
            nomInterlocuteur.text = allDialogues[sequenceIndex][dialogueIndex].character;
            StartCoroutine(AnimateText(allDialogues[sequenceIndex][dialogueIndex].dialogue, 0.02F));
            //boiteDialogue.text = allDialogues[sequenceIndex][dialogueIndex].dialogue;

            if (dialogueIndex < allDialogues[sequenceIndex].Count-1)
            {
                dialogueIndex++;
            }
            else
            {
                sequenceIndex++;
                dialogueIndex = 0;
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
            StartCoroutine(AnimateText(_boiteDialogue, 0.02F));
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
            yield return new WaitForSeconds(speed);
        }
        textDisplayed = false;
    }
    
}
