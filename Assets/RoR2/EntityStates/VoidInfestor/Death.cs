using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidInfestor
{
	// Token: 0x02000159 RID: 345
	public class Death : GenericCharacterDeath
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x00019F30 File Offset: 0x00018130
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > Death.deathDelay && NetworkServer.active && !this.hasDied)
			{
				this.hasDied = true;
				EffectManager.SimpleImpactEffect(Death.deathEffectPrefab, base.characterBody.corePosition, Vector3.up, true);
				base.DestroyBodyAsapServer();
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x0400075B RID: 1883
		public static float deathDelay;

		// Token: 0x0400075C RID: 1884
		public static GameObject deathEffectPrefab;

		// Token: 0x0400075D RID: 1885
		private bool hasDied;
	}
}
