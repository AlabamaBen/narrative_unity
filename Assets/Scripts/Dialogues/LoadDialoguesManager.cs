using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialoguesManager : MonoBehaviour {
    [HideInInspector]
    public List<DataObject> dialogueSequenceTemp;
    [HideInInspector]
    public List<List<DataObject>> allDialogues;

    public static LoadDialoguesManager instance = null;

    public static int sequenceIndex;
    public static int dialogueIndex;

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

        //Get the path of the Game data folder
        string m_Path = Application.dataPath + "/Resources/dialogues.csv";

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
            //Debug.Log(line);
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

}
