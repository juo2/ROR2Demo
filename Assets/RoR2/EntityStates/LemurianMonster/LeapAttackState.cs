using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LemurianMonster
{
	// Token: 0x020002D6 RID: 726
	public class LeapAttackState : BaseState
	{
		// Token: 0x06000CEC RID: 3308 RVA: 0x00036768 File Offset: 0x00034968
		public override void OnEnter()
		{
			base.OnEnter();
			this.rootMotionAccumulator = base.GetModelRootMotionAccumulator();
			this.modelAnimator = base.GetModelAnimator();
			this.duration = LeapAttackState.baseDuration / this.attackSpeedStat;
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = LeapAttackState.damage;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "LeapAttack");
			}
			base.PlayCrossfade("Body", "LeapAttack", "LeapAttack.playbackRate", this.duration, 0.1f);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00036864 File Offset: 0x00034A64
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
				this.attack.forceVector = (base.characterDirection ? (base.characterDirection.forward * LeapAttackState.forceMagnitude) : Vector3.zero);
				if (this.modelAnimator && this.modelAnimator.GetFloat("LeapAttack.hitBoxActive") > 0.5f)
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

		// Token: 0x06000CEE RID: 3310 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000FD4 RID: 4052
		public static float baseDuration = 3.5f;

		// Token: 0x04000FD5 RID: 4053
		public static float damage = 10f;

		// Token: 0x04000FD6 RID: 4054
		public static float forceMagnitude = 16f;

		// Token: 0x04000FD7 RID: 4055
		private OverlapAttack attack;

		// Token: 0x04000FD8 RID: 4056
		private Animator modelAnimator;

		// Token: 0x04000FD9 RID: 4057
		private RootMotionAccumulator rootMotionAccumulator;

		// Token: 0x04000FDA RID: 4058
		private float duration;
	}
}
