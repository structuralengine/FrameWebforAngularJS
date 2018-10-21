using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ExternalConnect : MonoBehaviour {

    MainFrameManager mainFrameObject;
    void Start()
    {
        GameObject Obj = GameObject.Find("MainFrameManager");
        this.mainFrameObject = Obj.GetComponent<MainFrameManager>();
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


    /// <summary>
    /// Htmlから Jsonデータが届く
    /// </summary>
    /// <param name="strJson">json データ</param>
    public void ReceiveData(string strJson)
    {
       if(strJson.StartsWith("{")) {
            // データ全部
            mainFrameObject.InputDataChenge(strJson);
        }
        else {
            // 個別のデータのみ

        }
    }



    /// <summary>
    /// Htmlから キャプチャー画像の送付依頼がくる
    /// </summary>
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



    /// <summary>
    /// Htmlから モードの変更通知がくる
    /// </summary>
    /// <param name="strMode">描画モード名</param>
    public void ChengeMode(string strMode)
    {
        mainFrameObject.InputModeChange(strMode);
    }

    /// <summary>
    /// Htmlから セレクトアイテム変更の通知がくる
    /// </summary>
    /// <param name="strMode">描画モード名</param>
    public void SelectItemChange(string strMode, string id)
    {
        int i = 0;
        if (int.TryParse(id, out i))
        {
            mainFrameObject.SelectItemChange(i);
        }
    }
    #endregion

}
