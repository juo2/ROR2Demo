using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000210 RID: 528
	public abstract class BaseExpired : BaseBackpack
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x00026DC0 File Offset: 0x00024FC0
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			if (this.forceShieldRegen)
			{
				CharacterBody characterBody = base.characterBody;
				if (characterBody == null)
				{
					return;
				}
				HealthComponent healthComponent = characterBody.healthComponent;
				if (healthComponent == null)
				{
					return;
				}
				healthComponent.ForceShieldRegen();
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00026E13 File Offset: 0x00025013
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(this.InstantiateNextState());
			}
		}

		// Token: 0x0600095B RID: 2395
		protected abstract EntityState InstantiateNextState();

		// Token: 0x0600095C RID: 2396 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000AEE RID: 2798
		[SerializeField]
		public float duration;

		// Token: 0x04000AEF RID: 2799
		[SerializeField]
		public bool forceShieldRegen;

		// Token: 0x04000AF0 RID: 2800
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000AF1 RID: 2801
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000AF2 RID: 2802
		[SerializeField]
		public string animationPlaybackRateParam;
	}
}
