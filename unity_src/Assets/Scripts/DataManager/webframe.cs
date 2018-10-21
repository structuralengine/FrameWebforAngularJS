using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;

/// <summary> 描画モード </summary>
public enum InputPanelLabel
{
    Node,               //	節点
    Member,             //	要素
    NoticePoints,       //　着目点
    Element,            //　材料
    Panel,              //	パネル
    FixNode,            //　支点
    FixMember,          //　バネ
    Joint,              //　結合
    Load,               //　荷重
    Disg,               //　変位量
    Reac,               //　反力
    Fsec,               //　断面力

    Max,

    None = -1,
}

/// <summary> 骨組応答解析データ </summary>
public class webframe : Singleton<webframe>
{
    #region 入力データ

    // 格点データ
    public Dictionary<int, Vector3> listNodePoint { get; set; }

    //	要素データ
    public class MemberDataEx
    {
        public int ni = 0;  //	節点番号ｉ
        public int nj = 0;  //	節点番号ｊ
        public int e = 0;   //	Element番号
        public MemberDataEx(int _ni, int _nj, int _e)
        {
            this.ni = _ni;
            this.nj = _nj;
            this.e = _e;
        }

    }
    public Dictionary<int, MemberDataEx> ListMemberData { get; set; }

    //	着目点データ
    public Dictionary<int, double[]> ListNoticePoint { get; set; }

    //	属性データ
    public partial class ElementData
    {
        public float E = 0.0F;   //	ヤング率
        public float G = 0.0F;   //	せん断弾性係数
        public float Xp = 0.0F;  //	線膨張係数

        public float A = 0.0F;	//	断面積
        public float J = 0.0F;  //	ねじり定数
        public float Iz = 0.0F; //	断面２次モーメントZ軸まわり
        public float Iy = 0.0F; //	断面２次モーメントY軸まわり
    }
    public Dictionary<int, Dictionary<int, ElementData>> ListElementData { get; set; }

    //	パネルデータ
    public class PanelDataEx
    {
        public int no1 = 0; // 構成節点No1
        public int no2 = 0; // 構成節点No2
        public int no3 = 0;	// 構成節点No3
        public int e = 0;   // Element番号
        public PanelDataEx(int _no1, int _no2, int _no3, int _e)
        {
            this.no1 = _no1;
            this.no2 = _no2;
            this.no3 = _no3;
            this.e = _e;
        }
    }
    public Dictionary<int, PanelDataEx> ListPanelData { get; set; }

    //	支点データ
    public partial class FixNodeEx
    {
        public double tx = 0.0;
        public double ty = 0.0;
        public double tz = 0.0;
        public double rx = 0.0;
        public double ry = 0.0;
        public double rz = 0.0;
        public FixNodeEx(double _tx, double _ty, double _tz, double _rx, double _ry, double _rz)
        {
            this.tx = _tx;
            this.ty = _ty;
            this.tz = _tz;
            this.rx = _rx;
            this.ry = _ry;
            this.rz = _rz;
        }
    }
    public Dictionary<int, Dictionary<int, FixNodeEx>> ListFixNode { get; set; }

    //	バネデータ
    public partial class FixMemberEx
    {
        public double tx = 0.0;
        public double ty = 0.0;
        public double tz = 0.0;
        public double tr = 0.0;
        public FixMemberEx(double _tx, double _ty, double _tz, double _tr)
        {
            this.tx = _tx;
            this.ty = _ty;
            this.tz = _tz;
            this.tr = _tr;
        }
    }
    public Dictionary<int, Dictionary<int, FixMemberEx>> ListFixMember { get; set; }

    //	結合データ
    public partial class JointDataEx
    {
        public int xi = 1;
        public int yi = 1;
        public int zi = 1;
        public int xj = 1;
        public int yj = 1;
        public int zj = 1;
        public JointDataEx(int _xi, int _yi, int _zi, int _xj, int _yj, int _zj)
        {
            this.xi = _xi;
            this.yi = _yi;
            this.zi = _zi;
            this.xj = _xj;
            this.yj = _yj;
            this.zj = _zj;
        }
    }
    public Dictionary<int, Dictionary<int, JointDataEx>> ListJointData { get; set; }

