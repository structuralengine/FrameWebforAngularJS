using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SystemUtility;

/// <summary>
/// 骨組応答解析データ
/// </summary>
public class SkeletonResponseData : Singleton<SkeletonResponseData>
{
    #region ヘッダー情報
    /// <summary>
    /// ヘッダー情報
    /// </summary>
    [Serializable]
    public class HeaderData
    {
        public int elementTypeCount = 2;
        public int panelTypeCount = 2;
        public int supportTypeCount = 2;
        public int jointTypeCount = 2;
        public int memberSupportTypeCount = 2;
        public int attentionPointInputCount = 8;
        public int kumiawaseDefineInputCount = 14;
        public int kumiawaseCombineInputCount = 11;
        public int kumiawasePickupInputCount = 18;

        public int nodeCount = 20;
        public int elementDataCount = 20;
        public int panelDataCount = 20;
        public int supportDataCount = 20;
        public int jointDataCount = 20;
        public int memberSupportDataCount = 20;
        public int attentionPointDataCount = 20;
        public int kazyuDataCount = 20;
        public int kumiawaseDefineCount = 20;
        public int kumiawaseCombineCount = 20;
        public int kumiawasePickupCount = 20;
    }
    #endregion


    #region 属性関連情報
    //	属性タイプデータ
    [Serializable]
    public class ElementTypeData
    {
        public string danmenSeki = "";      //	断面積
        public string danmen2MomentZ = "";  //	断面２次モーメントZ軸まわり
        public string danmen2MomentY = "";  //	断面２次モーメントY軸まわり
        public string neziriTeisuu = "";        //	ねじり定数
    }


    //	属性剛域データ
    [Serializable]
    public class ElementGouikiData
    {
        public string BuzaiLengthI = "";        //	剛域部材長ｉ端側
        public string BuzaiLengthJ = "";        //	剛域部材長ｊ端側
        public string Danmenseki = "";      //	断面積
        public string Danmen2Morment = "";  //	断面積２次モーメント
    }


    //	属性データ
    [Serializable]
    public class ElementData
    {
        public string nodeI = "";                   //	節点番号ｉ
        public string nodeJ = "";                   //	節点番号ｊ
        public string mark = "";                    //	マーク
        public string buzaiSu = "";             //	部材数
        public string codeAngle = "";               //	コードアングル
        public string youngRate = "";               //	ヤング率
        public string senDanDnaseiKeisu = "";       //	せん断弾性係数
        public string senBouchouKeisu = "";     //	線膨張係数
        public ElementTypeData[] elementTypeData = null;
        public ElementGouikiData GouikiData = new ElementGouikiData();
    }
    #endregion



    #region パネル関連情報

    //	パネルタイプデータ
    [Serializable]
    public class PanelTypeData
    {
        public string danmenSeki = "";      //	断面積
        public string danmen2Morment = "";  //	断面積２次モーメント
    }

    //	パネルデータ
    [Serializable]
    public class PanelData
    {
        public string[] kouseiNodeNo = new string[4] { "", "", "", "", };       //	構成節点番号
        public string mark = "";                    //	マーク
        public string youngRate = "";               //	ヤング率
        public string senDanDnaseiKeisu = "";       //	せん断弾性係数
        public string senBouchouKeisu = "";     //	線膨張係数
        public PanelTypeData[] panelTypeData = null;
    }
    #endregion



    #region 支点関連情報
    //	支点タイプデータ
    [Serializable]
    public class SupportTypeData
    {
        public StringVector3 TValue;
        public StringVector3 LimitTValue;
        public StringVector3 RValue;
        public StringVector3 LimitRValue;
    }

    //	支点データ
    [Serializable]
    public class SupportData
    {
        public string nodeNo = "";              //	要素番号
        public SupportTypeData[] supportTypeData = null;
    }
    #endregion



    #region 結合関連情報
    //	支点タイプデータ
    [Serializable]
    public class JointTypeData
    {
        public StringVector3 IPointCondition;
        public StringVector3 JPointCondition;
    }

