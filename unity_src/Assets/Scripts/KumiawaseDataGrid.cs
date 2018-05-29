using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using SasakouFrame.InputLabel;
using SasakouFrame.ValueLabel;



public class KumiawaseDataGrid : DataGridBase
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
		DataGrid.CreateGridSettingData[] createGridSettingData = new DataGrid.CreateGridSettingData[Enum.GetNames(typeof(KumiawasePanelLabel)).Length];

		createGridSettingData[(int)KumiawasePanelLabel.Define].rowCount = headerData.kumiawaseDefineCount;
		createGridSettingData[(int)KumiawasePanelLabel.Define].repeatColumnCount = new int[]{
			headerData.kumiawaseCombineInputCount,
		};
		createGridSettingData[(int)KumiawasePanelLabel.Combine].rowCount = headerData.kumiawaseCombineCount;
		createGridSettingData[(int)KumiawasePanelLabel.Combine].repeatColumnCount = new int[]{
			headerData.kumiawaseCombineInputCount,
		};
		createGridSettingData[(int)KumiawasePanelLabel.PickUp].rowCount = headerData.kumiawasePickupCount;
		createGridSettingData[(int)KumiawasePanelLabel.PickUp].repeatColumnCount = new int[]{
			headerData.kumiawasePickupInputCount,
		};

		createGridSettingData[0].rowCount = headerData.nodeCount;

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

		int		i;
		int		r;
		int		sheet_id;		

		//	セルにデータを設定

		//	Define
		sheet_id = (int)KumiawasePanelLabel.Define;
		for( r = 0; r < skeletonResponseData.listKumiawaseDefineData.Count; r++ ) {
			SkeletonResponseData.KumiawaseDefineData srcData = skeletonResponseData.listKumiawaseDefineData[r];

			_dataGrid.SetCellValue( sheet_id, r, (int)KumiawaseDefineDataValue.DefNo, srcData.defNo );

			int		data_start = (int)KumiawaseDefineDataValue.InputStart;
			int		data_end = (int)KumiawaseDefineDataValue.InputEnd;

			for( i = 0; i < srcData.caseNo.Length; i++ ) {
				var	dstTypeData = srcData.caseNo[i];
				
				_dataGrid.SetCellRepeatValue( sheet_id, r, i, data_start, data_end, (int)KumiawaseDefineDataValue.CaseNo, dstTypeData );
			}
		}

		//	Combine
		sheet_id = (int)KumiawasePanelLabel.Combine;
		for( r = 0; r < skeletonResponseData.listKumiawaseCombineData.Count; r++ ) {
			SkeletonResponseData.KumiawaseCombineData srcData = skeletonResponseData.listKumiawaseCombineData[r];

			_dataGrid.SetCellValue( sheet_id, r, (int)KumiawaseCombineDataValue.WarimashiKeisuu, srcData.warimashiKeisuu );

			int		data_start = (int)KumiawaseCombineDataValue.InputStart;
			int		data_end = (int)KumiawaseCombineDataValue.InputEnd;

			for( i = 0; i < srcData.kumiawaseCombineValueData.Length; i++ ) {
				var	dstTypeData = srcData.kumiawaseCombineValueData[i];
				
				_dataGrid.SetCellRepeatValue( sheet_id, r, i, data_start, data_end, (int)KumiawaseCombineDataValue.CaseNo, dstTypeData.caseNo );
				_dataGrid.SetCellRepeatValue( sheet_id, r, i, data_start, data_end, (int)KumiawaseCombineDataValue.KazyuhoseiKeisuu, dstTypeData.kazyuhoseiKeisuu );
			}

			int		data_offset = ((data_end-data_start+1) * srcData.kumiawaseCombineValueData.Length) + data_start;

			_dataGrid.SetCellValue( sheet_id, r, (int)KumiawaseCombineDataValue.Mark-(int)KumiawaseCombineDataValue.InputTailStart+data_offset, srcData.mark );
			_dataGrid.SetCellValue( sheet_id, r, (int)KumiawaseCombineDataValue.KumiawaseKazyuName-(int)KumiawaseCombineDataValue.InputTailStart+data_offset, srcData.kumiawaseKazyuName );
		}

		//	PickUp
		sheet_id = (int)KumiawasePanelLabel.PickUp;
		for( r = 0; r < skeletonResponseData.listKumiawasePickupData.Count; r++ ) {
			SkeletonResponseData.KumiawasePickupData srcData = skeletonResponseData.listKumiawasePickupData[r];

			int		data_start = (int)KumiawasePickUpDataValue.InputStart;
			int		data_end = (int)KumiawasePickUpDataValue.InputEnd;
			

			for( i = 0; i < srcData.KumiawaseNo.Length; i++ ) {
				var	dstTypeData = srcData.KumiawaseNo[i];
				
				_dataGrid.SetCellRepeatValue( sheet_id, r, i, data_start, data_end, (int)KumiawasePickUpDataValue.KumiawaseNo, dstTypeData );
			}

			int		data_offset = ((data_end-data_start+1) * srcData.KumiawaseNo.Length) + data_start;

			_dataGrid.SetCellValue( sheet_id, r, (int)KumiawasePickUpDataValue.Mark-(int)KumiawasePickUpDataValue.InputTailStart+data_offset+1, srcData.mark );
			_dataGrid.SetCellValue( sheet_id, r, (int)KumiawasePickUpDataValue.Name-(int)KumiawasePickUpDataValue.InputTailStart+data_offset+1, srcData.name );
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
	
		
		if( s == (int)KumiawasePanelLabel.Define ) {
			var	dstData = skeletonResponseData.listKumiawaseDefineData[r];

			switch( (KumiawaseDefineDataValue)c ) {
				case KumiawaseDefineDataValue.DefNo:
					dstData.defNo = strValue;
					break;

				//	繰り返し入力
				default:
					int		repeat_count;
					int		repeat_id;

					InputId2DataId( out repeat_count, out repeat_id, c, (int)KumiawaseDefineDataValue.InputStart, (int)KumiawaseDefineDataValue.InputEnd );
					dstData.caseNo[repeat_count] = strValue;
					break;
			}
		}
		else if( s == (int)KumiawasePanelLabel.Combine ){
			var	dstData = skeletonResponseData.listKumiawaseCombineData[r];

			switch( (KumiawaseCombineDataValue)c ) {
				case KumiawaseCombineDataValue.WarimashiKeisuu:
					dstData.warimashiKeisuu = strValue;
					break;

				default:
					int		data_start = (int)KumiawaseCombineDataValue.InputStart;
					int		data_end = (int)KumiawaseCombineDataValue.InputEnd;

					int		data_offset = ((data_end-data_start+1) * dstData.kumiawaseCombineValueData.Length) + data_start;

					//	繰り返し入力
					if( data_offset > c ) {
						int		repeat_count;
						int		repeat_id;

						InputId2DataId( out repeat_count, out repeat_id, c, (int)KumiawaseCombineDataValue.InputStart, (int)KumiawaseCombineDataValue.InputEnd );
						var dstTypeData = dstData.kumiawaseCombineValueData[repeat_count];

						switch( (KumiawaseCombineDataValue)repeat_id ) {
							case	KumiawaseCombineDataValue.CaseNo:
								dstTypeData.caseNo = strValue;
								break;
							case	KumiawaseCombineDataValue.KazyuhoseiKeisuu:
								dstTypeData.kazyuhoseiKeisuu = strValue;
								break;
						}
					}
					//	末尾データ入力
					else {
						c = c-data_offset+(int)KumiawaseCombineDataValue.InputTailStart;
						switch( (KumiawaseCombineDataValue)(c) ) {
							case	KumiawaseCombineDataValue.Mark:
								dstData.mark = strValue;
								break;
							case	KumiawaseCombineDataValue.KumiawaseKazyuName:
								dstData.kumiawaseKazyuName = strValue;
								break;
						}
					}
					break;
			}
		}
		else if( s == (int)KumiawasePanelLabel.PickUp ){
			var	dstData = skeletonResponseData.listKumiawasePickupData[r];

			int		data_start = (int)KumiawasePickUpDataValue.InputStart;
			int		data_end = (int)KumiawasePickUpDataValue.InputEnd;
			int		data_offset = ((data_end-data_start+1) * dstData.KumiawaseNo.Length) + data_start;

			//	繰り返し入力
			if( data_offset > c ) {
				int		repeat_count;
				int		repeat_id;

				InputId2DataId( out repeat_count, out repeat_id, c, (int)KumiawasePickUpDataValue.InputStart, (int)KumiawasePickUpDataValue.InputEnd );
				dstData.KumiawaseNo[repeat_count] = strValue;
			}
			//	末尾データ入力
			else {
				c = c-data_offset+(int)KumiawasePickUpDataValue.InputTailStart-1;
				switch( (KumiawasePickUpDataValue)(c) ) {
					case	KumiawasePickUpDataValue.Mark:
						dstData.mark = strValue;
						break;
					case	KumiawasePickUpDataValue.Name:
						dstData.name = strValue;
						break;
				}
			}
		}
	}


}
