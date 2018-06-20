using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class MainFrameManager : MonoBehaviour
{

    #region テスト用 出荷時は削除すること

    [SerializeField]
    private Text test_textUI = null;

    public void testBotton_Click()
    {
        SendAngular("test messege sended !");
    }

    #endregion


    //	表示用ワークバッファ
    class PartsDispWork
    {
		public	GameObject			partsGameObject;
		public	PartsDispManager	partsDispManager;
	}

	[SerializeField]
	GameObject[]			_dispPrefabs = new GameObject[(int)InputPanelLabel.Max];

	SkeletonResponseData	_skeletonResponseData = null;
    private InputPanelLabel inputMode = InputPanelLabel.None;

    PartsDispWork[]					_partsDispWorks = new PartsDispWork[(int)InputPanelLabel.Max];

	private	NodeDispManager			_nodeDispManager;
	public	NodeDispManager			NodeDispManager{ get{ return _nodeDispManager; } }

    private	ElementDispManager		_elementDispManager;
	public	ElementDispManager		ElementDispManager{ get{ return _elementDispManager; } }

    private	PanelDispManager		_panelDispManager;
	public	PanelDispManager		PanelDispManager{ get{ return _panelDispManager; } }


    #region AngularJSとの連携

    /// <summary>
    /// Unity→Html (UnityからJS内でイベント発火)
    /// </summary>
    /// <param name="message"></param>
    public void SendAngular(string message)
    {
        string SendMessage = string.Format("input mode:{0}, ", inputMode) + message;
        Application.ExternalCall("ReceiveUnity", SendMessage);
    }

    /// <summary>
    /// Html→Unity (JSからUnity内でイベント発火)
    /// </summary>
    /// <param name="message"></param>
    public void ReceiveAngular(string message)
    {
        test_textUI.text = message;

        try
        {
            string[] words = message.Substring(0, 25).Trim().Split(':');
            string mode = words[0];
            switch (mode)
            {
                case "input mode change":
                    string name = words[1];
                    switch (name)
                    {
                        case "node":
                            this.inputMode = InputPanelLabel.Node;
                            break;
                        case "member":
                            this.inputMode = InputPanelLabel.Element;
                            break;
                        case "panel":
                            this.inputMode = InputPanelLabel.Panel;
                            break;
                        default:
                            this.inputMode = InputPanelLabel.None;
                            break;
                    }
                    SetActiveDispManager(this.inputMode);
                    break;

                case "select item change":
                    if (this.inputMode == InputPanelLabel.None)
                    {
                        SendAngular("what kind of input mode is it now?");
                    }
                    else
                    {
                        string id = words[1];
                        PartsDispWork partsDispWork = _partsDispWorks[(int)this.inputMode];
                        partsDispWork.partsDispManager.ChengeForcuseBlock(int.Parse(id));
                    }
                    break;

                default: // data change: receive json data
                    _skeletonResponseData.Load(message);
                    break;
            }
        }
        catch (Exception ex)
        {
            test_textUI.text = ex.Message;
        }
    }
    #endregion

    /// <summary>
    /// 表示用オブジェクトのインスタンス化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dispObject"></param>
    /// <param name="dispManager"></param>
    void InstantiateDispPrefab( out PartsDispWork partsDispWork, GameObject baseObject )
	{
		partsDispWork = new PartsDispWork();
		if( baseObject == null ) {
			return;
		}

		partsDispWork.partsGameObject = Instantiate( baseObject ) as GameObject;
		partsDispWork.partsGameObject.transform.parent = this.gameObject.transform;

		partsDispWork.partsDispManager = partsDispWork.partsGameObject.GetComponent<PartsDispManager>();
	}

	/// <summary>
	/// プレハブをインスタンス化
	/// </summary>
	void	InstantiatePrefab()
	{
		int		i;

		for( i=0; i<_partsDispWorks.Length; i++ ){
			InstantiateDispPrefab( out _partsDispWorks[i], _dispPrefabs[i] );	
		}

		_nodeDispManager	= _partsDispWorks[(int)InputPanelLabel.Node].partsDispManager as NodeDispManager;
		_elementDispManager = _partsDispWorks[(int)InputPanelLabel.Element].partsDispManager as ElementDispManager;
		_panelDispManager	= _partsDispWorks[(int)InputPanelLabel.Panel].partsDispManager as PanelDispManager;
	}



	/// <summary>
	/// 描画パーツの作成
	/// </summary>
	public	void	CreateParts()
	{
		int		i;

		//	パーツの作成
		for( i=0; i<_partsDispWorks.Length; i++ ){
			if( _partsDispWorks[i].partsDispManager == null ) {
				continue;
			}
			_partsDispWorks[i].partsDispManager.CreateParts();
			if( (InputPanelLabel)i != InputPanelLabel.Element ) {
                _partsDispWorks[i].partsGameObject.SetActive(false);      //	最初は全部無効にしておく
            }
        }
	}



	/// <summary>
	/// 
	/// </summary>
	IEnumerator  Start ()
	{
        #if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
        #endif

        _skeletonResponseData = SkeletonResponseData.Instance;

        //	描画マネージャを起動する
        InstantiatePrefab();
		
		yield	return	0;

		CreateParts();

		
		SetAllBlockStatus();

    }



    /// <summary>
    /// マウスの制御
    /// </summary>
    void InputMouse()
	{
		if( this.inputMode == InputPanelLabel.None ) {
			return;
		}

		PartsDispWork	partsDispWork = _partsDispWorks[(int)this.inputMode];
		
		if( partsDispWork.partsGameObject == null ) {
			return;
		}
		if( partsDispWork.partsGameObject.activeSelf == false ){
			return;
		}
		if( partsDispWork.partsDispManager == null ){
			return;
		}
			
		partsDispWork.partsDispManager.InputMouse();
	}



	/// <summary>
	/// 
	/// </summary>
	void Update ()
	{
		InputMouse();
	}


	/// <summary>
	/// 全部、または指定された節点と一致するブロックを設定する
	/// </summary>
	/// <param name="search_node"></param>
	public	void	SetAllBlockStatus( int search_node=-1 )
	{
		//	全て設定する
		if( search_node == -1 ) {
			_nodeDispManager.CalcNodeBlockScale();
			_nodeDispManager.SetBlockStatusAll();
			_elementDispManager.SetBlockStatusAll();
			_panelDispManager.SetBlockStatusAll();
		}
		//	指定されたものと関わっているものだけ更新する
		else {
			_nodeDispManager.SetBlockStatus( search_node );
			if( _nodeDispManager.CalcNodeBlockScale(search_node) ) {	//	サイズが更新されていたら節点のサイズも更新する
				_elementDispManager.SetBlockStatusAll();
				_panelDispManager.SetBlockStatusAll();
			}

			_elementDispManager.CheckNodeAndUpdateStatus( search_node );
			_panelDispManager.CheckNodeAndUpdateStatus( search_node );
		}
	}


	
	/// <summary>
	/// アクティブな表示モードを切り替える
	/// </summary>
	/// <param name="label"></param>
	public	void	SetActiveDispManager( InputPanelLabel label )
	{
		int			i;

		for( i = 0; i < _partsDispWorks.Length; i++ ) {
			if( _partsDispWorks[i] == null ){
				continue;
			}
			if( _partsDispWorks[i].partsGameObject == null ){
				continue;
			}
			//	要素の時は非表示にせずに表示モードを切り替える
			if( (InputPanelLabel)i == InputPanelLabel.Element ) {
				if( label == InputPanelLabel.Element ) {
					_elementDispManager.ChangeDispMode( ElementDispManager.DispType.Block );
				}
				else { 
					_elementDispManager.ChangeDispMode( ElementDispManager.DispType.Line );
				}
			}
			else {
				_partsDispWorks[i].partsGameObject.SetActive( (InputPanelLabel)i == label );
			}
		}
	}
}
