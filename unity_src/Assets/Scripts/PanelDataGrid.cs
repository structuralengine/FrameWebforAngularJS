using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using SasakouFrame.ValueLabel;




public class PanelDataGrid : DataGridBase
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

		createGridSettingData[0].rowCount = headerData.panelDataCount;
		createGridSettingData[0].repeatColumnCount = new int[]{
			headerData.panelTypeCount,
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
			SkeletonResponseData.PanelData srcData = skeletonResponseData.listPanelData[r];

			_dataGrid.SetCellValue( 0, r, (int)PanelDataValue.KouseiNodeNo0, srcData.kouseiNodeNo[0] );
			_dataGrid.SetCellValue( 0, r, (int)PanelDataValue.KouseiNodeNo1, srcData.kouseiNodeNo[1] );
			_dataGrid.SetCellValue( 0, r, (int)PanelDataValue.KouseiNodeNo2, srcData.kouseiNodeNo[2] );
			_dataGrid.SetCellValue( 0, r, (int)PanelDataValue.KouseiNodeNo3, srcData.kouseiNodeNo[3] );
			_dataGrid.SetCellValue( 0, r, (int)PanelDataValue.Mark, srcData.mark );
			_dataGrid.SetCellValue( 0, r, (int)PanelDataValue.YoungRate, srcData.youngRate );
			_dataGrid.SetCellValue( 0, r, (int)PanelDataValue.SenDanDnaseiKeisu, srcData.senDanDnaseiKeisu );
			_dataGrid.SetCellValue( 0, r, (int)PanelDataValue.SenBouchouKeisu, srcData.senBouchouKeisu );

			int		data_start = (int)PanelDataValue.DanmenSekiStart;
			int		data_end = (int)PanelDataValue.DanmenSekiEnd;

			for( i = 0; i < srcData.panelTypeData.Length; i++ ) {
				var	dstTypeData = srcData.panelTypeData[i];
				
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)PanelDataValue.DanmenSeki, dstTypeData.danmenSeki );
				_dataGrid.SetCellRepeatValue( 0, r, i, data_start, data_end, (int)PanelDataValue.Danmen2Morment, dstTypeData.danmen2Morment );
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
		SkeletonResponseData.PanelData	dstData = skeletonResponseData.listPanelData[r];

		
		switch( (PanelDataValue)c ) {
			//	構成節点番号
			case PanelDataValue.KouseiNodeNo0:
			case PanelDataValue.KouseiNodeNo1:
			case PanelDataValue.KouseiNodeNo2:
			case PanelDataValue.KouseiNodeNo3:
				dstData.kouseiNodeNo[c-(int)(PanelDataValue.KouseiNodeNo0)] = strValue;
				//	入力情報で画面などを更新
				if( _mainFrameManager != null ) { 
					_mainFrameManager.panelDispManager.SetBlockStatus( r );
				}
				break;

			//	マーク
			case PanelDataValue.Mark:
				dstData.mark = strValue;
				break;

			//	ヤング率
			case PanelDataValue.YoungRate:
				dstData.youngRate = strValue;
				break;

			//	せん断弾性係数
			case PanelDataValue.SenDanDnaseiKeisu:
				dstData.senDanDnaseiKeisu = strValue;
				break;

			//	線膨張係数
			case PanelDataValue.SenBouchouKeisu:
				dstData.senBouchouKeisu = strValue;
				break;

			default:
				int		repeat_count;
				int		repeat_id;

				InputId2DataId( out repeat_count, out repeat_id, c, (int)PanelDataValue.DanmenSekiStart, (int)PanelDataValue.DanmenSekiEnd );
				var dstTypeData = dstData.panelTypeData[repeat_count];

				switch( (PanelDataValue)repeat_id ) {
					//	断面積
					case	PanelDataValue.DanmenSeki:
						dstTypeData.danmenSeki = strValue;
						break;

					//	断面２次モーメントZ軸まわり
					case	PanelDataValue.Danmen2Morment:
						dstTypeData.danmen2Morment = strValue;
						break;
				}
				break;
		}
	}
}
