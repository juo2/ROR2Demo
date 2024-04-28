using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x0200029A RID: 666
	public abstract class BaseThrowBombState : BaseState
	{
		// Token: 0x06000BD2 RID: 3026 RVA: 0x000311DC File Offset: 0x0002F3DC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.PlayThrowAnimation();
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, "MuzzleLeft", false);
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, "MuzzleRight", false);
			}
			this.Fire();
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00031249 File Offset: 0x0002F449
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00031274 File Offset: 0x0002F474
		private void Fire()
		{
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				if (this.projectilePrefab != null)
				{
					float num = Util.Remap(this.charge, 0f, 1f, this.minDamageCoefficient, this.maxDamageCoefficient);
					float num2 = this.charge * this.force;
					FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
					{
						projectilePrefab = this.projectilePrefab,
						position = aimRay.origin,
						rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
						owner = base.gameObject,
						damage = this.damageStat * num,
						force = num2,
						crit = base.RollCrit()
					};
					this.ModifyProjectile(ref fireProjectileInfo);
					ProjectileManager.instance.FireProjectile(fireProjectileInfo);
				}
				if (base.characterMotor)
				{
					base.characterMotor.ApplyForce(aimRay.direction * (-this.selfForce * this.charge), false, false);
				}
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00031385 File Offset: 0x0002F585
		protected virtual void PlayThrowAnimation()
		{
			base.PlayAnimation("Gesture, Additive", "FireNovaBomb", "FireNovaBomb.playbackRate", this.duration);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void ModifyProjectile(ref FireProjectileInfo projectileInfo)
		{
		}

		// Token: 0x04000E04 RID: 3588
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04000E05 RID: 3589
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x04000E06 RID: 3590
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000E07 RID: 3591
		[SerializeField]
		public float minDamageCoefficient;

		// Token: 0x04000E08 RID: 3592
		[SerializeField]
		public float maxDamageCoefficient;

		// Token: 0x04000E09 RID: 3593
		[SerializeField]
		public float force;

		// Token: 0x04000E0A RID: 3594
		[SerializeField]
		public float selfForce;

		// Token: 0x04000E0B RID: 3595
		protected float duration;

		// Token: 0x04000E0C RID: 3596
		public float charge;
	}
}
