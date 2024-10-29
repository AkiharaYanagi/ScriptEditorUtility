using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;


namespace ScriptEditor
{

	//テキストファイルから名前の指定
	public class CB_Names : ComboBox
	{
		//対象スクリプト
		public Script Scp { get; set; } = new Script ();

		//ゲッタセッタ
		public ScriptParam<string> ScpPrm { get; set; }
			= new ScriptParam<string> ( (s,str)=>{}, s=>"" );


		//コンストラクタ
		public CB_Names ()
		{
			this.Width	 = 200;
		}


		//ファイルから読込
		public void LoadData ( string filename )
		{
			try
			{
				_LoadData ( filename );
			}
			catch
			{
				this.Items.Add ( "NoData" );
			}

		}

		private void _LoadData ( string filename )
		{
			using ( StreamReader sr = new StreamReader ( filename ) )
			{

			this.Items.Clear ();

			//該当なしを最初に追加
			this.Items.Add ( "" );

			do
			{
				string name = sr.ReadLine ();
				if ( name == null ) { break; }

				this.Items.Add ( name );
			}
			while ( ! sr.EndOfStream );
			
			}	//using


			if ( Items.Count > 0)
			{
				this.SelectedIndex = 0;
			}
	
		}


		public void UpdateData ()
		{
			string name =  ScpPrm.Getter( Scp );
			if ( this.Items.Contains ( name ) )
			{ 
				this.SelectedItem = name;
				this.BackColor = SystemColors.Control;
			}
			else
			{
				if ( this.Items.Count == 0 ) { return; }
				this.SelectedIndex = 0;	//該当なし
				this.BackColor = Color.Red;
			}
		}

		public void Assosiate ( Script s )
		{
			Scp = s;
			UpdateData ();
		}

		//閉じたときに更新された場合のイベント
		protected override void OnSelectionChangeCommitted ( EventArgs e )
		{
			ScpPrm.Setter ( Scp, (string)this.SelectedItem );
			this.BackColor = SystemColors.Control;
			All_Ctrl.Inst.UpdateData_scp ();

			base.OnSelectionChangeCommitted ( e );
		}

		//開いたときのイベント
		protected override void OnDropDown ( EventArgs e )
		{
			this.BackColor = SystemColors.Control;
			base.OnDropDown ( e );
		}
	}
}
