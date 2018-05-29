using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using SasakouFrame.ValueLabel;




public class MemberSupportDataGrid : DataGridBase
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

		createGridSettingData[0].rowCount = headerData.memberSupportDataCount;
		createGridSettingData[0].repeatColumnCount = new int[]{
			headerData.memberSupportTypeCount,
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
			SkeletonResponseData.MemberSupportData srcData = skeletonResponseData.listMemberSupportData[r];

			_dataGrid.SetCellValue( 0, r, (int)MemberSupportDataValue.NodeNo, srcData.nodeNo );

			int		data_start = (int)MemberSupportDataValue.InputStart;
			int		data_end = (int)MemberSupportDataValue.InputEnd;

			for( i = 0; i < srcData.memberSupportTypeData.Length; i++ ) {
				var	dstTypeData = srcData.memberSupportTypeData[i];
				
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.Tx, dstTypeData.TValue.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.Ty, dstTypeData.TValue.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.Tz, dstTypeData.TValue.z );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.Rx, dstTypeData.RValue.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.Ry, dstTypeData.RValue.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.Rz, dstTypeData.RValue.z );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.LimitTx, dstTypeData.LimitTValue.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.LimitTy, dstTypeData.LimitTValue.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.LimitTz, dstTypeData.LimitTValue.z );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.LimitRx, dstTypeData.LimitRValue.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.LimitRy, dstTypeData.LimitRValue.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)MemberSupportDataValue.LimitRz, dstTypeData.LimitRValue.z );
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
		SkeletonResponseData.MemberSupportData	dstData = skeletonResponseData.listMemberSupportData[r];

		
		switch( (MemberSupportDataValue)c ) {
			//	線膨張係数
			case MemberSupportDataValue.NodeNo:
				dstData.nodeNo = strValue;
				break;

			default:
				int		repeat_count;
				int		repeat_id;

				InputId2DataId( out repeat_count, out repeat_id, c, (int)MemberSupportDataValue.InputStart, (int)MemberSupportDataValue.InputEnd );
				var dstTypeData = dstData.memberSupportTypeData[repeat_count];

				switch( (MemberSupportDataValue)repeat_id ) {
					case	MemberSupportDataValue.Tx:
						dstTypeData.TValue.x = strValue;
						break;
					case	MemberSupportDataValue.Ty:
						dstTypeData.TValue.y = strValue;
						break;
					case	MemberSupportDataValue.Tz:
						dstTypeData.TValue.z = strValue;
						break;
					case	MemberSupportDataValue.Rx:
						dstTypeData.RValue.x = strValue;
						break;
					case	MemberSupportDataValue.Ry:
						dstTypeData.RValue.y = strValue;
						break;
					case	MemberSupportDataValue.Rz:
						dstTypeData.RValue.z = strValue;
						break;
					case	MemberSupportDataValue.LimitTx:
						dstTypeData.LimitTValue.x = strValue;
						break;
					case	MemberSupportDataValue.LimitTy:
						dstTypeData.LimitTValue.y = strValue;
						break;
					case	MemberSupportDataValue.LimitTz:
						dstTypeData.LimitTValue.z = strValue;
						break;
					case	MemberSupportDataValue.LimitRx:
						dstTypeData.LimitRValue.x = strValue;
						break;
					case	MemberSupportDataValue.LimitRy:
						dstTypeData.LimitRValue.y = strValue;
						break;
					case	MemberSupportDataValue.LimitRz:
						dstTypeData.LimitRValue.z = strValue;
						break;
				}
				break;
		}
	}
}
