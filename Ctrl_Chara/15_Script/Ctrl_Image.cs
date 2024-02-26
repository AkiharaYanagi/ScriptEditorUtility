using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	using BD_ImgDt = BindingDictionary < ImageData >;

	public partial class Ctrl_Image : UserControl
	{
		// イメージリスト参照
		private BD_ImgDt BD_ImageData { get; set; } = new BD_ImgDt ();
		
		//親フォーム
		private EditorSubForm FormParent { get; set; } = new EditorSubForm ();

		//編集
		public EditCompend EditCompend { get; set; } = new EditCompend ();

		//対象指定
		private RB_ScriptTarget Rb_ScpTgt = new RB_ScriptTarget ();


		public Ctrl_Image ()
		{
			InitializeComponent ();

			this.Controls.Add ( Rb_ScpTgt );

			Lb_Image.DisplayMember = "Name";
			Lb_Image.ValueMember = "Image";
		}

		public void SetParent ( EditorSubForm f )
		{
			FormParent = f;
		}

		public void SetEditCompend ( EditCompend ec )
		{
			EditCompend = ec;
			BD_ImageData = ec.Compend.BD_Image;

			//リストボックスに反映
			Lb_Image.DataSource = BD_ImageData.GetBindingList ();
		}

		public void UpdateData ()
		{
			BD_ImageData.ResetItems ();
		}


		//リストボックスの選択位置変更時
		private void LB_SelectedIndexChanged ( object sender, EventArgs e )
		{
			//選択されていないとき何もしない
			if ( null == Lb_Image.SelectedItem ) { return; }

			//画像の更新
			pbArchiveImage.Image = ( ( ImageData ) Lb_Image.SelectedItem ).Img;
		}

		//キャンセル
		private void Btn_Cancel_Click ( object sender, EventArgs e )
		{
			FormParent.Hide ();
		}

		private void Btn_ArchiveOK_Click ( object sender, EventArgs e )
		{
			//選択されているイメージ名
			string name = ( (ImageData)Lb_Image.SelectedItem ).Name;

			//編集対象
			switch ( Rb_ScpTgt.EditTarget )
			{
			//選択中スクリプトにイメージの設定
			case EditTargetScript.SINGLE:
				EditCompend.SelectedScript.ImgName = name;
			break;

			//グループにも変更を反映
			case EditTargetScript.GROUP:
				EditCompend.EditScript.DoSetterInGroup_T ( (s,n)=>{ s.ImgName = n; }, name );
			break;
			
			//選択
			case EditTargetScript.SELECT: 
				EditCompend.DoSetterInSpan_T ( (s,n)=>{ s.ImgName = n; }, name );
			break;
			
			//全体
			case EditTargetScript.ALL: 
				EditCompend.EditSequence.DoSetterInSqc_T ( (s,n)=>{ s.ImgName = n; }, name );
			break;
			
			default: break;
			}


			//全体更新
			All_Ctrl.Inst.UpdateData ();
		}
	}
}