    //	支点データ
    [Serializable]
    public class JointData
    {
        public string nodeNo = "";              //	要素番号
        public JointTypeData[] jointTypeData = null;
    }
    #endregion



    #region バネ関連データ
    //	バネタイプデータ
    [Serializable]
    public class MemberSupportTypeData
    {
        public StringVector3 TValue;
        public StringVector3 LimitTValue;
        public StringVector3 RValue;
        public StringVector3 LimitRValue;
    }

    //	バネデータ
    [Serializable]
    public class MemberSupportData
    {
        public string nodeNo = "";              //	要素番号
        public MemberSupportTypeData[] memberSupportTypeData = null;
    }
    #endregion



    #region 着目点データ
    [Serializable]
    //	着目点入力データ
    public class AttentionPointInputData
    {
        public string pointValue = "";
        public string pointName = "";
    }

    [Serializable]
    //	着目点データ
    public class AttentionPointData
    {
        public string buzaiNo = "";
        public string buzaiChou = "";
        public string buzaiName = "";
        public AttentionPointInputData[] attentionPointInputData = new AttentionPointInputData[8];
    }
    #endregion



    #region 各荷重入力データ
    //	荷重解析条件データ
    [Serializable]
    public class KazyNameData
    {
        public string KazyuMark = "";
        public string KazyuName = "";
        public string Support = "";             //	支点
        public string MemberSupport = "";           //	バネ
        public string Danmen = "";              //	断面
        public string Joint = "";                   //	結合
    }

    //	荷重要素データ
    [Serializable]
    public class KazyuElementData
    {
        public string Start = "";
        public string End = "";
        public string Mark = "";
        public string XYZ = "";
        public string L1 = "";
        public string L2 = "";
        public string P1 = "";
        public string P2 = "";
    }

    //	荷重節点データ
    [Serializable]
    public class KazyuNodeData
    {
        public string nodeNo = "";
        public StringVector3 Point;
        public StringVector3 Morment;
    }

    //	荷重データ
    [Serializable]
    public class KazyuData
    {
        public string kazyuNo = "";
        public KazyNameData kazyNameData;
        public KazyuElementData kazyuElementData;
        public KazyuNodeData kazyuNodeData;
    }
    #endregion



    #region 組合せデータ関連
    //	組合せDefineデータ
    [Serializable]
    public class KumiawaseDefineData
    {
        public string defNo = "";
        public string[] caseNo;
    }

    //	組合せCombine入力値データ
    [Serializable]
    public class KumiawaseCombineValueData
    {
        public string caseNo = "";
        public string kazyuhoseiKeisuu = "";
    }

    //	組合せCombineデータ
    [Serializable]
    public class KumiawaseCombineData
    {
        public string warimashiKeisuu = "";
        public KumiawaseCombineValueData[] kumiawaseCombineValueData;
        public string mark = "";
        public string kumiawaseKazyuName = "";
    }

    //	組合せPickupデータ
    [Serializable]
    public class KumiawasePickupData
    {
        public string[] KumiawaseNo;
        public string mark = "";
        public string name = "";
    }


    //	組合せデータ
    [Serializable]
    public class KumiawaseData
    {
        public KumiawaseDefineData[] kumiawaseDefineData;
        public KumiawaseCombineData[] kumiawaseCombineData;
        public KumiawasePickupData[] kumiawasePickupData;
    }
    #endregion



    //	ヘッダーデータ
    private HeaderData _haderData = new HeaderData();
    public HeaderData headerData
    {
        get { return _haderData; }
    }


    //	節点ポイントリスト
    private List<SystemUtility.StringVector3> _listNodePoint = new List<SystemUtility.StringVector3>();
    public List<SystemUtility.StringVector3> listNodePoint
    {
        get { return _listNodePoint; }
    }


    //	属性データリスト
    List<ElementData> _listElementData = new List<ElementData>();
    public List<ElementData> listElementData
    {
        get { return _listElementData; }
    }

