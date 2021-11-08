namespace ScriptEditor
{
	//==================================================================================
	//	キャラ データ ver110
	//==================================================================================

	//==================================================================================
	//	◆キャラ	
	//		┣ビヘイビア  ( [] アクション )
	//		┣ガーニッシュ( [] エフェクト )
	//		┣[] コマンド
	//		┣[] ブランチ
	//		┣[] ルート
	//
	//	イメージリストはコンペンド(キャラのビヘイビアとガーニッシュ)が持つように変更
	//
	//[]指定子でintしか扱えないため
	//配列のインデックスでもuintではなくintにする
	//==================================================================================
	using BD_Cmd = BindingDictionary < Command >;
	using BD_Brc = BindingDictionary < Branch >;
	using BD_Rut= BindingDictionary < Route >;

	public class Chara
	{
		//---------------------------------------------------------------------
		//Compend(一覧)継承のアクションとエフェクト
		public Behavior behavior = new Behavior ();		//アクションの集合
		public Garnish garnish = new Garnish ();        //エフェクトの集合

		public BD_Cmd BD_Command { get; } = new BD_Cmd ();		//コマンドリスト
		public BD_Brc BD_Branch { get; } = new BD_Brc ();		//ブランチリスト

		//---------------------------------------------------------------------
		//エディタ 一時変数
		public BD_Rut BD_Route { get; } = new BD_Rut ();		//ルートリスト

		//---------------------------------------------------------------------

		//@todo 基本状態アクションIDをアクションリストの先頭から固定番号配置にする
		//		-> 基本状態アクションID項目の削除
		//		別位置で順番を定義する必要がある
#if false
		//---------------------------------------------------------------------
		//基本状態アクションID
		public enum BasicAction
		{
			STAND, POISED, CLANG, AVOID, DOTTY, DAMAGED, THROWN, DOWN, WIN
		};
		public RefInt[] BsAction { get; set; }
#endif

		//---------------------------------------------------------------------
		//コンストラクタ
		public Chara ()
		{
			//空(null)状態を許容する
			//ダミーを用いない
			//各生成、受取時に１つ以上あるかチェックする
#if false
			//基本状態アクションID
			BsAction = new RefInt[ Enum.GetNames ( typeof ( BasicAction ) ).Length ];
			int index = 0;
			foreach ( RefInt refi in BsAction )
			{
				BsAction [ index ++ ] = new RefInt ( 0 );
			}
#endif
		}

		//コピーコンストラクタ
		public Chara ( Chara srcChara )
		{
			Clear ();

			behavior.Copy ( srcChara.behavior );
			garnish.Copy ( srcChara.garnish );

			BD_Command.Copy ( srcChara.BD_Command );
			BD_Branch.Copy ( srcChara.BD_Branch );
			BD_Route.Copy ( srcChara.BD_Route );
		}

		//クリア
		//	すべてのデータについてメモリを解放し、個数を０にする
		public void Clear ()
		{
			behavior.Clear ();
			garnish.Clear ();
			BD_Command.Clear ();
			BD_Branch.Clear ();
			BD_Route.Clear ();

#if false
			//個数は不変かつ、RefIntは即値のみなのでメモリは解放しない
			foreach ( RefInt refi in BsAction )
			{
				refi.i = 0;
			}
#endif
		}

		//イメージリスト(メイン,Ef)を残しコンペンドからスクリプトだけをクリア
		public void ClearScript ()
		{
			behavior.Clear ();
			garnish.Clear ();
			BD_Command.Clear ();
			BD_Branch.Clear ();
			BD_Route.Clear ();
		}

		//コピー
		public void Copy ( Chara ch )
		{
			Clear ();

			behavior.Copy ( ch.behavior );
			garnish.Copy ( ch.garnish );

			BD_Command.Copy ( ch.BD_Command );
			BD_Branch.Copy ( ch.BD_Branch );
			BD_Route.Copy ( ch.BD_Route );

#if false
			//個数は不変、RefIntの即値だけコピー
			int index = 0;
			foreach ( RefInt refi in BsAction )
			{
				refi.i = chara.BsAction[ index++ ].i;
			}
#endif
		}


	}
}
