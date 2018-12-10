using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemUtility;



namespace FrameWebforJS
{
	/// <summary>
	/// 変位量データ
	/// </summary>
	public partial class DisgData
    {
        public double dx = 0.0;
        public double dy = 0.0;
        public double dz = 0.0;
        public double rx = 0.0;
        public double ry = 0.0;
        public double rz = 0.0;
        public DisgData(double _dx, double _dy, double _dz, double _rx, double _ry, double _rz)
        {
            this.dx = _dx;
            this.dy = _dy;
            this.dz = _dz;
            this.rx = _rx;
            this.ry = _ry;
            this.rz = _rz;
        }
    }

	public class DisgDataManager : Dictionary<int, DisgData>
	{
		/// <summary>
		/// 変位量データを読み込む
		/// </summary>
		/// <param name="objJson"></param>
		public void SetData(Dictionary<string, object> objJson)
		{
			try { 
				string key_id = "1";

				if( objJson.ContainsKey(key_id))
				{
					Dictionary<string, object> test_data = objJson[key_id] as Dictionary<string, object>;

					if (test_data.ContainsKey("disg"))
					{
						this.Clear();

						Dictionary<string, object> disg1 = test_data["disg"] as Dictionary<string, object>;
						foreach (string key in disg1.Keys)
						{
							try
							{
								Dictionary<string, object> disg2 = disg1[key] as Dictionary<string, object>;

								int id = int.Parse(key);

								if (this.ContainsKey(id))
								{
									double dx = ComonFunctions.ConvertToDouble(disg2["dx"]);
									double dy = ComonFunctions.ConvertToDouble(disg2["dy"]);
									double dz = ComonFunctions.ConvertToDouble(disg2["dz"]);
									double rx = ComonFunctions.ConvertToDouble(disg2["rx"]);
									double ry = ComonFunctions.ConvertToDouble(disg2["ry"]);
									double rz = ComonFunctions.ConvertToDouble(disg2["rz"]);

									DisgData ex = new DisgData(dx, dy, dz, rx, ry, rz);
									this.Add(id, ex);
								}
							}
							catch
							{
								continue;
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Debug.Log("Error!! at webframe SetDisgData");
				Debug.Log(e.Message);
			}
		}
	}
}