    //	荷重データ
    public partial class LoadDataEx
    {
        public int fix_node = 1;
        public int fix_member = 1;
        public int element = 1;
        public int joint = 1;
        public List<LoadNodeEx> load_node;
        public List<LoadMemberEx> load_member;
    }
    public partial class LoadNodeEx
    {
        public int n = 0;
        public double tx = 0.0;
        public double ty = 0.0;
        public double tz = 0.0;
        public double rx = 0.0;
        public double ry = 0.0;
        public double rz = 0.0;
        public LoadNodeEx(int _n, double _tx, double _ty, double _tz, double _rx, double _ry, double _rz)
        {
            this.n = _n;
            this.tx = _tx;
            this.ty = _ty;
            this.tz = _tz;
            this.rx = _rx;
            this.ry = _ry;
            this.rz = _rz;
        }
    }
    public partial class LoadMemberEx
    {
        public int m = 0;
        public string direction = "";
        public int mark = 0;
        public double L1 = 0.0;
        public double L2 = 0.0;
        public double P1 = 0.0;
        public double P2 = 0.0;
        public LoadMemberEx(int _m, string _direction, int _mark, double _L1, double _L2, double _P1, double _P2)
        {
            this.m = _m;
            this.direction = _direction;
            this.mark = _mark;
            this.L1 = _L1;
            this.L2 = _L2;
            this.P1 = _P1;
            this.P2 = _P2;
        }
    }
    public Dictionary<int, LoadDataEx> ListLoadData { get; set; }

    //	変位量データ
    public partial class DisgData
    {
        public double dx = 0.0;
        public double dy = 0.0;
        public double dz = 0.0;
        public double rx = 0.0;
        public double ry = 0.0;
        public double rz = 0.0;
    }
    public Dictionary<int, DisgData> ListDisgData { get; set; }

    //	反力データ
    public partial class ReacData
    {
        public double tx = 0.0;
        public double ty = 0.0;
        public double tz = 0.0;
        public double mx = 0.0;
        public double my = 0.0;
        public double mz = 0.0;
    }
    public Dictionary<int, ReacData> ListReacData { get; set; }

    //	断面力データ
    public partial class FsecData
    {
        public double fxi = 0.0;
        public double fyi = 0.0;
        public double fzi = 0.0;
        public double mxi = 0.0;
        public double myi = 0.0;
        public double mzi = 0.0;
        public double fxj = 0.0;
        public double fyj = 0.0;
        public double fzj = 0.0;
        public double mxj = 0.0;
        public double myj = 0.0;
        public double mzj = 0.0;
        public double L = 0.0;
    }
    public Dictionary<int, FsecData> ListFsecData { get; set; }


