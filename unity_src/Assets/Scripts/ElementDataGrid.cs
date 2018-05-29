using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;
using SasakouFrame.InputLabel;
using SasakouFrame.ValueLabel;



public class ElementDataGrid : DataGridBase
{
	/// <summary>
	/// 長さのセルの表示の設定
	/// </summary>
	void	SetLengthCellDisp( int r )
	{
		_mainFrameManager = FindObjectOfType<MainFrameManager>();
		if( _mainFrameManager == null ) {
			return;
		}

		SkeletonResponseData	skeletonResponseData = SkeletonResponseData.Instance;
		SkeletonResponseData.ElementData	elementData = skeletonResponseData.listElementData[r];

		int		node_i = SUFunctions.GetNodeId( elementData.nodeI );
		int		node_j = SUFunctions.GetNodeId( elementData.nodeJ );

		if( skeletonResponseData.CheckNodePosition(node_i, node_j) == false ) {
			_dataGrid.SetCellValue( 0, r, (int)ElementBuzaiDataValue.Length, "" );
			_dataGrid.SetCellValue( 1, r, (int)ElementGouikiDataValue.Length, "" );
		}

		float	length;
		string	strFromat;
		string	strTemp;


		//	距離の表示
		if( skeletonResponseData.GetNodeLength( node_i, node_j, out length ) ){
			strFromat = _dataGrid.GetCellFormat( 0, r, (int)ElementBuzaiDataValue.Length);
			strTemp = length.ToString( strFromat );
			_dataGrid.SetCellValue( 0, r, (int)ElementBuzaiDataValue.Length, strTemp );
			_dataGrid.SetCellValue( 1, r, (int)ElementGouikiDataValue.Length, strTemp );
		}
		else{
			_dataGrid.SetCellValue( 0, r, (int)ElementBuzaiDataValue.Length, "" );
			_dataGrid.SetCellValue( 1, r, (int)ElementGouikiDataValue.Length, "" );
		}
	}



