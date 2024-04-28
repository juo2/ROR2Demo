using System;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000211 RID: 529
	public abstract class BaseOnline : BaseBackpack
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x00026E42 File Offset: 0x00025042
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			this.animator = base.GetModelAnimator();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00026E68 File Offset: 0x00025068
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.skillLocator.special.skillDef != this.requiredSkillDef)
			{
				this.outer.SetNextState(new Disconnected());
				return;
			}
			if (this.animator)
			{
				float num = base.skillLocator.special.CalculateFinalRechargeInterval();
				float value = 0f;
				if (num > 0f)
				{
					value = base.skillLocator.special.cooldownRemaining / num;
				}
				this.animator.SetFloat(this.cooldownParamName, value);
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00026EF5 File Offset: 0x000250F5
		public override void OnExit()
		{
			if (this.animator)
			{
				this.animator.SetFloat(this.cooldownParamName, 0f);
			}
			base.OnExit();
		}

		// Token: 0x04000AF3 RID: 2803
		[SerializeField]
		public SkillDef requiredSkillDef;

		// Token: 0x04000AF4 RID: 2804
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000AF5 RID: 2805
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000AF6 RID: 2806
		[SerializeField]
		public string cooldownParamName;

		// Token: 0x04000AF7 RID: 2807
		private Animator animator;
	}
}
