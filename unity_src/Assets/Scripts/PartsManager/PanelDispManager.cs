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
        if ( _webframe == null ) {
            Debug.Log("PanelDispManager _webframe == null");
            return;
		}

        CreatePanels();

	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count"></param>
    //protected IEnumerable	CreatePartsCommon( int parts_count, string partsName )
    private void CreatePanels()
    {
        try
        {
            BlockWorkData blockWorkData;
            MeshFilter meshFileter;

            // 前のオブジェクトを消す
            foreach (string id in base._blockWorkData.Keys)
            {
                try
                {
                    Destroy(base._blockWorkData[id].renderer.sharedMaterial);
                    Destroy(base._blockWorkData[id].gameObject);
                }
                catch { }
            }
            base._blockWorkData.Clear();

            // 新しいオブジェクトを生成する
            foreach (int i in _webframe.ListPanelData.Keys)
            {
                blockWorkData = new BlockWorkData { gameObject = Instantiate(_blockPrefab) };
                base._blockWorkData.Add("Panel[" + i + "]", blockWorkData);
            }

            // 新しいオブジェクトのプロパティを設定する
            foreach (int i in _webframe.ListPanelData.Keys)
            {
                blockWorkData = base._blockWorkData["Panel[" + i + "]"];
                blockWorkData.gameObjectTransform = blockWorkData.gameObject.transform;
                blockWorkData.rootBlockTransform = blockWorkData.gameObjectTransform.Find("Root");
                blockWorkData.blockData = blockWorkData.gameObject.GetComponentInChildren<BlockData>();
                blockWorkData.blockData.id = i;
                blockWorkData.directionArrow = blockWorkData.gameObject.GetComponentInChildren<DirectionArrow>();
                blockWorkData.renderer = blockWorkData.gameObject.GetComponentInChildren<Renderer>();
                if (blockWorkData.renderer == null)
                {
                    continue;
                }
                blockWorkData.renderer.sharedMaterial = Instantiate(blockWorkData.renderer.sharedMaterial);
                blockWorkData.materialPropertyBlock = new MaterialPropertyBlock();
                blockWorkData.materialPropertyBlock.SetColor("_Color", Color.white);
                blockWorkData.renderer.SetPropertyBlock(blockWorkData.materialPropertyBlock);

                blockWorkData.gameObject.name = "Panel[" + i + "]";
                blockWorkData.gameObjectTransform.parent = this.gameObject.transform;
                blockWorkData.gameObject.SetActive(false);

                //	メシュの取得
                meshFileter = blockWorkData.gameObject.GetComponentInChildren<MeshFilter>();
                if (meshFileter != null)
                {
                    blockWorkData.mesh = meshFileter.mesh;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(" PanelDispManager CreatePanels" + e.Message);
        }
    }

    /// <summary>
    /// ブロックの色を変更
    /// </summary>
    /// <param name="id"></param>
    public override void ChengeForcuseBlock(int i)
    {
        base.ChengeForcuseBlock("Panel[" + i + "]");
    }

    /// <summary>
    /// 
    /// </summary>
    public	override void	SetBlockStatus( string id )
	{
        if (!base._blockWorkData.ContainsKey(id))
            return;

        BlockWorkData blockWorkData = base._blockWorkData[id];
        int i = blockWorkData.blockData.id;

        webframe.PanelData panelData = _webframe.ListPanelData[i];
        Dictionary<int, Vector3> ListNodeData = _webframe.listNodePoint;

        PartsDispStatus	partsDispStatus;

		//	パネルの有効をチェック
		List<int>	nodeNo = new List<int>();

		partsDispStatus.id = id;
		partsDispStatus.enable = false;

        int[] nodeNos = { panelData.no1, panelData.no2, panelData.no3 };
        foreach(int no in nodeNos)
        {
            if (ListNodeData.ContainsKey(no))
            {
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
        for ( int j=0; j<nodeNo.Count; j++ ){
			for( int k=j+1; k<nodeNo.Count; k++ ){
				if( nodeNo[j] == nodeNo[k] ) {
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

		for ( int j=0; j<nodeNo.Count; j++ ){
            Vector3 node = _webframe.listNodePoint[nodeNo[j]];
            position[j] = node;
		}
		panelBlock.SetPanelPointPosition( position );
    }



	/// <summary>
	/// 
	/// </summary>
	/// <param name="search_node"></param>
	public	void CheckNodeAndUpdateStatus( int search_node )
	{
        Dictionary<int, webframe.PanelData> listPanelData = _webframe.ListPanelData;

        foreach (int i in listPanelData.Keys) {
            bool UpdateFlg = false;
            int[] nodeNos = { listPanelData[i].no1, listPanelData[i].no2, listPanelData[i].no3 };
            foreach (int nodeNo in nodeNos) { 
				if( nodeNo == search_node ) {
                    UpdateFlg = true;
                    break;
				}
			}
			if( UpdateFlg == false ) {		//	同じのが見つからなかったため更新する必要はない
				continue;
			}
			SetBlockStatus("Panel[" + i + "]");
		}
	}
	
}
