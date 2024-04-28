using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Destructible
{
	// Token: 0x020003CE RID: 974
	public class AltarSkeletonDeath : BaseState
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600116A RID: 4458 RVA: 0x0004CC60 File Offset: 0x0004AE60
		// (remove) Token: 0x0600116B RID: 4459 RVA: 0x0004CC94 File Offset: 0x0004AE94
		public static event Action onDeath;

		// Token: 0x0600116C RID: 4460 RVA: 0x0004CCC7 File Offset: 0x0004AEC7
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(AltarSkeletonDeath.deathSoundString, base.gameObject);
			this.Explode();
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0000F997 File Offset: 0x0000DB97
		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0004CCE8 File Offset: 0x0004AEE8
		private void Explode()
		{
			if (base.modelLocator)
			{
				if (base.modelLocator.modelBaseTransform)
				{
					EntityState.Destroy(base.modelLocator.modelBaseTransform.gameObject);
				}
				if (base.modelLocator.modelTransform)
				{
					EntityState.Destroy(base.modelLocator.modelTransform.gameObject);
				}
			}
			if (AltarSkeletonDeath.explosionEffectPrefab && NetworkServer.active)
			{
				EffectManager.SpawnEffect(AltarSkeletonDeath.explosionEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = AltarSkeletonDeath.explosionRadius,
					rotation = Quaternion.identity
				}, false);
			}
			Action action = AltarSkeletonDeath.onDeath;
			if (action != null)
			{
				action();
			}
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001617 RID: 5655
		public static GameObject explosionEffectPrefab;

		// Token: 0x04001618 RID: 5656
		public static float explosionRadius;

		// Token: 0x04001619 RID: 5657
		public static string deathSoundString;

		// Token: 0x0400161B RID: 5659
		private float stopwatch;
	}
}
