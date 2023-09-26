using System;


namespace ScriptEditor
{
	public class EditEfGnrt
	{
		//親
		public EditCompend EditCompend { get; set; } = null;

		//対象のエフェクト生成オブジェクト
		//対象が存在しないときに空で生成して、null参照エラーの無いようにする
		public EffectGenerate SelectedEfGnrt { get; set; } = new EffectGenerate();


		//環境設定
		public void SetEnviron ( EditCompend ec )
		{
			EditCompend = ec;
		}

		public void Assosiate ( Script scp )
		{
			if ( scp.BD_EfGnrt.Count () > 0 )
			{
				SelectedEfGnrt = scp.BD_EfGnrt [ 0 ];
			}
			else
			{
				SelectedEfGnrt = new EffectGenerate ();
			}
		}

		public void Add ( EffectGenerate efgnrt )
		{
			Script scp = EditCompend.SelectedScript;
			scp.BD_EfGnrt.Add ( efgnrt );
		}
	}
}
