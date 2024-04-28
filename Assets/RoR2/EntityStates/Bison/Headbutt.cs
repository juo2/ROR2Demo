using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Bison
{
	// Token: 0x02000455 RID: 1109
	public class Headbutt : BaseState
	{
		// Token: 0x060013D2 RID: 5074 RVA: 0x0005851C File Offset: 0x0005671C
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			this.animator = modelTransform.GetComponent<Animator>();
			this.headbuttDuration = Headbutt.baseHeadbuttDuration / this.attackSpeedStat;
			Util.PlaySound(Headbutt.attackSoundString, base.gameObject);
			base.PlayCrossfade("Body", "Headbutt", "Headbutt.playbackRate", this.headbuttDuration, 0.2f);
			base.characterMotor.moveDirection = Vector3.zero;
			base.characterDirection.moveVector = base.characterDirection.forward;
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = Headbutt.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = Headbutt.hitEffectPrefab;
			this.attack.forceVector = Vector3.up * Headbutt.upwardForceMagnitude;
			this.attack.pushAwayForce = Headbutt.awayForceMagnitude;
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Headbutt");
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00058684 File Offset: 0x00056884
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.animator && this.animator.GetFloat("Headbutt.hitBoxActive") > 0.5f)
			{
				if (NetworkServer.active)
				{
					this.attack.Fire(null);
				}
				if (base.isAuthority && !this.hasAttacked)
				{
					this.hasAttacked = true;
				}
			}
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch > this.headbuttDuration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001967 RID: 6503
		public static float baseHeadbuttDuration;

		// Token: 0x04001968 RID: 6504
		public static float damageCoefficient;

		// Token: 0x04001969 RID: 6505
		public static string attackSoundString;

		// Token: 0x0400196A RID: 6506
		public static GameObject hitEffectPrefab;

		// Token: 0x0400196B RID: 6507
		public static float upwardForceMagnitude;

		// Token: 0x0400196C RID: 6508
		public static float awayForceMagnitude;

		// Token: 0x0400196D RID: 6509
		private float headbuttDuration;

		// Token: 0x0400196E RID: 6510
		private float stopwatch;

		// Token: 0x0400196F RID: 6511
		private OverlapAttack attack;

		// Token: 0x04001970 RID: 6512
		private Animator animator;

		// Token: 0x04001971 RID: 6513
		private bool hasAttacked;
	}
}
