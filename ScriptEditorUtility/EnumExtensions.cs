using System;

namespace ScriptEditor
{
	//===========================================================================
	// ◆ Enum拡張メソッド
	//
	//	static　T Func ( this T src ){} のようにthisを指定して定義すると t.Func() の記述で呼び出せる
	//
	//	Enumは where T : struct で受けると値型としてジェネリック型制約される
	//
	//	GetValues()で指定Enum型の配列を取得してインデックスを操作する
	//
	//===========================================================================
	public static class EnumExtensions
	{
		//次のEnumを返す
		public static T Next < T > ( this T src ) where T : struct
		{
			if ( ! typeof ( T ).IsEnum ) { throw new ArgumentException ( "Argument {0} is not Enum", typeof(T).FullName ); }

			T[] ary = (T[])Enum.GetValues ( src.GetType () );
			int j = Array.IndexOf ( ary, src ) + 1;
			return ( ary.Length == j ) ? ary[0] : ary[j]; 
		}

		//前のEnumを返す
		public static T Prev < T > ( this T src ) where T : struct
		{
			if ( ! typeof ( T ).IsEnum ) { throw new ArgumentException ( "Argument {0} is not Enum", typeof(T).FullName ); }

			T[] ary = (T[])Enum.GetValues ( src.GetType () );
			int j = Array.IndexOf ( ary, src ) - 1;
			return ( -1 == j ) ? ary[j] : ary[0]; 
		}
	}
	//===========================================================================

}
