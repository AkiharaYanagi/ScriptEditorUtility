using System;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	using BD_ImgDt = BindingDictionary < ImageData >;
	using LScp = List < Script >;


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


		//流し込み
		private void Btn_Stream_Click ( object sender, EventArgs e )
		{
			//選択中のスクリプトからグループごとにイメージを連続で設定

			//対象シークエンス
			Sequence sqc = EditCompend.SelectedSequence;
			if ( sqc is null ) { return; }
			if ( sqc.ListScript.Count <= 0 ) { return; }

			int start = EditCompend.SelectedScript.Frame;

			int nameIndex = -1 + Lb_Image.SelectedIndex;	//一つ前からスタート
			int group = -1;	//グループ対象外からスタート

			//対象スクリプト以下を変更
			//foreach (Script scp in sqc.ListScript)
			for ( int i = start; i < sqc.ListScript.Count; ++ i )
			{
				//イメージ数より多ければ終了
				if ( nameIndex >= ( Lb_Image.Items.Count - 1 ) ) { break; } 

				Script scp = sqc.ListScript [ i ];

				//グループごと
				if ( scp.Group != group )
				{
					group = scp.Group;
					++ nameIndex;
				}

				//イメージ名の設定
				string name = ((ImageData)Lb_Image.Items [nameIndex] ).Name;
				scp.ImgName = name;
			}

#if false
			//イメージ数が２より少ないとき何もしない
			if ( Lb_Image.Items.Count < 2 ) { return; } 

			//選択イメージ
			int nameIndex = Lb_Image.SelectedIndex;

			//対象グループ
			List < LScp > L_LScp = EditCompend.EditScript.L_ScriptGroup;

			foreach ( LScp lscp in L_LScp )
			{
				//イメージ数より多ければ終了
				if ( nameIndex >= Lb_Image.Items.Count ) { break; } 

				foreach ( Script scp in lscp )
				{
					//イメージ名の設定
					string name = ((ImageData)Lb_Image.Items [nameIndex] ).Name;
					scp.ImgName = name;
				}

				++ nameIndex;
			}
#endif

			//全体更新
			All_Ctrl.Inst.UpdateData ();
		}
	}
}
