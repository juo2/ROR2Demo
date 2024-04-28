using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.AcidLarva
{
	// Token: 0x020004A3 RID: 1187
	public class Death : GenericCharacterDeath
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x0005E7E8 File Offset: 0x0005C9E8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > Death.deathDelay && !this.hasDied)
			{
				this.hasDied = true;
				base.DestroyModel();
				EffectManager.SimpleImpactEffect(Death.deathEffectPrefab, base.characterBody.corePosition, Vector3.up, false);
				if (NetworkServer.active)
				{
					base.DestroyBodyAsapServer();
				}
			}
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x04001B20 RID: 6944
		public static float deathDelay;

		// Token: 0x04001B21 RID: 6945
		public static GameObject deathEffectPrefab;

		// Token: 0x04001B22 RID: 6946
		private bool hasDied;
	}
}
