using System;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	public partial class Ctrl_KeyConfig : UserControl
	{
		//操作用
		private List < RadioButton > L_RB = new List<RadioButton> (); 

//		private KeyboardInput;


		public Ctrl_KeyConfig ()
		{
			InitializeComponent ();

			L_RB.Add ( RB_P1Up );
			L_RB.Add ( RB_P1Down );
			L_RB.Add ( RB_P1Left );
			L_RB.Add ( RB_P1Right );
			L_RB.Add ( RB_P1Btn0 );
			L_RB.Add ( RB_P1Btn1 );
			L_RB.Add ( RB_P1Btn2 );
			L_RB.Add ( RB_P1Btn3 );
			L_RB.Add ( RB_P1Btn4 );
			L_RB.Add ( RB_P1Btn5 );
			L_RB.Add ( RB_P1Btn6 );
			L_RB.Add ( RB_P1Btn7 );

			L_RB.Add ( RB_P2Up );
			L_RB.Add ( RB_P2Down );
			L_RB.Add ( RB_P2Left );
			L_RB.Add ( RB_P2Right );
			L_RB.Add ( RB_P2Btn0 );
			L_RB.Add ( RB_P2Btn1 );
			L_RB.Add ( RB_P2Btn2 );
			L_RB.Add ( RB_P2Btn3 );
			L_RB.Add ( RB_P2Btn4 );
			L_RB.Add ( RB_P2Btn5 );
			L_RB.Add ( RB_P2Btn6 );
			L_RB.Add ( RB_P2Btn7 );

			foreach ( RadioButton rb in L_RB )
			{
				rb.PreviewKeyDown += new PreviewKeyDownEventHandler ( RB_PreviewKeyDown );
			}
		}


		//ラジオボタン共通イベント　カーソルキーでフォーカスの移動を禁止する
		private void RB_PreviewKeyDown ( object sender, PreviewKeyDownEventArgs e )
		{
			switch ( e.KeyCode )
			{
			case Keys.Up:
			case Keys.Down:
			case Keys.Left:
			case Keys.Right:
				e.IsInputKey = true;
				break;
			}
		}

		public void UpdateData ( string txt )
		{
			Tb_Up.Text = txt;
		}
	}
}
