using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// 
/// </summary>
public class MainFrameManager : MonoBehaviour
{

    webframe _webframe = null;

    #region 表示モード制御用

    private InputPanelLabel inputMode = InputPanelLabel.None;

    //	表示用ワークバッファ
    class PartsDispWork
    {
		public	GameObject			partsGameObject;
		public	PartsDispManager	partsDispManager;
	}

	[SerializeField]
	GameObject[]			        _dispPrefabs = new GameObject[(int)InputPanelLabel.Max];

    PartsDispWork[]					_partsDispWorks = new PartsDispWork[(int)InputPanelLabel.Max];

	private	NodeDispManager			_nodeDispManager;
	public	NodeDispManager			NodeDispManager{ get{ return _nodeDispManager; } }

    private	MemberDispManager		_memberDispManager;
	public	MemberDispManager		MemberDispManager{ get{ return _memberDispManager; } }

    private	PanelDispManager		_panelDispManager;
	public	PanelDispManager		PanelDispManager{ get{ return _panelDispManager; } }


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
		_memberDispManager = _partsDispWorks[(int)InputPanelLabel.Member].partsDispManager as MemberDispManager;
		_panelDispManager	= _partsDispWorks[(int)InputPanelLabel.Panel].partsDispManager as PanelDispManager;
	}

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
        #endif

        this._webframe = webframe.Instance;

        //	描画マネージャを起動する
        InstantiatePrefab();

        // javascript に起動時のデータを問い合わせる
        ExternalConnect.SendAngular("GetInputJSON");

        // javascript に起動時の画面のを問い合わせる
        ExternalConnect.SendAngular("GetInputMode");

    }


    /// <summary>
    /// アクティブな表示モードを切り替える
    /// </summary>
    /// <param name="label"></param>
    public void SetActiveDispManager(InputPanelLabel label)
    {
        int i;

        for (i = 0; i < _partsDispWorks.Length; i++)
        {
            if (_partsDispWorks[i] == null)
            {
                continue;
            }
            if (_partsDispWorks[i].partsGameObject == null)
            {
                continue;
            }
            //	要素の時は非表示にせずに表示モードを切り替える
            if ((InputPanelLabel)i == InputPanelLabel.Member)
            {
                if (label == InputPanelLabel.Member)
                {
                    _memberDispManager.ChangeDispMode(MemberDispManager.DispType.Block);
                }
                else
                {
                    _memberDispManager.ChangeDispMode(MemberDispManager.DispType.Line);
                }
            }
            else
            {
                _partsDispWorks[i].partsGameObject.SetActive((InputPanelLabel)i == label);
            }
        }
    }

    #endregion

    #region マウスの制御

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

    #endregion

    #region 描画パーツの表示制御

    /// <summary>
    /// 描画パーツの作成
    /// </summary>
    public void CreateParts()
    {
        int i;

        //	パーツの作成
        for (i = 0; i < _partsDispWorks.Length; i++){
            if (_partsDispWorks[i].partsDispManager == null){
                continue;
            }
            _partsDispWorks[i].partsDispManager.CreateParts();
            if ((InputPanelLabel)i != InputPanelLabel.Member){          // 要素は常に表示             
                _partsDispWorks[i].partsGameObject.SetActive(false);    // 最初は全部無効にしておく
            }
        }
    }

    /// <summary>
    /// 全部、または指定された節点と一致するブロックを設定する
    /// </summary>
    /// <param name="search_node"></param>
    public void	SetAllBlockStatus( int search_node=-1 )
	{
		//	全て設定する
		if( search_node == -1 ) {
			_nodeDispManager.CalcNodeBlockScale();
			_nodeDispManager.SetBlockStatusAll();
			_memberDispManager.SetBlockStatusAll();
			_panelDispManager.SetBlockStatusAll();
		}
		//	指定されたものと関わっているものだけ更新する
		else {
			_nodeDispManager.SetBlockStatus( search_node );
			if( _nodeDispManager.CalcNodeBlockScale(search_node) ) {	//	サイズが更新されていたら節点のサイズも更新する
				_memberDispManager.SetBlockStatusAll();
				_panelDispManager.SetBlockStatusAll();
			}

			_memberDispManager.CheckNodeAndUpdateStatus( search_node );
			_panelDispManager.CheckNodeAndUpdateStatus( search_node );
		}
	}

    #endregion


    #region Javascript と連携 

    /// <summary>
    /// JavaScript から Input モードの変更通知が来た
    /// </summary>
    /// <param name="message"></param>
    public void InputModeChange(string message)
    {
        bool flg = false;

        switch (message.Trim())
        {
            case "nodes":
                if (this.inputMode != InputPanelLabel.Node){
                    flg = true;
                    this.inputMode = InputPanelLabel.Node;
                }
                break;

            case "members":
                if (this.inputMode != InputPanelLabel.Member){
                    flg = true;
                    this.inputMode = InputPanelLabel.Member;
                }
                break;

            case "panels":
                if (this.inputMode != InputPanelLabel.Panel){
                    flg = true;
                    this.inputMode = InputPanelLabel.Panel;
                }
                break;

            case "fix_nodes":
                break;

            case "elements":
                break;

            case "joints":
                break;

            case "notice_points":
                break;

            case "fix_members":
                break;

            case "loads":
                break;

            case "combines":
                break;

            case "disg":
                break;

            case "fsec":
                break;

            case "comb.fsec":
                break;

            case "pic.fsec":
                break;

            case "reac":
                break;

            default:
                this.inputMode = InputPanelLabel.None;
                break;
        }
        if(flg == true)
            this.SetActiveDispManager(this.inputMode);
    }

    /// <summary>
    /// JavaScript から Active Item の変更通知が来た
    /// </summary>
    /// <param name="inputmode">現在のインプットモード</param>
    /// <param name="id"></param>
    public void SelectItemChange(string inputmode, string id)
    {
        InputModeChange(inputmode);

        if (this.inputMode == InputPanelLabel.None)
        {
            ExternalConnect.SendAngular("GetInputMode");
        }
        else
        {
            PartsDispWork partsDispWork = _partsDispWorks[(int)this.inputMode];
            partsDispWork.partsDispManager.ChengeForcuseBlock(int.Parse(id));
        }
    }

    /// <summary>
    /// JavaScript から インプットデータ の変更通知が来た
    /// </summary>
    /// <param name="inputmode"></param>
    /// <param name="json"></param>
    public void InputDataChenge(string json)
    {
        this._webframe.Create(json);
        this.CreateParts();
        this.SetAllBlockStatus();
    }
    public void InputDataChenge(string inputmode, string json)
    {
        // 個別の
    }


    #endregion
}
