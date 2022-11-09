using System;
using System.Drawing;

namespace ScriptEditor
{
	//スクリプトにおけるパラメータ(演出)
	//================================================================
	//	◆スクリプト		キャラにおけるアクションの１フレームの値
	//		┣暗転	┣全体振動	┣全体停止
	//		┣回転	┣残像間隔	┣残像個数	┣残像持続
	//		┣個別振動	┣色調変更	┣色調変更持続
	//================================================================
	public class ScriptParam_Staging
	{
		//演出(全体)
		public int BlackOut { get; set; } = 0;		//暗転[F]
		public int Vibration { get; set; } = 0;		//振動[F](全体)
		public int Stop { get; set; } = 0;			//停止[F](全体)

		//------
		//演出(個別)
		public int Radian { get; set; } = 0;		//回転
		public int AfterImage_N { get; set; } = 0;		//残像[個]
		public int AfterImage_time { get; set; } = 0;	//残像[F] 持続
		public int AfterImage_pitch { get; set; } = 0;	//残像[F] pitch
		public int Vibration_S { get; set; } = 0;		//振動[F](個別)
		public Color Color { get; set; } = new Color ();	//色調変更
		public int Color_time { get; set; } = 0;			//色調変更[F] 持続

		//================================================================

		//コンストラクタ
		public ScriptParam_Staging ()
		{
		}

		//コピーコンストラクタ
		public ScriptParam_Staging ( ScriptParam_Staging src )
		{
			this.BlackOut = src.BlackOut;
			this.Vibration = src.Vibration;
			this.Stop = src.Stop;

			this.Radian = src.Radian;
			this.AfterImage_pitch = src.AfterImage_pitch;
			this.AfterImage_N = src.AfterImage_N;
			this.AfterImage_time = src.AfterImage_time;
			this.Vibration_S = src.Vibration_S;
			this.Color = src.Color;
			this.Color_time = src.Color_time;
		}

		//初期化
		public void Clear ()
		{
			BlackOut = 0;
			Vibration = 0;
			Stop = 0;

			Radian = 0;
			AfterImage_pitch = 0;
			AfterImage_N = 0;
			AfterImage_time = 0;
			Vibration_S = 0;
			Color = new Color ();
			Color_time = 0;
		}

		//コピー
		public void Copy ( ScriptParam_Staging src )
		{
			this.BlackOut = src.BlackOut;
			this.Vibration = src.Vibration;
			this.Stop = src.Stop;

			this.Radian = src.Radian;
			this.AfterImage_pitch = src.AfterImage_pitch;
			this.AfterImage_N = src.AfterImage_N;
			this.AfterImage_time = src.AfterImage_time;
			this.Vibration_S = src.Vibration_S;
			this.Color = src.Color;
			this.Color_time = src.Color_time;
		}
	}
}
