using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonManager : MonoBehaviour {

    [SerializeField]
    private Text test_textUI;

    protected MainFrameManager _mainFrameManager = null;

    public void testBotton_Click()
    {
        test_textUI = GameObject.Find("TestTextBlock").GetComponent<Text>();
        _mainFrameManager.ReceiveAngular(test_textUI.text);
    }


    // Use this for initialization
    void Start () {
        _mainFrameManager = FindObjectOfType<MainFrameManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
