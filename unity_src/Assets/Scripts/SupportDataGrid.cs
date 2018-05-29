using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using SasakouFrame.ValueLabel;




public class SupportDataGrid : DataGridBase
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

		createGridSettingData[0].rowCount = headerData.supportDataCount;
		createGridSettingData[0].repeatColumnCount = new int[]{
			headerData.supportTypeCount,
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
			SkeletonResponseData.SupportData srcData = skeletonResponseData.listSupportData[r];

			_dataGrid.SetCellValue( 0, r, (int)SupportDataValue.NodeNo, srcData.nodeNo );

			int		data_start = (int)SupportDataValue.InputStart;
			int		data_end = (int)SupportDataValue.InputEnd;

			for( i = 0; i < srcData.supportTypeData.Length; i++ ) {
				var	dstTypeData = srcData.supportTypeData[i];
				
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.Tx, dstTypeData.TValue.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.Ty, dstTypeData.TValue.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.Tz, dstTypeData.TValue.z );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.Rx, dstTypeData.RValue.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.Ry, dstTypeData.RValue.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.Rz, dstTypeData.RValue.z );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.LimitTx, dstTypeData.LimitTValue.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.LimitTy, dstTypeData.LimitTValue.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.LimitTz, dstTypeData.LimitTValue.z );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.LimitRx, dstTypeData.LimitRValue.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.LimitRy, dstTypeData.LimitRValue.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)SupportDataValue.LimitRz, dstTypeData.LimitRValue.z );
			}
		}

		_isCreated = true;


		return	true;
	}



	/// <summary>
	/// 属性パネルの編集
	/// </summary>
	/// <param name="c"></param>
	/// <param name="r"></param>
	/// <param name="strValue"></param>
	void	onExtensionPanelEditEnd( int s, int r, int c, string strValue )
	{
		SkeletonResponseData	skeletonResponseData = SkeletonResponseData.Instance;
		SkeletonResponseData.SupportData	dstData = skeletonResponseData.listSupportData[r];

		
		switch( (SupportDataValue)c ) {
			//	線膨張係数
			case SupportDataValue.NodeNo:
				dstData.nodeNo = strValue;
				break;

			default:
				int		repeat_count;
				int		repeat_id;

				InputId2DataId( out repeat_count, out repeat_id, c, (int)SupportDataValue.InputStart, (int)SupportDataValue.InputEnd );
				var dstTypeData = dstData.supportTypeData[repeat_count];

				switch( (SupportDataValue)repeat_id ) {
					case	SupportDataValue.Tx:
						dstTypeData.TValue.x = strValue;
						break;
					case	SupportDataValue.Ty:
						dstTypeData.TValue.y = strValue;
						break;
					case	SupportDataValue.Tz:
						dstTypeData.TValue.z = strValue;
						break;
					case	SupportDataValue.Rx:
						dstTypeData.RValue.x = strValue;
						break;
					case	SupportDataValue.Ry:
						dstTypeData.RValue.y = strValue;
						break;
					case	SupportDataValue.Rz:
						dstTypeData.RValue.z = strValue;
						break;
					case	SupportDataValue.LimitTx:
						dstTypeData.LimitTValue.x = strValue;
						break;
					case	SupportDataValue.LimitTy:
						dstTypeData.LimitTValue.y = strValue;
						break;
					case	SupportDataValue.LimitTz:
						dstTypeData.LimitTValue.z = strValue;
						break;
					case	SupportDataValue.LimitRx:
						dstTypeData.LimitRValue.x = strValue;
						break;
					case	SupportDataValue.LimitRy:
						dstTypeData.LimitRValue.y = strValue;
						break;
					case	SupportDataValue.LimitRz:
						dstTypeData.LimitRValue.z = strValue;
						break;
				}
				break;
		}
	}
}
