﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ScriptEditor
{
	using BD_Tn = BindingDictionary < TName >;
	using BD_EfGn = BindingDictionary < EffectGenerate >;
	using LsRect = List < Rectangle >;

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
	//		┣パラメータ(戦闘)
	//		┣パラメータ(演出)
	//================================================================

	//スクリプト項目に追加するとき以下を更新
	// class Script
	// IOChara (ScriptEditor)
	// IOChara (GameMain)
	// GameMain

	[Serializable]
	public class Script
	{
		//--------------------------------------------------------------------
		//	ID
		//--------------------------------------------------------------------
		//シークエンス内スクリプトリストにおける自身のフレーム数(番目)
		public int Frame { get; set; } = 0;

		//編集用グループ値 (0はグループ無し、１からグループ生成)
		public int Group { get; set; } = 0;

		//--------------------------------------------------------------------
		//表示
		//--------------------------------------------------------------------
		//表示画像 名前指定
		public string ImgName { get; set; } = "ImgName";

		//--------------------------------------------------------------------
		//位置
		//--------------------------------------------------------------------
		//(基準位置からの画像表示補正位置)
		public Point Pos { get; set; } = new Point ( -250, -500 );
		public void SetPos ( int x, int y ) { Pos = new Point ( x, y ); }
		public void SetPosX ( int x ) { Pos = new Point ( x, Pos.Y ); }
		public void SetPosY ( int y ) { Pos = new Point ( Pos.X, y ); }

		//------------------------------------------------
		//ルートネームリスト (スクリプト分岐)
		//------------------------------------------------
		public BD_Tn BD_RutName = new BD_Tn ();

		//------------------------------------------------
		//枠
		//------------------------------------------------
		//接触枠(Collision), 攻撃枠(Attack), 当り枠(Hit), 相殺枠(Offset)
		//ScriptEditor と ゲームメイン では上限は定数(ConshChara.NumRect = 8)
		public LsRect ListCRect { get; set; } = new LsRect ();
		public LsRect ListHRect { get; set; } = new LsRect ();
		public LsRect ListARect { get; set; } = new LsRect ();
		public LsRect ListORect { get; set; } = new LsRect ();

		//------------------------------------------------
		//エフェクト生成
		//------------------------------------------------
		public BD_EfGn BD_EfGnrt { get; set; } = new BD_EfGn ();

		//------------------------------------------------
		//値
		//------------------------------------------------
		public ScriptParam_Battle Param_Btl = new ScriptParam_Battle ();
		public ScriptParam_Staging Param_Ef = new ScriptParam_Staging ();


		//================================================================
		//コンストラクタ
		public Script ()
		{
			ListCRect.Add ( new Rectangle ( -50, -350, 120, 350 ) );
			ListHRect.Add ( new Rectangle ( -60, -360, 130, 370 ) );
		}

		//コピーコンストラクタ
		public Script ( Script s )
		{
			this.Frame = s.Frame;
			this.Group = s.Group;
			this.ImgName = s.ImgName;
			this.Pos = s.Pos;

			this.BD_RutName = new BindingDictionary<TName> ();
			BD_RutName.DeepCopy ( s.BD_RutName );
			
			this.ListCRect = new List < Rectangle > ( s.ListCRect );
			this.ListHRect = new List < Rectangle > ( s.ListHRect );
			this.ListARect = new List < Rectangle > ( s.ListARect );
			this.ListORect = new List < Rectangle > ( s.ListORect );
			this.BD_EfGnrt = new BD_EfGn ( s.BD_EfGnrt );

			this.Param_Btl = new ScriptParam_Battle ( s.Param_Btl );
			this.Param_Ef = new ScriptParam_Staging ( s.Param_Ef );
		}

		//初期化
		public void Clear ()
		{
			Frame = 0;
			Group = 0;
			ImgName = "Clear";
			Pos = new Point ();

			BD_RutName.Clear ();
			ListCRect.Clear ();
			ListARect.Clear ();
			ListHRect.Clear ();
			ListORect.Clear ();

			BD_EfGnrt.Clear ();

			Param_Btl.Clear ();
			Param_Ef.Clear ();
		}

		//コピー
		public void Copy ( Script s )
		{
			this.Frame = s.Frame;
			this.Group = s.Group;
			this.ImgName = s.ImgName;
			this.Pos = s.Pos;
			this.BD_RutName.DeepCopy ( s.BD_RutName );
			this.ListCRect = new List < Rectangle > ( s.ListCRect );
			this.ListHRect = new List < Rectangle > ( s.ListHRect );
			this.ListARect = new List < Rectangle > ( s.ListARect );
			this.ListORect = new List < Rectangle > ( s.ListORect );
			this.BD_EfGnrt = new BD_EfGn ( s.BD_EfGnrt );

			this.Param_Btl.Copy ( s.Param_Btl );
			this.Param_Ef.Copy ( s.Param_Ef );
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
			if ( this.Group != s.Group ) { return false; }
			if ( this.ImgName != s.ImgName ) { return false; }
			if ( this.Pos != s.Pos ) { return false; }
			if ( ! this.BD_RutName.SequenceEqual ( s.BD_RutName ) ) { return false; }
			if ( ! this.ListCRect.SequenceEqual ( s.ListCRect ) ) { return false; }
			if ( ! this.ListHRect.SequenceEqual ( s.ListHRect ) ) { return false; }
			if ( ! this.ListARect.SequenceEqual ( s.ListARect ) ) { return false; }
			if ( ! this.ListORect.SequenceEqual ( s.ListORect ) ) { return false; }
			if ( ! this.BD_EfGnrt.SequenceEqual ( s.BD_EfGnrt ) ) { return false; }

			return true;
		}

		//Equals用ハッシュコード
		public override int GetHashCode ()
		{
			int i0  = Frame;
			int i1  = i0  ^ Group;
			int i2  = i1  ^ ImgName.GetHashCode ();
			int i3  = i2  ^ Pos.GetHashCode ();
			int i7  = i3  ^ BD_RutName.GetHashCode ();
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
