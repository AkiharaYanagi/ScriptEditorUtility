
namespace ScriptEditor
{
	public class TName : IName
	{
		public string Name { get; set; } = "TName";

		public TName ()
		{
		}

		public TName ( object ob )
		{
			Name = ob.ToString ();
		}

		public TName ( string n )
		{
			Name = n;
		}
	}
}
