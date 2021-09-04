using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ScriptEditor
{
	//================================================
	//◆アトリビュート
	//<Element Attribute.Name="Attribute.Value"></Element>
	//
	//Element内の属性を表す
	//名前と値を持つ
	//================================================
	public class Attribute
	{
		public string Name { set; get; }
		public string Value { set; get; }

		public Attribute ( string name, string strValue )
		{
			Name = name;
			Value = strValue;
		}

		public void Print ()
		{
			Console.Write ( "\t( " + Name + " = " + Value + " )\n" );
		}
	}


	//================================================
	//◆エレメント
	//<Element Attribute.Name="Attribute.Value">
	//	<Elem0> ... </Elem0>
	//</Element>
	//
	//<タグ>で囲まれた名前と構造
	//AttributeとElementを持つことができる
	//================================================
	public class Element
	{
		public string Name { set; get; }
		public List<Attribute> Attributes { get; } = new List<Attribute> ();

		public void AddAttribute ( Attribute attribute )
		{
			Attributes.Add ( attribute );
		}
		public List<Element> Elements { get; } = new List<Element> ();


		//コンストラクタ
		public Element () { }
		public Element ( string name )
		{
			Name = name;
		}
		public void AddElement ( Element element )
		{
			Elements.Add ( element );
		}
		public Element Parent { set; get; }


		public void Print ()
		{
			Console.Write ( Name + "\n" );
			foreach ( Attribute attribute in Attributes )
			{
				attribute.Print ();
			}
			foreach ( Element element in Elements )
			{
				element.Print ();
			}
		}
	}


	//------------------------------------------------------------
	//◆ドキュメント
	//	データのまとまり
	//	木構造をなすエレメントのルートを持つ
	//------------------------------------------------------------
	public class Document
	{
		public Element Root { get; } = new Element ( "root" );

		//読取状態
		enum STATE
		{
			START,
			ELEMENT_START,
			ELEMENT,
			ELEMENT_END,
			ATTRIBUTE_NAME_START,
			ATTRIBUTE_NAME_END,
			ATTRIBUTE_VALUE_START,
			ATTRIBUTE_VALUE,
			ATTRIBUTE_VALUE_END,
			ELEMENT_CLOSING,
		}

		//コンストラクタ
		//引数：テキストファイル名
		public Document ( string fileName )
		{
			//ファイルが存在しないとき
			if ( ! File.Exists ( fileName ) ) { return; }

			//ストリームの作成
			StreamReader streamReader = new StreamReader ( fileName, Encoding.UTF8 );
			DocumentFromStream ( streamReader );
		}

		//コンストラクタ
		//引数：ストリーム
		public Document ( Stream stream )
		{
			StreamReader streamReader = new StreamReader ( stream, Encoding.UTF8 );
			DocumentFromStream ( streamReader );
		}

		//ストリームから読込
		public void DocumentFromStream ( StreamReader strmRdr )
		{
			string str;
			string strElementName = "";
			string strAttributeName = "";
			string strAttributeValue = "";
			STATE mode = STATE.START;

			Element element = Root;
			Element nextElement;

			while ( ( str = strmRdr.ReadLine () ) != null )
			{
				//1文字ずつ読込
				for ( int i = 0; i < str.Length; ++i )
				{
					switch ( mode )
					{
					case STATE.START:
						if ( str[i] == '<' )
						{
//							Console.Write ( "START\n" );
							mode = STATE.ELEMENT_START;
						}
						break;

					case STATE.ELEMENT_START:
						if ( str[i] == '/' )
						{
							element = element.Parent;
//							Console.Write ( "ELEMENT_START if /\n" );
							mode = STATE.ELEMENT_CLOSING;
						}
						else
						{
							//新規生成
							nextElement = new Element ();
							element.AddElement ( nextElement );
							nextElement.Parent = element;

							element = nextElement;

//							Console.Write ( "ELEMENT_START new\n" );
							mode = STATE.ELEMENT;

							//最初の一文字
							strElementName += str[i];
						}
						break;

					case STATE.ELEMENT:
						if ( str[i] == ' ' )
						{
							element.Name = strElementName;
//							Console.Write ( "ELEMENT.Name: " + strElementName + "\n" );
							strElementName = "";

							mode = STATE.ATTRIBUTE_NAME_START;
						}
						else if ( str[i] == '>' )
						{
							//アトリビュートなしでエレメント終了
							mode = STATE.ELEMENT_END;
						}
						else
						{	//通常文字列
							//							Console.Write ( str[i] );
							strElementName += str[i];
						}
						break;

					case STATE.ATTRIBUTE_NAME_START:
						//						Console.Write ( "ATTRIBUTE_NAME_START\n" );
						if ( str[i] == '=' )
						{
							mode = STATE.ATTRIBUTE_VALUE_START;
						}
						else
						{
							strAttributeName += str[i];
						}
						break;

					case STATE.ATTRIBUTE_VALUE_START:
						//						Console.Write ( "ATTRIBUTE_VALUE_START\n" );
						if ( str[i] == '\"' )
						{
							mode = STATE.ATTRIBUTE_VALUE;
						}
						break;

					case STATE.ATTRIBUTE_VALUE:
						//						Console.Write ( "ATTRIBUTE_VALUE\n" );
						if ( str[i] == '\"' )
						{
							mode = STATE.ATTRIBUTE_VALUE_END;
						}
						else
						{
							strAttributeValue += str[i];
						}
						break;


					case STATE.ATTRIBUTE_VALUE_END:
						if ( str[i] == ' ' )
						{
//							Console.Write ( "ATTRIBUTE_VALUE_END new:" + strAttributeName + ", " + strAttributeValue + "\n" );
							//アトリビュートをエレメントに加える
							Attribute attribute = new Attribute ( strAttributeName, strAttributeValue );
							element.AddAttribute ( attribute );
							strAttributeName = "";
							strAttributeValue = "";

							mode = STATE.ATTRIBUTE_NAME_START;
						}
						else if ( str[i] == '>' )
						{
//							Console.Write ( "ATTRIBUTE_VALUE_END new (" + strAttributeName + ", " + strAttributeValue +")\n" );
							//アトリビュートをエレメントに加える
							Attribute attribute = new Attribute ( strAttributeName, strAttributeValue );
							element.AddAttribute ( attribute );
							strAttributeName = "";
							strAttributeValue = "";

							mode = STATE.ELEMENT_END;
						}
						break;

					case STATE.ELEMENT_END:
						if ( str[i] == '<' )
						{
							mode = STATE.ELEMENT_START;
						}
						break;

					case STATE.ELEMENT_CLOSING:
						if ( str[i] == '>' )
						{
							mode = STATE.START;
						}
						break;

					default:
						break;
					}
				}
			}

			strmRdr.Close ();

//			Console.Write ( "\nroot.Print()\n" );
//			root.Print ();
		}

	}

}
