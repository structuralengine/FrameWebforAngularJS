using UnityEngine;
using System.Collections;

public class InputCanvas : MonoBehaviour
{
	[SerializeField]
	GameObject		_dataGridTableObject = null;



	/// <summary>
	/// 初期化
	/// </summary>
	void Start ()
	{
		if( _dataGridTableObject != null ) {
			_dataGridTableObject = Instantiate(_dataGridTableObject);
			_dataGridTableObject.transform.SetParent( gameObject.transform );
			_dataGridTableObject.transform.localPosition = Vector3.zero;
		}
	}
}