    /// <summary> データを作成する </summary>
    public bool Create(string strJson)
    {
        try
        {
            /* Jsonデータを読み込む */
            object j1 = Json.Deserialize(strJson);
            Dictionary<string, object> _webframe = j1 as Dictionary<string, object>;


            /* 読み込んだデータをUnity内で使う用に編集・再定義 */
            // 格点データ
            this.listNodePoint = new Dictionary<int, Vector3>();

            this.listNodePoint.Add(1, new Vector3(0.0f, 1.0f, 1.5f));

            /* 2018/10/21 ここから sasa
            if (_webframe["node"] != null) {

                Dictionary<string, object> node1 = _webframe["node"] as Dictionary<string, object>;
                for (int i = 0; i < node1.Count; i++)
                {
                    try
                    {

                        string key = node1.Keys[i];
                        Dictionary<string, float> node2 = node1[i] as Dictionary<string, float>;
                        Vector3 xyz = new Vector3(node2["x"], node2["y"], node2["z"]);
                        int id = int.Parse(key);
                        this.listNodePoint.Add(id, xyz);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            //	属性データ
            this.ListElementData = new Dictionary<int, Dictionary<int, ElementData>>();
            foreach (string key1 in _webframe.element.Keys)
            {
                Dictionary<int, ElementData> tmp = new Dictionary<int, ElementData>();

                try
                {
                    int id = int.Parse(key1);
                    foreach (string key2 in _webframe.element[key1].Keys)
                    {
                        int type_no = int.Parse(key2);
                        ElementData e = _webframe.element[key1][key2];
                        tmp.Add(type_no, e);
                    }
                    this.ListElementData.Add(id, tmp);
                }
                catch
                {
                    continue;
                }
            }
            // 要素データ
            this.ListMemberData = new Dictionary<int, MemberDataEx>();
            foreach (string key in _webframe.member.Keys)
            {
                try
                {
                    MemberData m = _webframe.member[key];
                    int id = int.Parse(key);
                    int i = int.Parse(m.ni);
                    int j = int.Parse(m.nj);
                    int e = int.Parse(m.e);
                    if (this.listNodePoint.ContainsKey(i)
                        && this.listNodePoint.ContainsKey(j)
                        && this.ListElementData.ContainsKey(e))
                    {
                        MemberDataEx ex = new MemberDataEx(i, j, e);
                        this.ListMemberData.Add(id, ex);
                    }
                }
                catch
                {
                    continue;
                }
            }
            // 着目点データ
            this.ListNoticePoint = new Dictionary<int, double[]>();
            foreach (NoticePoint tmp in _webframe.notice_points)
            {
                try
                {
                    int id = int.Parse(tmp.m);
                    if (this.ListMemberData.ContainsKey(id))
                    {
                        this.ListNoticePoint.Add(id, tmp.Points);
                    }
                }
                catch
                {
                    continue;
                }
            }
            //	パネルデータ
            this.ListPanelData = new Dictionary<int, PanelDataEx>();
            foreach (string key in _webframe.panel.Keys)
            {
                try
                {
                    PanelData p = _webframe.panel[key];
                    int id = int.Parse(key);
                    int n1 = int.Parse(p.no1);
                    int n2 = int.Parse(p.no2);
                    int n3 = int.Parse(p.no3);
                    int e = int.Parse(p.e);
                    if (this.listNodePoint.ContainsKey(n1)
                        && this.listNodePoint.ContainsKey(n2)
                        && this.listNodePoint.ContainsKey(n3)
                        && this.ListElementData.ContainsKey(e))
                    {
                        PanelDataEx ex = new PanelDataEx(n1, n2, n3, e);
                        this.ListPanelData.Add(id, ex);
                    }
                }
                catch
                {
                    continue;
                }
            }
            //	支点データ
            this.ListFixNode = new Dictionary<int, Dictionary<int, FixNodeEx>>();
            foreach (string key1 in _webframe.fix_node.Keys)
            {
                Dictionary<int, FixNodeEx> tmp = new Dictionary<int, FixNodeEx>();
                try
                {
                    int typ = int.Parse(key1);
                    foreach (FixNodeData fn in _webframe.fix_node[key1])
                    {
                        int id = int.Parse(fn.n);
                        if (this.listNodePoint.ContainsKey(id) == false)
                            continue;

                        FixNodeEx ex = new FixNodeEx(fn.tx, fn.ty, fn.tx, fn.rx, fn.ry, fn.rz);
                        if (tmp.ContainsKey(id) == true)
                        {
                            ex.tx += tmp[id].tx;
                            ex.ty += tmp[id].ty;
                            ex.tz += tmp[id].tz;
                            ex.rx += tmp[id].rx;
                            ex.ry += tmp[id].ry;
                            ex.rz += tmp[id].rz;
                            tmp[id] = ex;
                        }
                        else
                        {
                            tmp.Add(id, ex);
                        }
                    }
                    this.ListFixNode.Add(typ, tmp);
                }
                catch
                {
                    continue;
                }
            }
            //	バネデータ
            this.ListFixMember = new Dictionary<int, Dictionary<int, FixMemberEx>>();
            foreach (string key1 in _webframe.fix_member.Keys)
            {
                Dictionary<int, FixMemberEx> tmp = new Dictionary<int, FixMemberEx>();
                try
                {
                    int typ = int.Parse(key1);
                    foreach (FixMemberData fm in _webframe.fix_member[key1])
                    {
                        int id = int.Parse(fm.m);
                        if (this.ListMemberData.ContainsKey(id) == false)
                            continue;

                        FixMemberEx ex = new FixMemberEx(fm.tx, fm.ty, fm.tx, fm.tr);
                        if (tmp.ContainsKey(id) == true)
                        {
                            ex.tx += tmp[id].tx;
                            ex.ty += tmp[id].ty;
                            ex.tz += tmp[id].tz;
                            ex.tr += tmp[id].tr;
                            tmp[id] = ex;
                        }
                        else
                        {
                            tmp.Add(id, ex);
                        }
                    }
                    this.ListFixMember.Add(typ, tmp);
                }
                catch
                {
                    continue;
                }
            }
            //	結合データ
            this.ListJointData = new Dictionary<int, Dictionary<int, JointDataEx>>();
            foreach (string key1 in _webframe.joint.Keys)
            {
                Dictionary<int, JointDataEx> tmp = new Dictionary<int, JointDataEx>();
                try
                {
                    int typ = int.Parse(key1);
                    foreach (JointData jo in _webframe.joint[key1])
                    {
                        int id = int.Parse(jo.m);
                        if (this.ListMemberData.ContainsKey(id) == false)
                            continue;

                        JointDataEx ex = new JointDataEx(jo.xi, jo.yi, jo.zi, jo.xj, jo.yj, jo.zj);
                        if (tmp.ContainsKey(id) == true)
                        {
                            tmp[id] = ex;
                        }
                        else
                        {
                            tmp.Add(id, ex);
                        }
                    }
                    this.ListJointData.Add(typ, tmp);
                }
                catch
                {
                    continue;
                }
            }
            //	荷重データ
            this.ListLoadData = new Dictionary<int, LoadDataEx>();
            foreach (string key1 in _webframe.load.Keys)
            {
                LoadDataEx tmp = new LoadDataEx();
                try
                {
                    int caseNo = int.Parse(key1);

                    LoadData lo = _webframe.load[key1];

                    tmp.fix_node = lo.fix_node;
                    tmp.fix_member = lo.fix_member;
                    tmp.element = lo.element;
                    tmp.joint = lo.joint;

                    List<LoadNodeEx> load_node = new List<LoadNodeEx>();
                    foreach (LoadNodeData ln in lo.load_node)
                    {
                        int id = int.Parse(ln.n);
                        if (this.listNodePoint.ContainsKey(id) == false)
                            continue;

                        LoadNodeEx ex = new LoadNodeEx(id, ln.tx, ln.ty, ln.tz, ln.rx, ln.ry, ln.rz);
                        load_node.Add(ex);
                    }
                    tmp.load_node = load_node;

                    List<LoadMemberEx> load_member = new List<LoadMemberEx>();
                    foreach (LoadMemberData ln in lo.load_member)
                    {
                        int id = int.Parse(ln.m);
                        if (this.ListMemberData.ContainsKey(id) == false)
                            continue;

                        LoadMemberEx ex = new LoadMemberEx(id, ln.direction, ln.mark, ln.L1, ln.L2, ln.P1, ln.P2);
                        load_member.Add(ex);
                    }
                    tmp.load_member = load_member;

                    this.ListLoadData.Add(caseNo, tmp);
                }
                catch
                {
                    continue;
                }
            }
            */
        }
        catch (Exception e)
        {
            throw e;
        }
        return true;
    }

    #endregion

    #region データヘルパー

    /// <summary>
    /// 節点間の距離を計算する
    /// </summary>
    /// <param name="node_i"></param>
    /// <param name="node_j"></param>
    public bool GetNodeLength(int node_i, int node_j, out float length)
    {
        if (this.listNodePoint.ContainsKey(node_i) == false)
        {
            length = 0;
            return false;
        }
        if (this.listNodePoint.ContainsKey(node_j) == false)
        {
            length = 0;
            return false;
        }

        Vector3 pos_i = this.listNodePoint[node_i];
        Vector3 pos_j = this.listNodePoint[node_j];

        length = Vector3.Distance(pos_i, pos_j);

        return true;
    }

    #endregion


}



