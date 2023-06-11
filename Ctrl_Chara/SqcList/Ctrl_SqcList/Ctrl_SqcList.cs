using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Ctrl_SqcList : UserControl
	{
		//----------------------------------------------------------------------
		//メインデータ
		private SqcListData Data = new SqcListData ();

		//対象データ
		private Compend Cmpd = null;

		//データ編集
		private EditSqcListData EditData = new EditSqcListData ();

		//設定ファイル
		private Ctrl_Settings Ctrl_Stgs { get; set; } = new Ctrl_Settings ();

		//新規オブジェクト作成
		public System.Func < Sequence > New_Object = ()=>new Sequence();

		//アクション/エフェクト 切替
		public enum CTRL_SQC { ACTION, EFFECT };
		private CTRL_SQC flag_sqc_derived = CTRL_SQC.ACTION;

		//----------------------------------------------------------------------

		//コンストラクタ
		public Ctrl_SqcList ()
		{
			//コンポーネント初期化
			InitializeComponent ();
			this.Dock = DockStyle.Fill;
		}

		//環境設定
		public void SetEnviroment ( CTRL_SQC cs, System.Func < Sequence > new_object, Ctrl_Settings stgs )
		{
			//シークエンス継承フラグ(アクション、エフェクト)
			flag_sqc_derived = cs;
			switch ( flag_sqc_derived )
			{
			case CTRL_SQC.ACTION: 
				ctrl_ImageTable1.SetAction (); 
				label1.Text = "[アクション]";
				ELB_Sqc.FilePath = stgs.File_ActionList;
				break;
			case CTRL_SQC.EFFECT: 
				ctrl_ImageTable1.SetEffect ();
				label1.Text = "[エフェクト]";
				ELB_Sqc.FilePath = stgs.File_EffectList;
				break;
			default: break;
			}			
			
			//新規オブジェクトの確保Func New()を指定
			New_Object = new_object;
			ELB_Sqc.New_T = ()=>new SequenceData(){Sqc=new_object()};
			
			//ディレクトリ設定
			Ctrl_Stgs = stgs;
			ctrl_ImageTable1.SetEnviroment ( cs, EditData, stgs );
		}

		//データ設置
		public void SetCharaData ( Compend cmpd )
		{
			Cmpd = cmpd;
			Data.SetData ( cmpd );
			EditData.Compend = Cmpd;
		}

		//データ読込
		public void LoadData ()
		{
			switch ( flag_sqc_derived )
			{
			case CTRL_SQC.ACTION: 
				ELB_Sqc.LoadData ( Ctrl_Stgs.File_ActionList );
				break;
			case CTRL_SQC.EFFECT: 
				ELB_Sqc.LoadData ( Ctrl_Stgs.File_EffectList );
				break;
			default: break;
			}

			ApplyData ();

			//イメージ
			ctrl_ImageTable1.LoadImage ();

			//スクリプト内イメージ指定名
			EditData.ResetImageName ();

			//更新
			this.Refresh ();
		}

		//描画
		protected override void OnPaint ( PaintEventArgs e )
		{
			ctrl_ImageTable1.Invalidate ();
			ELB_Sqc.Invalidate ();
			base.OnPaint ( e ); 
		}


		//データ更新
		public void UpdateData ()
		{
			ctrl_ImageTable1.UpdateData ();

			
			ELB_Sqc._UpdateData ();


			ELB_Sqc.Refresh ();
		}

		//イメージのみ再読み込み
		public void UpdateImage ()
		{
			ctrl_ImageTable1.UpdateImage ();
		}

		//データ適用
		public void ApplyData ()
		{
			//SqcDtからCompendに戻す
			//@info Sequenceで生成しているとき、Actionにアップキャストできない問題
			//-> New_Action()をデリゲートで指定
			EditData.ApplyData ();
		}

	}
}
