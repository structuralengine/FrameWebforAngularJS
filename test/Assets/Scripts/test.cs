using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    /// ボタンをクリックした時の処理
    public void OnClick()
    {
        Application.ExternalCall("receiveUnity", "Button click!");
    }
    // JSからUnity内でイベント発火
    public void UpdateTarget(string str)
    {
        Application.ExternalEval("alert('hoge');");
    }
}
