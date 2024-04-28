using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bison
{
	// Token: 0x02000453 RID: 1107
	public class Charge : BaseState
	{
		// Token: 0x060013C8 RID: 5064 RVA: 0x000580B4 File Offset: 0x000562B4
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.childLocator = this.animator.GetComponent<ChildLocator>();
			FootstepHandler component = this.animator.GetComponent<FootstepHandler>();
			if (component)
			{
				this.baseFootstepString = component.baseFootstepString;
				component.baseFootstepString = Charge.footstepOverrideSoundString;
			}
			Util.PlaySound(Charge.startSoundString, base.gameObject);
			base.PlayCrossfade("Body", "ChargeForward", 0.2f);
			this.ResetOverlapAttack();
			this.SetSprintEffectActive(true);
			if (this.childLocator)
			{
				this.sphereCheckTransform = this.childLocator.FindChild("SphereCheckTransform");
			}
			if (!this.sphereCheckTransform && base.characterBody)
			{
				this.sphereCheckTransform = base.characterBody.coreTransform;
			}
			if (!this.sphereCheckTransform)
			{
				this.sphereCheckTransform = base.transform;
			}
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000581A9 File Offset: 0x000563A9
		private void SetSprintEffectActive(bool active)
		{
			if (this.childLocator)
			{
				Transform transform = this.childLocator.FindChild("SprintEffect");
				if (transform == null)
				{
					return;
				}
				transform.gameObject.SetActive(active);
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000581D8 File Offset: 0x000563D8
		public override void OnExit()
		{
			base.OnExit();
			base.characterMotor.moveDirection = Vector3.zero;
			Util.PlaySound(Charge.endSoundString, base.gameObject);
			Util.PlaySound("stop_bison_charge_attack_loop", base.gameObject);
			this.SetSprintEffectActive(false);
			FootstepHandler component = this.animator.GetComponent<FootstepHandler>();
			if (component)
			{
				component.baseFootstepString = this.baseFootstepString;
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00058244 File Offset: 0x00056444
		public override void FixedUpdate()
		{
			this.targetMoveVector = Vector3.ProjectOnPlane(Vector3.SmoothDamp(this.targetMoveVector, base.inputBank.aimDirection, ref this.targetMoveVectorVelocity, Charge.turnSmoothTime, Charge.turnSpeed), Vector3.up).normalized;
			base.characterDirection.moveVector = this.targetMoveVector;
			Vector3 forward = base.characterDirection.forward;
			float value = this.moveSpeedStat * Charge.chargeMovementSpeedCoefficient;
			base.characterMotor.moveDirection = forward * Charge.chargeMovementSpeedCoefficient;
			this.animator.SetFloat("forwardSpeed", value);
			if (base.isAuthority && this.attack.Fire(null))
			{
				Util.PlaySound(Charge.headbuttImpactSound, base.gameObject);
			}
			if (this.overlapResetStopwatch >= 1f / Charge.overlapResetFrequency)
			{
				this.overlapResetStopwatch -= 1f / Charge.overlapResetFrequency;
			}
			if (base.isAuthority && Physics.OverlapSphere(this.sphereCheckTransform.position, Charge.overlapSphereRadius, LayerIndex.world.mask).Length != 0)
			{
				Util.PlaySound(Charge.headbuttImpactSound, base.gameObject);
				EffectManager.SimpleMuzzleFlash(Charge.hitEffectPrefab, base.gameObject, "SphereCheckTransform", true);
				base.healthComponent.TakeDamageForce(forward * Charge.selfStunForce, true, false);
				StunState stunState = new StunState();
				stunState.stunDuration = Charge.selfStunDuration;
				this.outer.SetNextState(stunState);
				return;
			}
			this.stopwatch += Time.fixedDeltaTime;
			this.overlapResetStopwatch += Time.fixedDeltaTime;
			if (this.stopwatch > Charge.chargeDuration)
			{
				this.outer.SetNextStateToMain();
			}
			base.FixedUpdate();
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0005840C File Offset: 0x0005660C
		private void ResetOverlapAttack()
		{
			if (!this.hitboxGroup)
			{
				Transform modelTransform = base.GetModelTransform();
				if (modelTransform)
				{
					this.hitboxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Charge");
				}
			}
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = Charge.damageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = Charge.hitEffectPrefab;
			this.attack.forceVector = Vector3.up * Charge.upwardForceMagnitude;
			this.attack.pushAwayForce = Charge.awayForceMagnitude;
			this.attack.hitBoxGroup = this.hitboxGroup;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001948 RID: 6472
		public static float chargeDuration;

		// Token: 0x04001949 RID: 6473
		public static float chargeMovementSpeedCoefficient;

		// Token: 0x0400194A RID: 6474
		public static float turnSpeed;

		// Token: 0x0400194B RID: 6475
		public static float turnSmoothTime;

		// Token: 0x0400194C RID: 6476
		public static float impactDamageCoefficient;

		// Token: 0x0400194D RID: 6477
		public static float impactForce;

		// Token: 0x0400194E RID: 6478
		public static float damageCoefficient;

		// Token: 0x0400194F RID: 6479
		public static float upwardForceMagnitude;

		// Token: 0x04001950 RID: 6480
		public static float awayForceMagnitude;

		// Token: 0x04001951 RID: 6481
		public static GameObject hitEffectPrefab;

		// Token: 0x04001952 RID: 6482
		public static float overlapResetFrequency;

		// Token: 0x04001953 RID: 6483
		public static float overlapSphereRadius;

		// Token: 0x04001954 RID: 6484
		public static float selfStunDuration;

		// Token: 0x04001955 RID: 6485
		public static float selfStunForce;

		// Token: 0x04001956 RID: 6486
		public static string startSoundString;

		// Token: 0x04001957 RID: 6487
		public static string endSoundString;

		// Token: 0x04001958 RID: 6488
		public static string footstepOverrideSoundString;

		// Token: 0x04001959 RID: 6489
		public static string headbuttImpactSound;

		// Token: 0x0400195A RID: 6490
		private float stopwatch;

		// Token: 0x0400195B RID: 6491
		private float overlapResetStopwatch;

		// Token: 0x0400195C RID: 6492
		private Animator animator;

		// Token: 0x0400195D RID: 6493
		private Vector3 targetMoveVector;

		// Token: 0x0400195E RID: 6494
		private Vector3 targetMoveVectorVelocity;

		// Token: 0x0400195F RID: 6495
		private ContactDamage contactDamage;

		// Token: 0x04001960 RID: 6496
		private OverlapAttack attack;

		// Token: 0x04001961 RID: 6497
		private HitBoxGroup hitboxGroup;

		// Token: 0x04001962 RID: 6498
		private ChildLocator childLocator;

		// Token: 0x04001963 RID: 6499
		private Transform sphereCheckTransform;

		// Token: 0x04001964 RID: 6500
		private string baseFootstepString;
	}
}
