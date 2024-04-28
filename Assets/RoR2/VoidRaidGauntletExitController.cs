using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008F4 RID: 2292
	public class VoidRaidGauntletExitController : MonoBehaviour
	{
		// Token: 0x0600339A RID: 13210 RVA: 0x000D9715 File Offset: 0x000D7915
		private void OnEnable()
		{
			if (this.exitZone)
			{
				this.exitZone.onBodyTeleport += this.OnBodyTeleport;
			}
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000D973B File Offset: 0x000D793B
		private void OnDisable()
		{
			if (this.exitZone)
			{
				this.exitZone.onBodyTeleport -= this.OnBodyTeleport;
			}
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000D9761 File Offset: 0x000D7961
		private void OnBodyTeleport(CharacterBody body)
		{
			if (Util.HasEffectiveAuthority(body.gameObject) && body.masterObject.GetComponent<PlayerCharacterMasterController>() && VoidRaidGauntletController.instance)
			{
				VoidRaidGauntletController.instance.OnAuthorityPlayerExit();
			}
		}

		// Token: 0x04003494 RID: 13460
		[SerializeField]
		private MapZone exitZone;
	}
}
