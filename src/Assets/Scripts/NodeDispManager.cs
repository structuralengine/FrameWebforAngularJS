using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public class NodeDispManager : PartsDispManager
{
    const float NODE_SCALE = 0.02f;

    float _maxNodeLangth = 0.0f;
    float _nodeScale = 1.0f;


    /// <summary>
    /// パーツを作成する
    /// </summary>
    public override void CreateParts()
    {
        if (_skeletonResponseData == null)
        {
            return;
        }

        List<SystemUtility.StringVector3> listNodePoint = _skeletonResponseData.listNodePoint;

        CreatePartsCommon(listNodePoint.Count, "Node");
    }



    /// <summary>
    /// 
    /// </summary>
    public override void SetBlockStatus(int id)
    {
        SystemUtility.StringVector3 nodePoint = _skeletonResponseData.listNodePoint[id];

        PartsDispManager.PartsDispStatus partsDispStatus;

        partsDispStatus.id = id;
        partsDispStatus.enable = !nodePoint.isNullOrEmpty;

        if (SetBlockStatusCommon(partsDispStatus) == false)
        {
            return;
        }


        //	表示に必要なパラメータを用意する
        BlockWorkData blockWorkData = _blockWorkData[id];

        //	姿勢を設定
        blockWorkData.gameObjectTransform.position = nodePoint;
        blockWorkData.gameObjectTransform.localScale = new Vector3(_nodeScale, _nodeScale, _nodeScale);
    }



    /// <summary>
    /// 接点を表示するためのサイズの計算をする
    /// </summary>
    public bool CalcNodeBlockScale(int search_node = -1)
    {
        List<SystemUtility.StringVector3> listNodePoint = _skeletonResponseData.listNodePoint;
        Vector3 startPos, endPos, disVec;
        int i, j;
        float max_length = 0.0f;
        float length = 0.0f;


        //	全検索
        if (search_node == -1)
        {
            for (i = 0; i < listNodePoint.Count; i++)
            {
                if (listNodePoint[i].isNullOrEmpty)
                {
                    continue;
                }
                startPos = listNodePoint[i];

                for (j = i + 1; j < listNodePoint.Count; j++)
                {
                    if (listNodePoint[j].isNullOrEmpty)
                    {
                        continue;
                    }
                    endPos = listNodePoint[j];
                    disVec = endPos - startPos;
                    length = Vector3.Dot(disVec, disVec);       //	高速化のためsqrtはしない
                    if (max_length < length)
                    {
                        max_length = length;
                    }
                }
            }
        }
        else
        {
            //	空にされたら全検索し直す
            if (listNodePoint[search_node].isNullOrEmpty == true)
            {
                return CalcNodeBlockScale();
            }
            //	指定されたものだけ検索をする
            else
            {
                startPos = listNodePoint[search_node];

                for (i = 0; i < listNodePoint.Count; i++)
                {
                    if (listNodePoint[i].isNullOrEmpty)
                    {
                        continue;
                    }
                    endPos = listNodePoint[i];
                    disVec = endPos - startPos;
                    length = Vector3.Dot(disVec, disVec);       //	高速化のためsqrtはしない
                    if (max_length < length)
                    {
                        max_length = length;
                    }
                }
            }
        }

        max_length = (float)System.Math.Sqrt(max_length);
        if (_maxNodeLangth == max_length)
        {
            return false;
        }

        _maxNodeLangth = max_length;
        _nodeScale = _maxNodeLangth * NODE_SCALE;

        return true;
    }
}
