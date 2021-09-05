
namespace ScriptEditor
{
	public class TName : IName
	{
		public string Name { get; set; } = "TName";

		public TName ()
		{
		}

		public TName ( string n )
		{
			Name = n;
		}
	}
}
