using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BeetleMonster
{
	// Token: 0x0200045D RID: 1117
	public class HeadbuttState : BaseState
	{
		// Token: 0x060013F8 RID: 5112 RVA: 0x00059140 File Offset: 0x00057340
		public override void OnEnter()
		{
			base.OnEnter();
			this.rootMotionAccumulator = base.GetModelRootMotionAccumulator();
			this.modelAnimator = base.GetModelAnimator();
			this.duration = HeadbuttState.baseDuration / this.attackSpeedStat;
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = HeadbuttState.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = HeadbuttState.hitEffectPrefab;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Headbutt");
			}
			Util.PlaySound(HeadbuttState.attackSoundString, base.gameObject);
			base.PlayCrossfade("Body", "Headbutt", "Headbutt.playbackRate", this.duration, 0.1f);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00059264 File Offset: 0x00057464
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.rootMotionAccumulator)
			{
				Vector3 vector = this.rootMotionAccumulator.ExtractRootMotion();
				if (vector != Vector3.zero && base.isAuthority && base.characterMotor)
				{
					base.characterMotor.rootMotion += vector;
				}
			}
			if (base.isAuthority)
			{
				this.attack.forceVector = (base.characterDirection ? (base.characterDirection.forward * HeadbuttState.forceMagnitude) : Vector3.zero);
				if (base.characterDirection && base.inputBank)
				{
					base.characterDirection.moveVector = base.inputBank.aimDirection;
				}
				if (this.modelAnimator && this.modelAnimator.GetFloat("Headbutt.hitBoxActive") > 0.5f)
				{
					this.attack.Fire(null);
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040019A5 RID: 6565
		public static float baseDuration = 3.5f;

		// Token: 0x040019A6 RID: 6566
		public static float damageCoefficient;

		// Token: 0x040019A7 RID: 6567
		public static float forceMagnitude = 16f;

		// Token: 0x040019A8 RID: 6568
		public static GameObject hitEffectPrefab;

		// Token: 0x040019A9 RID: 6569
		public static string attackSoundString;

		// Token: 0x040019AA RID: 6570
		private OverlapAttack attack;

		// Token: 0x040019AB RID: 6571
		private Animator modelAnimator;

		// Token: 0x040019AC RID: 6572
		private RootMotionAccumulator rootMotionAccumulator;

		// Token: 0x040019AD RID: 6573
		private float duration;
	}
}
