using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)] //Start を最後に呼び出すため
public class TestButtonManager : MonoBehaviour {
    [SerializeField]
    private Text init_textUI = null; //aka

    [SerializeField]
    private Text test_textUI = null; //kiro

	[SerializeField]
	private MainFrameManager.InputModeType _inputModeType = (MainFrameManager.InputModeType)0;
    private ExternalConnect script;

    void Start()
    {
        GameObject connecter = GameObject.Find("ExternalConnect");
        this.script = connecter.GetComponent<ExternalConnect>();

        this.script.ReceiveData(init_textUI.text);
        this.script.ChengeMode(_inputModeType);

    }

    public void testBotton_Click()
    {
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
