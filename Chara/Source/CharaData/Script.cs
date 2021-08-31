using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;


namespace ScriptEditor
{
	using BD_Brc = BindingDictionary < Branch0 >;
	using BD_EfGn = BindingDictionary < EffectGenerate >;

	//計算状態
	public enum CLC_ST
	{
		CLC_MAINTAIN,	//持続
		CLC_SUBSTITUDE,	//代入
		CLC_ADD,		//加算
	}

	//================================================================
	//	◆スクリプト		キャラにおけるアクションの１フレームの値
	//		┣フレーム数
	//		┣イメージID (Name)
	//		┣画像表示位置
	//		┣速度
	//		┣加速度
	//		┣計算状態(持続/代入/加算)
	//		┣ブランチ[]
	//		┣接触枠[]
	//		┣攻撃枠[]
	//		┣ヒット枠[]
	//		┣攻撃値
	//		┣エフェクト発生[]
	//
	//================================================================
	//[]指定子でintしか扱えないため
	//配列のインデックスでもuintではなくintにする

	public class Script
	{
		//--------------------------------------------------------------------
		//アクション内スクリプトリストにおける自身のフレーム数(番目)
		public int Frame { get; set; } = 0;

		//編集用グループ値
		public int Group { get; set; } = 0;

		//--------------------------------------------------------------------
		//位置
		//--------------------------------------------------------------------
		//キャラ内のイメージリストにおけるインデックス
//		public int ImgIndex { get; set; } = 0;
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
		//ブランチリスト
		public BD_Brc ListBranch { get; set; } = new BD_Brc ();

		//------------------------------------------------
		//枠
		//------------------------------------------------
		//接触枠(Collision), 攻撃枠(Attack), 当り枠(Hit), 相殺枠(Offset)
		public List<Rectangle> ListCRect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListHRect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListARect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListORect { get; set; } = new List<Rectangle> ();

		//------------------------------------------------
		//値
		//------------------------------------------------
		//攻撃値
		public int Power { get; set; } = 0;

		//------------------------------------------------
		//エフェクト生成
		//------------------------------------------------
//		public BindingList<EffectGenerate> ListGenerateEf { get; set; } = new BindingList<EffectGenerate> ();
		public BD_EfGn BD_EfGnrt { get; set; } = new BD_EfGn ();

		//------------------------------------------------
		//強制変更アクション(相手)
		//------------------------------------------------
//		public string ForceActionName { get; set; } = "";
			
		//------------------------------------------------
		//暗転[F]
		//------------------------------------------------
//		public int BlackOut { get; set; } = 0;
			
		//------------------------------------------------
		//振動[F]
		//------------------------------------------------
//		public int Vibration { get; set; } = 0;
			

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
//			this.ImgIndex = s.ImgIndex;
			this.ImgName = s.ImgName;
			this.Pos = s.Pos;
			this.Vel = s.Vel;
			this.Acc = s.Acc;
			this.CalcState = s.CalcState;
			ListBranch = new BD_Brc ( s.ListBranch );
			ListCRect = new List < Rectangle > ( s.ListCRect );
			ListHRect = new List < Rectangle > ( s.ListHRect );
			ListARect = new List < Rectangle > ( s.ListARect );
			ListORect = new List < Rectangle > ( s.ListORect );
			this.Power = s.Power;
			BD_EfGnrt = new BD_EfGn ( s.BD_EfGnrt );
		}

		//初期化
		public void Clear ()
		{
			Frame = 0;
//			ImgIndex = 0;
			ImgName = "Clear";
			CalcState = CLC_ST.CLC_MAINTAIN;
			ListBranch.Clear ();
			ListCRect.Clear ();
			ListARect.Clear ();
			ListHRect.Clear ();
			ListORect.Clear ();
			Power = 0;
			BD_EfGnrt.Clear ();
		}

		//コピー
		public void Copy ( Script s )
		{
			this.Frame = s.Frame;
//			this.ImgIndex = s.ImgIndex;
			this.ImgName = s.ImgName;
			this.Pos = s.Pos;
			this.Vel = s.Vel;
			this.Acc = s.Acc;
			this.CalcState = s.CalcState;
			ListBranch = new BD_Brc ( s.ListBranch );
			ListCRect = new List < Rectangle > ( s.ListCRect );
			ListHRect = new List < Rectangle > ( s.ListHRect );
			ListARect = new List < Rectangle > ( s.ListARect );
			ListORect = new List < Rectangle > ( s.ListORect );
			this.Power = s.Power;
			BD_EfGnrt = new BD_EfGn ( s.BD_EfGnrt );
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
//			if ( this.ImgIndex != s.ImgIndex ) { return false; }
			if ( this.ImgName != s.ImgName ) { return false; }
			if ( this.Pos != s.Pos ) { return false; }
			if ( this.Vel != s.Vel ) { return false; }
			if ( this.Acc != s.Acc ) { return false; }
			if ( this.CalcState != s.CalcState ) { return false; }
			if ( ! this.ListBranch.SequenceEqual ( s.ListBranch ) ) { return false; }
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
//			int i1  = i0  ^ ImgIndex;
			int i2  = i0  ^ ImgName.GetHashCode ();
			int i3  = i2  ^ Pos.GetHashCode ();
			int i4  = i3  ^ Vel.GetHashCode ();
			int i5  = i4  ^ Acc.GetHashCode ();
			int i6  = i5  ^ CalcState.GetHashCode ();
			int i7  = i6  ^ ListBranch.GetHashCode ();
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
