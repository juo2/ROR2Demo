using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidBarnacle
{
	// Token: 0x02000160 RID: 352
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x0001AA6C File Offset: 0x00018C6C
		public override void OnEnter()
		{
			base.OnEnter();
			if (DeathState.deathFXPrefab != null)
			{
				EffectManager.SimpleEffect(DeathState.deathFXPrefab, base.transform.position, base.transform.rotation, true);
			}
			base.PlayAnimation(DeathState.animationLayerName, DeathState.animationStateName, DeathState.animationPlaybackRateName, DeathState.duration);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001AAC7 File Offset: 0x00018CC7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= DeathState.duration && NetworkServer.active)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x04000788 RID: 1928
		public static string animationLayerName;

		// Token: 0x04000789 RID: 1929
		public static string animationStateName;

		// Token: 0x0400078A RID: 1930
		public static string animationPlaybackRateName;

		// Token: 0x0400078B RID: 1931
		public static float duration;

		// Token: 0x0400078C RID: 1932
		public static GameObject deathFXPrefab;
	}
}
