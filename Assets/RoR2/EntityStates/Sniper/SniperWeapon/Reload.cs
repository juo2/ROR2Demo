using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Sniper.SniperWeapon
{
	// Token: 0x020001C6 RID: 454
	public class Reload : BaseState
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x000227E8 File Offset: 0x000209E8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Reload.baseDuration / this.attackSpeedStat;
			this.reloadTime = this.duration * Reload.reloadTimeFraction;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture", "PrepBarrage", "PrepBarrage.playbackRate", this.duration);
			}
			if (base.skillLocator)
			{
				GenericSkill secondary = base.skillLocator.secondary;
				if (secondary)
				{
					this.scopeStateMachine = secondary.stateMachine;
				}
			}
			if (base.isAuthority && this.scopeStateMachine)
			{
				this.scopeStateMachine.SetNextState(new LockSkill());
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000228A8 File Offset: 0x00020AA8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.reloaded && base.fixedAge >= this.reloadTime)
			{
				if (base.skillLocator)
				{
					GenericSkill primary = base.skillLocator.primary;
					if (primary)
					{
						primary.Reset();
						Util.PlaySound(Reload.soundString, base.gameObject);
					}
				}
				this.reloaded = true;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00022932 File Offset: 0x00020B32
		public override void OnExit()
		{
			if (base.isAuthority && this.scopeStateMachine)
			{
				this.scopeStateMachine.SetNextStateToMain();
			}
			base.OnExit();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000996 RID: 2454
		public static float baseDuration = 1f;

		// Token: 0x04000997 RID: 2455
		public static float reloadTimeFraction = 0.75f;

		// Token: 0x04000998 RID: 2456
		public static string soundString = "";

		// Token: 0x04000999 RID: 2457
		private float duration;

		// Token: 0x0400099A RID: 2458
		private float reloadTime;

		// Token: 0x0400099B RID: 2459
		private Animator modelAnimator;

		// Token: 0x0400099C RID: 2460
		private bool reloaded;

		// Token: 0x0400099D RID: 2461
		private EntityStateMachine scopeStateMachine;
	}
}
