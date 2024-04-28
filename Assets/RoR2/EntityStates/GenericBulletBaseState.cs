using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000BF RID: 191
	public abstract class GenericBulletBaseState : BaseState
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		protected BulletAttack GenerateBulletAttack(Ray aimRay)
		{
			float num = 0f;
			if (base.characterBody)
			{
				num = base.characterBody.spreadBloomAngle;
			}
			return new BulletAttack
			{
				aimVector = aimRay.direction,
				origin = aimRay.origin,
				owner = base.gameObject,
				weapon = null,
				bulletCount = (uint)this.bulletCount,
				damage = this.damageStat * this.damageCoefficient,
				damageColorIndex = DamageColorIndex.Default,
				damageType = DamageType.Generic,
				falloffModel = BulletAttack.FalloffModel.Buckshot,
				force = this.force,
				HitEffectNormal = false,
				procChainMask = default(ProcChainMask),
				procCoefficient = this.procCoefficient,
				maxDistance = this.maxDistance,
				radius = this.bulletRadius,
				isCrit = base.RollCrit(),
				muzzleName = this.muzzleName,
				minSpread = this.minSpread,
				maxSpread = this.maxSpread + num,
				hitEffectPrefab = this.hitEffectPrefab,
				smartCollision = this.useSmartCollision,
				sniper = false,
				spreadPitchScale = this.spreadPitchScale,
				spreadYawScale = this.spreadYawScale,
				tracerEffectPrefab = this.tracerEffectPrefab
			};
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void PlayFireAnimation()
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000DD56 File Offset: 0x0000BF56
		protected virtual void DoFireEffects()
		{
			Util.PlaySound(this.fireSoundString, base.gameObject);
			if (this.muzzleTransform)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleFlashPrefab, base.gameObject, this.muzzleName, false);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void ModifyBullet(BulletAttack bulletAttack)
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000DD90 File Offset: 0x0000BF90
		protected virtual void FireBullet(Ray aimRay)
		{
			base.StartAimMode(aimRay, 3f, false);
			this.DoFireEffects();
			this.PlayFireAnimation();
			base.AddRecoil(-1f * this.recoilAmplitudeY, -1.5f * this.recoilAmplitudeY, -1f * this.recoilAmplitudeX, 1f * this.recoilAmplitudeX);
			if (base.isAuthority)
			{
				BulletAttack bulletAttack = this.GenerateBulletAttack(aimRay);
				this.ModifyBullet(bulletAttack);
				bulletAttack.Fire();
				this.OnFireBulletAuthority(aimRay);
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnFireBulletAuthority(Ray aimRay)
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000DE10 File Offset: 0x0000C010
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.muzzleTransform = base.FindModelChild(this.muzzleName);
			this.FireBullet(base.GetAimRay());
			base.characterBody.AddSpreadBloom(this.spreadBloomValue);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000DE65 File Offset: 0x0000C065
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(this.InstantiateNextState());
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000DE8C File Offset: 0x0000C08C
		protected virtual EntityState InstantiateNextState()
		{
			return EntityStateCatalog.InstantiateState(this.outer.mainStateType);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400036C RID: 876
		[SerializeField]
		public float baseDuration = 0.1f;

		// Token: 0x0400036D RID: 877
		[SerializeField]
		public int bulletCount = 1;

		// Token: 0x0400036E RID: 878
		[SerializeField]
		public float maxDistance = 50f;

		// Token: 0x0400036F RID: 879
		[SerializeField]
		public float bulletRadius;

		// Token: 0x04000370 RID: 880
		[SerializeField]
		public bool useSmartCollision;

		// Token: 0x04000371 RID: 881
		[SerializeField]
		public float damageCoefficient = 0.1f;

		// Token: 0x04000372 RID: 882
		[SerializeField]
		public float procCoefficient = 1f;

		// Token: 0x04000373 RID: 883
		[SerializeField]
		public float force = 100f;

		// Token: 0x04000374 RID: 884
		[SerializeField]
		public float minSpread;

		// Token: 0x04000375 RID: 885
		[SerializeField]
		public float maxSpread;

		// Token: 0x04000376 RID: 886
		[SerializeField]
		public float spreadPitchScale = 0.5f;

		// Token: 0x04000377 RID: 887
		[SerializeField]
		public float spreadYawScale = 1f;

		// Token: 0x04000378 RID: 888
		[SerializeField]
		public float spreadBloomValue = 0.2f;

		// Token: 0x04000379 RID: 889
		[SerializeField]
		public float recoilAmplitudeY;

		// Token: 0x0400037A RID: 890
		[SerializeField]
		public float recoilAmplitudeX;

		// Token: 0x0400037B RID: 891
		[SerializeField]
		public string muzzleName;

		// Token: 0x0400037C RID: 892
		[SerializeField]
		public string fireSoundString;

		// Token: 0x0400037D RID: 893
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x0400037E RID: 894
		[SerializeField]
		public GameObject tracerEffectPrefab;

		// Token: 0x0400037F RID: 895
		[SerializeField]
		public GameObject hitEffectPrefab;

		// Token: 0x04000380 RID: 896
		protected float duration;

		// Token: 0x04000381 RID: 897
		protected Transform muzzleTransform;
	}
}
