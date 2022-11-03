using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections.Generic;


namespace ScriptEditor
{
	//==================================================================================
	//	SaveCharaBin
	//		キャラデータをbinaryで.datファイルに保存
	//		Document形式を介さない
	//==================================================================================
	public partial class SaveCharaBin
	{
		//---------------------------------------------------------------------
		//behavior
		public void SaveBinBehavior ( BinaryWriter bw, Chara chara )
		{
			//Action : Sequence
			Behavior bhv = chara.behavior;
			byte nAct = (byte)bhv.BD_Sequence.Count ();
			bw.Write ( nAct );
			foreach ( Action act in bhv.BD_Sequence.GetEnumerable () )
			{
				bw.Write ( (byte)chara.GetIndexOfAction ( act.NextActionName ) ); 
				bw.Write ( (byte)act.Category );
				bw.Write ( (byte)act.Posture );
				bw.Write ( (byte)act.HitNum );
				bw.Write ( (byte)act.HitPitch );
				bw.Write ( (byte)act.Balance );

				SaveBinListScript ( bw, chara, act.ListScript );
			}
		}
		//---------------------------------------------------------------------
		//gernish
		public void SaveBinGarnish ( BinaryWriter bw, Chara chara )
		{
			//Effect : Sequence
			Garnish gns = chara.garnish;
			byte nGns = (byte)gns.BD_Sequence.Count ();
			bw.Write ( nGns );
			foreach ( Effect efc in gns.BD_Sequence.GetEnumerable () )
			{
				SaveBinListScript ( bw, chara, efc.ListScript );
			}
		}

		//---------------------------------------------------------------------
		//Command
		public void SaveBinCommand ( BinaryWriter bw, Chara chara )
		{
			//個数 [1byte]
			byte nCommand = (byte)chara.BD_Command.Count ();
			bw.Write ( nCommand );

			//実データ [sizeof ( Command ) * n]
			foreach ( Command cmd in chara.BD_Command.GetEnumerable () )
			{
				bw.Write ( cmd.Name );		//string (length , [UTF8])
				bw.Write ( (byte)cmd.LimitTime );

				//ゲームキー
				byte n = (byte)cmd.ListGameKeyCommand.Count;
				bw.Write ( n );
				foreach ( GameKeyCommand gkc in cmd.ListGameKeyCommand )
				{
					//否定
					bw.Write ( gkc.Not );
					//レバー
					//enum なので 個数は固定 GameKeyData.Lever.LVR_N = 8;
					foreach ( GameKeyData.Lever lvr in gkc.DctLvrSt.Keys )
					{
						bw.Write ( (byte)gkc.DctLvrSt [ lvr ] );
					}
					//ボタン
					//enum なので 個数は固定 GameKeyData.Button.BTN_N = 8;
					foreach ( GameKeyData.Button btn in gkc.DctBtnSt.Keys )
					{
						bw.Write ( (byte)gkc.DctBtnSt [ btn ] ); 
					}
				}
			}
		}

		//---------------------------------------------------------------------
		//Branch
		void SaveBinBranch ( BinaryWriter bw, Chara chara )
		{
			//個数 [1byte]
			byte nBrc = (byte)chara.BD_Branch.Count ();
			bw.Write ( nBrc );

			//実データ [sizeof ( Branch ) * n]
			foreach ( Branch brc in chara.BD_Branch.GetEnumerable () )
			{
				bw.Write ( brc.Name );		//string (length , [UTF8])
				bw.Write ( (byte)brc.Condition );	//enum -> byte
				bw.Write ( (byte)chara.GetIndexOfCommand ( brc.NameCommand ) );	//int -> byte
				bw.Write ( (byte)chara.GetIndexOfAction ( brc.NameSequence ) );	//int -> byte
				bw.Write ( (byte)brc.Frame );	//int -> byte
			}

		}

		//---------------------------------------------------------------------
		//Route
		void SaveBinRoute ( BinaryWriter bw, Chara chara )
		{
			//個数 [1byte]
			byte nRut = (byte)chara.BD_Route.Count ();
			bw.Write ( nRut );

			//実データ [sizeof ( Route ) * n]
			foreach ( Route rut in chara.BD_Route.GetEnumerable () )
			{
				bw.Write ( rut.Name );      //string (length , [UTF8])
				byte nBrnName = (byte)rut.BD_BranchName.Count ();
				bw.Write ( nBrnName );
				foreach ( TName brcName in rut.BD_BranchName.GetEnumerable () )
				{
					bw.Write ( (byte)chara.GetIndexOfBranch ( brcName.Name ) );	//int -> byte
				}
			}
		}

