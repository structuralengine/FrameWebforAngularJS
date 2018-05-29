//using MeteorMindsTools;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SaveText : MonoBehaviour {

    public string defaultPathToSearch;
    public bool startSearchOnDefaultPath;
    //public InputField inputField;
    public string[] fileTypes;

    //private string filePath;

    public InputField inputField;
    public InputField textField;

    //public string[] fileTypes;
    //public string defaultPathToSearch;
    //public bool startSearchOnDefaultPath;

    //MeteorToolsObject meteorObj;
    
    private string filePath;
    private string[] stringArray;

    void Start()
    {
        //meteorObj = new MeteorToolsObject();
        //filePath = string.Empty;
       
        ////Adding an "On Click" listener so when you click/press it, it will call SaveToFile() method
        //GetComponent<Button>().onClick.AddListener(() => { SaveToFile(); });
        
    }

    void SaveToFile()
    {
        string directoryToStartTheSearch = string.Empty;

        ////Check the directory to start the search
        ////IF it's NOT to start the search on default path, and the LastKnownDirectory is not null or empty
        ////ELSE search on defaultPathToSearch
        //if (!startSearchOnDefaultPath && !String.IsNullOrEmpty(meteorObj.LastKnownDirectory()))
        //{
        //    directoryToStartTheSearch = meteorObj.LastKnownDirectory();
        //}
        //else
        //{
        //    directoryToStartTheSearch = defaultPathToSearch;
        //}

        ////Store the file path that results from the Save File Dialog in 'filePath' variable
        //filePath = meteorObj.GetFileNameWithPathToSave(directoryToStartTheSearch, fileTypes);

        ////Set the file path to the inputField
        //inputField.text = filePath;


        
        //StringBuilder sb = new StringBuilder();
        //stringArray = textField.text.Split('\n');

        //if (!String.IsNullOrEmpty(filePath))
        //{
        //    //Block of code to write text to a file
        //    StreamWriter writer = new StreamWriter(filePath);
        //    foreach (string s in stringArray)
        //    {
        //        sb.AppendLine(s);
        //        writer.WriteLine(s);
        //    }

        //    writer.Close();
        //}
    }
}
