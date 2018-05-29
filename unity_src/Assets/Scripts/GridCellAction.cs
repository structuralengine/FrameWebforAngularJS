using UnityEngine;
using UnityEngine.UI;
using System;
using SystemUtility;



public class GridCellAction : MonoBehaviour
{
	public	enum EventType
	{
		EditEnd,
	}

	public	delegate void	eventEditEnd( int s, int r, int c, string strValue );

	private	eventEditEnd	_onEditEnd = null;
	public	eventEditEnd	onEditEnd{
		set{ _onEditEnd = value; }
		get{ return _onEditEnd;	}
	}
	private	string	_valueFormat = "";
	public string	valueFormat
	{
		set{ _valueFormat = value; }
		get{ return _valueFormat;	}
	}

	DataGrid.SheetWorkInfo	_sheetWorkInfo;
	int		_sheetNo = 0;
	int		_rowNo = 0;
	int		_cullumNo = 0;



	/// <summary>
	/// セルのIDをセットする
	/// </summary>
	public	void	SetCellPos( DataGrid.SheetWorkInfo sheetWorkInfo, int s, int r, int c )
	{
		_sheetWorkInfo = sheetWorkInfo;
		_sheetNo = s;
		_rowNo = r;
		_cullumNo = c;
	}


    InputField _gridCell;

    // Use this for initialization
    public void Start()
    {
		_gridCell = GetComponent<InputField>();
        _gridCell.onEndEdit.AddListener(delegate { CellEditEnded(); });
    }



	/// <summary>
	/// 
	/// </summary>
    private void CellEditEnded()
    {
		_gridCell.text = SUFunctions.GetFloatFormatString( _gridCell.text, _valueFormat );

		if( _onEditEnd != null ) {
			_onEditEnd( _sheetNo, _rowNo, _cullumNo, _gridCell.text );
		}
    }

    #region セルの動作

    void Update()
    {
		if( _gridCell != null ) { 
			if (_gridCell.isFocused){
				if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Tab) ){
					CellEnter();
					//Event.current.Use();
				}
			}
		}
    }

    /// <summary>
    /// 次のセルを見つけて移動
    /// </summary>
    public void CellEnter()
    {
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Tab) ){
		}
		else {
			return;
		}
		
		DataGrid.GridCellTableInfo[][] gridCellTableInfo = _sheetWorkInfo.gridCellTableInfo;

		int		allCount = gridCellTableInfo.Length * gridCellTableInfo[0].Length;
		int		NowCount = gridCellTableInfo[0].Length * _rowNo + _cullumNo;

		NowCount++;
		if( NowCount >= allCount ) {
			NowCount = 0;
		}
		int		r = NowCount / gridCellTableInfo[0].Length;
		int		c = NowCount % gridCellTableInfo[0].Length;

		gridCellTableInfo[r][c].gridCell.Select();
		gridCellTableInfo[r][c].gridCell.ActivateInputField();
		GUI.FocusControl(gridCellTableInfo[r][c].gridCell.name);
    }
    #endregion

}//class end
