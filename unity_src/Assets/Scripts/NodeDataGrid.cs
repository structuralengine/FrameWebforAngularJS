using UnityEngine;
using System.Collections;

public class NodeDataGrid : DataGridBase
{
	// <summary>
	/// グリッド作成用データオーバーライド
	/// </summary>
	/// <returns></returns>
	static	public  DataGrid.CreateGridSettingData[] GetCreateGridSettingData()
	{
		SkeletonResponseData	skeletonResponseData = SkeletonResponseData.Instance;
		SkeletonResponseData.HeaderData	headerData = skeletonResponseData.headerData;

		//	入力フィールドの設定データの作成
		DataGrid.CreateGridSettingData[] createGridSettingData = new DataGrid.CreateGridSettingData[1];

		createGridSettingData[0].rowCount = headerData.nodeCount;

		return	createGridSettingData;
	}


	/// <summary>
	/// 
	/// </summary>
	override public	bool	CreateGrid()
	{
		SkeletonResponseData	skeletonResponseData = SkeletonResponseData.Instance;
		

		//	共通初期化
		base.onEditEndCallback = onExtensionPanelEditEnd;
		if( base.CreateGridCommon() == false ) {
			return	false;
		}

		int		r, c;
	
		//	セルにデータを設定
		for( r = 0; r < skeletonResponseData.listNodePoint.Count; r++ ) {
			for( c = 0; c < 3; c++ ) {
				_dataGrid.SetCellValue( 0, r, c, skeletonResponseData.listNodePoint[r][c] );
			}
		}

		_isCreated = true;


		return	true;
	}



	/// <summary>
	/// データの入力対応
	/// </summary>
	/// <param name="c"></param>
	/// <param name="r"></param>
	/// <param name="strValue"></param>
	void	onExtensionPanelEditEnd( int s, int r, int c, string strValue )
	{
		SkeletonResponseData	skeletonResponseData = SkeletonResponseData.Instance;

		SystemUtility.StringVector3	stringVector = 	skeletonResponseData.listNodePoint[r];


		if( stringVector[c] != strValue ) { 
			stringVector[c] = strValue;
			_mainFrameManager.SetAllBlockStatus( r );
		}
	}
}
