using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000819 RID: 2073
	public class PortalStatueBehavior : MonoBehaviour
	{
		// Token: 0x06002CFB RID: 11515 RVA: 0x000BFDF8 File Offset: 0x000BDFF8
		public void GrantPortalEntry()
		{
			PortalStatueBehavior.PortalType portalType = this.portalType;
			if (portalType != PortalStatueBehavior.PortalType.Shop)
			{
				if (portalType == PortalStatueBehavior.PortalType.Goldshores)
				{
					if (TeleporterInteraction.instance)
					{
						TeleporterInteraction.instance.shouldAttemptToSpawnGoldshoresPortal = true;
					}
					EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
					{
						origin = base.transform.position,
						rotation = Quaternion.identity,
						scale = 1f,
						color = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Money)
					}, true);
				}
			}
			else
			{
				if (TeleporterInteraction.instance)
				{
					TeleporterInteraction.instance.shouldAttemptToSpawnShopPortal = true;
				}
				EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ShrineUseEffect"), new EffectData
				{
					origin = base.transform.position,
					rotation = Quaternion.identity,
					scale = 1f,
					color = ColorCatalog.GetColor(ColorCatalog.ColorIndex.LunarItem)
				}, true);
			}
			foreach (PortalStatueBehavior portalStatueBehavior in UnityEngine.Object.FindObjectsOfType<PortalStatueBehavior>())
			{
				if (portalStatueBehavior.portalType == this.portalType)
				{
					PurchaseInteraction component = portalStatueBehavior.GetComponent<PurchaseInteraction>();
					if (component)
					{
						component.Networkavailable = false;
					}
				}
			}
		}

		// Token: 0x04002F21 RID: 12065
		public PortalStatueBehavior.PortalType portalType;

		// Token: 0x0200081A RID: 2074
		public enum PortalType
		{
			// Token: 0x04002F23 RID: 12067
			Shop,
			// Token: 0x04002F24 RID: 12068
			Goldshores,
			// Token: 0x04002F25 RID: 12069
			Count
		}
	}
}
