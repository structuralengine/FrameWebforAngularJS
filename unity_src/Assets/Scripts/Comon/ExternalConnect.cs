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
        SendAngular("GetInputJSON"); 
    }

    #region Unity → Html へ メッセージを送る

    /// <summary>
    /// Html へ メッセージを送る
    /// </summary>
    /// <param name="message"></param>
    internal static void SendAngular(string message)
    {
        Application.ExternalCall("ReceiveUnity", message);
    }

    internal static void SendAngularSelectItemChenge(int id)
    {
        Application.ExternalCall("ReceiveUnitySelectItemChenge", id);
    }

    #endregion

    #region Html→Unity (JSからUnity内でイベント発火)

    /// <summary> Htmlから Jsonデータが一式届く </summary>
    public void ReceiveData(string strJson)
    {
        Debug.Log(strJson);
        mainFrameObject.InputDataChenge(strJson);
    }

    /// <summary> Htmlから 現在のモードのJsonデータが届く </summary>
    public void ReceiveModeData(string strJson)
    {
        mainFrameObject.InputModeDataChenge(strJson);
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
        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();
        byte[] img = tex.EncodeToPNG();
        CanvasCapture(img, img.Length);
    }

    /// <summary> Htmlから モードの変更通知がくる </summary>
    public void ChengeMode(string strMode)
    {
        mainFrameObject.InputModeChange(strMode);
    }

    /// <summary> Htmlから セレクトアイテム変更の通知がくる </summary>
    /// <param name="strMode">描画モード名</param>
    /// <param name="i">セレクトアイテムid</param>
    public void SelectItemChange(string strMode, int i)
    {
        mainFrameObject.SelectItemChange(i);
    }

    #endregion

}
