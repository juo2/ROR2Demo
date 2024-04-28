using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ImpMonster
{
	// Token: 0x0200030A RID: 778
	public class Backstab : BaseState
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003B350 File Offset: 0x00039550
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Backstab.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = Backstab.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = Backstab.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Backstab");
			}
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture", "Backstab", "Backstab.playbackRate", this.duration);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003B49C File Offset: 0x0003969C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator && this.modelAnimator.GetFloat("Bite.hitBoxActive") > 0.1f)
			{
				if (!this.hasBit)
				{
					EffectManager.SimpleMuzzleFlash(Backstab.biteEffectPrefab, base.gameObject, "MuzzleMouth", true);
					this.hasBit = true;
				}
				this.attack.forceVector = base.transform.forward * Backstab.forceMagnitude;
				this.attack.Fire(null);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001138 RID: 4408
		public static float baseDuration = 3.5f;

		// Token: 0x04001139 RID: 4409
		public static float damageCoefficient = 4f;

		// Token: 0x0400113A RID: 4410
		public static float forceMagnitude = 16f;

		// Token: 0x0400113B RID: 4411
		public static float radius = 3f;

		// Token: 0x0400113C RID: 4412
		public static GameObject hitEffectPrefab;

		// Token: 0x0400113D RID: 4413
		public static GameObject biteEffectPrefab;

		// Token: 0x0400113E RID: 4414
		private OverlapAttack attack;

		// Token: 0x0400113F RID: 4415
		private Animator modelAnimator;

		// Token: 0x04001140 RID: 4416
		private float duration;

		// Token: 0x04001141 RID: 4417
		private bool hasBit;
	}
}