    //	パネルデータリスト
    List<PanelData> _listPanelData = new List<PanelData>();
    public List<PanelData> listPanelData
    {
        get { return _listPanelData; }
    }

    //	支点データリスト
    List<SupportData> _listSupportData = new List<SupportData>();
    public List<SupportData> listSupportData
    {
        get { return _listSupportData; }
    }

    //	結合データリスト
    List<JointData> _listJointData = new List<JointData>();
    public List<JointData> listJointData
    {
        get { return _listJointData; }
    }

    //	結合データリスト
    List<MemberSupportData> _listMemberSupportData = new List<MemberSupportData>();
    public List<MemberSupportData> listMemberSupportData
    {
        get { return _listMemberSupportData; }
    }

    //	着目点データリスト
    List<AttentionPointData> _listAttentionPointData = new List<AttentionPointData>();
    public List<AttentionPointData> listAttentionPointData
    {
        get { return _listAttentionPointData; }
    }

    //	荷重データリスト
    List<KazyuData> _listKazyuData = new List<KazyuData>();
    public List<KazyuData> listKazyuData
    {
        get { return _listKazyuData; }
    }

    //	組合せデータリスト
    List<KumiawaseDefineData> _listKumiawaseDefineData = new List<KumiawaseDefineData>();
    public List<KumiawaseDefineData> listKumiawaseDefineData
    {
        get { return _listKumiawaseDefineData; }
    }
    List<KumiawaseCombineData> _listKumiawaseCombineData = new List<KumiawaseCombineData>();
    public List<KumiawaseCombineData> listKumiawaseCombineData
    {
        get { return _listKumiawaseCombineData; }
    }
    List<KumiawasePickupData> _listKumiawasePickupData = new List<KumiawasePickupData>();
    public List<KumiawasePickupData> listKumiawasePickupData
    {
        get { return _listKumiawasePickupData; }
    }



    //	保存ファイルパス
    private string _filePath = "";
    public string filePath { get { return _filePath; } }



    /// <summary>
    /// セーブデータ
    /// </summary>
    [Serializable]
    public class SaveData
    {
        public HeaderData headerData;
        public StringVector3[] nodePoints;
        public ElementData[] elementData;
        public PanelData[] panelData;
        public SupportData[] supportData;
        public JointData[] jointData;
        public MemberSupportData[] memberSupportData;
        public AttentionPointData[] attentionPointData;
        public KazyuData[] kazyuData;
        public KumiawaseData kumiawaseData;
    }



