using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesManager : MonoBehaviour {
    [SerializeField]
    private List<DataObject> dialogueSequenceTemp;
    [SerializeField]
    private List<List<DataObject>> allDialogues;

    [SerializeField]
    private Text nomInterlocuteur;
    [SerializeField]
    private Text boiteDialogue;

    private int sequenceIndex;
    private int dialogueIndex;
    // Use this for initialization
    void Start()
    {
        // Init Data
        allDialogues = new List<List<DataObject>>();

        // Care not to open the csv file (in excel or other app) when launching script
        // Check that your file is UTF 8 encoded 
        StreamReader reader = new StreamReader(@"C:\Users\TARA\Desktop\test.csv");

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

            DataObject tempObject = new DataObject(rowData[0],rowData[1], rowData[2], rowData[3], rowData[4], rowData[5]);
            if (int.Parse(rowData[0])==compteur)
            {
                dialogueSequenceTemp.Add(tempObject); // first column is the key name
            }
            else
            {
                allDialogues.Add(dialogueSequenceTemp);
                dialogueSequenceTemp = new List<DataObject>();
                compteur++;
            }

        }
        allDialogues.Add(dialogueSequenceTemp);

        sequenceIndex = 0;
        dialogueIndex = 0;

        Debug.Log("allDialogues.Count " + allDialogues[0].Count);
        Debug.Log("allDialogues.Count " + allDialogues[1].Count);
        Debug.Log("allDialogues.Count " + allDialogues[2].Count);

        /*
        foreach (KeyValuePair<string,DataObject> obj in csvDataDict)
        {
            Debug.Log(obj.Key + " : " + obj.Value.ToString());
        }
        foreach (Transform childGroup in this.transform)
        {
            foreach (Transform childElement in childGroup)
            {
                Debug.Log(childElement.name);
            }
        }*/
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if(sequenceIndex< allDialogues.Count)
            {
                //Debug.Log("sequenceIndex " + sequenceIndex);
                Debug.Log("dialogueIndex " + dialogueIndex);

                Debug.Log("sequenceIndex " + sequenceIndex);

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
                }

            }
            else
            {
                Debug.Log("Fin des dialogues");
            }
        }
    }
}
