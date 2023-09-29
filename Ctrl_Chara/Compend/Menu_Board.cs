using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;


namespace ScriptEditor
{
	public partial class Menu_Board : UserControl
	{
		//編集
		public EditCompend EditCompend { get; set; } = null;

		//親コントロール
		private System.Action ActDisp = ()=>{};		//からの表示
		private System.Action ActAssosiate = ()=>{};	//関連付け


		//ボード上でスクリプト配列を編集するコントロール
		public Menu_Board ()
		{
			InitializeComponent ();
		}

		//環境設定
		public void SetEnviron ( EditCompend ec, _Ctrl_Compend ctrl_cmpd )
		{
			EditCompend = ec;
			ActDisp = ctrl_cmpd.Disp;
			ActAssosiate = ctrl_cmpd.Assosiate;
		}


		//---------------------------------------------------------------------
		//ボタンイベント

		//単体スクリプト 挿入
		private void Btn_Ins_Click ( object sender, EventArgs e )
		{
			Script scp = Make_Script_with_Rect ();
			EditCompend.InsertScript ( scp );
			ReSelect ();
			ActDisp ();
		}

		//単体スクリプト 追加
		private void Btn_Add_Click ( object sender, EventArgs e )
		{
			Script scp = Make_Script_with_Rect ();
			EditCompend.AddScript ( scp );
			ReSelect ();
			ActDisp ();
		}

		//単体スクリプト 削除
		private void Btn_Del_Click ( object sender, EventArgs e )
		{
			EditCompend.RemScript ();
			ReSelect ();
			ActDisp ();
		}

		//--------------------------------------------------------------------
		//複数スクリプト 挿入
		private void Btn_MltIns_Click ( object sender, EventArgs e )
		{
			Script scp = Make_Script_with_Rect ();
			EditCompend.MultiInsert ( scp );
			ReSelect ();
			ActDisp ();
		}

		//複数スクリプト 追加
		private void Btn_MltAdd_Click ( object sender, EventArgs e )
		{
			Script scp = Make_Script_with_Rect ();
			EditCompend.MultiAdd ( scp );
			ReSelect ();
			ActDisp ();
		}

		//複数スクリプト 削除
		private void Btn_MltDel_Click ( object sender, EventArgs e )
		{
			EditCompend.MultiRem ();
			ReSelect ();
			ActDisp ();
		}

		//--------------------------------------------------------------------
		//グループ 作成
		private void Btn_GrpMake_Click ( object sender, EventArgs e )
		{
			EditCompend ec = EditCompend;
			EditScript es = EditCompend.EditScript;

			List < Script > lsScp = ec.SelectedSequence.ListScript;
			int start = ec.SelectedSpanStart;
			int end = ec.SelectedSpanEnd;

			es.MakeGroup ( lsScp, start, end );

			ActDisp ();
		}

		//グループ 解除
		private void BtnGrpDismantle_Click ( object sender, EventArgs e )
		{
			EditCompend ec = EditCompend;
			EditScript es = EditCompend.EditScript;

			es.DismantleGroup ( ec.SelectedSequence.ListScript, ec.SelectedScript.Group );

			ActDisp ();
		}

		//グループ 同一化
		private void BtnUnify_Click ( object sender, EventArgs e )
		{
			EditScript es = EditCompend.EditScript;
			es.PasteGroup ( EditCompend.SelectedScript );
			ActDisp ();
		}


		//--------------------------------------------------------------------
		//内部関数
		//--------------------------------------------------------------------
		//仮枠つきスクリプト
		Script Make_Script_with_Rect ()
		{
			Script scp = new Script ();
			scp.ListCRect.Add ( new Rectangle ( -50, -350, 120, 350 ) );
			scp.ListHRect.Add ( new Rectangle ( -60, -360, 130, 370 ) );
			return scp;
		}

		//再選択
		private void ReSelect ()
		{
			int i = EditCompend.SelectedScript.Frame;

			EditCompend.SelectFrame ( i );
			EditCompend.SelectedSpanStart = i;
			EditCompend.SelectedSpanEnd = i;

			ActAssosiate ();
			ActDisp ();
		}

	}
}
