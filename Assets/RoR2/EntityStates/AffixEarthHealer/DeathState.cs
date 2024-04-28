using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.AffixEarthHealer
{
	// Token: 0x020004A2 RID: 1186
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x0005E78C File Offset: 0x0005C98C
		public override void OnEnter()
		{
			base.OnEnter();
			if (DeathState.initialExplosion)
			{
				EffectManager.SimpleEffect(DeathState.initialExplosion, base.transform.position, base.transform.rotation, false);
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0005E7C1 File Offset: 0x0005C9C1
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge > DeathState.duration)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x04001B1D RID: 6941
		public static GameObject initialExplosion;

		// Token: 0x04001B1E RID: 6942
		public static float duration;

		// Token: 0x04001B1F RID: 6943
		public static string enterSoundString;
	}
}
