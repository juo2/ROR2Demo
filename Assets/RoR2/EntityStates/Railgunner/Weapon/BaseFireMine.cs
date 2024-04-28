using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001F0 RID: 496
	public class BaseFireMine : BaseState
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x000251C4 File Offset: 0x000233C4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			float crossfadeDuration = this.baseCrossfadeDuration / this.attackSpeedStat;
			Util.PlaySound(this.enterSoundString, base.gameObject);
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			if (base.GetModelAnimator())
			{
				base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration, crossfadeDuration);
			}
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * this.damageCoefficient, this.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000252D0 File Offset: 0x000234D0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000A5E RID: 2654
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000A5F RID: 2655
		[SerializeField]
		public float baseCrossfadeDuration;

		// Token: 0x04000A60 RID: 2656
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x04000A61 RID: 2657
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04000A62 RID: 2658
		[SerializeField]
		public string muzzleName;

		// Token: 0x04000A63 RID: 2659
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x04000A64 RID: 2660
		[SerializeField]
		public float force;

		// Token: 0x04000A65 RID: 2661
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000A66 RID: 2662
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000A67 RID: 2663
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000A68 RID: 2664
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000A69 RID: 2665
		private float duration;
	}
}
