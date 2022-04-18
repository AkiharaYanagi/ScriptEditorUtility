using System.Drawing;


namespace ScriptEditor
{
	public partial class TestCharaData
	{
		public void SetCharaData ( Chara chara )
		{
		}

		private Action NewAction ( Chara chara, string name, int nextIndex, int category, int posture, int balance )
		{
			Action action = new Action
			{
				Name = name,
				Category = ( ActionCategory ) category,
				Posture = ( ActionPosture ) posture,
			};
			return action;
		}

		private Script NewScript ( Action action, int imgIndex, Point pt, Point vel, Point acc, int power )
		{
			Script script = new Script
			{
				Pos = pt,
			};
			script.Param_Btl.Vel = vel;
			script.Param_Btl.Acc = acc;
			script.Param_Btl.Power = power;
			return script;
		}
	}
}