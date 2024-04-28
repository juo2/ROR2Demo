using System;
using RoR2.Skills;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x02000109 RID: 265
	public class EnterSwingMelee : BaseState, SteppedSkillDef.IStepSetter
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0001429A File Offset: 0x0001249A
		void SteppedSkillDef.IStepSetter.SetStep(int i)
		{
			this.step = i;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000142A3 File Offset: 0x000124A3
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write((byte)this.step);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000142B9 File Offset: 0x000124B9
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.step = (int)reader.ReadByte();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000142D0 File Offset: 0x000124D0
		public override void OnEnter()
		{
			base.OnEnter();
			switch (this.step)
			{
			case 0:
				this.outer.SetNextState(new SwingMelee1());
				return;
			case 1:
				this.outer.SetNextState(new SwingMelee2());
				return;
			case 2:
				this.outer.SetNextState(new SwingMelee3());
				return;
			default:
				this.outer.SetNextState(new SwingMelee1());
				return;
			}
		}

		// Token: 0x04000555 RID: 1365
		public int step;
	}
}
