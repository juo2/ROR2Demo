using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000F3 RID: 243
	public class FireMegaBlasterBase : BaseState
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x00011E50 File Offset: 0x00010050
		public override void OnEnter()
		{
			base.OnEnter();
			Ray aimRay = base.GetAimRay();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.StartAimMode(this.duration + 2f, false);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.attackSoundString, base.gameObject);
			base.AddRecoil(-1f * this.recoilAmplitude, -1.5f * this.recoilAmplitude, -0.25f * this.recoilAmplitude, 0.25f * this.recoilAmplitude);
			base.characterBody.AddSpreadBloom(this.bloom);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzle, false);
			}
			if (base.isAuthority)
			{
				this.FireProjectiles();
			}
			base.characterBody.characterMotor.ApplyForce(-this.selfKnockbackForce * aimRay.direction, false, false);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00011F60 File Offset: 0x00010160
		private void FireProjectiles()
		{
			Ray aimRay = base.GetAimRay();
			aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, this.spread, 1f, 1f, 0f, 0f);
			FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
			fireProjectileInfo.projectilePrefab = this.projectilePrefab;
			fireProjectileInfo.position = aimRay.origin;
			fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
			fireProjectileInfo.owner = base.gameObject;
			fireProjectileInfo.damage = this.damageStat * this.damageCoefficient;
			fireProjectileInfo.force = this.force;
			fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00012034 File Offset: 0x00010234
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000468 RID: 1128
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04000469 RID: 1129
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x0400046A RID: 1130
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x0400046B RID: 1131
		[SerializeField]
		public float damageCoefficient = 1.2f;

		// Token: 0x0400046C RID: 1132
		[SerializeField]
		public float force = 20f;

		// Token: 0x0400046D RID: 1133
		[SerializeField]
		public string attackSoundString;

		// Token: 0x0400046E RID: 1134
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x0400046F RID: 1135
		[SerializeField]
		public float bloom;

		// Token: 0x04000470 RID: 1136
		[SerializeField]
		public string muzzle;

		// Token: 0x04000471 RID: 1137
		[SerializeField]
		public float spread;

		// Token: 0x04000472 RID: 1138
		[SerializeField]
		public float yawPerProjectile;

		// Token: 0x04000473 RID: 1139
		[SerializeField]
		public float selfKnockbackForce;

		// Token: 0x04000474 RID: 1140
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000475 RID: 1141
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000476 RID: 1142
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000477 RID: 1143
		private float duration;
	}
}
