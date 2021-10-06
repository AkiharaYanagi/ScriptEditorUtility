using System;
using System.Collections.Generic;
using System.Drawing;

namespace ScriptEditor
{
	using BD_Seq = BindingDictionary < Sequence >;
	using BL_Script = List < Script >;

	public partial class TestCharaData
	{
		//Stand
		public void MakeAction_Stand ( BD_Seq bd_act )
		{
			//Stand
			MakeScript ( (int)ENM_ACTION.Stand, 288 );

			BL_Script bl_scp = bd_act.Get ( (int)ENM_ACTION.Stand ).ListScript;

#if false
			for ( int i = 0; i < 128; ++ i )
			{
				bl_scp [ i ].Group = 1;
				bl_scp [ i ].ImgName = "Stand_00.png";
			}
			for ( int i = 128; i < 144; ++ i )
			{
				bl_scp [ i ].Group = 2;
				bl_scp [ i ].ImgName = "Stand_01.png";
			}
			for ( int i = 144; i < 272; ++ i )
			{
				bl_scp [ i ].Group = 3;
				bl_scp [ i ].ImgName = "Stand_02.png";
			}
			for ( int i = 272; i < 288; ++ i )
			{
				bl_scp [ i ].Group = 4;
				bl_scp [ i ].ImgName = "Stand_01.png";
			}
			for ( int i = 0; i < 288; ++ i )
			{
				bl_scp [ i ].Pos = new Point ( -250, -450 );
				bl_scp [ i ].BL_RutName.Add ( new TName ( ENM_RUT.地上移動.ToString() ) );
				bl_scp [ i ].BL_RutName.Add ( new TName ( ENM_RUT.地上通常技.ToString() ) );
			}
#endif
			_MakeAction_Part ( bl_scp, 0  , 128, 1, "Stand_00.png" );
			_MakeAction_Part ( bl_scp, 128, 144, 2, "Stand_01.png" );
			_MakeAction_Part ( bl_scp, 144, 272, 3, "Stand_02.png" );
			_MakeAction_Part ( bl_scp, 272, 288, 4, "Stand_01.png" );
		}

		private void _MakeAction_Part ( BL_Script bl_s, int start, int end, int group, string imgname )
		{
			for ( int i = start; i < end; ++ i )
			{
				bl_s [ i ].Group = group;
				bl_s [ i ].ImgName = imgname;

				bl_s [ i ].Pos = new Point ( -250, -450 );
				bl_s [ i ].BL_RutName.Add ( new TName ( ENM_RUT.地上移動.ToString() ) );
				bl_s [ i ].BL_RutName.Add ( new TName ( ENM_RUT.地上通常技.ToString() ) );
			}
		}
	}
}
