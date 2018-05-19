using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;



public class ElementDispManager : PartsDispManager
{
    static readonly Color s_lineTypeBlockColor = new Color(0.8f, 0.0f, 0.0f);


    public enum DispType
    {
        Block,
        Line,
    }

    private DispType _dispType = DispType.Line;

    [SerializeField]
    float _elementScale = 1.0f;

    /// <summary>
    /// パーツを作成する
    /// </summary>
    public override void CreateParts()
    {
        if (_skeletonResponseData == null)
        {
            return;
        }

        List<SkeletonResponseData.ElementData> elementData = _skeletonResponseData.listElementData;

        CreatePartsCommon(elementData.Count, "Element");
    }




    /// <summary>
    /// 
    /// </summary>
    public override void SetBlockStatus(int id)
    {
        SkeletonResponseData.ElementData elementData = _skeletonResponseData.listElementData[id];
        int nodeI = SUFunctions.GetNodeId(elementData.nodeI);
        int nodeJ = SUFunctions.GetNodeId(elementData.nodeJ);

        bool bEnable = _skeletonResponseData.CheckNodePosition(nodeI, nodeJ);


        PartsDispManager.PartsDispStatus partsDispStatus;

        partsDispStatus.id = id;
        partsDispStatus.enable = bEnable;

        if (SetBlockStatusCommon(partsDispStatus) == false)
        {
            return;
        }


        //	表示に必要なパラメータを用意する
        Vector3 pos_i = _skeletonResponseData.listNodePoint[nodeI];
        Vector3 pos_j = _skeletonResponseData.listNodePoint[nodeJ];
        float length = Vector3.Distance(pos_i, pos_j);

        Vector3 scale;
        BlockWorkData blockWorkData = _blockWorkData[id];

        //	幅と高さを設定する
        if (_dispType == DispType.Block)
        {
            SkeletonResponseData.ElementTypeData elementTypeData = elementData.elementTypeData[0];

            float danmenseki = SUFunctions.GetFloatValue(elementTypeData.danmenSeki);
            float moment_z = SUFunctions.GetFloatValue(elementTypeData.danmen2MomentZ);
            float moment_y = SUFunctions.GetFloatValue(elementTypeData.danmen2MomentY);


            //	スケール値を計算
            if (danmenseki > 0.0f)
            {
                scale.x = (float)System.Math.Sqrt((double)(12.0f * moment_z / danmenseki)) * _elementScale;
                scale.y = (float)System.Math.Sqrt((double)(12.0f * moment_y / danmenseki)) * _elementScale;
            }
            else
            {
                scale.x = 1.0f;
                scale.y = 1.0f;
            }
        }
        else
        {
            scale.x = 0.05f;
            scale.y = 0.05f;
        }

        scale.z = length;

        //	姿勢を設定
        blockWorkData.rootBlockTransform.position = pos_i;
        blockWorkData.rootBlockTransform.LookAt(pos_j);
        blockWorkData.rootBlockTransform.localScale = scale;

        //	方向矢印の表示
        if (blockWorkData.directionArrow != null)
        {
            if (_dispType == DispType.Block)
            {

                Quaternion rotate = Quaternion.LookRotation(pos_j - pos_i, Vector3.forward);
                Vector3 arrowCenter = Vector3.Lerp(pos_i, pos_j, 0.5f);
                Vector3 arrowSize = new Vector3(1.0f, 1.0f, length * 0.25f);

                blockWorkData.directionArrow.SetArrowDirection(arrowCenter, rotate, arrowSize);
                blockWorkData.directionArrow.EnableRenderer(enabled);
            }
            else
            {
                blockWorkData.directionArrow.EnableRenderer(false);
            }
        }


        //	色の指定
        Color color;
        int i;

        if (_dispType == DispType.Block)
        {
            color = s_noSelectColor;
        }
        else
        {
            color = s_lineTypeBlockColor;
        }
        for (i = 0; i < _blockWorkData.Count; i++)
        {
            SetPartsColor(i, false, color);
        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="search_node"></param>
    public void CheckNodeAndUpdateStatus(int search_node)
    {
        List<SkeletonResponseData.ElementData> listElementData = _skeletonResponseData.listElementData;
        int i;
        int node_i;
        int node_j;


        for (i = 0; i < listElementData.Count; i++)
        {
            node_i = SUFunctions.GetNodeId(listElementData[i].nodeI);
            node_j = SUFunctions.GetNodeId(listElementData[i].nodeJ);

            if (_skeletonResponseData.CheckNodePosition(node_i, node_j) == false)
            {
                continue;
            }
            if (search_node != node_i && search_node != node_j)
            {
                continue;       //	関わっていないので無視
            }
            SetBlockStatus(i);
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public override void InputMouse()
    {
        if (_dispType == DispType.Block)
        {
            base.InputMouse();
        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="dispType"></param>
    public void ChangeDispMode(DispType dispType)
    {
        if (_dispType == dispType)
        {
            return;
        }

        _dispType = dispType;
        SetBlockStatusAll();
    }

}
