using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFrameManager : MonoBehaviour {

    //	表示用ワークバッファ
    class PartsDispWork
    {
        public GameObject partsGameObject;
        public PartsDispManager partsDispManager;
    }


    MainFrameManager _mainFrameManager;
    SkeletonResponseData _skeletonResponseData = null;


    private NodeDispManager _nodeDispManager;
    public NodeDispManager nodeDispManager { get { return _nodeDispManager; } }
    private ElementDispManager _elementDispManager;
    public ElementDispManager elementDispManager { get { return _elementDispManager; } }
    private PanelDispManager _panelDispManager;
    public PanelDispManager panelDispManager { get { return _panelDispManager; } }


    /// <summary>
    /// 表示用オブジェクトのインスタンス化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dispObject"></param>
    /// <param name="dispManager"></param>
    void InstantiateDispPrefab(out PartsDispWork partsDispWork, GameObject baseObject)
    {
        partsDispWork = new PartsDispWork();
        if (baseObject == null)
        {
            return;
        }

        partsDispWork.partsGameObject = Instantiate(baseObject) as GameObject;
        partsDispWork.partsGameObject.transform.parent = this.gameObject.transform;

        partsDispWork.partsDispManager = partsDispWork.partsGameObject.GetComponent<PartsDispManager>();
    }



    /// <summary>
    /// プレハブをインスタンス化
    /// </summary>
    void InstantiatePrefab()
    {
        int i;

        for (i = 0; i < _partsDispWorks.Length; i++)
        {
            InstantiateDispPrefab(out _partsDispWorks[i], _dispPrefabs[i]);
        }

        _nodeDispManager = _partsDispWorks[(int)InputPanelLabel.Node].partsDispManager as NodeDispManager;
        _elementDispManager = _partsDispWorks[(int)InputPanelLabel.Element].partsDispManager as ElementDispManager;
        _panelDispManager = _partsDispWorks[(int)InputPanelLabel.Panel].partsDispManager as PanelDispManager;
    }



    /// <summary>
    /// 描画パーツの作成
    /// </summary>
    public void CreateParts()
    {
        int i;


        //	パーツの作成
        for (i = 0; i < _partsDispWorks.Length; i++)
        {
            if (_partsDispWorks[i].partsDispManager == null)
            {
                continue;
            }
            _partsDispWorks[i].partsDispManager.CreateParts();
            if ((InputPanelLabel)i != InputPanelLabel.Element)
            {
                _partsDispWorks[i].partsGameObject.SetActive(false);      //	最初は全部無効にしておく
            }
        }
    }



    /// <summary>
    /// 
    /// </summary>
    IEnumerator Start()
    {
        _skeletonResponseData = SkeletonResponseData.Instance;
        _mainFrameManager = FindObjectOfType<MainFrameManager>();
        _inputPanelManager = GetComponent<InputPanelManager>();

        //	描画マネージャを起動する
        InstantiatePrefab();

        yield return 0;

        CreateParts();


        SetAllBlockStatus();
    }




    /// <summary>
    /// パネルの取得
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dstPanel"></param>
    /// <param name="label"></param>
    void UpdateInputPanelCommon<T>(ref T dstPanel, InputPanelLabel label) where T : DataGridBase
    {
        InputPanelManager.InputPanelInfo inputPanelInfo = _inputPanelManager.GetInputPanelInfo(label);
        if (inputPanelInfo != null)
        {
            dstPanel = inputPanelInfo.dataGrid as T;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    void UpdateInputPanel()
    {
        DataGridBase dataGridBase = null;
        int i;

        for (i = 0; i < _partsDispWorks.Length; i++)
        {
            if (_partsDispWorks[i].partsGameObject == null)
            {
                continue;
            }
            if (_partsDispWorks[i].partsDispManager == null)
            {
                continue;
            }

            UpdateInputPanelCommon(ref dataGridBase, (InputPanelLabel)i);
            if (dataGridBase == null)
            {
                continue;
            }

            _partsDispWorks[i].partsDispManager.dataGridBase = dataGridBase;
        }
    }



    /// <summary>
    /// マウスの制御
    /// </summary>
    void InputMouse()
    {
        if (_inputPanelManager.inputMode == InputPanelLabel.None)
        {
            return;
        }

        PartsDispWork partsDispWork = _partsDispWorks[(int)_inputPanelManager.inputMode];

        if (partsDispWork.partsGameObject == null)
        {
            return;
        }
        if (partsDispWork.partsGameObject.activeSelf == false)
        {
            return;
        }
        if (partsDispWork.partsDispManager == null)
        {
            return;
        }


        partsDispWork.partsDispManager.InputMouse();
    }



    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        UpdateInputPanel();
        InputMouse();
    }



    /// <summary>
    /// 全部、または指定された節点と一致するブロックを設定する
    /// </summary>
    /// <param name="search_node"></param>
    public void SetAllBlockStatus(int search_node = -1)
    {
        //	全て設定する
        if (search_node == -1)
        {
            _nodeDispManager.CalcNodeBlockScale();
            _nodeDispManager.SetBlockStatusAll();
            _elementDispManager.SetBlockStatusAll();
            _panelDispManager.SetBlockStatusAll();
        }
        //	指定されたものと関わっているものだけ更新する
        else
        {
            _nodeDispManager.SetBlockStatus(search_node);
            if (_nodeDispManager.CalcNodeBlockScale(search_node))
            {   //	サイズが更新されていたら節点のサイズも更新する
                _elementDispManager.SetBlockStatusAll();
                _panelDispManager.SetBlockStatusAll();
            }

            _elementDispManager.CheckNodeAndUpdateStatus(search_node);
            _panelDispManager.CheckNodeAndUpdateStatus(search_node);
        }
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
            if ((InputPanelLabel)i == InputPanelLabel.Element)
            {
                if (label == InputPanelLabel.Element)
                {
                    _elementDispManager.ChangeDispMode(ElementDispManager.DispType.Block);
                }
                else
                {
                    _elementDispManager.ChangeDispMode(ElementDispManager.DispType.Line);
                }
            }
            else
            {
                _partsDispWorks[i].partsGameObject.SetActive((InputPanelLabel)i == label);
            }
        }
    }
}
