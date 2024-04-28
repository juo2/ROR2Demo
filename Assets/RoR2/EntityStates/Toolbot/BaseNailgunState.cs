using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x02000197 RID: 407
	public class BaseNailgunState : BaseToolbotPrimarySkillState
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001EF7A File Offset: 0x0001D17A
		public override string baseMuzzleName
		{
			get
			{
				return BaseNailgunState.muzzleName;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000137EE File Offset: 0x000119EE
		protected virtual float GetBaseDuration()
		{
			return 0f;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001EF81 File Offset: 0x0001D181
		public override void OnEnter()
		{
			base.OnEnter();
			this.PullCurrentStats();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001EF8F File Offset: 0x0001D18F
		protected void PullCurrentStats()
		{
			this.attackSpeedStat = base.characterBody.attackSpeed;
			this.critStat = base.characterBody.crit;
			this.duration = this.GetBaseDuration() / this.attackSpeedStat;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001EFC8 File Offset: 0x0001D1C8
		protected void FireBullet(Ray aimRay, int bulletCount, float spreadPitchScale, float spreadYawScale)
		{
			this.fireNumber++;
			base.StartAimMode(aimRay, 3f, false);
			if (base.isAuthority)
			{
				new BulletAttack
				{
					aimVector = aimRay.direction,
					origin = aimRay.origin,
					owner = base.gameObject,
					weapon = base.gameObject,
					bulletCount = (uint)bulletCount,
					damage = this.damageStat * BaseNailgunState.damageCoefficient,
					damageColorIndex = DamageColorIndex.Default,
					damageType = DamageType.Generic,
					falloffModel = BulletAttack.FalloffModel.DefaultBullet,
					force = BaseNailgunState.force,
					HitEffectNormal = false,
					procChainMask = default(ProcChainMask),
					procCoefficient = BaseNailgunState.procCoefficient,
					maxDistance = BaseNailgunState.maxDistance,
					radius = 0f,
					isCrit = base.RollCrit(),
					muzzleName = ((IToolbotPrimarySkillState)this).muzzleName,
					minSpread = 0f,
					hitEffectPrefab = BaseNailgunState.hitEffectPrefab,
					maxSpread = base.characterBody.spreadBloomAngle,
					smartCollision = false,
					sniper = false,
					spreadPitchScale = spreadPitchScale * spreadPitchScale,
					spreadYawScale = spreadYawScale * spreadYawScale,
					tracerEffectPrefab = BaseNailgunState.tracerEffectPrefab
				}.Fire();
			}
			if (base.characterBody)
			{
				base.characterBody.AddSpreadBloom(BaseNailgunState.spreadBloomValue);
			}
			Util.PlaySound(BaseNailgunState.fireSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(BaseNailgunState.muzzleFlashPrefab, base.gameObject, BaseNailgunState.muzzleName, false);
			if (!base.isInDualWield)
			{
				this.PlayAnimation("Gesture, Additive", "FireNailgun");
				return;
			}
			BaseToolbotPrimarySkillStateMethods.PlayGenericFireAnim<BaseNailgunState>(this, base.gameObject, base.skillLocator, 0.2f);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001F184 File Offset: 0x0001D384
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001F18C File Offset: 0x0001D38C
		protected bool animateNailgunFiring
		{
			get
			{
				return this._animateNailgunFiring;
			}
			set
			{
				if (this._animateNailgunFiring == value)
				{
					return;
				}
				this._animateNailgunFiring = value;
				base.GetModelAnimator().SetBool("isFiringNailgun", value);
			}
		}

		// Token: 0x040008CB RID: 2251
		public static float damageCoefficient = 0.1f;

		// Token: 0x040008CC RID: 2252
		public static float procCoefficient = 1f;

		// Token: 0x040008CD RID: 2253
		public static float force = 100f;

		// Token: 0x040008CE RID: 2254
		public static float maxDistance = 50f;

		// Token: 0x040008CF RID: 2255
		public new static string muzzleName;

		// Token: 0x040008D0 RID: 2256
		public static GameObject hitEffectPrefab;

		// Token: 0x040008D1 RID: 2257
		public static float spreadPitchScale = 0.5f;

		// Token: 0x040008D2 RID: 2258
		public static float spreadYawScale = 1f;

		// Token: 0x040008D3 RID: 2259
		public static GameObject tracerEffectPrefab;

		// Token: 0x040008D4 RID: 2260
		public static string fireSoundString;

		// Token: 0x040008D5 RID: 2261
		public static GameObject muzzleFlashPrefab;

		// Token: 0x040008D6 RID: 2262
		public static float spreadBloomValue = 0.2f;

		// Token: 0x040008D7 RID: 2263
		protected float duration;

		// Token: 0x040008D8 RID: 2264
		protected int fireNumber;

		// Token: 0x040008D9 RID: 2265
		private bool _animateNailgunFiring;
	}
}
