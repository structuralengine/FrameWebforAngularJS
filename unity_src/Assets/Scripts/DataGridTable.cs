using UnityEngine;
using System.Collections;

public class DataGridTable : MonoBehaviour
{
	[SerializeField]
	private	GameObject		_ContentObject = null;
	public GameObject ContentObject
	{
		get {  return	_ContentObject; }
	}
}
