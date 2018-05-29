using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using SasakouFrame.ValueLabel;




public class  AttentionPointDataGrid : DataGridBase
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

		createGridSettingData[0].rowCount = headerData.attentionPointDataCount;
		createGridSettingData[0].repeatColumnCount = new int[]{
			headerData.attentionPointInputCount,
		};


		return	createGridSettingData;
	}



	/// <summary>
	/// グリッドの生成
	/// </summary>
	override public	bool 	CreateGrid()
	{
		SkeletonResponseData	skeletonResponseData = SkeletonResponseData.Instance;


		//	共通初期化
		base.onEditEndCallback = onExtensionPanelEditEnd;
		if( base.CreateGridCommon() == false ) {
			return	false;
		}

		int		r;
		int		i;


		//	セルにデータを設定
		for( r = 0; r < skeletonResponseData.listPanelData.Count; r++ ) {
			SkeletonResponseData.AttentionPointData srcData = skeletonResponseData.listAttentionPointData[r];

			_dataGrid.SetCellValue( 0, r, (int)AttentionPointDataValue.BuzaiNo, srcData.buzaiNo );
			_dataGrid.SetCellValue( 0, r, (int)AttentionPointDataValue.BuzaiChou, srcData.buzaiChou );
			_dataGrid.SetCellValue( 0, r, (int)AttentionPointDataValue.BuzaiName, srcData.buzaiName );

			int		data_start = (int)AttentionPointDataValue.InputStart;
			int		data_end = (int)AttentionPointDataValue.InputEnd;

			for( i = 0; i < srcData.attentionPointInputData.Length; i++ ) {
				var	dstTypeData = srcData.attentionPointInputData[i];
				
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)AttentionPointDataValue.PointValue, dstTypeData.pointValue );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)AttentionPointDataValue.PointName, dstTypeData.pointName );
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
		SkeletonResponseData.AttentionPointData	dstData = skeletonResponseData.listAttentionPointData[r];

		
		switch( (AttentionPointDataValue)c ) {
			//	線膨張係数
			case AttentionPointDataValue.BuzaiNo:
				dstData.buzaiNo = strValue;
				break;

			case AttentionPointDataValue.BuzaiChou:
				dstData.buzaiChou = strValue;
				break;

			case AttentionPointDataValue.BuzaiName:
				dstData.buzaiName = strValue;
				break;

			default:
				int		repeat_count;
				int		repeat_id;

				InputId2DataId( out repeat_count, out repeat_id, c, (int)AttentionPointDataValue.InputStart, (int)AttentionPointDataValue.InputEnd );
				var dstTypeData = dstData.attentionPointInputData[repeat_count];

				switch( (AttentionPointDataValue)repeat_id ) {
					case	AttentionPointDataValue.PointValue:
						dstTypeData.pointValue = strValue;
						break;
					case	AttentionPointDataValue.PointName:
						dstTypeData.pointName = strValue;
						break;
				}
				break;
		}
	}
}
