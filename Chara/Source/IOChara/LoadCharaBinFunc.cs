using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;


namespace ScriptEditor
{
	using GK_L = GameKeyData.Lever;
	using GK_B = GameKeyData.Button;
	using GK_ST = GameKeyData.GameKeyState;


	//==================================================================================
	//	LoadCharaBin
	//		キャラデータをbinaryで.datファイルから読込
	//		Document形式を介さない
	//==================================================================================
	public partial class LoadCharaBin
	{
		//ビヘイビア
		private void LoadBinBehavior ( BinaryReader br, Chara chara )
		{
			//Action : Sequence
			Behavior bhv = chara.behavior;

			//アクション個数
			uint N_Act = br.ReadUInt32 ();

			for ( uint i = 0; i < N_Act; ++ i )
			{
				Action act = new Action ()
				{
					Name = br.ReadString (),
					NextActionName = "Act_" + br.ReadUInt32 ().ToString(),	//uintから後に名前に変換する
					Category = (ActionCategory) br.ReadByte (),
					Posture = (ActionPosture) br.ReadByte (),
					HitNum = br.ReadByte (),
					HitPitch = br.ReadByte (),
					Balance = br.ReadInt32 (),
				};

				//[]スクリプト
				LoadBinListScript ( br, act.ListScript );

				bhv.BD_Sequence.Add ( act );
			}

			//すべてのアクションを読み込んでから、
			//"次アクション名" をIDからstringとして得る
			foreach ( Action act in chara.behavior.BD_Sequence.GetEnumerable () )
			{
				int nextActionID = GetIndex ( act.NextActionName, "Act_" );
				act.NextActionName = chara.behavior[ nextActionID ].Name;
			}
		}

		//ガーニッシュ
		private void LoadBinGarnish ( BinaryReader br, Chara chara )
		{
			//Effect : Sequence
			Garnish gns = chara.garnish;

			//エフェクト個数
			uint N_Efc = br.ReadUInt32 ();

			for ( uint i = 0; i < N_Efc; ++ i )
			{
				Effect efc = new Effect ()
				{
					Name = br.ReadString (),
				};

				//[]スクリプト
				LoadBinListScript ( br, efc.ListScript );

				gns.BD_Sequence.Add ( efc );
			}
		}

		//スクリプトリスト
		private void LoadBinListScript ( BinaryReader br, List<Script> lscp )
		{
			//スクリプト個数
			uint N_Scp = br.ReadUInt32 ();
			
			for ( uint i = 0; i < N_Scp; ++ i )
			{
				Script scp = new Script ()
				{
					ImgName = "Img_" + br.ReadUInt32 ().ToString (),	//後にイメージ名に変換
					Pos = new Point ( br.ReadInt32 (), br.ReadInt32 () ),
				};

				//ルート名リスト
				uint nRut = br.ReadUInt32 ();
				for ( uint iRut = 0; iRut < nRut; ++ iRut )
				{
					//後にルート名に変換
					scp.BD_RutName.Add ( new TName ( "Rut_" + br.ReadUInt32 ().ToString () ) );
				}

				//枠リスト
				LoadBinListRect ( br, scp.ListCRect );
				LoadBinListRect ( br, scp.ListHRect );
				LoadBinListRect ( br, scp.ListARect );
				LoadBinListRect ( br, scp.ListORect );

				//エフェクト生成
				uint nEfGnrt = br.ReadUInt32 ();	//個数[uint]
				for ( uint iEG = 0; iEG < nEfGnrt; ++ iEG )
				{
					EffectGenerate efgnrt = new EffectGenerate ()
					{ 
						EfName = "Ef_" + br.ReadUInt32 ().ToString(),
						Pt = new Point ( br.ReadInt32 (), br.ReadInt32 () ),
						Z_PER100F = br.ReadInt32 (),
						Gnrt = br.ReadBoolean (),
						Loop = br.ReadBoolean (),
						Sync = br.ReadBoolean (),
					};
					scp.BD_EfGnrt.Add ( efgnrt );
				}

				//バトルパラメータ
				ScriptParam_Battle btlPrm = new ScriptParam_Battle ()
				{
					CalcState = (CLC_ST)br.ReadInt32 (),
					Vel = new Point ( br.ReadInt32(), br.ReadInt32() ),
					Acc = new Point ( br.ReadInt32(), br.ReadInt32() ),
					Power = br.ReadInt32 (),
					Warp = br.ReadInt32 (),
					Recoil_I = br.ReadInt32 (),
					Recoil_E = br.ReadInt32 (),
					Blance_I = br.ReadInt32 (),
					Blance_E = br.ReadInt32 (),
				};
				scp.BtlPrm = btlPrm;

				//ステージングパラメータ
				ScriptParam_Staging stgPrm = new ScriptParam_Staging ()
				{
					BlackOut = br.ReadByte (),
					Vibration = br.ReadByte (),
					Stop = br.ReadByte (),
					Radian = br.ReadInt32 (),
					AfterImage_N = br.ReadByte (),
					AfterImage_time = br.ReadByte (),
					AfterImage_pitch = br.ReadByte (),
					Vibration_S = br.ReadByte (),
					Color = Color.FromArgb ( (int) br.ReadUInt32 () ),
					Color_time = br.ReadByte (),
				};

				lscp.Add ( scp );
			}
		}

