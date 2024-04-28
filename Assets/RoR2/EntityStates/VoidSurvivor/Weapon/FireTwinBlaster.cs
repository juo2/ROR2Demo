using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x02000105 RID: 261
	public class FireTwinBlaster : BaseSkillState
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00013818 File Offset: 0x00011A18
		public override void OnEnter()
		{
			base.OnEnter();
			base.activatorSkillSlot = base.skillLocator.primary;
			base.GetAimRay();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.StartAimMode(this.duration + 2f, false);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlayAttackSpeedSound(this.attackSoundString, base.gameObject, 1f + (float)this.step * this.attackSoundPitchPerStep);
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
			Debug.Log(this.step);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00013938 File Offset: 0x00011B38
		private void FireProjectiles()
		{
			Ray aimRay = base.GetAimRay();
			aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, base.characterBody.spreadBloomAngle * this.spread, 1f, 1f, 0f, 0f);
			FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
			fireProjectileInfo.position = aimRay.origin;
			fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
			fireProjectileInfo.owner = base.gameObject;
			fireProjectileInfo.damage = this.damageStat * this.damageCoefficient;
			fireProjectileInfo.force = this.force;
			fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
			fireProjectileInfo.projectilePrefab = this.projectile1Prefab;
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			fireProjectileInfo.projectilePrefab = this.projectile2Prefab;
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00013A30 File Offset: 0x00011C30
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400050F RID: 1295
		[SerializeField]
		public GameObject projectile1Prefab;

		// Token: 0x04000510 RID: 1296
		[SerializeField]
		public GameObject projectile2Prefab;

		// Token: 0x04000511 RID: 1297
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x04000512 RID: 1298
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x04000513 RID: 1299
		[SerializeField]
		public float damageCoefficient = 1.2f;

		// Token: 0x04000514 RID: 1300
		[SerializeField]
		public float force = 20f;

		// Token: 0x04000515 RID: 1301
		[SerializeField]
		public string attackSoundString;

		// Token: 0x04000516 RID: 1302
		[SerializeField]
		public float attackSoundPitchPerStep;

		// Token: 0x04000517 RID: 1303
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x04000518 RID: 1304
		[SerializeField]
		public float bloom;

		// Token: 0x04000519 RID: 1305
		[SerializeField]
		public string muzzle;

		// Token: 0x0400051A RID: 1306
		[SerializeField]
		public float spread;

		// Token: 0x0400051B RID: 1307
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400051C RID: 1308
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400051D RID: 1309
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x0400051E RID: 1310
		private float duration;

		// Token: 0x0400051F RID: 1311
		private float interruptDuration;

		// Token: 0x04000520 RID: 1312
		public int step;
	}
}
