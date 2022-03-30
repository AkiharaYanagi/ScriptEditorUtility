using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class Ctrl_SqcList : UserControl
	{
		//リストボックスの更新
		private void UpdateElb()
		{
			foreach (SequenceData sqcDt in ELB_Sqc.GetList())
			{
				sqcDt.Name = sqcDt.Sqc.Name;
			}
			ELB_Sqc.ResetItems();
			ELB_Sqc.Invalidate();
		}

		//ピクチャボックスの更新
		private void UpdateCtrl ()
		{
			ctrl_ImageTable1.UpdateData();
			ctrl_ImageTable1.Invalidate();
		}

		//全体更新
		private void UpdateAll()
		{
			UpdateElb();
			UpdateCtrl ();

			this.Invalidate ();
		}
	}
}
