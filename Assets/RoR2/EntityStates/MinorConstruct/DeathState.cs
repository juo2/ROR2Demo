using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MinorConstruct
{
	// Token: 0x02000267 RID: 615
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x0002C22A File Offset: 0x0002A42A
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				EffectManager.SimpleEffect(DeathState.explosionPrefab, base.transform.position, base.transform.rotation, true);
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x04000C50 RID: 3152
		public static GameObject explosionPrefab;
	}
}
