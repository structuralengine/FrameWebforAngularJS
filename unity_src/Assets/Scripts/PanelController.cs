using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
	[SerializeField]
	GameObject		_titleBarObject = null;

	[SerializeField]
	string			_titleBarText = "";

	[SerializeField]
	float			_titleBarHeight = 20.0f;

	[SerializeField]
	GameObject		_inputCanvasObject = null;

	[SerializeField]
	float			_canvasWitdh = 200.0f;

	[SerializeField]
	float			_canvasHeight = 200.0f;


	BoxCollider2D	_titleBarCollider = null;
	bool			_isTitleBarDrag = false;
	bool			_isFirstUpdate = true;
	Vector2			_beforeMousePosition = Vector2.zero;



	/// <summary>
	/// 初期化
	/// </summary>
	void Start ()
	{
		RectTransform	ractTransform;


		//	タイトルバーの設定
		if( _titleBarObject != null ) {
			_titleBarObject = Instantiate( _titleBarObject );
			_titleBarObject.transform.SetParent( this.gameObject.transform );

			Text text = _titleBarObject.GetComponentInChildren<Text>();
			if( text != null ) {
				text.text = _titleBarText;
			}

			ractTransform = _titleBarObject.GetComponent<RectTransform>();
			if( ractTransform != null ) { 
				ractTransform.localPosition = Vector3.zero;
				ractTransform.sizeDelta = new Vector2( _canvasWitdh, _titleBarHeight );
			}

			_titleBarCollider = _titleBarObject.GetComponent<BoxCollider2D>();

			_titleBarCollider.offset = new Vector2( _canvasWitdh*0.5f, -_titleBarHeight*0.5f );
			_titleBarCollider.size = new Vector2( _canvasWitdh, _titleBarHeight );
		}


		//	キャンバスの設定
		if( _inputCanvasObject != null ){
			_inputCanvasObject = Instantiate( _inputCanvasObject );
			_inputCanvasObject.transform.SetParent( this.gameObject.transform );

			ractTransform = _inputCanvasObject.GetComponent<RectTransform>();
			if( ractTransform != null ) { 
				ractTransform.localPosition = new Vector3( 0.0f, -_titleBarHeight, 0.0f );
				ractTransform.sizeDelta = new Vector2( _canvasWitdh, _titleBarHeight + _canvasHeight );
			}
		}
	}



	/// <summary>
	/// 
	/// </summary>
	void	FirstUpdate()
	{
		if( _isFirstUpdate == false ) {
			return;
		}

		RectTransform	ractTransform;
		DataGridTable	dataGridTable = _inputCanvasObject.GetComponentInChildren<DataGridTable>();

		ractTransform = dataGridTable.GetComponent<RectTransform>();
		ractTransform.sizeDelta = new Vector2( _canvasWitdh, _titleBarHeight + _canvasHeight );
	}


	/// <summary>
	/// 更新処理
	/// </summary>
	void	Update()
	{
		FirstUpdate();

		//	タイトルバーの判定
		if( _titleBarCollider != null ) {
			Vector2		nowMousePosition = new Vector2( Input.mousePosition.x, Input.mousePosition.y );

			//	マウスを左クリックしたとき
			if (Input.GetMouseButtonDown(SystemDefile.MOUSE_BUTTON_LEFT)) {
				if( _titleBarCollider.OverlapPoint( nowMousePosition ) ){
					_isTitleBarDrag = true;
				}
			}
			else if (Input.GetMouseButtonUp(SystemDefile.MOUSE_BUTTON_LEFT)) {
				_isTitleBarDrag = false;
			}

			//	ドラッグの処理
			if( _isTitleBarDrag ) {
				Vector2		movePosition = nowMousePosition - _beforeMousePosition;
				Vector3		panelPosition = gameObject.transform.position;

				panelPosition.x += movePosition.x;
				panelPosition.y += movePosition.y;
				gameObject.transform.position = panelPosition;
			}

			_beforeMousePosition = nowMousePosition;
		}
	}
}
