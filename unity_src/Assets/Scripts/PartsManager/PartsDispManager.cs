using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PartsDispManager : MonoBehaviour
{
	public struct PartsDispStatus
	{
		public	int			id;
		public	bool		enable;
	}
	

	protected	static readonly Color	s_selectColor = new Color( 1.0f, 0.5f, 1.0f );
	protected	static readonly Color	s_noSelectColor = new Color( 1.0f, 1.0f, 1.0f );

	protected webframe	_webframe = null;
    protected MainFrameManager _mainFrameManager = null;


    /// <summary>
    /// 
    /// </summary>
    protected class BlockWorkData {
		public	GameObject		gameObject;
		public	Transform		gameObjectTransform;
		public	Transform		rootBlockTransform;
		public	Renderer		renderer;
		public	BlockData		blockData;
		public	DirectionArrow	directionArrow;
		public	MaterialPropertyBlock	materialPropertyBlock;
		public	Mesh		mesh;
	}

	[SerializeField]
	GameObject			_blockPrefab = null;

	protected	List<BlockWorkData>	_blockWorkData = new List<BlockWorkData>();

	

	/// <summary>
	/// 
	/// </summary>
	/// <param name="count"></param>
	//protected IEnumerable	CreatePartsCommon( int parts_count, string partsName )
	protected void	CreatePartsCommon( int parts_count, string partsName )
	{
		if( _blockPrefab == null ) {
			//yield	break;
			return;
		}

		int		i;


		BlockWorkData	blockWorkData;
		MeshFilter		meshFileter;


		for( i = 0; i < _blockWorkData.Count; i++ ) {
			Destroy( _blockWorkData[i].renderer.sharedMaterial );
			Destroy( _blockWorkData[i].gameObject );
		}

		_blockWorkData.Clear();
		for( i = 0; i < parts_count; i++ ) {
            blockWorkData = new BlockWorkData{ gameObject = Instantiate(_blockPrefab) };
            _blockWorkData.Add( blockWorkData );
		}

		for( i = 0; i < parts_count; i++ ) {
			blockWorkData = _blockWorkData[i];
			blockWorkData.gameObjectTransform   = blockWorkData.gameObject.transform;
			blockWorkData.rootBlockTransform = blockWorkData.gameObjectTransform.Find("Root");
			blockWorkData.blockData = blockWorkData.gameObject.GetComponentInChildren<BlockData>();
			blockWorkData.blockData.id = i; 
			blockWorkData.directionArrow =  blockWorkData.gameObject.GetComponentInChildren<DirectionArrow>();
			blockWorkData.renderer = blockWorkData.gameObject.GetComponentInChildren<Renderer>();
			if( blockWorkData.renderer == null ) {
				continue;
			}
			blockWorkData.renderer.sharedMaterial = Instantiate( blockWorkData.renderer.sharedMaterial );
			blockWorkData.materialPropertyBlock = new MaterialPropertyBlock();
			blockWorkData.materialPropertyBlock.SetColor("_Color", Color.white );
			blockWorkData.renderer.SetPropertyBlock( blockWorkData.materialPropertyBlock );

			blockWorkData.gameObject.name  = partsName + "[" + i + "]";
			blockWorkData.gameObjectTransform.parent = this.gameObject.transform;
			blockWorkData.gameObject.SetActive( false );
				
			//	メシュの取得
			meshFileter = blockWorkData.gameObject.GetComponentInChildren<MeshFilter>();
			if( meshFileter != null ) { 
				blockWorkData.mesh = meshFileter.mesh;
			}
		}
	}



	/// <summary>
	/// 
	/// </summary>
	/// <param name="statusBase"></param>
	/// <returns></returns>
	protected bool	SetBlockStatusCommon( PartsDispStatus partsDispStatus )
	{
		if( partsDispStatus.id >= _blockWorkData.Count ) {
			return	false;
		}

		BlockWorkData	blockWorkData = _blockWorkData[partsDispStatus.id];

		//	無効な場合
		if( partsDispStatus.enable == false ){
			if( blockWorkData.gameObject != null ) { 
				if( blockWorkData.gameObject.activeSelf ){
					blockWorkData.gameObject.SetActive( false );
				}
			}
			return	false;
		}


		//	有効な場合
		if( blockWorkData.gameObject == null ) { 
			return	false;
		}

		if( !blockWorkData.gameObject.activeSelf ){
			blockWorkData.gameObject.SetActive( true );
		}

		return	true;
	}


	/// <summary>
	/// パーツの作成の仮想関数
	/// </summary>
	public	virtual void	CreateParts()
	{

	}



	/// <summary>
	/// 表示ブロックの設定の仮想関数
	/// </summary>
	public	virtual	void	SetBlockStatus( int id )
	{
	}


	/// <summary>
	/// 表示ブロックの設定の仮想関数
	/// </summary>
	public	virtual	void	SetBlockStatusAll()
	{
		int		i;

		for( i = 0; i < _blockWorkData.Count; i++ ) {
			SetBlockStatus( i );
		}
	}



	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	/// <param name="color"></param>
	protected	void	SetPartsColor( int id, bool onoff, Color color )
	{
		BlockWorkData	blockWorkData = _blockWorkData[id];

		blockWorkData.materialPropertyBlock.SetColor("_Color", color );
		blockWorkData.renderer.SetPropertyBlock( blockWorkData.materialPropertyBlock );
		if( blockWorkData.directionArrow != null ) {
			blockWorkData.directionArrow.EnableRenderer( onoff );
		}
	}



	/// <summary>
	/// マウスの入力制御
	/// </summary>
	public	virtual void	InputMouse()
	{
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
 
			if (Physics.Raycast(ray, out hit)){
				GameObject	obj = hit.collider.gameObject; 

				//	ブロックが選択された
				{
					BlockData	blockData;
					blockData = obj.GetComponentInParent<BlockData>();
					if( blockData != null ){
                        _mainFrameManager.SelectItemChange(blockData.id); // ここで AngularJS に通知する
                    }
				}
			}
		}
	}

    /// <summary>
    /// ブロックの色を変更
    /// </summary>
    /// <param name="id"></param>
    public void ChengeForcuseBlock(int id) {

        for (int i = 0; i < _blockWorkData.Count; i++){
            if ( i == id )
                SetPartsColor(i, true, s_selectColor);
            else
                SetPartsColor(i, false, s_noSelectColor);
        }
    }

    // Use this for initialization
    void Start () {
		_webframe = webframe.Instance;
        _mainFrameManager = FindObjectOfType<MainFrameManager>();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
