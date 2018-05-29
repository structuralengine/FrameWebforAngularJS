using UnityEngine;
using System.Collections;

public class TabButton : MonoBehaviour
{
	public delegate	void onClickDelegate( int tabNo );

	private	int	_tabNo = 0;
	public int tabNo { set { _tabNo=value; } }
	
	public onClickDelegate onClickCallback;
	
	public void onClick()
	{
		if( onClickCallback != null ) {
			onClickCallback( _tabNo );
		}
	}
}
