using System.Drawing;
using System.Collections.Generic;


namespace ScriptEditor
{
	using L_Rct = List < Rectangle >;

	//---------------------------------------------------------------------
	//枠リストを編集する
	public class EditListRect
	{
		//対象
		public L_Rct L_Rct { get; set; } = new L_Rct ();

		//選択
		public int SelectedIndex { get; set; } = 0;
		public Rectangle SelectedRect { get; set; } = new Rectangle ();

		//関連付け
		public void Assosiate ( L_Rct lrct )
		{
			L_Rct = lrct;
			Select ( 0 );
		}

		//選択(範囲チェック)
		public void Select ( int index )
		{
			if ( index < 0 ) { return; }
			if ( L_Rct.Count <= index ) { return; }

			SelectedRect = L_Rct [ index ];
			SelectedIndex = index;
		}

		//値設定
		public void SetRect ( Rectangle r )
		{
			if ( SelectedIndex < 0 ) { return; }
			if ( L_Rct.Count <= SelectedIndex ) { return; }

			L_Rct [ SelectedIndex ] = r;
		}
	}

	//---------------------------------------------------------------------
	//すべての枠リストを編集する
	public class EditAllRect
	{
		public EditListRect L_CRect { get; set; } = new EditListRect ();
		public EditListRect L_HRect { get; set; } = new EditListRect ();
		public EditListRect L_ARect { get; set; } = new EditListRect ();
		public EditListRect L_ORect { get; set; } = new EditListRect ();

		public void Assosiate ( Script scp )
		{
			L_CRect.Assosiate ( scp.ListCRect );
			L_HRect.Assosiate ( scp.ListHRect );
			L_ARect.Assosiate ( scp.ListARect );
			L_ORect.Assosiate ( scp.ListORect );
		}
	}
}
