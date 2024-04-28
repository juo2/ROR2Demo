using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VagrantMonster
{
	// Token: 0x020002E3 RID: 739
	public class DeathState : BaseState
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x000377F0 File Offset: 0x000359F0
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(DeathState.deathString, base.gameObject);
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
			if (base.isAuthority && DeathState.initialExplosion)
			{
				EffectManager.SimpleImpactEffect(DeathState.initialExplosion, base.transform.position, Vector3.up, true);
			}
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001014 RID: 4116
		public static GameObject initialExplosion;

		// Token: 0x04001015 RID: 4117
		public static string deathString;
	}
}
