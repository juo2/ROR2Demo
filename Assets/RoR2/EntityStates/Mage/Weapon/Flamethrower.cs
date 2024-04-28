using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x020002A8 RID: 680
	public class Flamethrower : BaseState
	{
		// Token: 0x06000C03 RID: 3075 RVA: 0x000320EC File Offset: 0x000302EC
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.entryDuration = Flamethrower.baseEntryDuration / this.attackSpeedStat;
			this.flamethrowerDuration = Flamethrower.baseFlamethrowerDuration;
			Transform modelTransform = base.GetModelTransform();
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.entryDuration + this.flamethrowerDuration + 1f);
			}
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
				this.leftMuzzleTransform = this.childLocator.FindChild("MuzzleLeft");
				this.rightMuzzleTransform = this.childLocator.FindChild("MuzzleRight");
			}
			int num = Mathf.CeilToInt(this.flamethrowerDuration * Flamethrower.tickFrequency);
			this.tickDamageCoefficient = Flamethrower.totalDamageCoefficient / (float)num;
			if (base.isAuthority && base.characterBody)
			{
				this.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			}
			base.PlayAnimation("Gesture, Additive", "PrepFlamethrower", "Flamethrower.playbackRate", this.entryDuration);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00032208 File Offset: 0x00030408
		public override void OnExit()
		{
			Util.PlaySound(Flamethrower.endAttackSoundString, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "ExitFlamethrower", 0.1f);
			if (this.leftFlamethrowerTransform)
			{
				EntityState.Destroy(this.leftFlamethrowerTransform.gameObject);
			}
			if (this.rightFlamethrowerTransform)
			{
				EntityState.Destroy(this.rightFlamethrowerTransform.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0003227C File Offset: 0x0003047C
		private void FireGauntlet(string muzzleString)
		{
			Ray aimRay = base.GetAimRay();
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = 0f,
					damage = this.tickDamageCoefficient * this.damageStat,
					force = Flamethrower.force,
					muzzleName = muzzleString,
					hitEffectPrefab = Flamethrower.impactEffectPrefab,
					isCrit = this.isCrit,
					radius = Flamethrower.radius,
					falloffModel = BulletAttack.FalloffModel.None,
					stopperMask = LayerIndex.world.mask,
					procCoefficient = Flamethrower.procCoefficientPerTick,
					maxDistance = this.maxDistance,
					smartCollision = true,
					damageType = (Util.CheckRoll(Flamethrower.ignitePercentChance, base.characterBody.master) ? DamageType.IgniteOnHit : DamageType.Generic)
				}.Fire();
				if (base.characterMotor)
				{
					base.characterMotor.ApplyForce(aimRay.direction * -Flamethrower.recoilForce, false, false);
				}
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000323B4 File Offset: 0x000305B4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.entryDuration && !this.hasBegunFlamethrower)
			{
				this.hasBegunFlamethrower = true;
				Util.PlaySound(Flamethrower.startAttackSoundString, base.gameObject);
				base.PlayAnimation("Gesture, Additive", "Flamethrower", "Flamethrower.playbackRate", this.flamethrowerDuration);
				if (this.childLocator)
				{
					Transform transform = this.childLocator.FindChild("MuzzleLeft");
					Transform transform2 = this.childLocator.FindChild("MuzzleRight");
					if (transform)
					{
						this.leftFlamethrowerTransform = UnityEngine.Object.Instantiate<GameObject>(this.flamethrowerEffectPrefab, transform).transform;
					}
					if (transform2)
					{
						this.rightFlamethrowerTransform = UnityEngine.Object.Instantiate<GameObject>(this.flamethrowerEffectPrefab, transform2).transform;
					}
					if (this.leftFlamethrowerTransform)
					{
						this.leftFlamethrowerTransform.GetComponent<ScaleParticleSystemDuration>().newDuration = this.flamethrowerDuration;
					}
					if (this.rightFlamethrowerTransform)
					{
						this.rightFlamethrowerTransform.GetComponent<ScaleParticleSystemDuration>().newDuration = this.flamethrowerDuration;
					}
				}
				this.FireGauntlet("MuzzleCenter");
			}
			if (this.hasBegunFlamethrower)
			{
				this.flamethrowerStopwatch += Time.deltaTime;
				float num = 1f / Flamethrower.tickFrequency / this.attackSpeedStat;
				if (this.flamethrowerStopwatch > num)
				{
					this.flamethrowerStopwatch -= num;
					this.FireGauntlet("MuzzleCenter");
				}
				this.UpdateFlamethrowerEffect();
			}
			if (this.stopwatch >= this.flamethrowerDuration + this.entryDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00032568 File Offset: 0x00030768
		private void UpdateFlamethrowerEffect()
		{
			Ray aimRay = base.GetAimRay();
			Vector3 direction = aimRay.direction;
			Vector3 direction2 = aimRay.direction;
			if (this.leftFlamethrowerTransform)
			{
				this.leftFlamethrowerTransform.forward = direction;
			}
			if (this.rightFlamethrowerTransform)
			{
				this.rightFlamethrowerTransform.forward = direction2;
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000E60 RID: 3680
		[SerializeField]
		public GameObject flamethrowerEffectPrefab;

		// Token: 0x04000E61 RID: 3681
		public static GameObject impactEffectPrefab;

		// Token: 0x04000E62 RID: 3682
		public static GameObject tracerEffectPrefab;

		// Token: 0x04000E63 RID: 3683
		[SerializeField]
		public float maxDistance;

		// Token: 0x04000E64 RID: 3684
		public static float radius;

		// Token: 0x04000E65 RID: 3685
		public static float baseEntryDuration = 1f;

		// Token: 0x04000E66 RID: 3686
		public static float baseFlamethrowerDuration = 2f;

		// Token: 0x04000E67 RID: 3687
		public static float totalDamageCoefficient = 1.2f;

		// Token: 0x04000E68 RID: 3688
		public static float procCoefficientPerTick;

		// Token: 0x04000E69 RID: 3689
		public static float tickFrequency;

		// Token: 0x04000E6A RID: 3690
		public static float force = 20f;

		// Token: 0x04000E6B RID: 3691
		public static string startAttackSoundString;

		// Token: 0x04000E6C RID: 3692
		public static string endAttackSoundString;

		// Token: 0x04000E6D RID: 3693
		public static float ignitePercentChance;

		// Token: 0x04000E6E RID: 3694
		public static float recoilForce;

		// Token: 0x04000E6F RID: 3695
		private float tickDamageCoefficient;

		// Token: 0x04000E70 RID: 3696
		private float flamethrowerStopwatch;

		// Token: 0x04000E71 RID: 3697
		private float stopwatch;

		// Token: 0x04000E72 RID: 3698
		private float entryDuration;

		// Token: 0x04000E73 RID: 3699
		private float flamethrowerDuration;

		// Token: 0x04000E74 RID: 3700
		private bool hasBegunFlamethrower;

		// Token: 0x04000E75 RID: 3701
		private ChildLocator childLocator;

		// Token: 0x04000E76 RID: 3702
		private Transform leftFlamethrowerTransform;

		// Token: 0x04000E77 RID: 3703
		private Transform rightFlamethrowerTransform;

		// Token: 0x04000E78 RID: 3704
		private Transform leftMuzzleTransform;

		// Token: 0x04000E79 RID: 3705
		private Transform rightMuzzleTransform;

		// Token: 0x04000E7A RID: 3706
		private bool isCrit;

		// Token: 0x04000E7B RID: 3707
		private const float flamethrowerEffectBaseDistance = 16f;
	}
}