		//---------------------------------------------------------------------
		//ListScript
		void SaveBinListScript ( BinaryWriter bw, Chara chara, List < Script > lsScp )
		{
			byte nScp = (byte)lsScp.Count;
			bw.Write ( nScp ); 
			foreach ( Script scp in lsScp )
			{ 
				//イメージインデックス
				bw.Write ( chara.GetIndexOfImage ( scp.ImgName ) );

				//位置
				bw.Write ( scp.Pos.X );		//int
				bw.Write ( scp.Pos.Y );		//int
					
				//ルート
				bw.Write ( (byte)scp.BD_RutName.Count() );
				foreach ( TName tn in scp.BD_RutName.GetEnumerable () )
				{
					bw.Write ( chara.GetIndexOfRoute ( tn.Name ) );
				}

				//枠
				SaveBinListRect ( bw, scp.ListCRect );
				SaveBinListRect ( bw, scp.ListHRect );
				SaveBinListRect ( bw, scp.ListARect );
				SaveBinListRect ( bw, scp.ListORect );

				//エフェクト生成
				bw.Write ( scp.BD_EfGnrt.Count () );
				foreach ( EffectGenerate efGnrt in scp.BD_EfGnrt.GetEnumerable () )
				{ 
					bw.Write ( (uint)chara.GetIndexOfEffect ( efGnrt.EfName ) );	//uint
					bw.Write ( efGnrt.Pt.X );	//int
					bw.Write ( efGnrt.Pt.Y );	//int
					bw.Write ( efGnrt.Z_PER100F );		//int
					bw.Write ( efGnrt.Gnrt );	//bool
					bw.Write ( efGnrt.Loop );	//bool
					bw.Write ( efGnrt.Sync );	//bool
				}

				//バトルパラメータ
				SaveBinScrPrmBtl ( bw, scp );

				//ステージング(演出)パラメータ
				SaveBinScrPrmStg ( bw, scp );
			}
		}

		//---------------------------------------------------------------------
		//ListRect
		void SaveBinListRect ( BinaryWriter bw, List < Rectangle > listRect )
		{
			//上限は固定 ConstChara.NumRect = 8
			//それ以下は０からの可変
			foreach ( Rectangle rct in listRect )
			{
				bw.Write ( rct.Top );
				bw.Write ( rct.Left );
				bw.Write ( rct.Bottom );
				bw.Write ( rct.Right );
			}
		}

		//---------------------------------------------------------------------
		//ScriptParam_Battle
		void SaveBinScrPrmBtl ( BinaryWriter bw, Script scp )
		{
			ScriptParam_Battle prm = scp.Param_Btl;
			bw.Write ( (int)prm.CalcState );
			bw.Write ( prm.Vel.X );
			bw.Write ( prm.Vel.Y );
			bw.Write ( prm.Acc.X );
			bw.Write ( prm.Acc.Y );
			bw.Write ( prm.Power );
			bw.Write ( prm.Warp );
			bw.Write ( prm.Recoil_I );
			bw.Write ( prm.Recoil_E );
			bw.Write ( prm.Blance_I );
			bw.Write ( prm.Blance_E );
		}

		//---------------------------------------------------------------------
		//ScriptParam_Staging
		void SaveBinScrPrmStg ( BinaryWriter bw, Script scp )
		{
			ScriptParam_Staging prm = scp.Param_Ef;
			bw.Write ( prm.BlackOut );
			bw.Write ( prm.Vibration );
			bw.Write ( prm.Stop );
			bw.Write ( prm.Radian );
			bw.Write ( prm.AfterImage_pitch );
			bw.Write ( prm.AfterImage_N );
			bw.Write ( prm.AfterImage_time );
			bw.Write ( prm.Vibration_S );
			bw.Write ( (uint)prm.Color.ToArgb () );
			bw.Write ( prm.Color_time );
		}
		
	}
}
