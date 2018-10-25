using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[DefaultExecutionOrder(100)] //Start を最後に呼び出すため
public class ExternalConnect : MonoBehaviour {

    MainFrameManager mainFrameObject;
    void Start()
    {
        GameObject Obj = GameObject.Find("MainFrameManager");
        this.mainFrameObject = Obj.GetComponent<MainFrameManager>();

        // javascript に起動時のデータを問い合わせる
        Debug.Log("Unity ExternalConnect Start実行");
        SendAngular("GetInputJSON");
    }


    #region Unity→Html (UnityからJS内でイベント発火)

    /// <summary>
    /// Html へ メッセージを送る
    /// </summary>
    /// <param name="message"></param>
    internal static void SendAngular(string message)
    {
        Application.ExternalCall("ReceiveUnity", message);
    }

    #endregion

    #region Html→Unity (JSからUnity内でイベント発火)

    /// <summary> Htmlから Jsonデータが一式届く </summary>
    public void ReceiveData(string strJson)
    {
        Debug.Log("Unity ExternalConnect ReceiveData実行");
        Debug.Log(strJson);
        mainFrameObject.InputDataChenge(strJson);
    }

    /// <summary> Htmlから 現在のモードのJsonデータが届く </summary>
    public void ReceiveModeData(string strMode, string strJson)
    {
        Debug.Log("Unity ExternalConnect ReceiveData(strMode... 実行");
        mainFrameObject.InputDataChenge(strMode, strJson);
    }


    /// <summary> Htmlから キャプチャー画像の送付依頼がくる </summary>
    public void SendCapture()
    {
        StartCoroutine(_Execute());
    }

    [DllImport("__Internal")]
    private static extern void CanvasCapture(byte[] img, int size);

    IEnumerator _Execute()
    {
        Debug.Log("Unity SendCapture実行");
        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();
        byte[] img = tex.EncodeToPNG();
        Debug.Log("Unity SendCapture CanvasCaptureを実行");
        CanvasCapture(img, img.Length);
    }

    /// <summary> Htmlから モードの変更通知がくる </summary>
    public void ChengeMode(string strMode)
    {
        Debug.Log("Unity ExternalConnect ChengeMode実行");
        mainFrameObject.InputModeChange(strMode);
    }

    /// <summary> Htmlから セレクトアイテム変更の通知がくる </summary>
    /// <param name="strMode">描画モード名</param>
    /// <param name="id">セレクトアイテムid</param>
    public void SelectItemChange(string strMode, string id)
    {
        Debug.Log("Unity ExternalConnect SelectItemChange実行");
        int i = 0;
        if (int.TryParse(id, out i))
        {
            mainFrameObject.SelectItemChange(i);
        }
    }

    #endregion

}
