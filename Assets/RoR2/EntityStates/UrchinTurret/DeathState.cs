using System;
using RoR2;
using UnityEngine;

namespace EntityStates.UrchinTurret
{
	// Token: 0x0200016C RID: 364
	public class DeathState : BaseState
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0001B618 File Offset: 0x00019818
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(DeathState.deathString, base.gameObject);
			Transform transform = base.FindModelChild("Muzzle");
			if (base.isAuthority)
			{
				if (DeathState.initialExplosion)
				{
					EffectManager.SpawnEffect(DeathState.initialExplosion, new EffectData
					{
						origin = transform.position,
						scale = DeathState.effectScale
					}, true);
				}
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040007BC RID: 1980
		public static GameObject initialExplosion;

		// Token: 0x040007BD RID: 1981
		public static float effectScale;

		// Token: 0x040007BE RID: 1982
		public static string deathString;
	}
}
