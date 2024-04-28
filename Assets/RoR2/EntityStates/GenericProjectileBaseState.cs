using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000C5 RID: 197
	public class GenericProjectileBaseState : BaseState
	{
		// Token: 0x0600039A RID: 922 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.delayBeforeFiringProjectile = this.baseDelayBeforeFiringProjectile / this.attackSpeedStat;
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
			this.PlayAnimation(this.duration);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void PlayAnimation(float duration)
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000EC60 File Offset: 0x0000CE60
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.delayBeforeFiringProjectile && !this.firedProjectile)
			{
				this.firedProjectile = true;
				this.FireProjectile();
				this.DoFireEffects();
			}
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		protected virtual void FireProjectile()
		{
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				aimRay = this.ModifyProjectileAimRay(aimRay);
				aimRay.direction = Util.ApplySpread(aimRay.direction, this.minSpread, this.maxSpread, 1f, 1f, 0f, this.projectilePitchBonus);
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * this.damageCoefficient, this.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000ED84 File Offset: 0x0000CF84
		protected virtual Ray ModifyProjectileAimRay(Ray aimRay)
		{
			return aimRay;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000ED88 File Offset: 0x0000CF88
		protected virtual void DoFireEffects()
		{
			Util.PlaySound(this.attackSoundString, base.gameObject);
			base.AddRecoil(-2f * this.recoilAmplitude, -3f * this.recoilAmplitude, -1f * this.recoilAmplitude, 1f * this.recoilAmplitude);
			if (this.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, this.targetMuzzle, false);
			}
			base.characterBody.AddSpreadBloom(this.bloom);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000398 RID: 920
		[SerializeField]
		public GameObject effectPrefab;

		// Token: 0x04000399 RID: 921
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x0400039A RID: 922
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x0400039B RID: 923
		[SerializeField]
		public float force;

		// Token: 0x0400039C RID: 924
		[SerializeField]
		public float minSpread;

		// Token: 0x0400039D RID: 925
		[SerializeField]
		public float maxSpread;

		// Token: 0x0400039E RID: 926
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x0400039F RID: 927
		[SerializeField]
		public float recoilAmplitude = 1f;

		// Token: 0x040003A0 RID: 928
		[SerializeField]
		public string attackSoundString;

		// Token: 0x040003A1 RID: 929
		[SerializeField]
		public float projectilePitchBonus;

		// Token: 0x040003A2 RID: 930
		[SerializeField]
		public float baseDelayBeforeFiringProjectile;

		// Token: 0x040003A3 RID: 931
		[SerializeField]
		public string targetMuzzle;

		// Token: 0x040003A4 RID: 932
		[SerializeField]
		public float bloom;

		// Token: 0x040003A5 RID: 933
		protected float stopwatch;

		// Token: 0x040003A6 RID: 934
		protected float duration;

		// Token: 0x040003A7 RID: 935
		protected float delayBeforeFiringProjectile;

		// Token: 0x040003A8 RID: 936
		protected bool firedProjectile;
	}
}
