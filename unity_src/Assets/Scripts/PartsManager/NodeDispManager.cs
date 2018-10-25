using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class NodeDispManager : PartsDispManager
{
	const	float	NODE_SCALE = 0.02f;

	float	_maxNodeLangth = 0.0f;
	float	_nodeScale = 1.0f;

    /// <summary>
    /// パーツを作成する
    /// </summary>
    public override void	CreateParts()
	{
        if ( _webframe == null ) {
            Debug.Log("NodeDispManager _webframe == null");
            return;
		}

        CreateNodes();
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count"></param>
    //protected IEnumerable	CreatePartsCommon( int parts_count, string partsName )
    private void CreateNodes()
    {
        try { 
            BlockWorkData blockWorkData;
            MeshFilter meshFileter;

            // 前のオブジェクトを消す
            foreach(string id in base._blockWorkData.Keys)
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
            foreach (int i in _webframe.listNodePoint.Keys)
            {
                blockWorkData = new BlockWorkData { gameObject = Instantiate(_blockPrefab) };
                base._blockWorkData.Add("Node[" + i + "]", blockWorkData);
            }

            // 新しいオブジェクトのプロパティを設定する
            foreach ( int i in _webframe.listNodePoint.Keys)
            {
                blockWorkData = base._blockWorkData["Node[" + i + "]"];
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

                blockWorkData.gameObject.name = "Node[" + i + "]";
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
            Debug.Log(" NodeDispManager CreateNodes" + e.Message);
        }
    }

    /// <summary>
    /// ブロックの色を変更
    /// </summary>
    /// <param name="id"></param>
    public override void ChengeForcuseBlock(int i)
    {
        base.ChengeForcuseBlock("Node[" + i + "]");
    }


    /// <summary>
    /// 
    /// </summary>
    public override void SetBlockStatus( string id )
	{
        if (!base._blockWorkData.ContainsKey(id))
            return;

        BlockWorkData blockWorkData = base._blockWorkData[id];
        int i = blockWorkData.blockData.id;

        Vector3	nodePoint = _webframe.listNodePoint[i];

		PartsDispManager.PartsDispStatus partsDispStatus;

		partsDispStatus.id	  = id;
		partsDispStatus.enable = true;

		if( base.SetBlockStatusCommon(partsDispStatus) == false ) {
			return;
		}
		
		//	表示に必要なパラメータを用意する

		//	姿勢を設定
		blockWorkData.gameObjectTransform.position = nodePoint;
		blockWorkData.gameObjectTransform.localScale = new Vector3( _nodeScale, _nodeScale, _nodeScale );
	}



	/// <summary>
	/// 接点を表示するためのサイズの計算をする
	/// </summary>
	public	bool	CalcNodeBlockScale( int search_node=-1 )
	{
        Dictionary<int, Vector3> listNodePoint = _webframe.listNodePoint;
		Vector3	startPos, endPos, disVec;
		float	max_length = 0.0f;
		float	length = 0.0f;


		//	全検索
		if( search_node == -1 ) { 

            foreach( int i in listNodePoint.Keys) {
				if( listNodePoint.ContainsKey(i) == false ) {
					continue;
				}
				startPos = listNodePoint[i];

                foreach (int j in listNodePoint.Keys)
                {
                    if (i == j)
                        continue;

                    if (listNodePoint.ContainsKey(j) == false)
						continue;

                    endPos = listNodePoint[j];
					disVec = endPos - startPos;
					length = Vector3.Dot( disVec, disVec );		//	高速化のためsqrtはしない
					if( max_length < length ) {
						max_length = length;
					}
				}
			}
		}
		else {
            //	空にされたら全検索し直す
            if (listNodePoint.ContainsKey(search_node) == false){
                return	CalcNodeBlockScale();
			}
			//	指定されたものだけ検索をする
			else{
				startPos = listNodePoint[search_node];

                foreach (int i in listNodePoint.Keys){
                    if (listNodePoint.ContainsKey(i) == false){
                        continue;
					}
					endPos = listNodePoint[i];
					disVec = endPos - startPos;
					length = Vector3.Dot( disVec, disVec );		//	高速化のためsqrtはしない
					if( max_length < length ) {
						max_length = length;
					}
				}
			}
		}

		max_length = (float)System.Math.Sqrt( max_length );
		if( _maxNodeLangth == max_length ) {
			return	false;
		}

		_maxNodeLangth = max_length;
		_nodeScale = _maxNodeLangth * NODE_SCALE;

		return	true;
	}
}
