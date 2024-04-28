using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BeetleQueenMonster
{
	// Token: 0x02000463 RID: 1123
	public class BeetleWardDeath : BaseState
	{
		// Token: 0x0600140C RID: 5132 RVA: 0x000595CC File Offset: 0x000577CC
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(BeetleWardDeath.deathString, base.gameObject);
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
			if (BeetleWardDeath.initialExplosion && NetworkServer.active)
			{
				EffectManager.SimpleImpactEffect(BeetleWardDeath.initialExplosion, base.transform.position, Vector3.up, true);
			}
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040019B8 RID: 6584
		public static GameObject initialExplosion;

		// Token: 0x040019B9 RID: 6585
		public static string deathString;
	}
}
