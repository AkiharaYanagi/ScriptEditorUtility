using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	//スクリプト内の計算状態を設定するコンポーネント
	public class Cmpnt_ClcSt : ComboBox, IScriptParam
	{
		//対象スクリプト
		public Script Scp { get; set; } = new Script ();

		//対象に値を設定するゲッタとセッタ
		public ScriptParam < CLC_ST > ScpPrm { get; set; } = null;

		//表示
		public System.Action Disp { get; set; } = () =>{};

		//対象指定
		private EditTarget editTarget = EditTarget.SINGLE;
	
		//編集用
		private EditCompend EditCompend = null;


		//コンストラクタ
		public Cmpnt_ClcSt ()
		{
			this.DropDownStyle = ComboBoxStyle.DropDownList;

			//計算状態 プルダウンメニュー初期化
			foreach ( CLC_ST clcst in Enum.GetValues ( typeof ( CLC_ST ) ) )
			{
				this.Items.Add ( clcst );
			}
			ScpPrm = new ScriptParam<CLC_ST> ( (s,c)=>s.CalcState=c, s=>s.CalcState );
		}

		//プルダウンメニューで選択したとき
		protected override void OnSelectionChangeCommitted ( EventArgs e )
		{
			SetValue ();
			base.OnSelectionChangeCommitted ( e );
		}

		public void SetValue ()
		{
			CLC_ST clsst = (CLC_ST)this.SelectedItem;

			switch ( editTarget )
			{
			case EditTarget.ALL: 
				EditCompend.EditSequence.DoSetterInSqc_T ( ScpPrm.Setter, clsst );
			break;
			
			case EditTarget.GROUP:
				EditCompend.EditScript.DoSetterInGroup_T ( ScpPrm.Setter, clsst );
			break;
			
			case EditTarget.SINGLE:
				ScpPrm.Setter ( Scp, clsst );
			break;

			default: break;
			}
		}


		// 環境指定
		public void SetEnvironment ( EditCompend ec, System.Action disp )
		{
			EditCompend = ec;
			Disp = disp;
		}

		// Setter, Getter 指定
		public void SetParam ( ScriptParam < CLC_ST > scpPrm  )
		{
			ScpPrm = scpPrm;
		}

		//更新
		public void UpdateData ()
		{
			int i = this.Items.IndexOf ( Scp.CalcState );
			this.SelectedIndex = i;
			Disp ();
		}

		//関連付け
		public void Assosiate ( Script s )
		{
			Scp = s;
			UpdateData ();
		}
		
		public void SetTarget_All () { editTarget = EditTarget.ALL; }
		public void SetTarget_Group () { editTarget = EditTarget.GROUP; }
		public void SetTarget_Single () { editTarget = EditTarget.SINGLE; }
		public void SetTarget ( EditTarget et ) { editTarget = et; }
	}
}
