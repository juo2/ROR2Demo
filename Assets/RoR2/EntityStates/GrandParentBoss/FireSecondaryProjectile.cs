using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.GrandParentBoss
{
	// Token: 0x02000351 RID: 849
	public class FireSecondaryProjectile : BaseState
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x00041600 File Offset: 0x0003F800
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.fireDelay = this.baseFireDelay / this.attackSpeedStat;
			if (this.fireDelay <= Time.fixedDeltaTime * 2f)
			{
				this.Fire();
			}
			base.PlayCrossfade(this.animationLayerName, this.animationStateName, this.playbackRateParam, this.duration, 0.2f);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00041678 File Offset: 0x0003F878
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (!this.hasFired && base.fixedAge >= this.fireDelay)
			{
				this.Fire();
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000416CC File Offset: 0x0003F8CC
		private void Fire()
		{
			this.hasFired = true;
			if (this.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.muzzleEffectPrefab, base.gameObject, this.muzzleName, false);
			}
			if (base.isAuthority && this.projectilePrefab)
			{
				Ray aimRay = base.GetAimRay();
				Transform modelTransform = base.GetModelTransform();
				if (modelTransform)
				{
					ChildLocator component = modelTransform.GetComponent<ChildLocator>();
					if (component)
					{
						aimRay.origin = component.FindChild(this.muzzleName).transform.position;
					}
				}
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * this.damageCoefficient, this.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x040012F3 RID: 4851
		[SerializeField]
		public float baseDuration = 3f;

		// Token: 0x040012F4 RID: 4852
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x040012F5 RID: 4853
		[SerializeField]
		public GameObject muzzleEffectPrefab;

		// Token: 0x040012F6 RID: 4854
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x040012F7 RID: 4855
		[SerializeField]
		public float force;

		// Token: 0x040012F8 RID: 4856
		[SerializeField]
		public string muzzleName = "SecondaryProjectileMuzzle";

		// Token: 0x040012F9 RID: 4857
		[SerializeField]
		public string animationStateName = "FireSecondaryProjectile";

		// Token: 0x040012FA RID: 4858
		[SerializeField]
		public string playbackRateParam = "FireSecondaryProjectile.playbackRate";

		// Token: 0x040012FB RID: 4859
		[SerializeField]
		public string animationLayerName = "Body";

		// Token: 0x040012FC RID: 4860
		[SerializeField]
		public float baseFireDelay;

		// Token: 0x040012FD RID: 4861
		private float duration;

		// Token: 0x040012FE RID: 4862
		private float fireDelay;

		// Token: 0x040012FF RID: 4863
		private bool hasFired;
	}
}
