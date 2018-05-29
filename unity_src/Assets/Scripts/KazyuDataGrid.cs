using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using SasakouFrame.InputLabel;
using SasakouFrame.ValueLabel;



public class KazyuDataGrid : DataGridBase
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
		DataGrid.CreateGridSettingData[] createGridSettingData = new DataGrid.CreateGridSettingData[Enum.GetNames(typeof(KazyuPanelLabel)).Length];

		createGridSettingData[(int)KazyuPanelLabel.KazyuName].rowCount = headerData.kazyuDataCount;
		createGridSettingData[(int)KazyuPanelLabel.KazyuPower].rowCount = headerData.kazyuDataCount;

		return	createGridSettingData;
	}



	/// <summary>
	/// 
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
				

		//	セルにデータを設定
		for( r = 0; r < skeletonResponseData.listElementData.Count; r++ ) {
			SkeletonResponseData.KazyuData srcData = skeletonResponseData.listKazyuData[r];

			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuName, r, (int)KazyNameDataValue.KazyuMark, srcData.kazyNameData.KazyuMark );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuName, r, (int)KazyNameDataValue.KazyuName, srcData.kazyNameData.KazyuName );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuName, r, (int)KazyNameDataValue.Support, srcData.kazyNameData.Support );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuName, r, (int)KazyNameDataValue.MemberSupport, srcData.kazyNameData.MemberSupport );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuName, r, (int)KazyNameDataValue.Danmen, srcData.kazyNameData.Danmen );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuName, r, (int)KazyNameDataValue.Joint, srcData.kazyNameData.Joint );
			
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.KazyuNo, srcData.kazyuNo );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.ElementStart, srcData.kazyuElementData.Start );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.ElementEnd, srcData.kazyuElementData.End );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.ElementMark, srcData.kazyuElementData.Mark );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.ElementXYZ, srcData.kazyuElementData.XYZ );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.ElementL1, srcData.kazyuElementData.L1 );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.ElementL2, srcData.kazyuElementData.L2 );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.ElementP1, srcData.kazyuElementData.P1 );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.ElementP2, srcData.kazyuElementData.P2 );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.NodeNo, srcData.kazyuNodeData.nodeNo );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.NodePointX, srcData.kazyuNodeData.Point.x );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.NodePointY, srcData.kazyuNodeData.Point.y );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.NodePointZ, srcData.kazyuNodeData.Point.z );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.NodeMormentX, srcData.kazyuNodeData.Morment.x );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.NodeMormentY, srcData.kazyuNodeData.Morment.y );
			_dataGrid.SetCellValue( (int)KazyuPanelLabel.KazyuPower, r, (int)KazyuPowerDataValue.NodeMormentZ, srcData.kazyuNodeData.Morment.z );
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
		SkeletonResponseData.KazyuData		dstData = skeletonResponseData.listKazyuData[r];
		
		
		if( s == (int)KazyuPanelLabel.KazyuName ) { 
			switch( (KazyNameDataValue)c ) {
				case KazyNameDataValue.KazyuMark:
					dstData.kazyNameData.KazyuMark = strValue;
					break;

				case KazyNameDataValue.KazyuName:
					dstData.kazyNameData.KazyuName = strValue;
					break;

				case KazyNameDataValue.Support:
					dstData.kazyNameData.Support = strValue;
					break;

				case KazyNameDataValue.MemberSupport:
					dstData.kazyNameData.MemberSupport = strValue;
					break;

				case KazyNameDataValue.Danmen:
					dstData.kazyNameData.Danmen = strValue;
					break;

				case KazyNameDataValue.Joint:
					dstData.kazyNameData.Joint = strValue;
					break;
			}
		}
		else if( s == (int)KazyuPanelLabel.KazyuPower ){
			switch( (KazyuPowerDataValue)c ) {
				case KazyuPowerDataValue.KazyuNo:
					dstData.kazyuNo = strValue;
					break;

				case KazyuPowerDataValue.ElementStart:
					dstData.kazyuElementData.Start = strValue;
					break;

				case KazyuPowerDataValue.ElementEnd:
					dstData.kazyuElementData.End = strValue;
					break;

				case KazyuPowerDataValue.ElementMark:
					dstData.kazyuElementData.Mark = strValue;
					break;

				case KazyuPowerDataValue.ElementXYZ:
					dstData.kazyuElementData.XYZ = strValue;
					break;

				case KazyuPowerDataValue.ElementL1:
					dstData.kazyuElementData.L1 = strValue;
					break;

				case KazyuPowerDataValue.ElementL2:
					dstData.kazyuElementData.L2 = strValue;
					break;

				case KazyuPowerDataValue.ElementP1:
					dstData.kazyuElementData.P1 = strValue;
					break;

				case KazyuPowerDataValue.ElementP2:
					dstData.kazyuElementData.P2 = strValue;
					break;

				case KazyuPowerDataValue.NodeNo:
					dstData.kazyuNodeData.nodeNo = strValue;
					break;

				case KazyuPowerDataValue.NodePointX:
					dstData.kazyuNodeData.Point.x = strValue;
					break;

				case KazyuPowerDataValue.NodePointY:
					dstData.kazyuNodeData.Point.y = strValue;
					break;

				case KazyuPowerDataValue.NodePointZ:
					dstData.kazyuNodeData.Point.z = strValue;
					break;

				case KazyuPowerDataValue.NodeMormentX:
					dstData.kazyuNodeData.Morment.x = strValue;
					break;

				case KazyuPowerDataValue.NodeMormentY:
					dstData.kazyuNodeData.Morment.y = strValue;
					break;

				case KazyuPowerDataValue.NodeMormentZ:
					dstData.kazyuNodeData.Morment.z = strValue;
					break;
			}
		}
	}


}
