﻿using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;


public enum InputPanelLabel
{
    Node,               //	節点
    Member,             //	要素
    Panel,              //	パネル

    Max,

    None = -1,
}

/// <summary>
/// 骨組応答解析データ
/// </summary>
public class webframe : Singleton<webframe>
{
    #region 入力データ

    /// <summary>
    /// セーブ用データ
    /// </summary>
    [Serializable]
    public class SaveData
    {
        public StringVector3[] node;
        public MemberData[] member;
        public PanelData[] panel;
        public ElementData[] element;
    }

    //	要素データ
    [Serializable]
    public class MemberData
    {
        public string ni = "";  //	節点番号ｉ
        public string nj = "";  //	節点番号ｊ
        public string e = "";   //	Element番号
    }
    
    //	パネルタイプデータ
    [Serializable]
    public class PanelData
    {
        public string no1 = ""; //	構成節点No1
        public string no2 = ""; //	構成節点No2
        public string no3 = "";	// 構成節点No3
        public string e = "";   //	Element番号
    }

    //	属性データ
    [Serializable]
	public class ElementData
	{
        public string E = "";   //	ヤング率
        public string G = "";   //	せん断弾性係数
        public string Xp = "";  //	線膨張係数

        public string A1 = "";	//	断面積
        public string J1 = "";  //	ねじり定数
        public string Iz1 = ""; //	断面２次モーメントZ軸まわり
        public string Iy1 = ""; //	断面２次モーメントY軸まわり

        public string A2 = "";	//	断面積
        public string J2 = "";  //	ねじり定数
        public string Iz2 = ""; //	断面２次モーメントZ軸まわり
        public string Iy2 = ""; //	断面２次モーメントY軸まわり

        public string A3 = "";	//	断面積
        public string J3 = "";  //	ねじり定数
        public string Iz3 = ""; //	断面２次モーメントZ軸まわり
        public string Iy3 = ""; //	断面２次モーメントY軸まわり
    }


    //	節点ポイントリスト
    private	List<SystemUtility.StringVector3>	_listNodePoint = new List<SystemUtility.StringVector3>();
	public List<SystemUtility.StringVector3> ListNodePoint{
		get{	return	_listNodePoint;	}
	}

	//	要素データリスト
	List<MemberData>		_listMemberData = new List<MemberData>();
	public List<MemberData> ListMemberData{
		get{	return	_listMemberData;	}
	}

	//	パネルデータリスト
	List<PanelData>			_listPanelData = new List<PanelData>();
	public List<PanelData> ListPanelData{
		get{	return	_listPanelData;	}
	}

    //	属性データリスト
    List<ElementData> _listElementData = new List<ElementData>();
    public List<ElementData> ListElementData {
        get { return _listElementData; }
    }

    #endregion

    #region データヘルパー

    /// <summary>
    /// 節点の有効かチェックする
    /// </summary>
    /// <param name="i_pos"></param>
    /// <param name="j_pos"></param>
    public bool CheckNodePosition(int node_i, int node_j)
    {
        if (node_i < 0){
            return false;
        }
        if (node_i >= _listNodePoint.Count){
            return false;
        }
        if (node_j < 0){
            return false;
        }
        if (node_j >= _listNodePoint.Count){
            return false;
        }
        if (node_i == node_j){
            return false;
        }

        Vector3 pos_i = _listNodePoint[node_i];
        Vector3 pos_j = _listNodePoint[node_j];

        if (pos_i == pos_j){
            return false;
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node_i"></param>
    /// <param name="node_j"></param>
    public bool GetNodeLength(int node_i, int node_j, out float length)
    {
        bool bEnable = CheckNodePosition(node_i, node_j);
        if (bEnable == false)
        {
            length = 0.0f;
            return false;
        }

        Vector3 pos_i = _listNodePoint[node_i];
        Vector3 pos_j = _listNodePoint[node_j];

        length = Vector3.Distance(pos_i, pos_j);

        return true;
    }


    #endregion


    /// <summary>
    /// データを作成する
    /// </summary>
    /// <param name="nodeCount"></param>
    /// <param name="elementCount"></param>
    /// <returns></returns>
    public bool	Create(string strJson)
    {
		_listNodePoint.Clear();
		_listMemberData.Clear();
        _listPanelData.Clear();
        _listElementData.Clear();

        SaveData saveData = JsonUtility.FromJson(strJson, typeof(SaveData)) as SaveData;

        int i;
        for ( i = 0; i < saveData.node.Length; i++ ) {
            if (ComonFunctions.IsEnableData(saveData.node[i].x, saveData.node[i].y, saveData.node[i].z)){
                _listNodePoint.Add(saveData.node[i]);
            }
        }

		for( i = 0; i < saveData.member.Length; i++ ) {
            if (ComonFunctions.IsEnableData(saveData.member[i].ni, saveData.member[i].nj)){
                _listMemberData.Add(saveData.member[i]);
            }
		}

		for( i = 0; i < saveData.panel.Length; i++ ) {
            if (ComonFunctions.IsEnableData(saveData.panel[i].no1, saveData.panel[i].no2, saveData.panel[i].no3)){
                _listPanelData.Add(saveData.panel[i]);
            }
		}

        for (i = 0; i < saveData.element.Length; i++){
            if (ComonFunctions.IsEnableData(saveData.element[i].E, saveData.element[i].G, saveData.element[i].Xp,
                                            saveData.element[i].A1, saveData.element[i].J1, saveData.element[i].Iy1, saveData.element[i].Iz1,
                                            saveData.element[i].A2, saveData.element[i].J2, saveData.element[i].Iy2, saveData.element[i].Iz2,
                                            saveData.element[i].A3, saveData.element[i].J3, saveData.element[i].Iy3, saveData.element[i].Iz3
                                           )){
                _listElementData.Add(saveData.element[i]);
            }
        }

		return	true;
	}
}


