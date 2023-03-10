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
			script.BtlPrm.Vel = vel;
			script.BtlPrm.Acc = acc;
			script.BtlPrm.Power = power;
			return script;
		}
	}
}