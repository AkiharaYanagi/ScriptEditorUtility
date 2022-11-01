using System.Drawing;

namespace ScriptEditor
{
	//---------------------------------------------------------------------
	//	スクリプト内でエフェクトを生成するクラス
	//---------------------------------------------------------------------
	
	public class EffectGenerate : IName
	{
		public string Name { get; set; } = "EfGnrt";    //エフェクトジェネレート名

		public string EfName { get; set; } = "Ef";	//対象のエフェクト名
		public Point Pt { get; set; } = new Point ( 0, 0 );	//位置
		public int Z = 50;							//Z位置 実行時100分の１でfloat扱い
		public bool Gnrt { get; set; } = false;		//生成(または非生成で繰返)
		public bool Loop { get; set; } = false;		//ループ
		public bool Sync { get; set; } = false;		//位置同期

		public EffectGenerate ()
		{
		}

	}
}
