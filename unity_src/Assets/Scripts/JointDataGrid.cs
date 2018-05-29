using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using SasakouFrame.ValueLabel;




public class JointDataGrid : DataGridBase
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

		createGridSettingData[0].rowCount = headerData.jointDataCount;
		createGridSettingData[0].repeatColumnCount = new int[]{
			headerData.jointTypeCount,
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
			SkeletonResponseData.JointData srcData = skeletonResponseData.listJointData[r];

			_dataGrid.SetCellValue( 0, r, (int)JointDataValue.NodeNo, srcData.nodeNo );

			int		data_start = (int)JointDataValue.InputStart;
			int		data_end = (int)JointDataValue.InputEnd;

			for( i = 0; i < srcData.jointTypeData.Length; i++ ) {
				var	dstTypeData = srcData.jointTypeData[i];
				
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)JointDataValue.IPointConditionX, dstTypeData.IPointCondition.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)JointDataValue.IPointConditionY, dstTypeData.IPointCondition.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)JointDataValue.IPointConditionZ, dstTypeData.IPointCondition.z );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)JointDataValue.JPointConditionX, dstTypeData.JPointCondition.x );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)JointDataValue.JPointConditionY, dstTypeData.JPointCondition.y );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)JointDataValue.JPointConditionZ, dstTypeData.JPointCondition.z );
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
		SkeletonResponseData.JointData	dstData = skeletonResponseData.listJointData[r];

		
		switch( (JointDataValue)c ) {
			//	線膨張係数
			case JointDataValue.NodeNo:
				dstData.nodeNo = strValue;
				break;

			default:
				int		repeat_count;
				int		repeat_id;

				InputId2DataId( out repeat_count, out repeat_id, c, (int)JointDataValue.InputStart, (int)JointDataValue.InputEnd );
				var dstTypeData = dstData.jointTypeData[repeat_count];

				switch( (JointDataValue)repeat_id ) {
					case	JointDataValue.IPointConditionX:
						dstTypeData.IPointCondition.x = strValue;
						break;
					case	JointDataValue.IPointConditionY:
						dstTypeData.IPointCondition.y = strValue;
						break;
					case	JointDataValue.IPointConditionZ:
						dstTypeData.IPointCondition.z = strValue;
						break;
					case	JointDataValue.JPointConditionX:
						dstTypeData.JPointCondition.x = strValue;
						break;
					case	JointDataValue.JPointConditionY:
						dstTypeData.JPointCondition.y = strValue;
						break;
					case	JointDataValue.JPointConditionZ:
						dstTypeData.JPointCondition.z = strValue;
						break;
				}
				break;
		}
	}
}