    /// <summary>
    /// データを作成する
    /// </summary>
    /// <param name="nodeCount"></param>
    /// <param name="elementCount"></param>
    /// <returns></returns>
    public bool Create(HeaderData headerData)
    {
        if (headerData == null)
        {
            return false;
        }
        if (headerData.nodeCount <= 0)
        {
            return false;
        }
        if (headerData.elementDataCount <= 0)
        {
            return false;
        }
        if (headerData.panelDataCount <= 0)
        {
            return false;
        }
        if (headerData.supportDataCount <= 0)
        {
            return false;
        }
        if (headerData.jointDataCount <= 0)
        {
            return false;
        }
        if (headerData.memberSupportDataCount <= 0)
        {
            return false;
        }
        if (headerData.attentionPointDataCount <= 0)
        {
            return false;
        }
        if (headerData.kazyuDataCount <= 0)
        {
            return false;
        }
        if (headerData.kumiawaseDefineCount <= 0)
        {
            return false;
        }
        if (headerData.kumiawaseCombineCount <= 0)
        {
            return false;
        }
        if (headerData.kumiawasePickupCount <= 0)
        {
            return false;
        }


        int i;

        _haderData = headerData;

        _listNodePoint.Clear();
        _listElementData.Clear();

        SystemUtility.StringVector3 vec;

        for (i = 0; i < _haderData.nodeCount; i++)
        {
            vec = new SystemUtility.StringVector3();
            _listNodePoint.Add(vec);
        }

        for (i = 0; i < _haderData.elementDataCount; i++)
        {
            _listElementData.Add(new ElementData());
        }

        for (i = 0; i < _haderData.panelDataCount; i++)
        {
            _listPanelData.Add(new PanelData());
        }

        for (i = 0; i < _haderData.supportDataCount; i++)
        {
            _listSupportData.Add(new SupportData());
        }

        for (i = 0; i < _haderData.jointDataCount; i++)
        {
            _listJointData.Add(new JointData());
        }

        for (i = 0; i < _haderData.memberSupportDataCount; i++)
        {
            _listMemberSupportData.Add(new MemberSupportData());
        }

        for (i = 0; i < _haderData.attentionPointDataCount; i++)
        {
            _listAttentionPointData.Add(new AttentionPointData());
        }

        for (i = 0; i < _haderData.kazyuDataCount; i++)
        {
            _listKazyuData.Add(new KazyuData());
        }

        for (i = 0; i < _haderData.kumiawaseDefineCount; i++)
        {
            _listKumiawaseDefineData.Add(new KumiawaseDefineData());
        }
        for (i = 0; i < _haderData.kumiawaseCombineCount; i++)
        {
            _listKumiawaseCombineData.Add(new KumiawaseCombineData());
        }
        for (i = 0; i < _haderData.kumiawasePickupCount; i++)
        {
            _listKumiawasePickupData.Add(new KumiawasePickupData());
        }

        CreateListData(ref _listElementData, LoadOrCreateDataDelegateElement);
        CreateListData(ref _listPanelData, LoadOrCreateDataDelegatePanel);
        CreateListData(ref _listSupportData, LoadOrCreateDataDelegateSupport);
        CreateListData(ref _listJointData, LoadOrCreateDataDelegateJoint);
        CreateListData(ref _listMemberSupportData, LoadOrCreateDataDelegateMemberSupport);
        CreateListData(ref _listAttentionPointData, LoadOrCreateDataDelegateAttentionPointData);
        CreateListData(ref _listKumiawaseDefineData, LoadOrCreateDataDelegateKumiawaseDefine);
        CreateListData(ref _listKumiawaseCombineData, LoadOrCreateDataDelegateKumiawaseCombine);
        CreateListData(ref _listKumiawasePickupData, LoadOrCreateDataDelegateKumiawasePickup);


        return true;
    }



    /// <summary>
    /// データの作成を行う
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="listDstData"></param>
    /// <param name="arrSrcData"></param>
    /// <param name="maxCount"></param>

    public delegate void onLoadorCreateData(object o);

    void LoadOrCreateData<T>(ref List<T> listDstData, T[] arrSrcData, int maxCount, onLoadorCreateData callback = null) where T : new()
    {
        if (listDstData == null)
        {
            listDstData = new List<T>();
        }
        else
        {
            listDstData.Clear();
        }

        int i = 0;
        int start = 0;

        if (arrSrcData != null)
        {
            int count = Math.Min(arrSrcData.Length, maxCount);

            for (i = 0; i < count; i++)
            {
                if (callback != null)
                {
                    callback((object)arrSrcData[i]);
                }
                listDstData.Add(arrSrcData[i]);
            }
            start = count;
        }

        T newData;

        for (i = start; i < maxCount; i++)
        {
            newData = new T();
            if (callback != null)
            {
                callback((object)newData);
            }
            listDstData.Add(newData);
        }
    }

    void CreateListData<T>(ref List<T> listDstData, onLoadorCreateData callback)
    {
        if (listDstData == null)
        {
            return;
        }
        if (callback == null)
        {
            return;
        }

        int i;

        for (i = 0; i < listDstData.Count; i++)
        {
            callback((object)listDstData[i]);
        }
    }



