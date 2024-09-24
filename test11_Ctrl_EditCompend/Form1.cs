using System;
using System.Drawing;
using System.Windows.Forms;

using ScriptEditor;
using ScriptEditorUtility;


namespace test11_Ctrl_EditCompend
{
	public partial class Form1 : Form
	{
		private SubForm_CmpdInfo SubForm_CmpdInfo = new SubForm_CmpdInfo();
		private Ctrl_SubFormBtn ctrlBtn = new Ctrl_SubFormBtn ();
		private Ctrl_EditCompend ctrlEdtCmpd = new Ctrl_EditCompend ();


		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			//�e�X�g�f�[�^�̍쐬
			Chara chara = new Chara ();
			TestCharaData testCh = new TestCharaData ();
			testCh.Make ( chara );

			//�R���g���[��
			this.Controls.Add ( ctrlBtn );

			//�T�u�t�H�[��
			SubForm_CmpdInfo.FormMain = this;
			ctrlBtn.FB_EditInfo.Form = SubForm_CmpdInfo;

			//�ҏW�̐ݒ�
			EditCompend ec = new EditCompend ();
			ctrlEdtCmpd.SetEnvironment ( ec );
			ctrlEdtCmpd.Assosiate ();
		}
	}
}
