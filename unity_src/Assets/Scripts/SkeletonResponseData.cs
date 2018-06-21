using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;


public enum InputPanelLabel
{
    Node,               //	節点
    Element,            //	要素
    Panel,              //	パネル

    Max,

    None = -1,
}

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
	public class HeaderData {
		public	int		elementTypeCount = 2;
		public	int		panelTypeCount = 2;
		
		public	int		nodeCount = 20;
		public	int		elementDataCount = 20;
		public	int		panelDataCount = 20;

	}
	#endregion


	#region 属性関連情報
	//	属性タイプデータ
	[Serializable]
	public	class ElementTypeData
	{
		public	string	danmenSeki = "";		//	断面積
		public	string	danmen2MomentZ = "";	//	断面２次モーメントZ軸まわり
		public	string	danmen2MomentY = "";	//	断面２次モーメントY軸まわり
		public	string	neziriTeisuu = "";		//	ねじり定数
	}


	//	属性剛域データ
	[Serializable]
	public	class ElementGouikiData
	{
		public	string	BuzaiLengthI = "";		//	剛域部材長ｉ端側
		public	string	BuzaiLengthJ = "";		//	剛域部材長ｊ端側
		public	string	Danmenseki = "";		//	断面積
		public	string	Danmen2Morment = "";	//	断面積２次モーメント
	}


	//	属性データ
	[Serializable]
	public class ElementData
	{
		public	string	nodeI = "";					//	節点番号ｉ
		public	string	nodeJ = "";					//	節点番号ｊ
		public	string	mark = "";					//	マーク
		public	string	buzaiSu = "";				//	部材数
		public	string	codeAngle = "";				//	コードアングル
		public	string	youngRate = "";				//	ヤング率
		public	string	senDanDnaseiKeisu = "";		//	せん断弾性係数
		public	string	senBouchouKeisu = "";		//	線膨張係数
		public	ElementTypeData[]	elementTypeData =  null;
		public	ElementGouikiData	GouikiData = new ElementGouikiData();
	}
	#endregion



	#region パネル関連情報

	//	パネルタイプデータ
	[Serializable]
	public class PanelTypeData
	{
		public	string	danmenSeki = "";		//	断面積
		public	string	danmen2Morment = "";	//	断面積２次モーメント
	}

	//	パネルデータ
	[Serializable]
	public class PanelData
	{
		public	string[]	kouseiNodeNo = new string[3]{ "", "", "" };		//	構成節点番号
		public	string	mark = "";					//	マーク
		public	string	youngRate = "";				//	ヤング率
		public	string	senDanDnaseiKeisu = "";		//	せん断弾性係数
		public	string	senBouchouKeisu = "";		//	線膨張係数
		public	PanelTypeData[]		panelTypeData = null;
	}
	#endregion


	//	ヘッダーデータ
	private	HeaderData	_haderData = new HeaderData();
	public HeaderData headerData {
		get { return _haderData; }
	}


	//	節点ポイントリスト
	private	List<SystemUtility.StringVector3>	_listNodePoint = new List<SystemUtility.StringVector3>();
	public List<SystemUtility.StringVector3> ListNodePoint{
		get{	return	_listNodePoint;	}
	}


	//	属性データリスト
	List<ElementData>		_listElementData = new List<ElementData>();
	public List<ElementData> ListElementData{
		get{	return	_listElementData;	}
	}

	//	パネルデータリスト
	List<PanelData>			_listPanelData = new List<PanelData>();
	public List<PanelData> ListPanelData{
		get{	return	_listPanelData;	}
	}



	/// <summary>
	/// セーブデータ
	/// </summary>
	[Serializable]
	public class SaveData
	{
		public	HeaderData				headerData;
		public	StringVector3[]			nodePoints;
		public	ElementData[]			elementData;
		public	PanelData[]				panelData;
	}


	/// <summary>
	/// データを作成する
	/// </summary>
	/// <param name="nodeCount"></param>
	/// <param name="elementCount"></param>
	/// <returns></returns>
	public	bool	Create( HeaderData headerData )
	{
		if( headerData == null ) {
			return	false;
		}
		if( headerData.nodeCount <= 0 ) {
			return	false;
		}
		if( headerData.elementDataCount <= 0 ) {
			return	false;
		}
		if( headerData.panelDataCount <= 0 ) {
			return	false;
		}

		int		i;

		_haderData = headerData;

		_listNodePoint.Clear();
		_listElementData.Clear();

		SystemUtility.StringVector3	vec;

		for( i = 0; i < _haderData.nodeCount; i++ ) {
			vec = new SystemUtility.StringVector3();
			_listNodePoint.Add( vec );
		}

		for( i = 0; i < _haderData.elementDataCount; i++ ) {
			_listElementData.Add( new ElementData() );
		}

		for( i = 0; i < _haderData.panelDataCount; i++ ) {
			_listPanelData.Add( new PanelData() );
		}

		CreateListData( ref _listElementData, LoadOrCreateDataDelegateElement );
		CreateListData( ref _listPanelData, LoadOrCreateDataDelegatePanel );

		return	true;
	}



	/// <summary>
	/// データの作成を行う
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="listDstData"></param>
	/// <param name="arrSrcData"></param>
	/// <param name="maxCount"></param>
	
	public delegate void onLoadorCreateData( object o );

	void	LoadOrCreateData<T>( ref List<T> listDstData, T[] arrSrcData, int maxCount, onLoadorCreateData callback=null ) where T : new()
	{
		if( listDstData == null ) {
			listDstData = new List<T>();
		}
		else {
			listDstData.Clear();
		}

		int		i = 0;
		int		start = 0;

		if( arrSrcData != null ){
			int		count = Math.Min( arrSrcData.Length, maxCount );

			for( i = 0; i < count; i++ ) {
				if( callback != null ) {
					callback( (object)arrSrcData[i] );
				}
				listDstData.Add( arrSrcData[i] );
			}
			start = count;
		}

		T	newData;

		for( i=start; i < maxCount; i++ ) {
			newData = new T();
			if( callback != null ) {
				callback( (object)newData );
			}
			listDstData.Add( newData );
		}
	}

	void CreateListData<T>( ref List<T> listDstData, onLoadorCreateData callback )
	{
		if( listDstData == null ) {
			return;
		}
		if( callback == null ) {
			return;
		}

		int		i;

		for( i = 0; i < listDstData.Count; i++ ) {
			callback( (object)listDstData[i] );
		}
	}



	/// <summary>
	/// 読み込んだときのデータ成型の基底処理
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="typeData"></param>
	/// <param name="count"></param>
	void	LoadOrCreateDataDelegateData<T>( ref T[] typeData, int count )
	{
		if( typeData == null ){
			typeData = new T[count];
		}
		else if( typeData.Length != count ){
			T[]	newElementTypeData = new T[count];
			
			Array.Copy( typeData, newElementTypeData, Math.Min(typeData.Length, count) );
			typeData = newElementTypeData;
		}
	}
	void	LoadOrCreateDataDelegateClass<T>( ref T[] typeData, int count ) where T : new()
	{
		if( typeData == null ){
			typeData = new T[count];
		}
		else if( typeData.Length != count ){
			T[]	newElementTypeData = new T[count];
			
			Array.Copy( typeData, newElementTypeData, Math.Min(typeData.Length, count) );
			typeData = newElementTypeData;
		}

		//	要素がないときは作る
		int		i;

		for( i = 0; i < typeData.Length; i++ ) {
			if( typeData[i] == null ) { 
				typeData[i] = new T();
			}
		}
	}


	/// <summary>
	/// 節点の有効かチェックする
	/// </summary>
	/// <param name="i_pos"></param>
	/// <param name="j_pos"></param>
	public	bool CheckNodePosition( int node_i, int node_j )
	{
		if( node_i < 0 ) {
			return	false;
		}
		if( node_i >= _listNodePoint.Count ) {
			return	false;
		}
		if( node_j < 0 ) {
			return	false;
		}
		if( node_j >= _listNodePoint.Count ) {
			return	false;
		}
		if( node_i == node_j ) {
			return	false;
		}

		Vector3		pos_i = _listNodePoint[node_i];
		Vector3		pos_j = _listNodePoint[node_j];

		if( pos_i == pos_j ) {
			return	false;
		}

		return		true;
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="node_i"></param>
	/// <param name="node_j"></param>
	public	bool	GetNodeLength( int node_i, int node_j, out float length )
	{
		bool	bEnable = CheckNodePosition( node_i, node_j );
		if( bEnable == false ) {
			length = 0.0f;
			return	false;
		}

		Vector3		pos_i = _listNodePoint[node_i];
		Vector3		pos_j = _listNodePoint[node_j];

		length =  Vector3.Distance( pos_i, pos_j );

		return	true;
	}


	/// <summary>
	/// 要素の成型処理
	/// </summary>
	/// <param name="o"></param>
	void	LoadOrCreateDataDelegateElement( object o )
	{
		var	srcData = o as ElementData;
		LoadOrCreateDataDelegateClass( ref srcData.elementTypeData, _haderData.elementTypeCount );
	}

	/// <summary>
	/// パネルの成型処理
	/// </summary>
	/// <param name="o"></param>
	void	LoadOrCreateDataDelegatePanel( object o )
	{
		var	srcData = o as PanelData;
		LoadOrCreateDataDelegateClass( ref srcData.panelTypeData, _haderData.panelTypeCount );
	}

	/// <summary>
	/// データの読み込み
	/// </summary>
	/// <param name="fileName"></param>
	/// <returns></returns>
	public	bool	Load( string strJson)
	{
		SaveData	saveData = JsonUtility.FromJson( strJson, typeof(SaveData) ) as SaveData;

		_haderData = saveData.headerData;
		LoadOrCreateData( ref _listNodePoint, saveData.nodePoints, _haderData.nodeCount );
		LoadOrCreateData( ref _listElementData, saveData.elementData, _haderData.elementDataCount, LoadOrCreateDataDelegateElement );
		LoadOrCreateData( ref _listPanelData, saveData.panelData, _haderData.panelDataCount, LoadOrCreateDataDelegatePanel );
		
		return	true;
	}


}



