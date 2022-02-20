using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace ScriptEditor
{
	using BD_EfGn = BindingDictionary < EffectGenerate >;

	//================================================================
	//	◆スクリプト		キャラにおけるアクションの１フレームの値
	//		┣フレーム数
	//		┣(グループ) ScriptEditor内での編集用
	//		┣イメージID (Name)
	//		┣画像表示位置	┣速度	┣加速度
	//		┣計算状態(持続/代入/加算)
	//		┣[]ルート
	//		┣[]接触枠	┣[]攻撃枠	┣[]防御枠	┣[]相殺枠
	//		┣[]エフェクト発生
	//		┣攻撃値
	//		┣暗転	┣振動
	//================================================================

	//スクリプト項目追加
	// class Script
	// IOChara (ScriptEditor)
	// IOChara (GameMain)
	// GameMain

	public class Script
	{
		//--------------------------------------------------------------------
		//アクション内スクリプトリストにおける自身のフレーム数(番目)
		public int Frame { get; set; } = 0;

		//編集用グループ値 (0はグループ無し、１からグループ生成)
		public int Group { get; set; } = 0;

		//--------------------------------------------------------------------
		//位置
		//--------------------------------------------------------------------
		//キャラ内のイメージリストにおけるイメージ名
		public string ImgName { get; set; } = "ImgName";

		//--------------------------------------------------------------------
		//位置
		//--------------------------------------------------------------------
		public Point Pos { get; set; } = new Point ( 0, 0 );
		public void SetPos ( int x, int y ) { Pos = new Point ( x, y ); }
		public void SetPosX ( int x ) { Pos = new Point ( x, Pos.Y ); }
		public void SetPosY ( int y ) { Pos = new Point ( Pos.X, y ); }

		public Point Vel { get; set; } = new Point ( 0, 0 );
		public void SetVel ( int x, int y ) { Vel = new Point ( x, y ); }
		public void SetVelX ( int x ) { Vel = new Point ( x, Vel.Y ); }
		public void SetVelY ( int y ) { Vel = new Point ( Vel.X, y ); }

		public Point Acc { get; set; } = new Point ( 0, 0 );
		public void SetAcc ( int x, int y ) { Acc = new Point ( x, y ); }
		public void SetAccX ( int x ) { Acc = new Point ( x, Acc.Y ); }
		public void SetAccY ( int y ) { Acc = new Point ( Acc.X, y ); }

		//計算状態(加算/代入/持続)
		public CLC_ST CalcState { get; set; } = new CLC_ST ();

		//------------------------------------------------
		//スクリプト分岐
		//------------------------------------------------

		//ルートネームリスト
		public BindingDictionary < TName > BD_RutName = new BindingDictionary <TName> ();

		//------------------------------------------------
		//枠
		//------------------------------------------------
		//接触枠(Collision), 攻撃枠(Attack), 当り枠(Hit), 相殺枠(Offset)
		public List<Rectangle> ListCRect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListHRect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListARect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListORect { get; set; } = new List<Rectangle> ();

		//------------------------------------------------
		//エフェクト生成
		//------------------------------------------------
		public BD_EfGn BD_EfGnrt { get; set; } = new BD_EfGn ();

		//------------------------------------------------
		//値
		//------------------------------------------------
		public int Power { get; set; } = 0;			//攻撃値
		public int BlackOut { get; set; } = 0;		//暗転[F]
		public int Vibration { get; set; } = 0;		//振動[F](全体)
		public int Stop { get; set; } = 0;			//停止[F](全体)

		//------
		public int Radian { get; set; } = 0;			//回転
		public int AfterImage_pitch { get; set; } = 0;	//残像[F] pitch
		public int AfterImage_N { get; set; } = 0;		//残像[個]
		public int AfterImage_time { get; set; } = 0;	//残像[F] 持続
		public int Vibration_S { get; set; } = 0;		//振動[F](個別)
		public Color Color { get; set; } = new Color ();	//色調変更
		public int Color_time { get; set; } = 0;			//色調変更[F] 持続


		//================================================================
		//コンストラクタ
		public Script ()
		{
			CalcState = CLC_ST.CLC_MAINTAIN;
		}

		//コピーコンストラクタ
		public Script ( Script s )
		{
			this.Frame = s.Frame;
			this.ImgName = s.ImgName;
			this.Pos = s.Pos;
			this.Vel = s.Vel;
			this.Acc = s.Acc;
			this.CalcState = s.CalcState;

			this.BD_RutName = new BindingDictionary<TName> ();
			BD_RutName.DeepCopy ( s.BD_RutName );
			
			this.ListCRect = new List < Rectangle > ( s.ListCRect );
			this.ListHRect = new List < Rectangle > ( s.ListHRect );
			this.ListARect = new List < Rectangle > ( s.ListARect );
			this.ListORect = new List < Rectangle > ( s.ListORect );
			this.Power = s.Power;
			this.BD_EfGnrt = new BD_EfGn ( s.BD_EfGnrt );
			this.BlackOut = s.BlackOut;
			this.Vibration = s.Vibration;
		}

		//初期化
		public void Clear ()
		{
			Frame = 0;
			ImgName = "Clear";
			CalcState = CLC_ST.CLC_MAINTAIN;
			BD_RutName.Clear ();
			ListCRect.Clear ();
			ListARect.Clear ();
			ListHRect.Clear ();
			ListORect.Clear ();
			Power = 0;
			BD_EfGnrt.Clear ();
			BlackOut = 0;
			Vibration = 0;
		}

		//コピー
		public void Copy ( Script s )
		{
			this.Frame = s.Frame;
			this.ImgName = s.ImgName;
			this.Pos = s.Pos;
			this.Vel = s.Vel;
			this.Acc = s.Acc;
			this.CalcState = s.CalcState;
			this.BD_RutName.DeepCopy ( s.BD_RutName );
			this.ListCRect = new List < Rectangle > ( s.ListCRect );
			this.ListHRect = new List < Rectangle > ( s.ListHRect );
			this.ListARect = new List < Rectangle > ( s.ListARect );
			this.ListORect = new List < Rectangle > ( s.ListORect );
			this.Power = s.Power;
			this.BD_EfGnrt = new BD_EfGn ( s.BD_EfGnrt );
			this.BlackOut = s.BlackOut;
			this.Vibration = s.Vibration;
		}


		//同値比較
		public override bool Equals ( object obj )
		{
			//(Object)型で比較する
			//nullまたは型が異なるときfalse
			if ( null == obj || this.GetType () != obj.GetType () )
			{
				return false;
			}
			
			//キャストして各値を比較する
			Script s = (Script)obj;

			if ( this.Frame != s.Frame ) { return false; }
			if ( this.ImgName != s.ImgName ) { return false; }
			if ( this.Pos != s.Pos ) { return false; }
			if ( this.Vel != s.Vel ) { return false; }
			if ( this.Acc != s.Acc ) { return false; }
			if ( this.CalcState != s.CalcState ) { return false; }
			if ( ! this.BD_RutName.SequenceEqual ( s.BD_RutName ) ) { return false; }
			if ( ! this.ListCRect.SequenceEqual ( s.ListCRect ) ) { return false; }
			if ( ! this.ListHRect.SequenceEqual ( s.ListHRect ) ) { return false; }
			if ( ! this.ListARect.SequenceEqual ( s.ListARect ) ) { return false; }
			if ( ! this.ListORect.SequenceEqual ( s.ListORect ) ) { return false; }
			if ( this.Power != s.Power ) { return false; }
			if ( ! this.BD_EfGnrt.SequenceEqual ( s.BD_EfGnrt ) ) { return false; }

			return true;
		}

		//Equals用ハッシュコード
		public override int GetHashCode ()
		{
			int i0  = Frame;
			int i2  = i0  ^ ImgName.GetHashCode ();
			int i3  = i2  ^ Pos.GetHashCode ();
			int i4  = i3  ^ Vel.GetHashCode ();
			int i5  = i4  ^ Acc.GetHashCode ();
			int i6  = i5  ^ CalcState.GetHashCode ();
			int i7  = i6  ^ BD_RutName.GetHashCode ();
			int i8  = i7  ^ ListCRect.GetHashCode ();
			int i9  = i8  ^ ListHRect.GetHashCode ();
			int i10 = i9  ^ ListARect.GetHashCode ();
			int i11 = i10 ^ ListORect.GetHashCode ();
			int i12 = i11 ^ ListORect.GetHashCode ();
			int i13 = i12 ^ ListORect.GetHashCode ();
			int i14 = i13 ^ ListORect.GetHashCode ();

			return i14;

			//匿名クラスのハッシュ値を返す
//			return new { }.GetHashCode ();
		}
	}

}