    /// <summary>
    /// 読み込んだときのデータ成型の基底処理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="typeData"></param>
    /// <param name="count"></param>
    void LoadOrCreateDataDelegateData<T>(ref T[] typeData, int count)
    {
        if (typeData == null)
        {
            typeData = new T[count];
        }
        else if (typeData.Length != count)
        {
            T[] newElementTypeData = new T[count];

            Array.Copy(typeData, newElementTypeData, Math.Min(typeData.Length, count));
            typeData = newElementTypeData;
        }
    }
    void LoadOrCreateDataDelegateClass<T>(ref T[] typeData, int count) where T : new()
    {
        if (typeData == null)
        {
            typeData = new T[count];
        }
        else if (typeData.Length != count)
        {
            T[] newElementTypeData = new T[count];

            Array.Copy(typeData, newElementTypeData, Math.Min(typeData.Length, count));
            typeData = newElementTypeData;
        }

        //	要素がないときは作る
        int i;

        for (i = 0; i < typeData.Length; i++)
        {
            if (typeData[i] == null)
            {
                typeData[i] = new T();
            }
        }
    }


    /// <summary>
    /// 節点の有効かチェックする
    /// </summary>
    /// <param name="i_pos"></param>
    /// <param name="j_pos"></param>
    public bool CheckNodePosition(int node_i, int node_j)
    {
        if (node_i < 0)
        {
            return false;
        }
        if (node_i >= _listNodePoint.Count)
        {
            return false;
        }
        if (node_j < 0)
        {
            return false;
        }
        if (node_j >= _listNodePoint.Count)
        {
            return false;
        }
        if (node_i == node_j)
        {
            return false;
        }

        Vector3 pos_i = _listNodePoint[node_i];
        Vector3 pos_j = _listNodePoint[node_j];

        if (pos_i == pos_j)
        {
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


    /// <summary>
    /// 要素の成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegateElement(object o)
    {
        var srcData = o as ElementData;
        LoadOrCreateDataDelegateClass(ref srcData.elementTypeData, _haderData.elementTypeCount);
    }

    /// <summary>
    /// パネルの成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegatePanel(object o)
    {
        var srcData = o as PanelData;
        LoadOrCreateDataDelegateClass(ref srcData.panelTypeData, _haderData.panelTypeCount);
    }

    /// <summary>
    /// 支点の成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegateSupport(object o)
    {
        var srcData = o as SupportData;
        LoadOrCreateDataDelegateClass(ref srcData.supportTypeData, _haderData.panelTypeCount);
    }

    /// <summary>
    /// 結合の成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegateJoint(object o)
    {
        var srcData = o as JointData;
        LoadOrCreateDataDelegateClass(ref srcData.jointTypeData, _haderData.jointTypeCount);
    }

    /// <summary>
    /// バネの成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegateMemberSupport(object o)
    {
        var srcData = o as MemberSupportData;
        LoadOrCreateDataDelegateClass(ref srcData.memberSupportTypeData, _haderData.memberSupportTypeCount);
    }

    /// <summary>
    /// 着目点の成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegateAttentionPointData(object o)
    {
        var srcData = o as AttentionPointData;
        LoadOrCreateDataDelegateClass(ref srcData.attentionPointInputData, _haderData.attentionPointInputCount);
    }

    /// <summary>
    /// 組合せDefineの成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegateKumiawaseDefine(object o)
    {
        var srcData = o as KumiawaseDefineData;
        LoadOrCreateDataDelegateData(ref srcData.caseNo, _haderData.kumiawaseDefineInputCount);
    }

    /// <summary>
    /// 組合せCombineの成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegateKumiawaseCombine(object o)
    {
        var srcData = o as KumiawaseCombineData;
        LoadOrCreateDataDelegateClass(ref srcData.kumiawaseCombineValueData, _haderData.kumiawaseCombineInputCount);
    }

    /// <summary>
    /// 組合せDefineの成型処理
    /// </summary>
    /// <param name="o"></param>
    void LoadOrCreateDataDelegateKumiawasePickup(object o)
    {
        var srcData = o as KumiawasePickupData;
        LoadOrCreateDataDelegateData(ref srcData.KumiawaseNo, _haderData.kumiawasePickupInputCount);
    }




    /// <summary>
    /// データの読み込み
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public bool Load(string fileName)
    {
        if (File.Exists(fileName) == false)
        {
            return false;
        }

        string strJson = SUFunctions.ReadText(fileName);

        /* Xml の場合
		SaveData saveData;
		System.Xml.Serialization.XmlSerializer serializer2 = new System.Xml.Serialization.XmlSerializer(typeof(SaveData));
		using (System.IO.StringReader reader = new System.IO.StringReader(strJson)) {
			saveData = serializer2.Deserialize(reader);
		}
		*/

        SaveData saveData = JsonUtility.FromJson(strJson, typeof(SaveData)) as SaveData;

        _haderData = saveData.headerData;
        LoadOrCreateData(ref _listNodePoint, saveData.nodePoints, _haderData.nodeCount);
        LoadOrCreateData(ref _listElementData, saveData.elementData, _haderData.elementDataCount, LoadOrCreateDataDelegateElement);
        LoadOrCreateData(ref _listPanelData, saveData.panelData, _haderData.panelDataCount, LoadOrCreateDataDelegatePanel);
        LoadOrCreateData(ref _listSupportData, saveData.supportData, _haderData.supportDataCount, LoadOrCreateDataDelegateSupport);
        LoadOrCreateData(ref _listJointData, saveData.jointData, _haderData.jointDataCount, LoadOrCreateDataDelegateJoint);
        LoadOrCreateData(ref _listMemberSupportData, saveData.memberSupportData, _haderData.memberSupportDataCount, LoadOrCreateDataDelegateMemberSupport);
        LoadOrCreateData(ref _listAttentionPointData, saveData.attentionPointData, _haderData.attentionPointDataCount, LoadOrCreateDataDelegateAttentionPointData);
        LoadOrCreateData(ref _listKazyuData, saveData.kazyuData, _haderData.kazyuDataCount);
        LoadOrCreateData(ref _listKumiawaseDefineData, saveData.kumiawaseData.kumiawaseDefineData, _haderData.kumiawaseDefineCount, LoadOrCreateDataDelegateKumiawaseDefine);
        LoadOrCreateData(ref _listKumiawaseCombineData, saveData.kumiawaseData.kumiawaseCombineData, _haderData.kumiawaseCombineCount, LoadOrCreateDataDelegateKumiawaseCombine);
        LoadOrCreateData(ref _listKumiawasePickupData, saveData.kumiawaseData.kumiawasePickupData, _haderData.kumiawaseCombineCount, LoadOrCreateDataDelegateKumiawasePickup);




        _filePath = fileName;


        return true;
    }



    /// <summary>
    /// データの保存
    /// </summary>
    /// <param name="fileName"></param>
    public bool Save(string fileName)
    {
        string strJson;
        SaveData saveData = new SaveData();

        saveData.headerData = _haderData;
        saveData.nodePoints = _listNodePoint.ToArray();
        saveData.elementData = _listElementData.ToArray();
        saveData.panelData = _listPanelData.ToArray();
        saveData.supportData = _listSupportData.ToArray();
        saveData.jointData = _listJointData.ToArray();
        saveData.memberSupportData = _listMemberSupportData.ToArray();
        saveData.attentionPointData = _listAttentionPointData.ToArray();
        saveData.kazyuData = _listKazyuData.ToArray();

        saveData.kumiawaseData = new KumiawaseData();
        saveData.kumiawaseData.kumiawaseDefineData = _listKumiawaseDefineData.ToArray();
        saveData.kumiawaseData.kumiawaseCombineData = _listKumiawaseCombineData.ToArray();
        saveData.kumiawaseData.kumiawasePickupData = _listKumiawasePickupData.ToArray();

        /* Xml の場合
		System.Xml.Serialization.XmlSerializer serializer1 = new System.Xml.Serialization.XmlSerializer(typeof(SaveData));
		using (System.IO.StringWriter writer = new System.IO.StringWriter ()) {
			serializer1.Serialize (writer, saveData);
			strJson = writer.ToString ();
		}
		*/

        strJson = JsonUtility.ToJson(saveData, true);

        bool b = SUFunctions.WriteText(fileName, strJson);

        if (b)
        {
            _filePath = fileName;
        }

        return b;
    }
}



