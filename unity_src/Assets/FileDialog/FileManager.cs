using UnityEngine;
using UnityEngine.UI;
//using MeteorMindsTools;
using SystemUtility;



public class FileManager : MonoBehaviour
{
	enum SelectType {
		None,
		New,
		Open,
		Save,
		NamedSave,
	}
    
    public string defaultPathToSearch;
    public bool startSearchOnDefaultPath;
    public string[] fileTypes;

    private string filePath;

 //   MeteorToolsObject		meteorObj;
	SkeletonResponseData	_skeletonResponseData;
	InputPanelManager		_inputPanelManager;

    // Use this for initialization
    void Start () {
//        meteorObj = new MeteorToolsObject();
        filePath = string.Empty;
		_skeletonResponseData = SkeletonResponseData.Instance;
		_inputPanelManager = FindObjectOfType<InputPanelManager>();
    }



    // Update is called once per frame
    void Update () {
	
	}



	/// <summary>
	/// 
	/// </summary>
	bool	OpneFile()
	{
		string directoryToStartTheSearch = string.Empty;
					
		//Check the directory to start the search
		//IF it's NOT to start the search on default path, and the LastKnownDirectory is not null or empty
		//ELSE search on defaultPathToSearch
		try
		{
			//if (!startSearchOnDefaultPath && !string.IsNullOrEmpty(meteorObj.LastKnownDirectory()))
			//{
			//	directoryToStartTheSearch = meteorObj.LastKnownDirectory();
			//}
			//else
			//{
			//	directoryToStartTheSearch = defaultPathToSearch;
			//}

			////Store the file path that results from the Open File Dialog in 'filePath' variable
			//filePath = meteorObj.GetFileNameWithPathToOpen( directoryToStartTheSearch, fileTypes );

			////Set the file path to the inputField
			//_skeletonResponseData.Load( filePath );

		}
		catch(System.Exception ex) {
			Debug.Log("FileOpen Error\n" + ex.Message);

			return	false;
		}

		return	true;
	}



	/// <summary>
	/// 名前をつけて保存
	/// </summary>
	bool	SaveNamedFile()
	{
		string directoryToStartTheSearch = string.Empty;
					
		//Check the directory to start the search
		//IF it's NOT to start the search on default path, and the LastKnownDirectory is not null or empty
		//ELSE search on defaultPathToSearch
		try
		{
			//if (!startSearchOnDefaultPath && !string.IsNullOrEmpty(meteorObj.LastKnownDirectory()))
			//{
			//	directoryToStartTheSearch = meteorObj.LastKnownDirectory();
			//}
			//else
			//{
			//	directoryToStartTheSearch = defaultPathToSearch;
			//}

			////Store the file path that results from the Open File Dialog in 'filePath' variable
			//filePath = meteorObj.GetFileNameWithPathToSave( directoryToStartTheSearch, fileTypes );

			////Set the file path to the inputField
			//_skeletonResponseData.Save( filePath );

		}
		catch(System.Exception ex) {
			Debug.Log("FileOpen Error\n" + ex.Message);
			return	false;
		}

		return	true;
	}


	/// <summary>
	/// 
	/// </summary>
    public void OpenFile( int value )
    {
		MainFrameManager	mainFrameManager = FindObjectOfType<MainFrameManager>();
		Dropdown	dropdown = GetComponent<Dropdown>();

		SelectType	select = (SelectType)dropdown.value;

		switch( select ) {
			//	新規作成
			case	SelectType.New:
				SkeletonResponseData.HeaderData	headeerData = new SkeletonResponseData.HeaderData();
				_skeletonResponseData.Create( headeerData );
				_inputPanelManager.RefreshInputPanel();
				mainFrameManager.CreateParts();
				mainFrameManager.SetAllBlockStatus();
				break;

			//	オープン
			case	SelectType.Open:
				if( OpneFile() ) {
					_inputPanelManager.RefreshInputPanel();
					mainFrameManager.CreateParts();
					mainFrameManager.SetAllBlockStatus();
				}
				break;

			//	保存
			case	SelectType.Save:
				if( SUFunctions.IsEnablePath(_skeletonResponseData.filePath) ||
					System.IO.File.Exists(_skeletonResponseData.filePath) == false ) {
					//	ファイルパスが無いので名前をつけて保存
					SaveNamedFile();
				}
				else {
					//	上書き保存
					_skeletonResponseData.Save( _skeletonResponseData.filePath );
				}
				break;

			//	名前をつけて保存
			case SelectType.NamedSave:
				SaveNamedFile();
				break;
		}

		//	選択をトップに戻す
		if( select != SelectType.None ) {
			dropdown.value = (int)SelectType.None;
		}
    }
}
