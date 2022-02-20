using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;


namespace ScriptEditor
{
	using BD_Seq = BindingDictionary < Sequence >;

	//テキストデータからアクションリストを作成する
	public partial class MakeActionData
	{
		public void Make ( Chara ch )
		{
			try
			{
				_Make ( ch );
			}
			//ファイルが無いとき
			catch ( FileNotFoundException e )
			{
				Debug.WriteLine ( e );

				//１つダミーを追加
				EditBehavior eb = EditChara.Inst.EditBehavior;
				eb.AddAction ();
			}
		}

		private void _Make ( Chara ch )
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;
			EditSequence ea = eb.EditSequence;
			BD_Seq bd_act = ch.behavior.BD_Sequence;

			_MakeList ( ch );

			MakeAction_Stand ( bd_act );
			MakeAction_FrontMove ( bd_act );
			MakeAction_BackMove ( bd_act );
#if false
			MakeAction_Jump ( eb, ENM_ACT.VerticalJump, "AirFrontDash_00.png", 0 );
			MakeAction_Jump ( eb, ENM_ACT.FrontJump, "AirFrontDash_00.png", 20 );
			MakeAction_Jump ( eb, ENM_ACT.BackJump, "AirFrontDash_00.png", -20 );
#endif
			MakeAction_FrontDash ( bd_act );
		}


		//アクションリストの作成
		private void _MakeList ( Chara ch )
		{
			//アクションデータ(.txtファイル)から作成
			FileStream fstrm = new FileStream ( "ActionList.txt", FileMode.Open, FileAccess.Read );
			StreamReader sr = new StreamReader ( fstrm, Encoding.UTF8 );

			EditBehavior eb = EditChara.Inst.EditBehavior;
			while ( ! sr.EndOfStream )
			{
				//行から各データに分割
				string str = sr.ReadLine ();
				string[] actionData = str.Split(',');

				//----
				//名前
				string name = actionData[(int)ActionData.Name];
				Action act = new Action ();
				act.Name = name;
				act.NextActionName = "Stand";

				//----
				//カテゴリ
				int category = 0;
				ActionCategory ac = ActionCategory.NEUTRAL;
				try
				{
					category = int.Parse ( actionData[(int)ActionData.Category] );
					ac = (ActionCategory)category;
				}
				catch
				{
					category = 0;
					ac = ActionCategory.NEUTRAL;
				}
				act.Category = ac;

				//----
				//スクリプト

				//最初のダミースクリプトを消去
				act.ListScript.Clear ();

				//スクリプト個数
				int nScript = 0;
				try
				{
					nScript = int.Parse ( actionData[(int)ActionData.ScriptNum] );
				}
				catch
				{
					nScript = 0;
				}

				//スクリプト追加
				for ( int i = 0; i < nScript; ++ i )
				{
					Script s = new Script ()
					{	
						Group = 1,
						Pos = new Point ( -250, -450 ),		
					};

					string imgname = ch.behavior.BD_Image.Get ( 0 )?.Name;
					if ( imgname != null )
					{
						s.ImgName = imgname;
					}

					s.BD_RutName.Add ( new TName ( ENM_RUT.地上超必 ) );
					s.BD_RutName.Add ( new TName ( ENM_RUT.地上必殺技 ) );
					s.BD_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
					s.BD_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
					s.BD_RutName.Add ( new TName ( ENM_RUT.地上移動 ) );

					act.AddScript ( s ); 
				}

				//ビヘイビアに追加
				eb.AddAction ( act );
			}
		}
	}
}
