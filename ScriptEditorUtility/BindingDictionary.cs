using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;


namespace ScriptEditor
{
	//=============================================================
	// 表示用バインディングリストと検索用ディクショナリを持つデータ構造
	//	検索名であるNameを扱うインターフェースを持つクラスを対象としたジェネリクス
	//	< T > where T: IName
	//	主に名前で検索し、リストボックスに表示する項目を扱う
	//	追加・削除は２つのデータを同期する
	//=============================================================
	public interface IName
	{
		string Name { get; set; }
	}

	public class BindingDictionary < T > where T : IName
	{
		//----------------------------------------
		//メインデータ
		//	各種操作のときに同期する

		//表示用バインディングリスト
		private BindingList < T > BL_t { get; set; } = new BindingList < T > ();
		
		//検索用ディクショナリ
		private Dictionary < string, T > DCT_t { get; set; } = new Dictionary < string, T > ();
		//----------------------------------------

		//名前が存在するかどうか
		public bool ContainKey ( string name )
		{
			return DCT_t.ContainsKey ( name );
		}


		//追加
		public void Add ( T t )
		{
			//同一の値のとき例外が発生するので、事前に名前に追加する
			int i = 0;
			string name = t.Name;
			while ( DCT_t.ContainsKey ( name ) )
			{
				name = "script" + i.ToString ();
				++ i;
			}
			t.Name = name;

			//追加
			BL_t.Add ( t );
			DCT_t.Add ( name, t );
		}

		public T Get ( string name )
		{
			//※ ジェネリックにおける "default" は対象の型の既定値を返す
			// 参照型はnull、数値型は0
			// 構造体はすべてのメンバーに対し0またはnull
			return DCT_t.TryGetValue ( name, out T t ) ? t : default ( T );
		}

		public void RemoveAt ( int index )
		{
			if ( index < 0 || BL_t.Count <= index ) { return; }
			string name = BL_t [ index ].Name;
			BL_t.RemoveAt ( index );
			DCT_t.Remove ( name );
		}

		public void Remove ( string name )
		{
			BL_t.Remove ( DCT_t [ name ] );
			DCT_t.Remove ( name );
		}

		public void Clear ()
		{
			BL_t.Clear ();
			DCT_t.Clear ();
		}

		public BindingList < T > GetBindingList ()
		{
			return BL_t;
		}

		public Dictionary < string, T > GetDictionary ()
		{
			return DCT_t;
		}

		public void Insert ( string name, T t )
		{
			int i = BL_t.IndexOf ( t );
			BL_t.Insert ( i, t );
			DCT_t.Add ( name, t );
		}

		public int Count ()
		{
			Debug.Assert ( BL_t.Count == DCT_t.Count );
			return BL_t.Count;
		}

		public void Up ( int index )
		{
			int count = Count();
			if ( count < 2 ) { return; }
			if ( count <= index ) { return; }
			if ( index == 0 ) { return; }

			int prev_index = index - 1;	//1つ前の位置

			//------------------------------------
			//バインディングリスト内の位置を更新
			//１つ前の位置に自身をコピー
			BL_t.Insert ( prev_index, BL_t [ index ] );

			//元の位置の後を削除
			BL_t.RemoveAt ( index + 1 );

			//ディクショナリは変更無し
		}

		public void Down ( int index )
		{
			int count = Count();
			if ( count < 2 ) { return; }		//２つ未満
			if ( count <= index ) { return; }	//指定位置が個数以上
			if ( index == count - 1 ) { return; }	//末尾

			int next_index = index + 2;	//1つ次の位置

			//------------------------------------
			//バインディングリスト内の位置を更新
			//１つ次の位置に自身をコピー
			BL_t.Insert ( next_index, BL_t [ index ] );

			//元の位置の後を削除
			BL_t.RemoveAt ( index );

			//ディクショナリは変更無し
		}
	}
}