	// <summary>
	/// グリッド作成用データオーバーライド
	/// </summary>
	/// <returns></returns>
	static	public  DataGrid.CreateGridSettingData[] GetCreateGridSettingData()
	{
		SkeletonResponseData	skeletonResponseData = SkeletonResponseData.Instance;
		SkeletonResponseData.HeaderData	headerData = skeletonResponseData.headerData;


		//	入力フィールドの設定データの作成
		DataGrid.CreateGridSettingData[] createGridSettingData = new DataGrid.CreateGridSettingData[Enum.GetNames(typeof(ElementPanelLabel)).Length];

		createGridSettingData[(int)ElementPanelLabel.BuzaiData].rowCount = headerData.elementDataCount;
		createGridSettingData[(int)ElementPanelLabel.BuzaiData].repeatColumnCount = new int[]{
			headerData.elementTypeCount,
		};
		createGridSettingData[(int)ElementPanelLabel.GouikiData].rowCount = headerData.elementDataCount;

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
		int		i;
		

		//	セルにデータを設定
		for( r = 0; r < skeletonResponseData.listElementData.Count; r++ ) {
			SkeletonResponseData.ElementData srcData = skeletonResponseData.listElementData[r];

			_dataGrid.SetCellValue( (int)ElementPanelLabel.BuzaiData, r, (int)ElementBuzaiDataValue.NodeI, srcData.nodeI );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.BuzaiData, r, (int)ElementBuzaiDataValue.NodeJ, srcData.nodeJ );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.BuzaiData, r, (int)ElementBuzaiDataValue.Mark, srcData.mark );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.BuzaiData, r, (int)ElementBuzaiDataValue.BuzaiSu, srcData.buzaiSu );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.BuzaiData, r, (int)ElementBuzaiDataValue.CodeAngle, srcData.codeAngle );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.BuzaiData, r, (int)ElementBuzaiDataValue.YoungRate, srcData.youngRate );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.BuzaiData, r, (int)ElementBuzaiDataValue.SenDanDnaseiKeisu, srcData.senDanDnaseiKeisu );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.BuzaiData, r, (int)ElementBuzaiDataValue.SenBouchouKeisu, srcData.senBouchouKeisu );

			int		data_start = (int)ElementBuzaiDataValue.DanmenSekiStart;
			int		data_end = (int)ElementBuzaiDataValue.DanmenSekiEnd;
			for( i = 0; i < srcData.elementTypeData.Length; i++ ) {
				var	dstTypeData = srcData.elementTypeData[i];
				
				_dataGrid.SetCellRepeatValue( (int)ElementPanelLabel.BuzaiData, r, i, data_start, data_end, (int)ElementBuzaiDataValue.DanmenSeki, dstTypeData.danmenSeki );
				_dataGrid.SetCellRepeatValue( (int)ElementPanelLabel.BuzaiData, r, i, data_start, data_end, (int)ElementBuzaiDataValue.Danmen2MomentZ, dstTypeData.danmen2MomentZ );
				_dataGrid.SetCellRepeatValue( (int)ElementPanelLabel.BuzaiData, r, i, data_start, data_end, (int)ElementBuzaiDataValue.Danmen2MomentY, dstTypeData.danmen2MomentY );
				_dataGrid.SetCellRepeatValue( (int)ElementPanelLabel.BuzaiData, r, i, data_start, data_end, (int)ElementBuzaiDataValue.NeziriTeisuu, dstTypeData.neziriTeisuu );
			}

			_dataGrid.SetCellValue( (int)ElementPanelLabel.GouikiData, r, (int)ElementGouikiDataValue.Mark, srcData.mark );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.GouikiData, r, (int)ElementGouikiDataValue.BuzaiLengthI, srcData.GouikiData.BuzaiLengthI );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.GouikiData, r, (int)ElementGouikiDataValue.BuzaiLengthJ, srcData.GouikiData.BuzaiLengthJ );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.GouikiData, r, (int)ElementGouikiDataValue.Danmenseki, srcData.GouikiData.Danmenseki );
			_dataGrid.SetCellValue( (int)ElementPanelLabel.GouikiData, r, (int)ElementGouikiDataValue.Danmen2Morment, srcData.GouikiData.Danmen2Morment );
			
			SetLengthCellDisp( r );		//	距離の設定
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
		SkeletonResponseData.ElementData		dstData = skeletonResponseData.listElementData[r];
		
		
		if( s == (int)ElementPanelLabel.BuzaiData ) { 
			switch( (ElementBuzaiDataValue)c ) {
				//	節点番号ｉ
				case ElementBuzaiDataValue.NodeI:
					dstData.nodeI = strValue;
					SetLengthCellDisp( r );		//	距離の設定
					break;

				//	節点番号ｊ
				case ElementBuzaiDataValue.NodeJ:
					dstData.nodeJ = strValue;
					SetLengthCellDisp( r );		//	距離の設定
					break;

				//	距離は何もしない
				case ElementBuzaiDataValue.Length:
					break;

				//	マーク
				case ElementBuzaiDataValue.Mark:
					dstData.mark = strValue;
					break;

				//	部材数
				case ElementBuzaiDataValue.BuzaiSu:
					dstData.buzaiSu = strValue;
					break;

				//	コードアングル
				case ElementBuzaiDataValue.CodeAngle:
					dstData.codeAngle = strValue;
					break;

				//	ヤング率
				case ElementBuzaiDataValue.YoungRate:
					dstData.youngRate = strValue;
					break;

				//	せん断弾性係数
				case ElementBuzaiDataValue.SenDanDnaseiKeisu:
					dstData.senDanDnaseiKeisu = strValue;
					break;

				//	線膨張係数
				case ElementBuzaiDataValue.SenBouchouKeisu:
					dstData.senBouchouKeisu = strValue;
					break;

				//	繰り返し入力
				default:
					int		repeat_count;
					int		repeat_id;

					InputId2DataId( out repeat_count, out repeat_id, c, (int)ElementBuzaiDataValue.DanmenSekiStart, (int)ElementBuzaiDataValue.DanmenSekiEnd );
					var dstTypeData = dstData.elementTypeData[repeat_count];

					switch( (ElementBuzaiDataValue)repeat_id ) {
						//	断面積
						case	ElementBuzaiDataValue.DanmenSeki:
							dstTypeData.danmenSeki = strValue;
							break;

						//	断面２次モーメントZ軸まわり
						case	ElementBuzaiDataValue.Danmen2MomentZ:
							dstTypeData.danmen2MomentZ = strValue;
							break;

						//	断面２次モーメントY軸まわり
						case	ElementBuzaiDataValue.Danmen2MomentY:
							dstTypeData.danmen2MomentY = strValue;
							break;

						//	ねじり定数
						case	ElementBuzaiDataValue.NeziriTeisuu:
							dstTypeData.neziriTeisuu = strValue;
							break;
					}
					break;
			}

		
			//	入力情報で画面などを更新
			if( _mainFrameManager != null ) { 
				_mainFrameManager.elementDispManager.SetBlockStatus( r );
			}
		}
		else if( s == (int)ElementPanelLabel.GouikiData ){
			switch( (ElementGouikiDataValue)c ) {
				//	マーク
				case ElementGouikiDataValue.Mark:
					dstData.mark = strValue;
					break;

				//	剛域部材長ｉ端側
				case ElementGouikiDataValue.BuzaiLengthI:
					dstData.GouikiData.BuzaiLengthI = strValue;
					break;

				//	剛域部材長ｊ端側
				case ElementGouikiDataValue.BuzaiLengthJ:
					dstData.GouikiData.BuzaiLengthJ = strValue;
					break;

				//	断面積
				case ElementGouikiDataValue.Danmenseki:
					dstData.GouikiData.Danmenseki = strValue;
					break;

				//	断面積２次モーメント
				case ElementGouikiDataValue.Danmen2Morment:
					dstData.GouikiData.Danmen2Morment = strValue;
					break;
			}
		}
	}


}
