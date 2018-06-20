using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;



public class PanelDispManager : PartsDispManager
{
	/// <summary>
	/// パーツを作成する
	/// </summary>
	public	override void	CreateParts()
	{
		if( _skeletonResponseData == null ) {
			return;
		}

		List<SkeletonResponseData.PanelData>	listPanelData = _skeletonResponseData.ListPanelData;

		CreatePartsCommon( listPanelData.Count, "Panel" );
	}



	/// <summary>
	/// 
	/// </summary>
	public	override void	SetBlockStatus( int id )
	{
		SkeletonResponseData.PanelData	panelData = _skeletonResponseData.ListPanelData[id];
		BlockWorkData	blockWorkData = _blockWorkData[id];
		PartsDispStatus	partsDispStatus;

		//	パネルの有効をチェック
		List<int>	nodeNo = new List<int>();
		int			i, j;
		int			no;

		partsDispStatus.id = id;
		partsDispStatus.enable = true;

		for( i=0; i<panelData.kouseiNodeNo.Length; i++ ){
			no = SUFunctions.GetNodeId( panelData.kouseiNodeNo[i] );
			if( no < 0 ) {
				if( i < 3 ){		//	最初の３つは無かったら無効
					partsDispStatus.enable = false;
					break;
				}
				else{				//	最後は無くても有効
					break;
				}
			}
			nodeNo.Add( no );
		}
		//	同じIDがないか検索
		for( i=0; i<nodeNo.Count; i++ ){
			for( j=i+1; j<nodeNo.Count; j++ ){
				if( nodeNo[i] == nodeNo[j] ) {
					partsDispStatus.enable = false;
					break;
				}
			}
			if( j!=nodeNo.Count ) {
				break;
			}
		}

		
		if( SetBlockStatusCommon(partsDispStatus) == false ) {
			return;
		}

		
		//	頂点位置を再定義する
		PanelBlock	panelBlock = blockWorkData.gameObject.GetComponentInChildren<PanelBlock>();
		Vector3[]	position = new Vector3[nodeNo.Count];

		for ( i=0; i<nodeNo.Count; i++ ){
			position[i] = _skeletonResponseData.ListNodePoint[nodeNo[i]];
		}
		panelBlock.SetPanelPointPosition( position );
    }



	/// <summary>
	/// 
	/// </summary>
	/// <param name="search_node"></param>
	public	void CheckNodeAndUpdateStatus( int search_node )
	{
		List<SkeletonResponseData.PanelData>	listPanelData = _skeletonResponseData.ListPanelData;
		int		i, j;
		int		nodeNo;
		


		for( i = 0; i < listPanelData.Count; i++ ) {
			for( j=0; j<listPanelData[i].kouseiNodeNo.Length; j++ ) { 
				nodeNo = SUFunctions.GetNodeId( listPanelData[i].kouseiNodeNo[j] );
				if( nodeNo == search_node ) {
					break;
				}
			}
			if( j == listPanelData[i].kouseiNodeNo.Length ) {		//	同じのが見つからなかったため更新する必要はない
				continue;
			}
			
			SetBlockStatus( i );
		}
	}
	
}
