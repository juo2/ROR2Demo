using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VoidMegaCrab.Weapon
{
	// Token: 0x02000149 RID: 329
	public class FireCrabCannonBase : BaseState
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0001893C File Offset: 0x00016B3C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.fireDuration = this.baseFireDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			this.muzzleTransform = base.FindModelChild(this.muzzleName);
			if (!this.muzzleTransform)
			{
				this.muzzleTransform = base.characterBody.aimOriginTransform;
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000189C4 File Offset: 0x00016BC4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			int num = Mathf.FloorToInt(base.fixedAge / this.fireDuration * (float)this.projectileCount);
			if (this.projectilesFired <= num && this.projectilesFired < this.projectileCount)
			{
				this.FireProjectile();
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00018A34 File Offset: 0x00016C34
		private void FireProjectile()
		{
			Util.PlaySound(this.attackSound, base.gameObject);
			base.characterBody.SetAimTimer(3f);
			if (this.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				int num = Mathf.FloorToInt((float)this.projectilesFired - (float)(this.projectileCount - 1) / 2f);
				float bonusYaw = 0f;
				if (this.projectileCount > 1)
				{
					bonusYaw = (float)num / (float)(this.projectileCount - 1) * this.totalYawSpread;
				}
				Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, this.spread, 1f, 1f, bonusYaw, this.bonusPitch);
				Vector3 position = this.muzzleTransform.position;
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, position, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * this.damageCoefficient, this.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
			this.projectilesFired++;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040006CE RID: 1742
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x040006CF RID: 1743
		[SerializeField]
		public GameObject muzzleflashEffectPrefab;

		// Token: 0x040006D0 RID: 1744
		[SerializeField]
		public int projectileCount = 3;

		// Token: 0x040006D1 RID: 1745
		[SerializeField]
		public float spread;

		// Token: 0x040006D2 RID: 1746
		[SerializeField]
		public float bonusPitch;

		// Token: 0x040006D3 RID: 1747
		[SerializeField]
		public float totalYawSpread = 5f;

		// Token: 0x040006D4 RID: 1748
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x040006D5 RID: 1749
		[SerializeField]
		public float baseFireDuration = 0.2f;

		// Token: 0x040006D6 RID: 1750
		[SerializeField]
		public float damageCoefficient = 1.2f;

		// Token: 0x040006D7 RID: 1751
		[SerializeField]
		public float force = 20f;

		// Token: 0x040006D8 RID: 1752
		[SerializeField]
		public string attackSound;

		// Token: 0x040006D9 RID: 1753
		[SerializeField]
		public string muzzleName;

		// Token: 0x040006DA RID: 1754
		[SerializeField]
		public string animationLayerName = "Gesture, Additive";

		// Token: 0x040006DB RID: 1755
		[SerializeField]
		public string animationStateName = "FireCrabCannon";

		// Token: 0x040006DC RID: 1756
		[SerializeField]
		public string animationPlaybackRateParam = "FireCrabCannon.playbackRate";

		// Token: 0x040006DD RID: 1757
		private float duration;

		// Token: 0x040006DE RID: 1758
		private float fireDuration;

		// Token: 0x040006DF RID: 1759
		private int projectilesFired;

		// Token: 0x040006E0 RID: 1760
		private Transform muzzleTransform;
	}
}
