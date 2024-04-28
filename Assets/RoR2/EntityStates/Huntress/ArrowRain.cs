using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Huntress
{
	// Token: 0x02000316 RID: 790
	public class ArrowRain : BaseArrowBarrage
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x0003C2E4 File Offset: 0x0003A4E4
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("FullBody, Override", "LoopArrowRain");
			if (ArrowRain.areaIndicatorPrefab)
			{
				this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(ArrowRain.areaIndicatorPrefab);
				this.areaIndicatorInstance.transform.localScale = new Vector3(ArrowRain.arrowRainRadius, ArrowRain.arrowRainRadius, ArrowRain.arrowRainRadius);
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003C348 File Offset: 0x0003A548
		private void UpdateAreaIndicator()
		{
			if (this.areaIndicatorInstance)
			{
				float maxDistance = 1000f;
				RaycastHit raycastHit;
				if (Physics.Raycast(base.GetAimRay(), out raycastHit, maxDistance, LayerIndex.world.mask))
				{
					this.areaIndicatorInstance.transform.position = raycastHit.point;
					this.areaIndicatorInstance.transform.up = raycastHit.normal;
				}
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003C3B8 File Offset: 0x0003A5B8
		public override void Update()
		{
			base.Update();
			this.UpdateAreaIndicator();
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003C3C6 File Offset: 0x0003A5C6
		protected override void HandlePrimaryAttack()
		{
			base.HandlePrimaryAttack();
			this.shouldFireArrowRain = true;
			this.outer.SetNextStateToMain();
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		protected void DoFireArrowRain()
		{
			EffectManager.SimpleMuzzleFlash(ArrowRain.muzzleFlashEffect, base.gameObject, "Muzzle", false);
			if (this.areaIndicatorInstance && this.shouldFireArrowRain)
			{
				ProjectileManager.instance.FireProjectile(ArrowRain.projectilePrefab, this.areaIndicatorInstance.transform.position, this.areaIndicatorInstance.transform.rotation, base.gameObject, this.damageStat * ArrowRain.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003C47B File Offset: 0x0003A67B
		public override void OnExit()
		{
			if (this.shouldFireArrowRain && !this.outer.destroying)
			{
				this.DoFireArrowRain();
			}
			if (this.areaIndicatorInstance)
			{
				EntityState.Destroy(this.areaIndicatorInstance.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x0400117E RID: 4478
		public static float arrowRainRadius;

		// Token: 0x0400117F RID: 4479
		public static float damageCoefficient;

		// Token: 0x04001180 RID: 4480
		public static GameObject projectilePrefab;

		// Token: 0x04001181 RID: 4481
		public static GameObject areaIndicatorPrefab;

		// Token: 0x04001182 RID: 4482
		public static GameObject muzzleFlashEffect;

		// Token: 0x04001183 RID: 4483
		private GameObject areaIndicatorInstance;

		// Token: 0x04001184 RID: 4484
		private bool shouldFireArrowRain;
	}
}
