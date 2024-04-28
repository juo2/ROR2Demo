using System;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000216 RID: 534
	public class Disconnected : BaseBackpack
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x00026F4C File Offset: 0x0002514C
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00026F68 File Offset: 0x00025168
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.skillLocator.special.skillDef == this.superSkillDef)
			{
				this.outer.SetNextState(new OnlineSuper());
				return;
			}
			if (base.skillLocator.special.skillDef == this.cryoSkillDef)
			{
				this.outer.SetNextState(new OnlineCryo());
			}
		}

		// Token: 0x04000AF8 RID: 2808
		[SerializeField]
		public SkillDef superSkillDef;

		// Token: 0x04000AF9 RID: 2809
		[SerializeField]
		public SkillDef cryoSkillDef;

		// Token: 0x04000AFA RID: 2810
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000AFB RID: 2811
		[SerializeField]
		public string animationStateName;
	}
}
