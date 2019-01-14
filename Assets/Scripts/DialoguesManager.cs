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

    public int sequenceIndex;
    public int dialogueIndex;

    private string str;
    public float speed;
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
        // Init Data
        InitCsvParser();
        Invoke("DisplayText", 3f);
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

    public bool DisplaySequenceDialogues()
    {
        bool sequenceIsFinished = false;
        if (sequenceIndex < allDialogues.Count)
        {
            //Debug.Log("sequenceIndex " + sequenceIndex);
            //Debug.Log("dialogueIndex " + dialogueIndex);
            nomInterlocuteur.text = allDialogues[sequenceIndex][dialogueIndex].character;
            boiteDialogue.text = allDialogues[sequenceIndex][dialogueIndex].dialogue;

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
        nomInterlocuteur.text = _nomInterlocuteur;
        boiteDialogue.text = _boiteDialogue;
    }
    
    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            boiteDialogue.text = str;
            yield return new WaitForSeconds(0.12F);
        }
    }
    public void DisplayText()
    {
        StartCoroutine(AnimateText("Ceci est un test"));
    }
}
