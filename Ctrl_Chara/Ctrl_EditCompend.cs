using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ScriptEditor;

namespace Ctrl_Chara
{
	using LScp = List < Script >;


	public partial class Ctrl_EditCompend : UserControl
	{

		//全体編集
		private EditCompend EditCompend = new EditCompend ();

		
		public Ctrl_EditCompend ()
		{
			InitializeComponent ();
		}


		public void SetEnvironment ( EditCompend ec )
		{
			EditCompend = ec;
		}


		public void Assosiate ()
		{
			EditCompend EC = EditCompend;

			//シークエンス名
			TB_SqcName.Text = EC.SelectedSequence.Name;

			//選択中スクリプト　インデックス
			TB_SlctIndex.Text = EC.SelectedScript.ToString ();

			int span_start = EC.SelectedSpanStart;
			int span_end = EC.SelectedSpanEnd;

			TB_SlctSpan.Text = "[ " + span_start + " - " + span_end + " ]";


			//選択中グループ
			EditScript ES = EC.EditScript;
			TB_SlctGroup.Text = ES.SelectedGroupIndex.ToString ();

			LScp lGrp = ES.SelectedGroup;

			if (lGrp != null)
			{
				int CntGrp = lGrp.Count;
				if ( CntGrp > 0 )
				{
					int grp_start = lGrp [ 0 ].Frame;
					int grp_end = lGrp [ CntGrp - 1 ].Frame;

					TB_GrpSpan.Text = "[ " + grp_start + " - " + grp_end + " ]";
				}
				else
				{
					TB_GrpSpan.Text = "[ 0 - 0 ]";
				}
			}
		}
	}
}
