using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DataGrid : MonoBehaviour
{
	//	作成するための情報
	public struct CreateGridSettingData
	{
		public	int		rowCount;
		public	int[]	repeatColumnCount;
	}

	//	データ引き渡し用
	static	private	CreateGridSettingData[]		_reserveCreateGridSettingData = null;
	static public CreateGridSettingData[] reserveCreateGridSettingData { set{ _reserveCreateGridSettingData=value; } }

	private	CreateGridSettingData[]		_createGridSettingData = null;
	public CreateGridSettingData[] createGridSettingData { get{ return _createGridSettingData; } }


	//	カラムのデータの入力設定
	[Serializable]
	class CullumSettingInfo
	{
		[Multiline]
		public	string					headerText = "";		//	ヘッダーのテキスト
		public	string					valueFormat = "";		//	表示フォーマット
		public	InputField.ContentType	contentType = InputField.ContentType.DecimalNumber;			//	コンテンツのタイプ
		public	float					widthScale = 1.0f;		
		public	bool					isReadOnly = false;		//	読み取り専用
	}

	[Serializable]
	class CullumSettingRepeatInfo
	{
		public	int		repeatCount = 3;
		public	string	repeatHeader = "タイプ";
		public	CullumSettingInfo[]		cullumSettingInfo = null;
	}

	[Serializable]
	class CullumSettingSheet
	{
		public	string	sheetName = "座標";
		public	int		rowCount = 5;
		public	CullumSettingInfo[]			cullumSettingInfo = null;
		public	CullumSettingRepeatInfo[]	cullumSettingRepeatInfo = null;
		public	CullumSettingInfo[]			cullumTailSettingInfo = null;
	}


	//	グリッド情報
	public class GridCellTableInfo
	{
		public	InputField			gridCell = null;
		public	GridCellAction		gridCellAction = null;
	}


	//	シート別情報
	public class SheetWorkInfo {
		public	GameObject				sheetGameObject = null;
		public	GameObject[]			cullumHeader = null;
		public	GridCellTableInfo[][]	gridCellTableInfo = null;
	}

	class TabButtonInfo {
		public	GameObject	tabButonObject;
		public	Button		button;
	}


	[SerializeField]
    public float	_pading = 1;


	[SerializeField]
	float			_headerButtonHeight = 30;

	[SerializeField]
	float			_repeatButtonHeight = 20;

	[SerializeField]
    float			_cellWidth = 90;

	[SerializeField]
    float			_cellHeight = 30;

    

	[SerializeField]
    Button				_tabButton = null;

	[SerializeField]
    Button				_headerButton = null;

	[SerializeField]
    InputField			_gridCell = null;

	[SerializeField]
	private	int		_focusSheetNo = 0;
	public int focusSheetNo
	{
		set{ _focusSheetNo = value; }
		get{ return _focusSheetNo; }
	}


	[SerializeField]
	CullumSettingSheet[]	_cullumSettingSheet = null;		//	カラム情報

	TabButtonInfo[]			_tabButtonInfo = null;
	SheetWorkInfo[]			_sheetWorkInfo = null;			//	ワークデータ
	RectTransform			_contentTransform = null;
	bool			_isCreateHeader = false;



	/// <summary>
	/// 
	/// </summary>
	void	Awake()
	{
		//	外部からデータの個数を設定
		if( _reserveCreateGridSettingData != null ) {
			int		count = Math.Min( _reserveCreateGridSettingData.Length, _cullumSettingSheet.Length );
			int		repeat_count;
			int		i, j;

			for( i = 0; i < count; i++ ) {
				_cullumSettingSheet[i].rowCount = _reserveCreateGridSettingData[i].rowCount;
				if( _reserveCreateGridSettingData[i].repeatColumnCount == null ) {
					continue;
				}
				if( _cullumSettingSheet[i].cullumSettingRepeatInfo == null ) {
					continue;
				}

				repeat_count = Math.Min( _reserveCreateGridSettingData[i].repeatColumnCount.Length, _cullumSettingSheet[i].cullumSettingRepeatInfo.Length );
				for( j = 0; j < repeat_count; j++ ) {
					_cullumSettingSheet[i].cullumSettingRepeatInfo[j].repeatCount = _reserveCreateGridSettingData[i].repeatColumnCount[j];
				}
			}

			_createGridSettingData = _reserveCreateGridSettingData;
			_reserveCreateGridSettingData = null;
		}
	}


	/// <summary>
	/// シートの行の数を設定
	/// </summary>
	/// <param name="no"></param>
	/// <param name="row_count"></param>
	public	void	SetSheetRowCount( int no, int row_count )
	{
		_cullumSettingSheet[no].rowCount = row_count;
	}



	/// <summary>
	///	リピートデータの数の設定
	/// </summary>
	/// <param name="no"></param>
	/// <param name="repeat_no"></param>
	/// <param name="repeat_count"></param>
	public void		SetSheetRepeatColumn( int no, int repeat_no, int repeat_count )
	{
		try { 
			_cullumSettingSheet[no].cullumSettingRepeatInfo[repeat_no].repeatCount = repeat_count;
		}
		catch( Exception e ) {
			Debug.LogError( e.ToString() );
		}
	}



	/// <summary>
	/// セルに文字列を設定する
	/// </summary>
	/// <param name="r"></param>
	/// <param name="c"></param>
	/// <param name="strValue"></param>
	public	void	SetCellValue( int s, int r, int c, object o )
	{
		SheetWorkInfo	sheetWorkInfo = _sheetWorkInfo[s];

		sheetWorkInfo.gridCellTableInfo[r][c].gridCell.text = o.ToString();
	}


	/// <summary>
	/// セルにリピート情報の文字列を設定する
	/// </summary>
	/// <param name="r"></param>
	/// <param name="c"></param>
	/// <param name="strValue"></param>
	public	void	SetCellRepeatValue( int s, int r, int repeat_count, int data_start, int data_end, int data_id, object o )
	{
		SheetWorkInfo	sheetWorkInfo = _sheetWorkInfo[s];
		int	repeat_length = (data_end+1-data_start);
		int	c = data_id + repeat_count * repeat_length;

		try { 
			sheetWorkInfo.gridCellTableInfo[r][c].gridCell.text = o.ToString();
		}
		catch( Exception e ) 
		{
			Debug.LogError( e.ToString() );
		}
	}



	/// <summary>
	/// 
	/// </summary>
	/// <param name="r"></param>
	/// <param name="c"></param>
	/// <returns></returns>
	public	string	GetCellFormat( int s, int r, int c )
	{
		SheetWorkInfo	sheetWorkInfo = _sheetWorkInfo[s];

		return	sheetWorkInfo.gridCellTableInfo[r][c].gridCellAction.valueFormat;
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="r"></param>
	/// <returns></returns>
	public	bool	IsRowForcused( int r )
	{
		if( _sheetWorkInfo == null ) {
			return	false;
		}

		SheetWorkInfo	sheetWorkInfo = _sheetWorkInfo[_focusSheetNo];

		int		c;

		for( c=0; c<sheetWorkInfo.gridCellTableInfo[r].Length; c++ ){
			if( sheetWorkInfo.gridCellTableInfo[r][c].gridCell.isFocused ) {
				return	true;
			}
		}
		
		return	false;
	}



	/// <summary>
	/// カラムの数を取得する
	/// </summary>
	/// <returns></returns>
	int		GetCullumCount( int sheetNo )
	{
		int		cullum_count = 0;
		int		i;


		CullumSettingSheet	cullumSettingSheet = _cullumSettingSheet[sheetNo];

		cullum_count += cullumSettingSheet.cullumSettingInfo.Length;
		for( i=0; i<cullumSettingSheet.cullumSettingRepeatInfo.Length; i++ ){
			cullum_count += cullumSettingSheet.cullumSettingRepeatInfo[i].repeatCount * cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo.Length;
		}
		cullum_count += cullumSettingSheet.cullumTailSettingInfo.Length;

		return	cullum_count;
	}


	/// <summary>
	/// キャンバスのサイズの計算
	/// </summary>
	void	SetCanvasSize()
	{
		float	width = 0;
        float	height = 0;
		int		cullum_count = 0;
		int		row_count = 0;
		int		i;


		for( i = 0; i<_cullumSettingSheet.Length; i++ ) {
			cullum_count = Math.Max( GetCullumCount( i ), cullum_count );
			row_count = Math.Max( _cullumSettingSheet[i].rowCount, row_count );
		}
		
		
		width += _pading;
		width += _cellWidth;
		width += _cellWidth * cullum_count;
		width += _pading;
		
		height += _pading;
		if( _cullumSettingSheet.Length > 1 ){
			height += 30;
		}
		height += _headerButtonHeight;
		height += _cellHeight * row_count;
		height += _pading;

        _contentTransform.sizeDelta = new Vector2(width, height);
	}



	/// <summary>
	/// タブボタンが押されたとき
	/// </summary>
	/// <param name="tabNo"></param>
	void	OnCallbackTabButton( int tabNo )
	{
		int		i;

		for( i = 0; i < _sheetWorkInfo.Length; i++ ) {
			if( i == tabNo ) {
				_sheetWorkInfo[i].sheetGameObject.SetActive( true );
				_tabButtonInfo[i].button.interactable = false;
			}
			else {
				_sheetWorkInfo[i].sheetGameObject.SetActive( false );
				_tabButtonInfo[i].button.interactable = true;
			}
		}
	}
	

	/// <summary>
	/// タブボタンの作成
	/// </summary>
	/// <param name="posX"></param>
	/// <param name="posY"></param>
	void	CreateTabButton( out TabButtonInfo tabButtonInfo, ref float posX, ref float posY, string headerText, int tabNo )
	{
		tabButtonInfo = null;
		if( _tabButton == null ) {
			return;
		}

		Button	button = Instantiate(_tabButton);

		button.transform.SetParent(_contentTransform, false);
		button.GetComponent<RectTransform>().sizeDelta = new Vector2(_cellWidth, _cellHeight);
		button.transform.position = new Vector3(posX, posY);
		button.GetComponentInChildren<Text>().text = headerText;
		button.gameObject.name = "TabButton[" + tabNo + "]";

		TabButton tabButton = button.GetComponent<TabButton>();
		if( tabButton == null ) {
			return;
		}
		
		tabButton.tabNo = tabNo;
		tabButton.onClickCallback = OnCallbackTabButton;
		button.onClick.AddListener( tabButton.onClick );
		
		posX += _cellWidth;


		tabButtonInfo = new TabButtonInfo();
		tabButtonInfo.tabButonObject = button.gameObject;
		tabButtonInfo.button = button;
	}



	/// <summary>
	/// リピートヘッダーボタンの作成
	/// </summary>
	/// <param name="posX"></param>
	/// <param name="posY"></param>
	void	CreateRepeatHeaderButton( ref float posX, ref float posY, float width, float height, Transform parentTransform, string headerText, int sheetNo, int cullumNo )
	{
		Button	RowHeaderButton = Instantiate(_headerButton);
		RowHeaderButton.transform.SetParent(parentTransform, false);
		RowHeaderButton.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		RowHeaderButton.transform.position = new Vector3(posX, posY);
		RowHeaderButton.GetComponentInChildren<Text>().text = headerText;
		RowHeaderButton.gameObject.name = "Repeat[" + sheetNo + "," + cullumNo + "]";
		posX += width;
	}


	/// <summary>
	/// ヘッダーボタンの作成
	/// </summary>
	/// <param name="posX"></param>
	/// <param name="posY"></param>
	void	CreateHeaderButton( ref float posX, ref float posY, float width, float height, Transform parentTransform, string headerText, int sheetNo, int cullumNo )
	{
		Button	RowHeaderButton = Instantiate(_headerButton);
		RowHeaderButton.transform.SetParent(parentTransform, false);
		RowHeaderButton.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		RowHeaderButton.transform.position = new Vector3(posX, posY);
		RowHeaderButton.GetComponentInChildren<Text>().text = headerText;
		RowHeaderButton.gameObject.name = "RowHeader[" + sheetNo + "," + cullumNo + "]";
		posX += width;
	}



	/// <summary>
	/// ヘッダーの作成
	/// </summary>
	void	CreateGridBase( RectTransform contentTransform )
	{
		if( _isCreateHeader ) {
			return;
		}
		_contentTransform = contentTransform;

		
		Vector3 DefPosition = _contentTransform.position;

		SetCanvasSize();

		CullumSettingSheet	cullumSettingSheet;
		SheetWorkInfo		sheetWorkInfo;
		string	strTemp;
		float	posY;
		float	posX;
		float	width;
		float	height;
		int		c;
		int		i;
		int		j;
		int		k;
		int		s;


		//	タブボタンの配置
		posY = DefPosition.y - _pading;
		posX = DefPosition.x + _pading;

		if( _cullumSettingSheet.Length > 1 ) {
			_tabButtonInfo = new TabButtonInfo[_cullumSettingSheet.Length];
			for( i = 0; i < _cullumSettingSheet.Length; i++ ) { 
				CreateTabButton( out _tabButtonInfo[i], ref posX, ref posY, _cullumSettingSheet[i].sheetName, i );
					
			}
			_tabButtonInfo[0].button.interactable  = false;
		}

		//	ワーク領域作成
		_sheetWorkInfo = new SheetWorkInfo[_cullumSettingSheet.Length];
		for( i = 0; i < _sheetWorkInfo.Length; i++ ) {
			_sheetWorkInfo[i] = new SheetWorkInfo();
			_sheetWorkInfo[i].sheetGameObject = new GameObject("Sheet:"+_cullumSettingSheet[i].sheetName );
			_sheetWorkInfo[i].sheetGameObject.transform.SetParent( _contentTransform );
			if( i != 0 ) {
				_sheetWorkInfo[i].sheetGameObject.SetActive( false );
			}
		}

		//	シートの初期化
		for( s = 0; s < _sheetWorkInfo.Length; s++ ) { 
			sheetWorkInfo = _sheetWorkInfo[s];
			cullumSettingSheet = _cullumSettingSheet[s];
			

			//	ヘッダーの高さを設定
			if( _cullumSettingSheet.Length > 1 ) {
				posY = DefPosition.y - (_pading + _cellHeight);
			}
			else {
				posY = DefPosition.y - _pading;
			}
			posX = DefPosition.x + _pading;

			//ヘッダーを配置
			var HeaderButton = Instantiate(_headerButton);
			HeaderButton.transform.SetParent(sheetWorkInfo.sheetGameObject.transform, false);
			HeaderButton.GetComponent<RectTransform>().sizeDelta = new Vector2(_cellWidth, _headerButtonHeight);
			HeaderButton.transform.position = new Vector3(posX, posY);
			HeaderButton.GetComponentInChildren<Text>().text = "";
			
			//	RowHeader を配置
			posX = DefPosition.x + _pading + _cellWidth;
			for (c = 0; c < cullumSettingSheet.cullumSettingInfo.Length; c++)
			{
				CreateHeaderButton( ref posX, ref posY, _cellWidth, _headerButtonHeight, sheetWorkInfo.sheetGameObject.transform, cullumSettingSheet.cullumSettingInfo[c].headerText, s, c );
			}

			//	リピートヘッダーの設定
			float		repeatPosX = posX;
			float		repeatPosY = posY;

			for( i = 0; i < cullumSettingSheet.cullumSettingRepeatInfo.Length; i++ ) {
				if( cullumSettingSheet.cullumSettingRepeatInfo[i].repeatCount == 1 ) {
					continue;
				}
				for( j = 0; j < cullumSettingSheet.cullumSettingRepeatInfo[i].repeatCount; j++ ) {
					if( cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo.Length <= 1 ) {		//	リピートが1個以下のときは必要ない
						continue;
					}
					if( cullumSettingSheet.cullumSettingRepeatInfo[i].repeatCount == 1 ) {
						strTemp = cullumSettingSheet.cullumSettingRepeatInfo[i].repeatHeader;
					}
					else {
						strTemp = cullumSettingSheet.cullumSettingRepeatInfo[i].repeatHeader + (j+1).ToString();
					}
					
					width  = _cellWidth*cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo.Length;
					height = _repeatButtonHeight;
					CreateRepeatHeaderButton( ref repeatPosX, ref repeatPosY, width, height,
							sheetWorkInfo.sheetGameObject.transform, strTemp, i, j );
				}
			}

			float		tailPosX = repeatPosX;
			float		tailPosY = repeatPosY;

			for (c = 0; c < cullumSettingSheet.cullumTailSettingInfo.Length; c++)
			{
				CreateHeaderButton( ref tailPosX, ref tailPosY, _cellWidth, _headerButtonHeight, sheetWorkInfo.sheetGameObject.transform, cullumSettingSheet.cullumTailSettingInfo[c].headerText, s, c );
			}


			//	リピート項目の設定
			float	startPosY = posY;
			
			width  = _cellWidth;

			for( i = 0; i < cullumSettingSheet.cullumSettingRepeatInfo.Length; i++ ) {
				for( j = 0; j < cullumSettingSheet.cullumSettingRepeatInfo[i].repeatCount; j++ ) {
					if( cullumSettingSheet.cullumSettingRepeatInfo[i].repeatCount==1 ||
						cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo.Length <= 1 ) {		//	リピートが1個以下のときそのままのサイズ
						posY = startPosY;
						height = _headerButtonHeight;
					}
					else {
						posY = startPosY - _repeatButtonHeight;
						height = _headerButtonHeight-_repeatButtonHeight;
					}
					for( k = 0; k < cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo.Length; k++ ) {
						strTemp = cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo[k].headerText;
						//	項目が１このときはこっちにIDをつける
						if( cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo.Length == 1 ) {
							strTemp += (j+1).ToString();
						}
						CreateHeaderButton( ref posX, ref posY, width, height,
							sheetWorkInfo.sheetGameObject.transform, strTemp, s, c );
						c++;
					}
				}
			}
		}

		_isCreateHeader = true;
	}




	/// <summary>
	/// 
	/// </summary>
	void	CreateCullumHeader()
	{
		Vector3 DefPosition = _contentTransform.position;
		SheetWorkInfo	sheetWorkInfo;
		int		i;
		int		s;


		for( s = 0; s < _sheetWorkInfo.Length; s++ ) {
			sheetWorkInfo = _sheetWorkInfo[s];

			if( sheetWorkInfo.cullumHeader != null ) {
				for( i = 0; i < sheetWorkInfo.cullumHeader.Length; i++ ) {
					if( sheetWorkInfo.cullumHeader[i] != null ){
						Destroy( sheetWorkInfo.cullumHeader[i] );
					}
				}
				sheetWorkInfo.cullumHeader = null;
			}
		}

        SetCanvasSize();

		CullumSettingSheet	cullumSettingSheet;
		float	posY;
		float	posX;
		int		r;

		for( s = 0; s < _sheetWorkInfo.Length; s++ ) { 
			sheetWorkInfo = _sheetWorkInfo[s];
			cullumSettingSheet = _cullumSettingSheet[s];

			//	CullumHeader を配置
			if( _cullumSettingSheet.Length > 1 ) {
				posY = DefPosition.y - (_pading + _headerButtonHeight) - (_pading + _cellHeight);
			}
			else {
				posY = DefPosition.y - (_pading + _headerButtonHeight);
			}
		
			posX = DefPosition.x + _pading;

			sheetWorkInfo.cullumHeader = new GameObject[cullumSettingSheet.rowCount];
			for ( r = 0; r < cullumSettingSheet.rowCount; r++)
			{
				var CullumHeaderButton = Instantiate(_headerButton);
				CullumHeaderButton.transform.SetParent(sheetWorkInfo.sheetGameObject.transform, false);
				CullumHeaderButton.GetComponent<RectTransform>().sizeDelta = new Vector2(_cellWidth, _cellHeight);
				CullumHeaderButton.transform.position = new Vector3(posX, posY);
				CullumHeaderButton.GetComponentInChildren<Text>().text = (r+1).ToString();
				sheetWorkInfo.cullumHeader[r] = CullumHeaderButton.gameObject;
				sheetWorkInfo.cullumHeader[r].name = "CullumHeader[" + s + "," + r + "]";
				posY -= _cellHeight;
			}
		}
	}



	/// <summary>
	/// セルのデータを作成
	/// </summary>
	void	CreateCellData( CullumSettingInfo cullumSettingInfo, ref float posX, ref float posY, int sheetNo, int r, int c )
	{
		SheetWorkInfo	sheetWorkInfo = _sheetWorkInfo[sheetNo];

		InputField			Cell;
		GridCellAction		gridCellAction;

        Cell = Instantiate(_gridCell);

		//	セルの設定を行う
		Cell.transform.SetParent(sheetWorkInfo.cullumHeader[r].transform, false);
        Cell.GetComponent<RectTransform>().sizeDelta = new Vector2(_cellWidth, _cellHeight);
        Cell.transform.position = new Vector3(posX, posY);
        Cell.name = string.Format("GridCell[{0},{1},{2}]", sheetNo, r, c);
		Cell.contentType = cullumSettingInfo.contentType;
		Cell.readOnly = cullumSettingInfo.isReadOnly;

		//	イベント用の設定
		gridCellAction = Cell.GetComponentInChildren<GridCellAction>();
		if( gridCellAction != null ) {
			gridCellAction.SetCellPos( sheetWorkInfo, sheetNo, r, c );
		}

		//	データを保持しておく
		sheetWorkInfo.gridCellTableInfo[r][c] = new GridCellTableInfo();
		sheetWorkInfo.gridCellTableInfo[r][c].gridCell = Cell;
		sheetWorkInfo.gridCellTableInfo[r][c].gridCellAction = gridCellAction;
		sheetWorkInfo.gridCellTableInfo[r][c].gridCellAction.valueFormat = cullumSettingInfo.valueFormat;
	
		posX += _cellWidth;
	}



	/// <summary>
	/// 
	/// </summary>
	void	CreateCell()
	{
		
		Vector3 DefPosition = _contentTransform.position;

		

		//	セルを配置
		CullumSettingSheet	cullumSettingSheet;
		SheetWorkInfo		sheetWorkInfo;
        float posY;
        float posX;
		int		s, r, c, i, j, k;
		int		cullum_count;


		//	セルの作成
		for( s = 0; s < _sheetWorkInfo.Length; s++ ) {
			sheetWorkInfo = _sheetWorkInfo[s];
			cullumSettingSheet = _cullumSettingSheet[s];
			cullum_count = GetCullumCount( s );

			if( _cullumSettingSheet.Length > 1 ) {
				posY = DefPosition.y - (_pading+_headerButtonHeight) - (_pading + _cellHeight);
			}
			else {
				posY = DefPosition.y - (_pading+_headerButtonHeight);
			}
        
			sheetWorkInfo.gridCellTableInfo = new GridCellTableInfo[cullumSettingSheet.rowCount][];

			for (r = 0; r < cullumSettingSheet.rowCount; r++)
			{
				sheetWorkInfo.gridCellTableInfo[r] = new GridCellTableInfo[cullum_count];
				posX = DefPosition.x + _pading + _cellWidth;

				//	固定カラムのセルの配置
				for (c = 0; c < cullumSettingSheet.cullumSettingInfo.Length; c++)
				{
					CreateCellData( cullumSettingSheet.cullumSettingInfo[c], ref posX, ref posY, s, r, c );
				}

				for( i = 0; i < cullumSettingSheet.cullumSettingRepeatInfo.Length; i++ ) {
					for( j = 0; j < cullumSettingSheet.cullumSettingRepeatInfo[i].repeatCount; j++ ) {
						for( k = 0; k < cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo.Length; k++ ) {
							CreateCellData( cullumSettingSheet.cullumSettingRepeatInfo[i].cullumSettingInfo[k], ref posX, ref posY, s, r, c );
							c++;
						}
					}
				}

				for (i = 0; i < cullumSettingSheet.cullumTailSettingInfo.Length; i++)
				{
					CreateCellData( cullumSettingSheet.cullumTailSettingInfo[i], ref posX, ref posY, s, r, c );
					c++;
				}

				//	繰り返しカラムのセルの配置
				posY -= _cellHeight;
			}
		}
	}



	//	セルの削除
	void	DeleteCell()
	{
		if( _sheetWorkInfo == null ) {
			return;
		}

		SheetWorkInfo	sheetWorkInfo;
		int				cullum_count;


		for( int s=0; s < _sheetWorkInfo.Length; s++ ) { 
			cullum_count = GetCullumCount( s );
			sheetWorkInfo = _sheetWorkInfo[s];

			for (int r = 0; r < sheetWorkInfo.gridCellTableInfo.Length; r++){
				for (int c = 0; c < cullum_count; c++){
					if( sheetWorkInfo.gridCellTableInfo[r][c].gridCell.gameObject != null ){
						Destroy( sheetWorkInfo.gridCellTableInfo[r][c].gridCell.gameObject );
					}
				}
			}
			if( sheetWorkInfo.sheetGameObject != null ) {
				Destroy( sheetWorkInfo.sheetGameObject );
			}
		}

		_sheetWorkInfo = null;
	}




    /// <summary>
    /// グリッドの配置を行う
    /// </summary>
    public void CreateGrid( RectTransform contentTransform )
    {
		CreateGridBase( contentTransform );
		CreateCullumHeader();
		CreateCell();
    }
    


	/// <summary>
	/// 
	/// </summary>
	/// <param name="?"></param>
	public	void	SetEventDelegate( GridCellAction.EventType eventType, object eventDelegate )
	{
		if( _sheetWorkInfo == null ){
			return;
		}

		int		s, r, c;

		SheetWorkInfo	sheetWorkInfo;


		for( s=0; s < _sheetWorkInfo.Length; s++ ) { 
			sheetWorkInfo = _sheetWorkInfo[s];

			for( r=0; r<sheetWorkInfo.gridCellTableInfo.Length; r++ ){
				for( c=0; c<sheetWorkInfo.gridCellTableInfo[r].Length; c++ ){
					switch( eventType ) {
						case	GridCellAction.EventType.EditEnd:
							try { 
								sheetWorkInfo.gridCellTableInfo[r][c].gridCellAction.onEditEnd = eventDelegate as GridCellAction.eventEditEnd;
							}
							catch( Exception e ) {
								Debug.LogError( e.ToString() );
							}
							break;
					}
				}
			}
		}
	}



	/// <summary>
	/// セルにフォーカスを当てる
	/// </summary>
	public	void	SetForcus( int r, int c )
	{
		SheetWorkInfo	sheetWorkInfo = _sheetWorkInfo[_focusSheetNo];

		sheetWorkInfo.gridCellTableInfo[r][c].gridCell.ActivateInputField();
	}
}
