using UnityEngine;
using System.Collections;
using System;
//using MeteorMindsTools;
using UnityEngine.UI;

public class OpenFileScript : MonoBehaviour 
{

    //public string defaultPathToSearch;
    //public bool startSearchOnDefaultPath;
    //public InputField inputField;
    //public string[] fileTypes;

    //private string filePath;

    //MeteorToolsObject meteorObj;

    void Start()
    {
        //meteorObj = new MeteorToolsObject();

        
        //filePath = string.Empty;

        ////Adding an "On Click" listener so when you click/press it, it will call OpenFile() method
        //GetComponent<Button>().onClick.AddListener(() => { OpenFile(); });
    }

    void OpenFile()
    {
        //string directoryToStartTheSearch = string.Empty;

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

        ////Store the file path that results from the Open File Dialog in 'filePath' variable
        //filePath = meteorObj.GetFileNameWithPathToOpen(directoryToStartTheSearch, fileTypes);

        ////Set the file path to the inputField
        //inputField.text = filePath;
    }

    void Update()
    {
        //Pressing ESC will close the application IF not in EDITOR MODE
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
