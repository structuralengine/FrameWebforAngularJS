using UnityEngine;
using System.Collections;

namespace SasakouFrame
{
	namespace InputLabel
	{
		public enum InputPanelLabel
		{
			Node,				//	節点
			Element,			//	要素
			Panel,				//	パネル
			Support,			//	支点
			Joint,				//	結合
			MemberSupport,		//	バネ
			AttentionPoint,		//	着目点
			Kazyu,				//	荷重
			Kumiawase,			//	組合せ

			Max,

			None = -1,
		}

		public enum ElementPanelLabel
		{
			BuzaiData,
			GouikiData,
		}

		public enum KazyuPanelLabel
		{
			KazyuName,
			KazyuPower,
		}

		public enum KumiawasePanelLabel
		{
			Define,
			Combine,
			PickUp,
		}
	}


	namespace ValueLabel
	{
		//	属性の部材データの項目番号
		public	enum ElementBuzaiDataValue
		{
			NodeI,					//	節点番号ｉ
			NodeJ,					//	節点番号ｊ
			Length,					//	距離
			Mark,					//	マーク
			BuzaiSu,				//	部材数
			CodeAngle,				//	コードアングル
			YoungRate,				//	ヤング率
			SenDanDnaseiKeisu,		//	せん断弾性係数
			SenBouchouKeisu,		//	線膨張係数

			//	ここからリピート
			DanmenSeki,				//	断面積
			Danmen2MomentZ,			//	断面２次モーメントZ軸まわり
			Danmen2MomentY,			//	断面２次モーメントY軸まわり
			NeziriTeisuu,			//	ねじり定数

			DanmenSekiStart = DanmenSeki,	//	断面積スタート
			DanmenSekiEnd = NeziriTeisuu,	//	断面積エンド
		}

		//	属性の剛域データの項目番号
		public	enum ElementGouikiDataValue
		{
			Length,					//	距離
			Mark,					//	マーク
			BuzaiLengthI,			//	剛域部材長ｉ端側
			BuzaiLengthJ,			//	剛域部材長ｊ端側
			Danmenseki,				//	断面積
			Danmen2Morment,			//	断面積２次モーメント
		}


		//	パネルデータの項目番号
		public	enum PanelDataValue
		{
			KouseiNodeNo0,			//	構成節点番号 0
			KouseiNodeNo1,			//	構成節点番号 1
			KouseiNodeNo2,			//	構成節点番号 2
			KouseiNodeNo3,			//	構成節点番号 3
			Mark,					//	マーク
			YoungRate,				//	ヤング率
			SenDanDnaseiKeisu,		//	せん断弾性係数
			SenBouchouKeisu,		//	線膨張係数

			//	ここからリピート
			DanmenSeki,				//	断面積
			Danmen2Morment,			//	断面積２次モーメント

			DanmenSekiStart = DanmenSeki,		//	断面積スタート
			DanmenSekiEnd	= Danmen2Morment,	//	断面積エンド
		}

		//	支点データの項目番号
		enum SupportDataValue
		{
			NodeNo,				//	要素番号

			//	ここからリピート
			Tx,
			LimitTx,
			Ty,
			LimitTy,
			Tz,
			LimitTz,
			Rx,
			LimitRx,
			Ry,
			LimitRy,
			Rz,
			LimitRz,

			InputStart	= Tx,
			InputEnd	= LimitRz,
		}

		//	結合データの項目番号
		public	enum JointDataValue
		{
			NodeNo,				//	要素番号

			//	ここからリピート
			IPointConditionX,
			IPointConditionY,
			IPointConditionZ,
			JPointConditionX,
			JPointConditionY,
			JPointConditionZ,
			
			InputStart	= IPointConditionX,
			InputEnd	= JPointConditionZ,
		}

		//	バネデータの項目番号
		public	enum MemberSupportDataValue
		{
			NodeNo,				//	要素番号

			//	ここからリピート
			Tx,
			LimitTx,
			Ty,
			LimitTy,
			Tz,
			LimitTz,
			Rx,
			LimitRx,
			Ry,
			LimitRy,
			Rz,
			LimitRz,

			InputStart	= Tx,
			InputEnd	= LimitRz,
		}

		//	着目点の項目番号
		public	enum AttentionPointDataValue
		{
			BuzaiNo,
			BuzaiChou,
			BuzaiName,

			//	ここからリピート
			PointValue,
			PointName,

			InputStart	= PointValue,
			InputEnd	= PointName,
		}

		//	荷重名称データの項目番号
		public enum KazyNameDataValue
		{
			KazyuMark,
			KazyuName,
			Support,
			MemberSupport,
			Danmen,
			Joint,
		}
		
		//	荷重強度データの項目番号
		public	enum KazyuPowerDataValue
		{
			KazyuNo,
			ElementStart,
			ElementEnd,
			ElementMark,
			ElementXYZ,
			ElementL1,
			ElementL2,
			ElementP1,
			ElementP2,
			NodeNo,
			NodePointX,
			NodePointY,
			NodePointZ,
			NodeMormentX,
			NodeMormentY,
			NodeMormentZ,
		}

		//	組合せDefineの項目番号
		public	enum KumiawaseDefineDataValue
		{
			DefNo,

			//	ここからリピート
			CaseNo,

			InputStart	= CaseNo,
			InputEnd	= CaseNo,
		}

		//	組合せCombineの項目番号
		public	enum KumiawaseCombineDataValue
		{
			WarimashiKeisuu,

			//	ここからリピート
			CaseNo,
			KazyuhoseiKeisuu,

			InputStart	= CaseNo,
			InputEnd	= KazyuhoseiKeisuu,

			//	ここから末尾
			Mark,
			KumiawaseKazyuName,
			InputTailStart = Mark,
		}

		//	組合せPickUpの項目番号
		public	enum KumiawasePickUpDataValue
		{
			//	ここからリピート
			KumiawaseNo,
			InputStart	= KumiawaseNo,
			InputEnd	= KumiawaseNo,

			//	ここから末尾
			Mark,
			Name,
			InputTailStart = Mark,
		}
	}
}
