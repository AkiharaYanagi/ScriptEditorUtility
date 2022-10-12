using System;
using System.Windows.Forms;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	//ラジオボタンとテキストボックス
	public struct RBTB
	{
		public RadioButton rb;
		public TextBox tb;

		public void Set ( RadioButton r, TextBox t )
		{
			rb = r;
			tb = t;
		}
	}

	//コントロール
	public partial class Ctrl_KeyConfig : UserControl
	{
		//コントロール操作用
		private RBTB [] ARY_RBTB;
		private const int RBTB_NUM = 24;

		//DirectInput入力
		private DxInput dxInput = new DxInput ();

		//現在ラジオボタン位置
//		private RadioButton rbPos;
		private RBTB rbtbPos;


		//コンストラクタ
		public Ctrl_KeyConfig ()
		{
			InitializeComponent ();

			//入力初期化
			dxInput.Load ();

			//コントロール操作用
			ARY_RBTB = new RBTB [ RBTB_NUM ];

			ARY_RBTB [  0 ].Set ( RB_P1Up,		Tb_P1Up );
			ARY_RBTB [  1 ].Set ( RB_P1Down,	Tb_P1Down );
			ARY_RBTB [  2 ].Set ( RB_P1Left,	Tb_P1Left );
			ARY_RBTB [  3 ].Set ( RB_P1Right,	Tb_P1Right );
			ARY_RBTB [  4 ].Set ( RB_P1Btn0,	Tb_P1Btn0 );
			ARY_RBTB [  5 ].Set ( RB_P1Btn1,	Tb_P1Btn1 );
			ARY_RBTB [  6 ].Set ( RB_P1Btn2,	Tb_P1Btn2 );
			ARY_RBTB [  7 ].Set ( RB_P1Btn3,	Tb_P1Btn3 );
			ARY_RBTB [  8 ].Set ( RB_P1Btn4,	Tb_P1Btn4 );
			ARY_RBTB [  9 ].Set ( RB_P1Btn5,	Tb_P1Btn5 );
			ARY_RBTB [ 10 ].Set ( RB_P1Btn6,	Tb_P1Btn6 );
			ARY_RBTB [ 11 ].Set ( RB_P1Btn7,	Tb_P1Btn7 );
			ARY_RBTB [ 12 ].Set ( RB_P2Up,		Tb_P2Up );
			ARY_RBTB [ 13 ].Set ( RB_P2Down,	Tb_P2Down );
			ARY_RBTB [ 14 ].Set ( RB_P2Left,	Tb_P2Left );
			ARY_RBTB [ 15 ].Set ( RB_P2Right,	Tb_P2Right );
			ARY_RBTB [ 16 ].Set ( RB_P2Btn0,	Tb_P2Btn0  );
			ARY_RBTB [ 17 ].Set ( RB_P2Btn1,	Tb_P2Btn1  );
			ARY_RBTB [ 18 ].Set ( RB_P2Btn2,	Tb_P2Btn2  );
			ARY_RBTB [ 19 ].Set ( RB_P2Btn3,	Tb_P2Btn3  );
			ARY_RBTB [ 20 ].Set ( RB_P2Btn4,	Tb_P2Btn4  );
			ARY_RBTB [ 21 ].Set ( RB_P2Btn5,	Tb_P2Btn5  );
			ARY_RBTB [ 22 ].Set ( RB_P2Btn6,	Tb_P2Btn6  );
			ARY_RBTB [ 23 ].Set ( RB_P2Btn7,	Tb_P2Btn7  );

			//コントロール共通初期化
			foreach ( RBTB rbtb in ARY_RBTB )
			{
				rbtb.rb.PreviewKeyDown += new PreviewKeyDownEventHandler ( RB_PreviewKeyDown );
				rbtb.rb.CheckedChanged += new EventHandler ( RB_Seleceted );
			}

			//選択位置
//			rbPos = RB_P1Up;
			rbtbPos = ARY_RBTB [ 0 ];

#if false
			//タスク
			Action act = Thread;
			Task task = Task.Run ( act );
#endif

			//タイマ
			Timer timer = new Timer ();
//			timer.Tick += new EventHandler ( UpdateData );
			timer.Tick += (e,s)=>
			{
				//入力の更新と取得
				dxInput.Update ();
				DeviceInput di = dxInput.PushInput ();

				//入力があったらテキストボックスを更新して次へ
				if ( DeviceType.Other != di.Type )
				{
					rbtbPos.tb.Text = di.ToString();
					RBTB_Next ( rbtbPos );
					rbtbPos.rb.Checked = true;
				}
			};
			timer.Interval = 16;
			timer.Start ();
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

		//ラジオボタン選択時
		private void RB_Seleceted ( object sender, EventArgs e )
		{
			//発生元を変換
			RadioButton rb = (RadioButton)sender;

			//検索
			foreach ( RBTB rbtb in ARY_RBTB )
			{
				if ( rbtb.rb == rb )
				{
					//選択
					rbtbPos = rbtb;
					break;
				}
			}
		}


		//@info timerからでもstaticでないメンバ変数が変更可能かどうか
		//->	ラムダ式でアクセス可能

		//ラジオボタンを次へ
		private void RBTB_Next ( RBTB rbtb )
		{
			//全検索
			for ( int i = 0; i < RBTB_NUM; ++ i ) 
			{
				//該当したら
				if ( rbtb.rb == ARY_RBTB [ i ].rb )
				{
					//末尾の場合
					if ( i == RBTB_NUM - 1 )
					{
						rbtbPos = ARY_RBTB [ 0 ];
					}
					else
					{
						rbtbPos = ARY_RBTB [ i + 1 ];	// i + 1
					}
				}
			}
		}

		//フォルダ
		private void Btn_Folder_Click ( object sender, EventArgs e )
		{
			FormUtility.OpenCurrentDir ( );
		}

		//保存
		private void Btn_Save_Click ( object sender, EventArgs e )
		{
		}

	}
}
