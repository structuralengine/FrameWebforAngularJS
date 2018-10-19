using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonManager : MonoBehaviour {

    [SerializeField]
    private Text test_textUI;

    public void testBotton_Click()
    {
        test_textUI = GameObject.Find("TestTextBlock").GetComponent<Text>();

        ExternalConnect connecter = new ExternalConnect();
        connecter.ReceiveData(test_textUI.text);
    }

}