		//枠リスト
		private void LoadBinListRect ( BinaryReader br, List < Rectangle > lrct )
		{
			int N_Rct = br.ReadByte ();
			for ( int i = 0; i < N_Rct; ++ i )
			{
				int left = br.ReadInt32 ();
				int top = br.ReadInt32 ();
				int right = br.ReadInt32 ();
				int bottom = br.ReadInt32 ();
				Rectangle r = new Rectangle ( left, top, right - left, bottom - top );

				lrct.Add ( r );
			}
		}


		//--------------------------------------------------------------
		//コマンド
		private void LoadBinCommand ( BinaryReader br, Chara chara )
		{
			uint N = br.ReadUInt32 ();
			for ( uint i = 0; i < N; ++ i )
			{
				Command cmd = new Command ()
				{
					Name = br.ReadString (),
					LimitTime = br.ReadByte (),
				};

				//ゲームキー
				int N_Key = br.ReadByte ();

				for ( int iKey = 0; iKey < N_Key; ++ iKey )
				{
					GameKeyCommand gkc = new GameKeyCommand ()
					{
						Not = br.ReadBoolean (),
					};

					//レバー
					foreach ( GK_L gkl in Enum.GetValues ( typeof(GK_L) ) )
					{
						if ( gkl == GK_L.LVR_N ) { continue; }	//ゲームキー保存はしないので飛ばす
						gkc.DctLvrSt [ gkl ] = (GK_ST)br.ReadByte ();
					}

					//ボタン
					foreach ( GK_B gkb in Enum.GetValues ( typeof(GK_B) ) )
					{
						if ( gkb == GK_B.BTN_N ) { continue; }	//ゲームキー保存はしないので飛ばす
						gkc.DctBtnSt [ gkb ] = (GK_ST)br.ReadByte ();
					}
				
					cmd.ListGameKeyCommand.Add ( gkc );
				}

				chara.BD_Command.Add ( cmd );
			}
		}

		//ブランチ
		private void LoadBinBranch ( BinaryReader br, Chara chara )
		{
			uint N = br.ReadUInt32 ();
			for ( uint i = 0; i < N; ++ i )
			{
				Branch brc = new Branch ()
				{
					Name = br.ReadString (),
					Condition = (BranchCondition)br.ReadByte (),
					NameCommand = "Cmd_" + br.ReadUInt32 (),
					NameSequence = "Seq_" + br.ReadUInt32 (),
					Frame = (int)br.ReadUInt32 (),
				};
				chara.BD_Branch.Add ( brc );
			}

			//コマンドとアクションの名前を再設定
			foreach ( Branch brc in chara.BD_Branch.GetEnumerable () )
			{
				int id = GetIndex ( brc.NameCommand, "Cmd_" );
				brc.NameCommand = chara.BD_Command [ id ].Name;
			}
			foreach ( Branch brc in chara.BD_Branch.GetEnumerable () )
			{
				int ActionID = GetIndex ( brc.NameSequence, "Seq_" );
				brc.NameSequence = chara.behavior[ ActionID ].Name;
			}
		}

		//ルート
		private void LoadBinRoute ( BinaryReader br, Chara chara )
		{
			//ルート個数
			uint N = br.ReadUInt32 ();
			for ( uint i = 0; i < N; ++ i )
			{
				Route rut = new Route ()
				{
					Name = br.ReadString (),
				};
				//ブランチ個数
				uint N_Brc = br.ReadUInt32 ();
				for ( uint iBrc = 0; iBrc < N_Brc; ++ iBrc )
				{
					TName t = new TName ();
					t.Name = "Brc_" + br.ReadUInt32 ();
					rut.BD_BranchName.Add ( t );
				}

				chara.BD_Route.Add ( rut );
			}

			//ブランチの名前を再設定
			foreach ( Route rut in chara.BD_Route.GetEnumerable () )
			{
				BindingDictionary < TName > BD_Brc = rut.BD_BranchName;

				//名前だけのリストを作成
				List < string > L_Tn = new List < string > ();
				foreach ( TName t in BD_Brc.GetEnumerable () )
				{
					int id = GetIndex ( t.Name, "Brc_" );
					L_Tn.Add ( chara.BD_Branch [ id ].Name );
				}

				//クリアして再追加
				BD_Brc.Clear ();
				foreach ( string brc_name in L_Tn )
				{
					BD_Brc.Add ( new TName ( brc_name ) );
				}
			}

			//スクリプトにおけるルート名の再設定
			foreach ( Action act in chara.behavior.BD_Sequence.GetEnumerable () )
			{
				foreach ( Script scp in act.ListScript )
				{
					//名前だけのリストを作成
					List < string > L_name = new List<string> ();
					foreach ( TName t in scp.BD_RutName.GetEnumerable () )
					{
						int id = GetIndex ( t.Name, "Rut_" );
						L_name.Add ( chara.BD_Route [ id ].Name );
					}

					//クリアして再追加
					scp.BD_RutName.Clear ();
					foreach ( string name in L_name )
					{
						scp.BD_RutName.Add ( new TName ( name ) );
					}
				}
			}

		}


		//----------------------
		//str_indexからheadを除き、Int.Parse()して返す
		private int GetIndex ( string str_index, string head )
		{
			int n = head.Length;
			int nextActionID = int.Parse ( str_index.Substring ( n, str_index.Length - n ) );
			return nextActionID;
		}
	}
}
