using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x02000107 RID: 263
	public class FireHandBeam : BaseState
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x00013B58 File Offset: 0x00011D58
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			Ray aimRay = base.GetAimRay();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			base.AddRecoil(-1f * this.recoilAmplitude, -2f * this.recoilAmplitude, -0.5f * this.recoilAmplitude, 0.5f * this.recoilAmplitude);
			base.StartAimMode(aimRay, 2f, false);
			Util.PlaySound(this.attackSoundString, base.gameObject);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzle, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					muzzleName = this.muzzle,
					maxDistance = this.maxDistance,
					minSpread = 0f,
					maxSpread = base.characterBody.spreadBloomAngle,
					radius = this.bulletRadius,
					falloffModel = BulletAttack.FalloffModel.None,
					smartCollision = true,
					damage = this.damageCoefficient * this.damageStat,
					procCoefficient = 1f / (float)this.bulletCount,
					force = this.force,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					damageType = DamageType.SlowOnHit,
					tracerEffectPrefab = this.tracerEffectPrefab,
					hitEffectPrefab = this.hitEffectPrefab
				}.Fire();
			}
			base.characterBody.AddSpreadBloom(this.spreadBloomValue);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00013D30 File Offset: 0x00011F30
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000529 RID: 1321
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x0400052A RID: 1322
		[SerializeField]
		public GameObject hitEffectPrefab;

		// Token: 0x0400052B RID: 1323
		[SerializeField]
		public GameObject tracerEffectPrefab;

		// Token: 0x0400052C RID: 1324
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x0400052D RID: 1325
		[SerializeField]
		public float maxDistance;

		// Token: 0x0400052E RID: 1326
		[SerializeField]
		public float force;

		// Token: 0x0400052F RID: 1327
		[SerializeField]
		public int bulletCount;

		// Token: 0x04000530 RID: 1328
		[SerializeField]
		public float bulletRadius;

		// Token: 0x04000531 RID: 1329
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x04000532 RID: 1330
		[SerializeField]
		public string attackSoundString;

		// Token: 0x04000533 RID: 1331
		[SerializeField]
		public float recoilAmplitude;

		// Token: 0x04000534 RID: 1332
		[SerializeField]
		public float spreadBloomValue = 0.3f;

		// Token: 0x04000535 RID: 1333
		[SerializeField]
		public float maxSpread;

		// Token: 0x04000536 RID: 1334
		[SerializeField]
		public string muzzle;

		// Token: 0x04000537 RID: 1335
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000538 RID: 1336
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000539 RID: 1337
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x0400053A RID: 1338
		private float duration;
	}
}
