using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001F7 RID: 503
	public class FirePistol : BaseState, IBaseWeaponState
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x000257E8 File Offset: 0x000239E8
		protected virtual void FireBullet(Ray aimRay)
		{
			base.StartAimMode(aimRay, 2f, false);
			Util.PlaySound(this.fireSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(this.muzzleFlashPrefab, base.gameObject, this.muzzleName, false);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			base.AddRecoil(this.recoilYMin, this.recoilYMax, this.recoilXMin, this.recoilXMax);
			if (base.isAuthority)
			{
				float num = 0f;
				if (base.characterBody)
				{
					num = base.characterBody.spreadBloomAngle;
				}
				Quaternion rhs = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.forward);
				Quaternion rhs2 = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, this.baseInaccuracyDegrees + num), Vector3.left);
				Quaternion rotation = Util.QuaternionSafeLookRotation(aimRay.direction, Vector3.up) * rhs * rhs2;
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					projectilePrefab = this.projectilePrefab,
					position = aimRay.origin,
					rotation = rotation,
					owner = base.gameObject,
					damage = this.damageStat * this.damageCoefficient,
					crit = base.RollCrit(),
					force = this.force,
					procChainMask = default(ProcChainMask),
					damageColorIndex = DamageColorIndex.Default
				};
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
				base.characterBody.characterMotor.ApplyForce(-this.selfKnockbackForce * aimRay.direction, false, false);
			}
			base.characterBody.AddSpreadBloom(this.spreadBloomValue);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000259A5 File Offset: 0x00023BA5
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.FireBullet(base.GetAimRay());
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000259CC File Offset: 0x00023BCC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool CanScope()
		{
			return true;
		}

		// Token: 0x04000A86 RID: 2694
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000A87 RID: 2695
		[SerializeField]
		[Header("Projectile")]
		public float damageCoefficient;

		// Token: 0x04000A88 RID: 2696
		[SerializeField]
		public float force;

		// Token: 0x04000A89 RID: 2697
		[SerializeField]
		public float spreadBloomValue;

		// Token: 0x04000A8A RID: 2698
		[SerializeField]
		public float recoilYMin;

		// Token: 0x04000A8B RID: 2699
		[SerializeField]
		public float recoilXMin;

		// Token: 0x04000A8C RID: 2700
		[SerializeField]
		public float recoilYMax;

		// Token: 0x04000A8D RID: 2701
		[SerializeField]
		public float recoilXMax;

		// Token: 0x04000A8E RID: 2702
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04000A8F RID: 2703
		[SerializeField]
		public float selfKnockbackForce;

		// Token: 0x04000A90 RID: 2704
		[SerializeField]
		public float baseInaccuracyDegrees;

		// Token: 0x04000A91 RID: 2705
		[SerializeField]
		[Header("Effects")]
		public string muzzleName;

		// Token: 0x04000A92 RID: 2706
		[SerializeField]
		public string fireSoundString;

		// Token: 0x04000A93 RID: 2707
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x04000A94 RID: 2708
		[Header("Animation")]
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000A95 RID: 2709
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000A96 RID: 2710
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000A97 RID: 2711
		protected float duration;
	}
}
