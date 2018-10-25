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

    public NodeDispManager NodeDispManager     { get; private set; }
    public MemberDispManager MemberDispManager { get; private set; }
    public PanelDispManager PanelDispManager   { get; private set; }


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

		NodeDispManager	= _partsDispWorks[(int)InputPanelLabel.Node].partsDispManager as NodeDispManager;
		MemberDispManager = _partsDispWorks[(int)InputPanelLabel.Member].partsDispManager as MemberDispManager;
		PanelDispManager	= _partsDispWorks[(int)InputPanelLabel.Panel].partsDispManager as PanelDispManager;
	}

    /// <summary> 初期化 </summary>
    void Start()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
        #endif

        this._webframe = webframe.Instance;

        //	描画マネージャを起動する
        InstantiatePrefab();

        Debug.Log("MainFrameManager Start() Done!!");

    }

    /// <summary>
    /// アクティブな表示モードを切り替える
    /// </summary>
    /// <param name="label"></param>
    public void SetActiveDispManager(InputPanelLabel label)
    {
        Debug.Log("MainFrameManager SetActiveDispManager実行");

        try {
            for (int i = 0; i < _partsDispWorks.Length; i++)
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
                        MemberDispManager.ChangeDispMode(MemberDispManager.DispType.Block);
                    }
                    else
                    {
                        MemberDispManager.ChangeDispMode(MemberDispManager.DispType.Line);
                    }
                }
                else
                {
                    _partsDispWorks[i].partsGameObject.SetActive((InputPanelLabel)i == label);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("MainFrameManager SetActiveDispManager エラー!!");
            Debug.Log(e.Message);
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
        //	パーツの作成
        for (int i = 0; i < _partsDispWorks.Length; i++){
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
			NodeDispManager.CalcNodeBlockScale();
			NodeDispManager.SetBlockStatusAll();
			MemberDispManager.SetBlockStatusAll();
			PanelDispManager.SetBlockStatusAll();
		}
		//	指定されたものと関わっているものだけ更新する
		else {
			NodeDispManager.SetBlockStatus("Node[" + search_node + "]" );
			if( NodeDispManager.CalcNodeBlockScale(search_node) ) {	//	サイズが更新されていたら節点のサイズも更新する
				MemberDispManager.SetBlockStatusAll();
				PanelDispManager.SetBlockStatusAll();
			}

			MemberDispManager.CheckNodeAndUpdateStatus( search_node );
			PanelDispManager.CheckNodeAndUpdateStatus( search_node );
		}
	}

    #endregion

    #region Javascript と連携 

    /// <summary> JavaScript から Input モードの変更通知が来た </summary>
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
                if (this.inputMode != InputPanelLabel.FixNode)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.FixNode;
                }
                break;

            case "elements":
                if (this.inputMode != InputPanelLabel.Element)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.Element;
                }
                break;

            case "joints":
                if (this.inputMode != InputPanelLabel.Joint)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.Joint;
                }
                break;

            case "notice_points":
                if (this.inputMode != InputPanelLabel.NoticePoints)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.NoticePoints;
                }
                break;

            case "fix_members":
                if (this.inputMode != InputPanelLabel.FixMember)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.FixMember;
                }
                break;

            case "loads":
                if (this.inputMode != InputPanelLabel.Load)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.Load;
                }
                break;

            case "fsec":
            case "comb.fsec":
            case "pic.fsec":
                if (this.inputMode != InputPanelLabel.Fsec)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.Fsec;
                }
                break;

            case "disg":
                if (this.inputMode != InputPanelLabel.Disg)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.Disg;
                }
                break;

            case "reac":
                if (this.inputMode != InputPanelLabel.Reac)
                {
                    flg = true;
                    this.inputMode = InputPanelLabel.Reac;
                }
                break;

            default:
                this.inputMode = InputPanelLabel.None;
                break;
        }
        if (flg == true){
            this.SetActiveDispManager(this.inputMode);
        }
    }

    /// <summary> JavaScript から Active Item の変更通知が来た </summary>
    public void SelectItemChange(int i)
    {
        if (this.inputMode == InputPanelLabel.None) {
            ExternalConnect.SendAngular("GetInputMode");
            return;
        }

        PartsDispWork partsDispWork = _partsDispWorks[(int)this.inputMode];
        partsDispWork.partsDispManager.ChengeForcuseBlock(i);
    }

    /// <summary> JavaScript から インプットデータ の変更通知が来た </summary>
    public void InputDataChenge(string json)
    {
        this._webframe.Create(json);
        Debug.Log("MainFrameManager _webframe.Create Done!!");
        this.CreateParts();
        Debug.Log("MainFrameManager CreateParts Done!!");
        this.SetAllBlockStatus();
        Debug.Log("MainFrameManager SetAllBlockStatus Done!!");

        if (this.inputMode == InputPanelLabel.None){
            Debug.Log("MainFrameManager Call GetInputMode");
            ExternalConnect.SendAngular("GetInputMode");
        }
    }

    /// <summary> JavaScript から インプットデータ の変更通知が来た </summary>
    public void InputDataChenge(string inputmode, string json)
    {
        InputModeChange(inputmode);

    }


    #endregion
}
