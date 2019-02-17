using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialoguesManager : MonoBehaviour {
    [HideInInspector]
    public List<DataObject> dialogueSequenceTemp;
    [HideInInspector]
    public List<List<DataObject>> allDialogues;
    public List<TextAsset> file;

    public static LoadDialoguesManager instance = null;

    [HideInInspector]
    public static int sequenceIndex;
    [HideInInspector]
    public static int dialogueIndex;
    public static int indexDialogueFile = 0;

    private string m_path;

    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            // Init Data
            InitCsvParser();
        }
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }


    private void InitCsvParser()
    {
        allDialogues = new List<List<DataObject>>();

        string fs = file[indexDialogueFile].text;
        string[] fLines = Regex.Split(fs, "\n");


        int allDialoguesCompteur = 0;
        //Debug.Log(fLines[0]);
        int counterCSV = 1; // Skip first line of csv

        dialogueSequenceTemp = new List<DataObject>();
        
        // Read file until end of file
        while (counterCSV< fLines.Length) // Foreach lines in the document
        {
            //Debug.Log(fLines[counterCSV]);
            //Separating columns to array
            //Define separator pattern
            Regex CSVParser = new Regex(";"); // (",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string[] rowData = CSVParser.Split(fLines[counterCSV]);

            DataObject tempObject = new DataObject(rowData[0], rowData[1], rowData[2], rowData[3], rowData[4], rowData[5], rowData[6]);
            if (int.Parse(rowData[0]) == allDialoguesCompteur)
            {
                dialogueSequenceTemp.Add(tempObject); // first column is the key name
            }
            else
            {
                allDialogues.Add(dialogueSequenceTemp);
                dialogueSequenceTemp = new List<DataObject>();
                dialogueSequenceTemp.Add(tempObject);
                allDialoguesCompteur++;
            }
            counterCSV++;

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

}
