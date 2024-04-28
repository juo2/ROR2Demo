using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Treebot
{
	// Token: 0x02000179 RID: 377
	public class TreebotFireFruitSeed : BaseState
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		public override void OnEnter()
		{
			base.OnEnter();
			EffectManager.SimpleMuzzleFlash(this.muzzleFlashPrefab, base.gameObject, this.muzzleName, false);
			this.duration = this.baseDuration / this.attackSpeedStat;
			Util.PlaySound(this.enterSoundString, base.gameObject);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.playbackRateParam, this.duration);
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					crit = base.RollCrit(),
					damage = this.damageCoefficient * this.damageStat,
					damageColorIndex = DamageColorIndex.Default,
					force = 0f,
					owner = base.gameObject,
					position = aimRay.origin,
					procChainMask = default(ProcChainMask),
					projectilePrefab = this.projectilePrefab,
					rotation = Quaternion.LookRotation(aimRay.direction),
					useSpeedOverride = false
				};
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001C7BE File Offset: 0x0001A9BE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000815 RID: 2069
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04000816 RID: 2070
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000817 RID: 2071
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x04000818 RID: 2072
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000819 RID: 2073
		[SerializeField]
		public string muzzleName;

		// Token: 0x0400081A RID: 2074
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x0400081B RID: 2075
		[SerializeField]
		public string animationLayerName = "Gesture, Additive";

		// Token: 0x0400081C RID: 2076
		[SerializeField]
		public string animationStateName = "FireFlower";

		// Token: 0x0400081D RID: 2077
		[SerializeField]
		public string playbackRateParam = "FireFlower.playbackRate";

		// Token: 0x0400081E RID: 2078
		private float duration;
	}
}
