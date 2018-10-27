using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)] //Start を最後に呼び出すため
public class TestButtonManager : MonoBehaviour {

    [SerializeField]
    private Text init_textUI;

    [SerializeField]
    private Text test_textUI;

    private ExternalConnect script;

    void Start()
    {
        init_textUI = GameObject.Find("InitTextBlock").GetComponent<Text>();

        GameObject connecter = GameObject.Find("ExternalConnect");
        this.script = connecter.GetComponent<ExternalConnect>();

        this.script.ReceiveData(init_textUI.text);
        this.script.ChengeMode("elements");

    }

    public void testBotton_Click()
    {
        test_textUI = GameObject.Find("TestTextBlock").GetComponent<Text>();

        this.script.ChengeMode("elements");
        this.script.ReceiveModeData(test_textUI.text);
        /*
            case "nodes":
            case "members":
            case "panels":
            case "fix_nodes":
            case "elements":
            case "joints":
            case "notice_points":
            case "fix_members":
            case "loads":
            case "fsec":
            case "comb.fsec":
            case "pic.fsec":
            case "disg":
            case "reac":
         */
    }

}
