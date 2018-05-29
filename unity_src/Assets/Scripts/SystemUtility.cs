using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Collections;



/// <summary>
/// 
/// </summary>
namespace SystemUtility
{
	[Serializable]
	public class StringVector3
	{
		public	string	x = "";
		public	string	y = "";
		public	string	z = "";

		public void Set( Vector3 vec )
		{
			x = vec.x.ToString();
			y = vec.y.ToString();
			z = vec.z.ToString();
		}

		public string this[int index]{
			set
			{
				switch( index ) {
					case	0:
						x = value;
						break;
					case	1:
						y = value;
						break;
					case	2:
						z = value;
						break;
				}
			}
			get
			{
				switch( index ) {
					case	0:
						return	x;
					case	1:
						return	y;
					case	2:
						return	z;
				}
				return	"";
			}
		}
		

		public static	implicit operator Vector3( StringVector3 sv )
		{
			Vector3		v;

			v.x = string.IsNullOrEmpty(sv.x) ? 0.0f : float.Parse(sv.x);
			v.y = string.IsNullOrEmpty(sv.y) ? 0.0f : float.Parse(sv.y);
			v.z = string.IsNullOrEmpty(sv.z) ? 0.0f : float.Parse(sv.z);
				
			return	v;
		}

		public bool	isNullOrEmpty
		{
			get{
				return	string.IsNullOrEmpty(this.x) || string.IsNullOrEmpty(this.y) || string.IsNullOrEmpty(this.z);
			}
		}
	}



	public	class SUFunctions
	{
		
		/// <summary>
		/// テキストの読み込み
		/// </summary>
		/// <param name="_filePath"></param>
		/// <returns></returns>
		public static string ReadText( string _filePath )
		{
			FileInfo fi = new FileInfo(_filePath);
			string returnSt = "";
	
			try {
				using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8)){
					returnSt = sr.ReadToEnd();
				}
			}
			catch (Exception e){
				Debug.LogError(e.Message);
				return	"";
			}
	
			return returnSt;
		}



		/// <summary>
		/// テキストの書き込み
		/// </summary>
		/// <param name="_filePath"></param>
		/// <param name="_contents"></param>
		public static bool WriteText( string _filePath, string _contents )
		{
			StreamWriter sw;
			sw = new StreamWriter( _filePath, false, Encoding.UTF8 );
			try {
				sw.WriteLine(_contents);
				sw.Close();
			}
			catch( Exception e ) {
				Debug.LogError(e.Message);
				return	false;
			}
		
			return	true;
		}

		public static bool IsEnablePath( string filePath )
		{
			if( string.IsNullOrEmpty(filePath) ) {
				return	false;
			}

			//ファイル名に使用できない文字を取得
			char[] invalidChars = Path.GetInvalidFileNameChars();

			if (filePath.IndexOfAny(invalidChars) >= 0){
				return	false;
			}

			return	true;
		}


		/// <summary>
		///	ノードIDを取得する
		/// </summary>
		/// <param name="strId"></param>
		/// <returns></returns>
		public static int GetNodeId( string strId )
		{
			if( string.IsNullOrEmpty(strId) ) {
				return	-1;
			}

			return	int.Parse( strId ) - 1;		//	１始まりのため
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static float GetFloatValue( string str )
		{
			if( string.IsNullOrEmpty(str) ) {
				return	0.0f;
			}

			return	float.Parse( str );
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="strValue"></param>
		/// <param name="strFormat"></param>
		/// <returns></returns>
		public	static string GetFloatFormatString( string strValue, string strFormat )
		{
			if( string.IsNullOrEmpty(strFormat) ) {
				return	strValue;
			}
			
			float	f;

			if( string.IsNullOrEmpty(strValue) ) {
				return	strValue;
			}
			else {
				f = float.Parse(strValue);
			}

			return	f.ToString(strFormat);
		}
	}
}
