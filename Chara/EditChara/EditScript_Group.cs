using System.Collections.Generic;

namespace ScriptEditor
{
	using LScp = List < Script >;

	//---------------------------------------------------------------------
	//グループ（スクリプトの参照リスト）
	//ScriptがグループIDを持つ
	//Sequenceが変更されるたびに編集用リストを再構築する
	//0(グループ外)も含め、グループ成立しているIDを記録する
	//---------------------------------------------------------------------
	public partial class EditScript
	{
		//対象スクリプトリスト(シークエンス内)
		public LScp L_Scp { get; set; } = new LScp ();

		//グループリスト
		public List < LScp > L_ScriptGroup { get; set; } = new List<LScp> ();

		//選択中グループ
		LScp SelectedGroup = new LScp ();
		public int SelectedGroupIndex { get; set; } = 0;


		//選択中のグループを再構築
		public void Restruct ( LScp lsScp, int frame )
		{
			//対象シークエンス内のスクリプト
			L_Scp = lsScp;

			//グループ個数確認
			int gn = 0;	//グループ種類数
			List < int > L_groupID = new List<int> ();	//グループIDの保存
			foreach ( Script s in lsScp )
			{
				if ( ! L_groupID.Exists ( i=>i==s.Group ) )
				{
					L_groupID .Add ( s.Group );
					++ gn;
				}
			}

			//グループ種類数で初期化(最大はスクリプト数)
			L_ScriptGroup = new List<LScp> ( gn );
			for ( int i = 0; i < gn; ++ i )
			{
				L_ScriptGroup.Add ( new LScp () );
			}

			//スクリプトの値から各グループに追加する(0を含む)
			foreach ( Script s in lsScp )
			{
				int i = L_groupID.IndexOf ( s.Group );
				L_ScriptGroup [ i ].Add ( s );
			}

			//選択
			if ( frame >= L_Scp.Count ) {  return; }
			SelectGroup ( L_Scp[frame].Group );
		}
		

		//グループリストから対象スクリプトリストを再構築する
		public void RestructListScript ()
		{
			L_Scp.Clear ();
			foreach ( LScp lScp in L_ScriptGroup )
			{
				foreach ( Script s in lScp )
				{ 
					L_Scp.Add ( s );
				}
			}
		}


		//選択範囲のスクリプトをグループにする
		public void MakeGroup ( LScp lsScp, int start, int end )
		{
			//チェック
			int count = 0;
			foreach ( Script s in lsScp )
			{
				//選択範囲のスクリプトをチェック
				if ( start <= count && count <= end )
				{
					//すでにグループになっているスクリプトが１つでもあれば中止
					if ( 0 != s.Group ) { return; }
				}
				++ count;
			}

			//未使用グループ値を検索
			int unusedGroup = GetUnusedIndex ( lsScp );

			//選択範囲のスクリプトをグループにする
			count = 0;
			foreach ( Script s in lsScp )
			{
				if ( start <= count && count <= end )
				{
					s.Group = unusedGroup;
				}
				++ count;
			}

			//再構築
			Restruct ( lsScp, start );
		}

		//０以外の未使用グループ値を検索
		public int GetUnusedIndex ( LScp lsScp )
		{
			int unusedGroup = 0;

			//リストのグループIDを記録
			List < int > L_Grp = new List<int> ();
			for ( int i = 0; i < lsScp.Count; ++ i )
			{
				L_Grp.Add ( lsScp [i].Group );
			}

			L_Grp.Sort();	//ソート

			//＋１ずつ比較
			for ( int i = 0; i < L_Grp.Count; ++ i )
			{ 
				int targetGroup = L_Grp [ i ];

				//グループ０は飛ばす
				if ( 0 == targetGroup ) { continue; }

				//同じときは続行
				if ( unusedGroup == targetGroup )
				{
					continue;
				}
				else
				{
					if (unusedGroup + 1 != targetGroup )
					//ソート済みであるから、unusedGroupと、 その+1 とも等しくないなら
					//　+1 の値は未使用
					{
						return 1 + unusedGroup;
					}

					//対象が＋１と同一ならチェック数値も＋１して続行
					++ unusedGroup;
				}
			}

			//対象がすべて連番だったとき、+1した値を返す
			return 1 + unusedGroup;
		}
		public int GetUnusedIndex ()
		{
			return GetUnusedIndex ( L_Scp );
		}

		//グループの選択
		public void SelectGroup ( int group )
		{
			if ( group > L_ScriptGroup.Count ) { return; }

			//値の記録
			SelectedGroupIndex = group;

			//該当リストの記録
			foreach ( LScp ls in L_ScriptGroup )
			{
				foreach ( Script s in ls )
				{
					if ( s.Group == group )
					{
						SelectedGroup = ls;
						return;
					}
				}
			}
		}


		//グループに選択スクリプトの内容のみを複製
		// 自分のフレーム数は残す
		public void PasteGroup ( Script scp )
		{
			//グループ０のときは何もしない
			if ( 0 == scp.Group ) { return; }

			//該当グループを取得
			LScp targetLs = null;
			foreach ( LScp ls in L_ScriptGroup )
			{
				if ( 0 >= ls.Count ) { continue; }
				
				if ( scp.Group == ls[0].Group )
				{
					targetLs = ls;
					break;
				}
			}
			if ( targetLs is null ) { return; }

			//値をコピー
			foreach ( Script s in targetLs )
			{
				//自分以外
				if ( s != scp )
				{
					//s.Copy ( scp );
					//フレーム数以外コピー
					s.Copy_Other_than_frame ( scp );
				}
			}
		}


		//対象グループを解除
		public void DismantleGroup ( LScp lsScp, int group_id )
		{
			foreach ( Script s in lsScp )
			{
				if ( group_id == s.Group )
				{
					s.Group = 0;
				}
			}
		}

		//全グループを解除
		public void DismantleAllGroup ()
		{
			foreach ( Script s in L_Scp )
			{
				s.Group = 0;
			}
		}

		//--------------------------------------------------------------------
		//グループのスクリプト操作
		//各グループに＋１
		public void GroupAdd ()
		{
			//グループのリストでループする
			foreach ( LScp lScp in L_ScriptGroup )
			{
				if ( lScp.Count <= 0 ) { continue; }
				if ( lScp [0].Group == 0 ) { continue; }

				//フレーム数以外コピー
				Script script = new Script ();
				script.Copy_Other_than_frame ( lScp [ 0 ] );
				lScp.Add ( script );
			}

			//グループリストから対象リストを再構築
			RestructListScript ();
		}

		//各グループにー１
		public void GroupDel ()
		{
			//グループのリストでループする
			foreach ( LScp lScp in L_ScriptGroup )
			{
				//1個の場合は飛ばす
				if ( lScp.Count <= 1 ) { continue; }
				if ( lScp [0].Group == 0 ) { continue; }

				lScp.RemoveAt ( 0 );
			}

			//グループリストから対象リストを再構築
			RestructListScript ();
		}


		//---------------------------------------------------------------------
		// スクリプト グループに対し編集をする
		//---------------------------------------------------------------------
		//グループ内スクリプトにセッタと値を反映
		public void DoSetterInGroup_T < T > ( System.Action < Script, T > Setter, T t )
		{
			foreach ( Script s in SelectedGroup ) { Setter ( s, t ); }
		}

		//グループ内スクリプトに対して処理
		public void DoGroup ( System.Action < Script > Func )
		{
			foreach ( Script s in SelectedGroup ) { Func ( s ); }
		}
	}
}
