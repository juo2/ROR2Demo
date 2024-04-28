using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Wisp1Monster
{
	// Token: 0x020000DB RID: 219
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00010604 File Offset: 0x0000E804
		public override void OnEnter()
		{
			base.OnEnter();
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
			if (NetworkServer.active)
			{
				EffectManager.SimpleEffect(DeathState.initialExplosion, base.transform.position, base.transform.rotation, true);
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x040003F8 RID: 1016
		public static GameObject initialExplosion;
	}
}
