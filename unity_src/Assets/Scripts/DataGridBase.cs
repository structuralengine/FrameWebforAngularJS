using UnityEngine;
using System.Collections;



/// <summary>
/// データグリッドのベースクラス
/// </summary>
public class DataGridBase : MonoBehaviour
{
	public	delegate	void	PanelEditEndCallback( int s, int r, int c, string strValue );


	protected	DataGrid	_dataGrid = null;
	public		DataGrid	dataGrid { get { return _dataGrid; } }

	protected	bool		_isCreated = false;
	public		bool		isCreated { get { return _isCreated; } }

	private		PanelEditEndCallback	_onEditEndCallback = null;
	public		PanelEditEndCallback	onEditEndCallback { set {  _onEditEndCallback=value; } }


	protected	MainFrameManager		_mainFrameManager = null;




	/// <summary>
	/// 
	/// </summary>
	protected virtual void	Start()
	{
		_mainFrameManager = FindObjectOfType<MainFrameManager>();
	}


	/// <summary>
	/// グリッドの共通初期化
	/// </summary>
	public virtual bool CreateGrid()
	{
		return	true;
	}
	public bool CreateGridCommon()
	{
		_dataGrid = GetComponent<DataGrid>();
		if( _dataGrid == null ) {
			return	false;
		}
		DataGridTable	dataGridTable = GetComponentInChildren<DataGridTable>();
		if( dataGridTable == null ) { 
			return	false;
		}
		RectTransform	rectTransfrom = dataGridTable.ContentObject.GetComponent<RectTransform>();
		int			i, j;

		DataGrid.CreateGridSettingData[] createGridSettingData = _dataGrid.createGridSettingData;

		if( createGridSettingData != null ){
			for( i=0; i<createGridSettingData.Length; i++ ){
				_dataGrid.SetSheetRowCount( i, createGridSettingData[i].rowCount );

				if( createGridSettingData[i].repeatColumnCount != null ){
					for( j=0; j<createGridSettingData[i].repeatColumnCount.Length; j++ ){
						_dataGrid.SetSheetRepeatColumn( i, j, createGridSettingData[i].repeatColumnCount[j] );
					}
				}
			}
		}

		_dataGrid.CreateGrid( rectTransfrom );

		_dataGrid.SetEventDelegate( GridCellAction.EventType.EditEnd, (object)(GridCellAction.eventEditEnd)(onPanelEditEnd) );


		return		true;
	}



	/// <summary>
	/// パネルの編集のコールバック
	/// </summary>
	/// <param name="r"></param>
	/// <param name="c"></param>
	/// <param name="strValue"></param>
	void	onPanelEditEnd( int s, int r, int c, string strValue )
	{
		//	何故かオーバーライドした関数は使えないのでデリゲートで対応する
		if( _onEditEndCallback != null ) {
			_onEditEndCallback( s, r, c, strValue );
		}
	}




	/// <summary>
	///	リピートのカウントの取得
	/// </summary>
	/// <param name="c"></param>
	/// <param name="start"></param>
	/// <param name="end"></param>
	/// <returns></returns>
	protected void		InputId2DataId( out int repeat_count, out int repeat_id, int c, int start, int end )
	{
		repeat_count = (c - start) / (end+1-start);
		repeat_id = (c - start) % (end+1-start) + start;
	}
}
