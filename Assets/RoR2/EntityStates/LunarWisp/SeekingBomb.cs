using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.LunarWisp
{
	// Token: 0x020002AE RID: 686
	public class SeekingBomb : BaseState
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x0003343C File Offset: 0x0003163C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = (SeekingBomb.baseDuration + SeekingBomb.spinUpDuration) / this.attackSpeedStat;
			this.chargeLoopSoundID = Util.PlaySound(SeekingBomb.spinUpSoundString, base.gameObject);
			base.PlayCrossfade("Gesture", "BombStart", 0.2f);
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000334B0 File Offset: 0x000316B0
		public override void OnExit()
		{
			base.OnExit();
			AkSoundEngine.StopPlayingID(this.chargeLoopSoundID);
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000334DC File Offset: 0x000316DC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SeekingBomb.spinUpDuration && !this.upToSpeed)
			{
				this.upToSpeed = true;
				Transform modelTransform = base.GetModelTransform();
				if (modelTransform)
				{
					ChildLocator component = modelTransform.GetComponent<ChildLocator>();
					if (component)
					{
						Transform transform = component.FindChild(SeekingBomb.muzzleName);
						if (transform && SeekingBomb.chargingEffectPrefab)
						{
							this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(SeekingBomb.chargingEffectPrefab, transform.position, transform.rotation);
							this.chargeEffectInstance.transform.parent = transform;
							this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>().newDuration = this.duration;
						}
					}
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.FireBomb();
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000335BC File Offset: 0x000317BC
		private void FireBomb()
		{
			Util.PlaySound(SeekingBomb.fireBombSoundString, base.gameObject);
			Ray aimRay = base.GetAimRay();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					aimRay.origin = component.FindChild(SeekingBomb.muzzleName).transform.position;
				}
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(SeekingBomb.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * SeekingBomb.bombDamageCoefficient, SeekingBomb.bombForce, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
			Util.PlaySound(SeekingBomb.spinDownSoundString, base.gameObject);
			base.PlayCrossfade("Gesture", "BombStop", 0.2f);
		}

		// Token: 0x04000ECF RID: 3791
		public static float baseDuration = 3f;

		// Token: 0x04000ED0 RID: 3792
		public static GameObject chargingEffectPrefab;

		// Token: 0x04000ED1 RID: 3793
		public static GameObject projectilePrefab;

		// Token: 0x04000ED2 RID: 3794
		public static string spinUpSoundString;

		// Token: 0x04000ED3 RID: 3795
		public static string fireBombSoundString;

		// Token: 0x04000ED4 RID: 3796
		public static string spinDownSoundString;

		// Token: 0x04000ED5 RID: 3797
		public static float bombDamageCoefficient;

		// Token: 0x04000ED6 RID: 3798
		public static float bombForce;

		// Token: 0x04000ED7 RID: 3799
		public static string muzzleName;

		// Token: 0x04000ED8 RID: 3800
		public float novaRadius;

		// Token: 0x04000ED9 RID: 3801
		private float duration;

		// Token: 0x04000EDA RID: 3802
		public static float spinUpDuration;

		// Token: 0x04000EDB RID: 3803
		private bool upToSpeed;

		// Token: 0x04000EDC RID: 3804
		private GameObject chargeEffectInstance;

		// Token: 0x04000EDD RID: 3805
		private uint chargeLoopSoundID;
	}
}
