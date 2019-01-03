using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class CsvParser : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string,DataObject> csvDataDict;

    // Use this for initialization
    void Start()
    {
        // Init dictionnary of data
        csvDataDict = new Dictionary<string, DataObject>();

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

        // Read file until end of file
        while ((line = reader.ReadLine()) != null) // Foreach lines in the document
        {
            //Separating columns to array
            string[] rowData = CSVParser.Split(line);
            DataObject tempObject = new DataObject(rowData[1], rowData[2]);

            csvDataDict.Add(rowData[0], tempObject); // first column is the key name
        }

        /*
        foreach (KeyValuePair<string,DataObject> obj in csvDataDict)
        {
            Debug.Log(obj.Key + " : " + obj.Value.ToString());
        }*/

        // 
        foreach (Transform childGroup in this.transform)
        {
            foreach (Transform childElement in childGroup)
            {
                Debug.Log(childElement.name);
            }
        }
    }
}
