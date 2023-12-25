using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ScriptEditor
{
	public static class ListUtility
	{
		public static T DeepClone < T > ( this T src )
		{
			using ( MemoryStream ms = new MemoryStream () )
			{
				BinaryFormatter bf = new BinaryFormatter ();
				bf.Serialize ( ms, src );
				ms.Seek ( 0, SeekOrigin.Begin );
				return (T)bf.Deserialize ( ms );
			}
		}
	}
}
