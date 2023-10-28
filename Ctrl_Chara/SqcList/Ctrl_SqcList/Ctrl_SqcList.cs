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

			//手動でコントロールの追加
			LoadCtrl ();
		}

		//環境設定
		public void SetEnviroment ( CTRL_SQC cs, Ctrl_Settings stgs )
		{
			//シークエンス継承フラグ(アクション、エフェクト)
			flag_sqc_derived = cs;
			switch ( flag_sqc_derived )
			{
			case CTRL_SQC.ACTION: 
				ctrl_ImageTable1.SetAction (); 
				label1.Text = "[アクション]";
				ELB_Sqc.New_T = ()=>new SequenceData( ()=>new Action() );
				ELB_Sqc.FilePath = stgs.File_ActionList;
				break;
			case CTRL_SQC.EFFECT: 
				ctrl_ImageTable1.SetEffect ();
				label1.Text = "[エフェクト]";
				ELB_Sqc.New_T = ()=>new SequenceData( ()=>new Effect() );
				ELB_Sqc.FilePath = stgs.File_EffectList;
				break;
			default: break;
			}			
			
			//ディレクトリ設定
			Ctrl_Stgs = stgs;
			ctrl_ImageTable1.SetEnviroment ( cs, EditData, stgs );

			//@info キャラ内のイメージにはシークエンス番号が無いのでフォルダから指定する
			ctrl_ImageTable1.LoadImage ();
		}

		//対象データ設置
		public void SetCharaData ( Chara ch )
		{
		}

		//コンペンド指定
		public void SetCompend ( Compend cmpd )
		{
			Cmpd = cmpd;
			Data.SetData ( cmpd );
			EditData.Compend = Cmpd;

			ctrl_ImageTable1.SetCompend ( cmpd );
		}

		//コンペンド ( ビヘイビア / ガーニッシュ ) 選択
		public void ChangeCompend ( Compend cmpd )
		{

		}

		//プレデータ読込
		public void LoadPreData ()
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
			UpdateData ();
		}

		//関連付け
		public void Assosiate ()
		{
			//シークエンスリストにはスクリプトの選択による関連付けは存在しない
		}

		//描画の要求
		public void Disp ()
		{
			ctrl_ImageTable1.Disp ();
			ELB_Sqc.Invalidate ();
			this.Invalidate ();
		}

		//描画
		protected override void OnPaint ( PaintEventArgs e )
		{
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
