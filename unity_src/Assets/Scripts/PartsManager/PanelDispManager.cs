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
		if( _webframe == null ) {
			return;
		}

        Dictionary<int, webframe.PanelDataEx> listPanelData = _webframe.ListPanelData;

		CreatePartsCommon( listPanelData.Count, "Panel" );
	}


	/// <summary>
	/// 
	/// </summary>
	public	override void	SetBlockStatus( int id )
	{
		webframe.PanelDataEx panelData = _webframe.ListPanelData[id];
		BlockWorkData	blockWorkData = _blockWorkData[id];
		PartsDispStatus	partsDispStatus;

		//	パネルの有効をチェック
		List<int>	nodeNo = new List<int>();
		int			i, j;
		int			no;

		partsDispStatus.id = id;
		partsDispStatus.enable = false;

        int[] nodeNos = { panelData.no1, panelData.no2, panelData.no3 };

		for( i=0; i< nodeNos.Length; i++ ){
			no = nodeNos[i];
			if( no > 0 ) {
                partsDispStatus.enable = true;
                nodeNo.Add(no);
            }
        }

        // 3つの頂点が有効でなかったら
        if (nodeNo.Count < 3){
            partsDispStatus.enable = false;
            return;
        }

        //	同じIDがないか検索
        for ( i=0; i<nodeNo.Count; i++ ){
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
            Vector3 node = _webframe.listNodePoint[nodeNo[i]];
            position[i] = node;
		}
		panelBlock.SetPanelPointPosition( position );
    }



	/// <summary>
	/// 
	/// </summary>
	/// <param name="search_node"></param>
	public	void CheckNodeAndUpdateStatus( int search_node )
	{
        Dictionary<int, webframe.PanelDataEx> listPanelData = _webframe.ListPanelData;
		int		i, j;
		int		nodeNo;

        for ( i = 0; i < listPanelData.Count; i++ ) {
            bool UpdateFlg = false;
            int[] nodeNos = { listPanelData[i].no1, listPanelData[i].no2, listPanelData[i].no3 };
            for (j = 0; j < nodeNos.Length; j++){
				nodeNo = nodeNos[j];
				if( nodeNo == search_node ) {
                    UpdateFlg = true;
                    break;
				}
			}
			if( UpdateFlg == false ) {		//	同じのが見つからなかったため更新する必要はない
				continue;
			}
			SetBlockStatus( i );
		}
	}
	
}
