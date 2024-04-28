using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000447 RID: 1095
	public class SprintBash : BasicMeleeAttack
	{
		// Token: 0x06001399 RID: 5017 RVA: 0x0005743D File Offset: 0x0005563D
		protected override void PlayAnimation()
		{
			base.PlayCrossfade("FullBody Override", "SprintBash", "SprintBash.playbackRate", this.duration, 0.05f);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00057460 File Offset: 0x00055660
		public override void OnEnter()
		{
			base.OnEnter();
			AimAnimator aimAnimator = base.GetAimAnimator();
			if (aimAnimator)
			{
				aimAnimator.enabled = true;
			}
			if (base.characterDirection)
			{
				base.characterDirection.forward = base.inputBank.aimDirection;
			}
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000574AC File Offset: 0x000556AC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.inputBank && base.skillLocator && base.skillLocator.utility.IsReady() && base.inputBank.skill3.justPressed)
			{
				base.skillLocator.utility.ExecuteIfReady();
				return;
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00057518 File Offset: 0x00055718
		public override void OnExit()
		{
			Transform transform = base.FindModelChild("SpinnyFX");
			if (transform)
			{
				transform.gameObject.SetActive(false);
			}
			base.PlayCrossfade("FullBody Override", "BufferEmpty", 0.1f);
			base.OnExit();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00057560 File Offset: 0x00055760
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge <= SprintBash.durationBeforePriorityReduces)
			{
				return InterruptPriority.PrioritySkill;
			}
			return InterruptPriority.Skill;
		}

		// Token: 0x04001909 RID: 6409
		public static float durationBeforePriorityReduces;
	}
}
