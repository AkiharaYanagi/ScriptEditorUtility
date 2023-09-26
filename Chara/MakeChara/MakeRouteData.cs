using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ScriptEditor
{
	//テキストデータからルートリストを作成する
	public class MakeRouteData
	{
		//デフォルトルート名
		private enum ENM_RUT
		{
			地上移動, 地上通常技, 前持続停止, 後持続停止, ジャンプ,
			地上必殺技, 特殊, 地上超必, 
			投げ分岐_自, 投げ分岐_相, 
			被ダメ_L, 被ダメ_M, 被ダメ_H,
		}

		//デフォルトブランチ名
		private enum ENM_BRC
		{
			L_立L, Ma_立Ma, Mb_立Mb, H_立H, 
			_6_F_Move, _4_B_Move, 
			_6離_Stand, _4離_Stand, _6_6_F_Dash, _4_4_B_Dash, 
			_8_V_Jump, _9_F_Jump, _7_B_Jump, 
			着地_Stand,
			SP01, 
			SP0, 
			OD0, 
			投げ分岐_自, 投げ分岐_相, 
			_4L, _4Ma, _4Mb, _4H, 
			_2L, _2Ma, _2Mb, _2H, 
			_6L, _6Ma, _6Mb, _6H, 
			_8L, _8Ma, _8Mb, _8H, 
			被ダメ_L, 被ダメ_M, 被ダメ_H,
		}

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
				EditChara.Inst.AddRoute ();
			}
		}

		private void _Make ( Chara ch )
		{
			//ルートデータ(.txtファイル)から作成
			FileStream fstrm = new FileStream ( "RouteList.txt", FileMode.Open, FileAccess.Read );
			StreamReader sr = new StreamReader ( fstrm, Encoding.UTF8 );

			EditBehavior eb = EditChara.Inst.EditBehavior;

			//"名前","要約","ブランチ名"...[不定数],[改行]
			List < string > lstr = new List<string> ();
			while ( ! sr.EndOfStream )
			{
				string str = sr.ReadLine ();
				string[] routeData = str.Split ( ',' );
				Route rut = new Route ( routeData[0], routeData[1] );

				string[] _ary = routeData.Skip ( 2 ).ToArray ();

				foreach ( string s in _ary )
				{
					//空白を消去
					string _s = s.TrimStart ( ' ' ).TrimEnd ( ' ' );
					//空文字のみは飛ばす
					if ( _s == "" ) { continue; }

					//ブランチ指定に追加
					rut.BD_BranchName.Add ( new TName ( _s ) );
				}

				//キャラに設定
				EditChara.Inst.AddRoute ( rut );
			}

			//キャラに設定
			foreach ( string str in lstr )
			{
				EditChara.Inst.AddRoute ( str );
			}

		}

		private void SetEnmBrc ( Route rut, ENM_BRC enm_brc )
		{
			rut.BD_BranchName.Add ( new TName ( enm_brc.ToString() ) );
		}
	}
}
