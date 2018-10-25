using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemUtility;



public class MemberDispManager : PartsDispManager
{
	static readonly Color		s_lineTypeBlockColor = new Color( 0.8f, 0.0f, 0.0f );


	public	enum DispType {
		Block,
		Line,
	}

	private	DispType	_dispType = DispType.Line;

	[SerializeField]
	float		_elementScale = 1.0f;

	/// <summary>
	/// パーツを作成する
	/// </summary>
	public	override void	CreateParts()
	{
		if( _webframe == null ) {
			return;
		}

        Dictionary<int, webframe.MemberData> memberData = _webframe.ListMemberData;
        Dictionary<int, Dictionary<int, webframe.ElementData>> elementData = _webframe.ListElementData;

        CreatePartsCommon( memberData.Count, "Member" );
	}


	/// <summary>
	/// 
	/// </summary>
	public	override void	SetBlockStatus( int id )
	{
        webframe.MemberData memberData = _webframe.ListMemberData[id];

        // 節点が有効かどうか調べる
        int nodeI = memberData.ni;
        int nodeJ = memberData.nj;

        float length = 0.0f;
        if (_webframe.GetNodeLength(nodeI, nodeJ, out length) == false)
            return;

        PartsDispStatus partsDispStatus;
		partsDispStatus.id = id;
		partsDispStatus.enable = true;

		if( SetBlockStatusCommon(partsDispStatus) == false ) {
			return;
		}

        //	表示に必要なパラメータを用意する
        Vector3 pos_i = _webframe.listNodePoint[nodeI];
        Vector3 pos_j = _webframe.listNodePoint[nodeJ];

        //	幅と高さを設定する
        Vector3 scale;
        scale.x = 0.05f;
        scale.y = 0.05f;
        scale.z = length;

        if ( _dispType == DispType.Block ) {
            // 材料情報が有効かどうか調べる
            int e = memberData.e;
            if (0 <= e && e < _webframe.ListElementData.Count){
                Dictionary<int, webframe.ElementData> tmp = _webframe.ListElementData[1]; // とりあえず タイプ1 を選択
                webframe.ElementData elementData = tmp[e];

                float	danmenseki = elementData.A;
			    float	moment_z = elementData.Iz;
			    float	moment_y = elementData.Iy;

			    //	スケール値を計算
			    if( danmenseki > 0.0f ) { 
				    scale.x = (float)System.Math.Sqrt( (double)(12.0f * moment_z/danmenseki) ) * _elementScale;
				    scale.y = (float)System.Math.Sqrt( (double)(12.0f * moment_y/danmenseki) ) * _elementScale;
			    }
			    else {
				    scale.x = 1.0f;
				    scale.y = 1.0f;
			    }
            }
        }

        BlockWorkData blockWorkData = _blockWorkData[id];

        //	姿勢を設定
        blockWorkData.rootBlockTransform.position = pos_i;
		blockWorkData.rootBlockTransform.LookAt( pos_j );
		blockWorkData.rootBlockTransform.localScale = scale;

		//	方向矢印の表示
		if( blockWorkData.directionArrow != null ) {
			if( _dispType == DispType.Block ) {

				Quaternion	rotate = Quaternion.LookRotation( pos_j-pos_i, Vector3.forward );
				Vector3		arrowCenter = Vector3.Lerp( pos_i, pos_j, 0.5f );
				Vector3		arrowSize = new Vector3( 1.0f, 1.0f, length * 0.25f );

				blockWorkData.directionArrow.SetArrowDirection( arrowCenter, rotate, arrowSize );
				blockWorkData.directionArrow.EnableRenderer( enabled );
			}
			else {
				blockWorkData.directionArrow.EnableRenderer( false );
			}
		}


		//	色の指定
		Color	color;
		int		i;

		if( _dispType == DispType.Block ) {
			color = s_noSelectColor;
		}
		else {
			color = s_lineTypeBlockColor;
		}
		for( i = 0; i < _blockWorkData.Count; i++ ) {
			SetPartsColor( i, false, color );
		}
	}

    /// <summary>
    /// 指定された節点と一致するブロックを設定する
    /// </summary>
    /// <param name="search_node"></param>
    public void CheckNodeAndUpdateStatus( int search_node )
	{
        Dictionary<int, webframe.MemberData> ListMemberData = _webframe.ListMemberData;

		for(int i = 0; i < ListMemberData.Count; i++ ) {
            int nodeI = ListMemberData[i].ni;
            int nodeJ = ListMemberData[i].nj;

            float length = 0.0f;
            if (_webframe.GetNodeLength(nodeI, nodeJ, out length) == false)
                continue;
			if( search_node != nodeI && search_node != nodeJ) {
				continue;		//	関わっていないので無視
			}
			SetBlockStatus( i );
		}
	}



	/// <summary>
	/// 
	/// </summary>
	public	override void	InputMouse()
	{
		if( _dispType == DispType.Block ) {
			base.InputMouse();
		}
	}



	/// <summary>
	/// 
	/// </summary>
	/// <param name="dispType"></param>
	public void ChangeDispMode( DispType dispType )
	{
		if( _dispType == dispType ) {
			return;
		}

		_dispType = dispType;
		SetBlockStatusAll();
	}

}
