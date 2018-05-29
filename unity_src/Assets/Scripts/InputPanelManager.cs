using UnityEngine;
using System;
using System.Collections;
using SasakouFrame.InputLabel;



public class InputPanelManager : MonoBehaviour
{
	const	float PANEL_Y_POS = 244;

	public class InputPanelInfo 
	{
		public	GameObject		targetObject = null;
		public	GameObject		panelObject = null;
		public	DataGridBase	dataGrid = null;
	}


	[SerializeField]
    GameObject	_mainCanvasObject = null;


	MainFrameManager			_mainFrameManager = null;
	InputPanelInfo[]			_inputPanelInfoArray;
	private	InputPanelLabel		_inputMode = InputPanelLabel.None;
	public	InputPanelLabel		inputMode{ get{ return _inputMode; } }


	//	パネル情報の取得
	public InputPanelInfo		GetInputPanelInfo( InputPanelLabel label )
	{
		if( _inputPanelInfoArray == null ) {
			return	null;
		}

		return	_inputPanelInfoArray[(int)label];
	}




	/// <summary>
	/// 
	/// </summary>
    void Start ()
	{
		_mainFrameManager = FindObjectOfType<MainFrameManager>();

		int		i;

		_inputPanelInfoArray = new InputPanelInfo[(int)InputPanelLabel.Max];
		for( i = 0; i<_inputPanelInfoArray.Length; i++ ) {
			_inputPanelInfoArray[i] = new InputPanelInfo();
		}
	}


	/// <summary>
	/// パネルの更新
	/// </summary>
	void	UpdatePanel()
	{
		if( _inputPanelInfoArray == null ) {
			return;
		}

		int		i;

		for( i = 0; i < _inputPanelInfoArray.Length; i++ ) {
			if( _inputPanelInfoArray[i] == null ) {
				continue;
			}
			if( _inputPanelInfoArray[i].dataGrid == null ) {
				continue;
			}
			if( _inputPanelInfoArray[i].dataGrid.isCreated == false ) {
				_inputPanelInfoArray[i].dataGrid.CreateGrid();
			}
		}
	}
	
	

	
	/// <summary>
	/// 
	/// </summary>
	void Update ()
	{
		UpdatePanel();
	}


	/// <summary>
	/// すでにあるインプットパネルをリフレッシュする
	/// </summary>
	public	void	RefreshInputPanel()
	{
		int		i;

		for( i = 0; i < _inputPanelInfoArray.Length; i++ ) {
			if( _inputPanelInfoArray[i].dataGrid == null ) {
				continue;
			}

			GameObject.Destroy( _inputPanelInfoArray[i].panelObject );
			_inputPanelInfoArray[i].panelObject = null;
		}
	}



	/// <summary>
	/// 
	/// </summary>
	/// <param name="label"></param>
	/// <returns></returns>
	DataGrid.CreateGridSettingData[]	GetCreateGridSettingData( InputPanelLabel label )
	{
		switch( label ) {
			case	InputPanelLabel.Node:				return	NodeDataGrid.GetCreateGridSettingData();			//	節点
			case	InputPanelLabel.Element:			return	ElementDataGrid.GetCreateGridSettingData();			//	要素
			case	InputPanelLabel.Panel:				return	PanelDataGrid.GetCreateGridSettingData();			//	パネル
			case	InputPanelLabel.Support:			return	SupportDataGrid.GetCreateGridSettingData();			//	支点
			case	InputPanelLabel.Joint:				return	JointDataGrid.GetCreateGridSettingData();			//	結合
			case	InputPanelLabel.MemberSupport:		return	MemberSupportDataGrid.GetCreateGridSettingData();	//	バネ
			case	InputPanelLabel.AttentionPoint:		return	AttentionPointDataGrid.GetCreateGridSettingData();	//	着目点
			case	InputPanelLabel.Kazyu:				return	KazyuDataGrid.GetCreateGridSettingData();			//	荷重
			case	InputPanelLabel.Kumiawase:			return	KumiawaseDataGrid.GetCreateGridSettingData();		//	組合せ
		}

		return	null;
	}


	/// <summary>
	/// パネルの表示
	/// </summary>
	/// <param name="panelObject"></param>
	/// <param name="targetPanel"></param>
	public void	ShowPanelCommon( InputPanelLabel label, GameObject targetPanel )
	{
		InputPanelInfo	inputPanelInfo = _inputPanelInfoArray[(int)label];
		int		i;

		//	無かったら作る
		if( inputPanelInfo.panelObject == null ) {
			DataGrid.reserveCreateGridSettingData = GetCreateGridSettingData( label );
			inputPanelInfo.targetObject = targetPanel;
			inputPanelInfo.panelObject = Instantiate(targetPanel) as GameObject;
			inputPanelInfo.panelObject.transform.SetParent(_mainCanvasObject.transform);
			Vector3		pos;

			pos = inputPanelInfo.panelObject.transform.localPosition;
			pos.y = PANEL_Y_POS;
			inputPanelInfo.panelObject.transform.localPosition = pos;

			inputPanelInfo.dataGrid = inputPanelInfo.panelObject.GetComponentInChildren<DataGridBase>();

			inputPanelInfo.dataGrid.CreateGrid();
			_inputMode = label;
		}
		//	あったら表示・非表示の切り替え
		else {
			if( inputPanelInfo.panelObject.activeSelf ) {
				_inputMode = InputPanelLabel.None;
			}
			else {
				_inputMode = label;
			}
			inputPanelInfo.panelObject.SetActive( !inputPanelInfo.panelObject.activeSelf );

		}

		//	自分を表示するとき、他のは非表示にする
		if( inputPanelInfo.panelObject.activeSelf ) {
			for( i = 0; i < _inputPanelInfoArray.Length; i++ ) {
				if( i == (int)label ) {
					continue;
				}
				if( _inputPanelInfoArray[i].panelObject != null ) {
					_inputPanelInfoArray[i].panelObject.SetActive( false );
				}
			}
		}

		_mainFrameManager.SetActiveDispManager( _inputMode );
	}


	


	/// <summary>
	/// 各パネルの表示
	/// </summary>
	/// <param name="TargetPanel"></param>
    public void ShowNodeInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.Node, targetPanel );
    }
    public void ShowElementInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.Element, targetPanel );
    }
    public void ShowPanelInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.Panel, targetPanel );
    }
    public void ShowSupportInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.Support, targetPanel );
    }
    public void ShowJointInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.Joint, targetPanel );
    }
    public void ShowMemberSupportInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.MemberSupport, targetPanel );
    }
    public void ShowAttentionPointInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.AttentionPoint, targetPanel );
    }
    public void ShowKazyuInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.Kazyu, targetPanel );
    }
    public void ShowKumiawaseInputPanel( GameObject targetPanel )
    {
		ShowPanelCommon( InputPanelLabel.Kumiawase, targetPanel );
    }


	
}
