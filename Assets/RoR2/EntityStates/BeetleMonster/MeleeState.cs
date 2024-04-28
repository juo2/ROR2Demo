using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BeetleMonster
{
	// Token: 0x02000460 RID: 1120
	public class MeleeState : EntityState
	{
		// Token: 0x06001403 RID: 5123 RVA: 0x00059410 File Offset: 0x00057610
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = 10f;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Melee1");
			}
			base.PlayCrossfade("Body", "Melee1", "Melee1.playbackRate", MeleeState.duration, 0.1f);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x000594EC File Offset: 0x000576EC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.attack.forceVector = (base.characterDirection ? (base.characterDirection.forward * MeleeState.forceMagnitude) : Vector3.zero);
				if (this.modelAnimator && this.modelAnimator.GetFloat("Melee1.hitBoxActive") > 0.5f)
				{
					this.attack.Fire(null);
				}
			}
			if (base.fixedAge >= MeleeState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040019B1 RID: 6577
		public static float duration = 3.5f;

		// Token: 0x040019B2 RID: 6578
		public static float damage = 10f;

		// Token: 0x040019B3 RID: 6579
		public static float forceMagnitude = 10f;

		// Token: 0x040019B4 RID: 6580
		private OverlapAttack attack;

		// Token: 0x040019B5 RID: 6581
		private Animator modelAnimator;
	}
}
