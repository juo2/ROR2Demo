using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LemurianBruiserMonster
{
	// Token: 0x020002D0 RID: 720
	public class Flamebreath : BaseState
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x00035D7C File Offset: 0x00033F7C
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.entryDuration = Flamebreath.baseEntryDuration / this.attackSpeedStat;
			this.exitDuration = Flamebreath.baseExitDuration / this.attackSpeedStat;
			this.flamethrowerDuration = Flamebreath.baseFlamethrowerDuration;
			Transform modelTransform = base.GetModelTransform();
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.entryDuration + this.flamethrowerDuration + 1f);
			}
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
				modelTransform.GetComponent<AimAnimator>().enabled = true;
			}
			float num = this.flamethrowerDuration * Flamebreath.tickFrequency;
			this.tickDamageCoefficient = Flamebreath.totalDamageCoefficient / num;
			if (base.isAuthority && base.characterBody)
			{
				this.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			}
			base.PlayAnimation("Gesture, Override", "PrepFlamebreath", "PrepFlamebreath.playbackRate", this.entryDuration);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00035E84 File Offset: 0x00034084
		public override void OnExit()
		{
			Util.PlaySound(Flamebreath.endAttackSoundString, base.gameObject);
			base.PlayCrossfade("Gesture, Override", "BufferEmpty", 0.1f);
			if (this.flamethrowerEffectInstance)
			{
				EntityState.Destroy(this.flamethrowerEffectInstance.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00035EDC File Offset: 0x000340DC
		private void FireFlame(string muzzleString)
		{
			base.GetAimRay();
			if (base.isAuthority && this.muzzleTransform)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = this.muzzleTransform.position,
					aimVector = this.muzzleTransform.forward,
					minSpread = 0f,
					maxSpread = Flamebreath.maxSpread,
					damage = this.tickDamageCoefficient * this.damageStat,
					force = Flamebreath.force,
					muzzleName = muzzleString,
					hitEffectPrefab = Flamebreath.impactEffectPrefab,
					isCrit = this.isCrit,
					radius = Flamebreath.radius,
					falloffModel = BulletAttack.FalloffModel.None,
					stopperMask = LayerIndex.world.mask,
					procCoefficient = Flamebreath.procCoefficientPerTick,
					maxDistance = Flamebreath.maxDistance,
					smartCollision = true,
					damageType = (Util.CheckRoll(Flamebreath.ignitePercentChance, base.characterBody.master) ? DamageType.IgniteOnHit : DamageType.Generic)
				}.Fire();
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00036008 File Offset: 0x00034208
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.entryDuration && this.stopwatch < this.entryDuration + this.flamethrowerDuration && !this.hasBegunFlamethrower)
			{
				this.hasBegunFlamethrower = true;
				Util.PlaySound(Flamebreath.startAttackSoundString, base.gameObject);
				base.PlayAnimation("Gesture, Override", "Flamebreath", "Flamebreath.playbackRate", this.flamethrowerDuration);
				if (this.childLocator)
				{
					this.muzzleTransform = this.childLocator.FindChild("MuzzleMouth");
					this.flamethrowerEffectInstance = UnityEngine.Object.Instantiate<GameObject>(Flamebreath.flamethrowerEffectPrefab, this.muzzleTransform).transform;
					this.flamethrowerEffectInstance.transform.localPosition = Vector3.zero;
					this.flamethrowerEffectInstance.GetComponent<ScaleParticleSystemDuration>().newDuration = this.flamethrowerDuration;
				}
			}
			if (this.stopwatch >= this.entryDuration + this.flamethrowerDuration && this.hasBegunFlamethrower)
			{
				this.hasBegunFlamethrower = false;
				base.PlayCrossfade("Gesture, Override", "ExitFlamebreath", "ExitFlamebreath.playbackRate", this.exitDuration, 0.1f);
			}
			if (this.hasBegunFlamethrower)
			{
				this.flamethrowerStopwatch += Time.deltaTime;
				if (this.flamethrowerStopwatch > 1f / Flamebreath.tickFrequency)
				{
					this.flamethrowerStopwatch -= 1f / Flamebreath.tickFrequency;
					this.FireFlame("MuzzleCenter");
				}
			}
			else if (this.flamethrowerEffectInstance)
			{
				EntityState.Destroy(this.flamethrowerEffectInstance.gameObject);
			}
			if (this.stopwatch >= this.flamethrowerDuration + this.entryDuration + this.exitDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000F9C RID: 3996
		public static GameObject flamethrowerEffectPrefab;

		// Token: 0x04000F9D RID: 3997
		public static GameObject impactEffectPrefab;

		// Token: 0x04000F9E RID: 3998
		public static GameObject tracerEffectPrefab;

		// Token: 0x04000F9F RID: 3999
		public static float maxDistance;

		// Token: 0x04000FA0 RID: 4000
		public static float radius;

		// Token: 0x04000FA1 RID: 4001
		public static float baseEntryDuration = 1f;

		// Token: 0x04000FA2 RID: 4002
		public static float baseExitDuration = 0.5f;

		// Token: 0x04000FA3 RID: 4003
		public static float baseFlamethrowerDuration = 2f;

		// Token: 0x04000FA4 RID: 4004
		public static float totalDamageCoefficient = 1.2f;

		// Token: 0x04000FA5 RID: 4005
		public static float procCoefficientPerTick;

		// Token: 0x04000FA6 RID: 4006
		public static float tickFrequency;

		// Token: 0x04000FA7 RID: 4007
		public static float force = 20f;

		// Token: 0x04000FA8 RID: 4008
		public static string startAttackSoundString;

		// Token: 0x04000FA9 RID: 4009
		public static string endAttackSoundString;

		// Token: 0x04000FAA RID: 4010
		public static float ignitePercentChance;

		// Token: 0x04000FAB RID: 4011
		public static float maxSpread;

		// Token: 0x04000FAC RID: 4012
		private float tickDamageCoefficient;

		// Token: 0x04000FAD RID: 4013
		private float flamethrowerStopwatch;

		// Token: 0x04000FAE RID: 4014
		private float stopwatch;

		// Token: 0x04000FAF RID: 4015
		private float entryDuration;

		// Token: 0x04000FB0 RID: 4016
		private float exitDuration;

		// Token: 0x04000FB1 RID: 4017
		private float flamethrowerDuration;

		// Token: 0x04000FB2 RID: 4018
		private bool hasBegunFlamethrower;

		// Token: 0x04000FB3 RID: 4019
		private ChildLocator childLocator;

		// Token: 0x04000FB4 RID: 4020
		private Transform flamethrowerEffectInstance;

		// Token: 0x04000FB5 RID: 4021
		private Transform muzzleTransform;

		// Token: 0x04000FB6 RID: 4022
		private bool isCrit;

		// Token: 0x04000FB7 RID: 4023
		private const float flamethrowerEffectBaseDistance = 16f;
	}
}
