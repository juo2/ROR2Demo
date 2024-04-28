using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x02000103 RID: 259
	public class FireCorruptDisks : BaseSkillState
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x000130B8 File Offset: 0x000112B8
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
			base.characterBody.characterMotor.ApplyForce(-this.selfKnockbackForce * aimRay.direction, false, false);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzle, false);
			}
			if (base.isAuthority)
			{
				this.FireProjectiles();
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000131C8 File Offset: 0x000113C8
		private void FireProjectiles()
		{
			for (int i = 0; i < this.projectileCount; i++)
			{
				float num = (float)i - (float)(this.projectileCount - 1) / 2f;
				float bonusYaw = num * this.yawPerProjectile;
				float d = num * this.offsetPerProjectile;
				Ray aimRay = base.GetAimRay();
				aimRay.direction = Util.ApplySpread(aimRay.direction, 0f, base.characterBody.spreadBloomAngle + this.spread, 1f, 1f, bonusYaw, this.bonusPitch);
				Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
				Vector3.ProjectOnPlane(onUnitSphere, aimRay.direction);
				Quaternion rotation = Util.QuaternionSafeLookRotation(aimRay.direction, onUnitSphere);
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = this.projectilePrefab;
				fireProjectileInfo.position = aimRay.origin + Vector3.Cross(aimRay.direction, Vector3.up) * d;
				fireProjectileInfo.rotation = rotation;
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.damage = this.damageStat * this.damageCoefficient;
				fireProjectileInfo.force = this.force;
				fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001331B File Offset: 0x0001151B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new Idle());
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040004E6 RID: 1254
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x040004E7 RID: 1255
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x040004E8 RID: 1256
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x040004E9 RID: 1257
		[SerializeField]
		public float damageCoefficient = 1.2f;

		// Token: 0x040004EA RID: 1258
		[SerializeField]
		public float force = 20f;

		// Token: 0x040004EB RID: 1259
		[SerializeField]
		public string attackSoundString;

		// Token: 0x040004EC RID: 1260
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x040004ED RID: 1261
		[SerializeField]
		public float bloom;

		// Token: 0x040004EE RID: 1262
		[SerializeField]
		public string muzzle;

		// Token: 0x040004EF RID: 1263
		[SerializeField]
		public float spread;

		// Token: 0x040004F0 RID: 1264
		[SerializeField]
		public int projectileCount;

		// Token: 0x040004F1 RID: 1265
		[SerializeField]
		public float yawPerProjectile;

		// Token: 0x040004F2 RID: 1266
		[SerializeField]
		public float offsetPerProjectile;

		// Token: 0x040004F3 RID: 1267
		[SerializeField]
		public float selfKnockbackForce;

		// Token: 0x040004F4 RID: 1268
		[SerializeField]
		public float bonusPitch;

		// Token: 0x040004F5 RID: 1269
		[SerializeField]
		public string animationLayerName;

		// Token: 0x040004F6 RID: 1270
		[SerializeField]
		public string animationStateName;

		// Token: 0x040004F7 RID: 1271
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x040004F8 RID: 1272
		private float duration;
	}
}
