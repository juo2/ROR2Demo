using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BDE RID: 3038
	public class NearbyDamageBonusBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044F5 RID: 17653 RVA: 0x0011F2D6 File Offset: 0x0011D4D6
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.NearbyDamageBonus;
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x0011F2DD File Offset: 0x0011D4DD
		private void OnEnable()
		{
			this.indicatorEnabled = true;
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x0011F2E6 File Offset: 0x0011D4E6
		private void OnDisable()
		{
			this.indicatorEnabled = false;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x0011F2EF File Offset: 0x0011D4EF
		// (set) Token: 0x060044F9 RID: 17657 RVA: 0x0011F2FC File Offset: 0x0011D4FC
		private bool indicatorEnabled
		{
			get
			{
				return this.nearbyDamageBonusIndicator;
			}
			set
			{
				if (this.indicatorEnabled == value)
				{
					return;
				}
				if (value)
				{
					GameObject original = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/NearbyDamageBonusIndicator");
					this.nearbyDamageBonusIndicator = UnityEngine.Object.Instantiate<GameObject>(original, base.body.corePosition, Quaternion.identity);
					this.nearbyDamageBonusIndicator.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.gameObject, null);
					return;
				}
				UnityEngine.Object.Destroy(this.nearbyDamageBonusIndicator);
				this.nearbyDamageBonusIndicator = null;
			}
		}

		// Token: 0x04004365 RID: 17253
		private GameObject nearbyDamageBonusIndicator;
	}
}
