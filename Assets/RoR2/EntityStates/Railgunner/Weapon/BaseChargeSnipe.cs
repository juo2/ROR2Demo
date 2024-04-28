using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001EF RID: 495
	public abstract class BaseChargeSnipe : BaseState, IBaseWeaponState
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x00025138 File Offset: 0x00023338
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Backpack");
			if (entityStateMachine)
			{
				entityStateMachine.SetNextState(this.InstantiateBackpackState());
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000251A1 File Offset: 0x000233A1
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool CanScope()
		{
			return false;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x060008CC RID: 2252
		protected abstract EntityState InstantiateBackpackState();

		// Token: 0x04000A58 RID: 2648
		private const string backpackStateMachineName = "Backpack";

		// Token: 0x04000A59 RID: 2649
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000A5A RID: 2650
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000A5B RID: 2651
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000A5C RID: 2652
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000A5D RID: 2653
		private float duration;
	}
}
