using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Items
{
	// Token: 0x02000BDD RID: 3037
	public class MushroomBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044F0 RID: 17648 RVA: 0x0011F160 File Offset: 0x0011D360
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.Mushroom;
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x0011F167 File Offset: 0x0011D367
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			MushroomBodyBehavior.mushroomWardPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MushroomWard");
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x0011F178 File Offset: 0x0011D378
		private void FixedUpdate()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			int stack = this.stack;
			bool flag = stack > 0 && base.body.GetNotMoving();
			float networkradius = base.body.radius + 1.5f + 1.5f * (float)stack;
			if (this.mushroomWardGameObject != flag)
			{
				if (flag)
				{
					this.mushroomWardGameObject = UnityEngine.Object.Instantiate<GameObject>(MushroomBodyBehavior.mushroomWardPrefab, base.body.footPosition, Quaternion.identity);
					this.mushroomWardTeamFilter = this.mushroomWardGameObject.GetComponent<TeamFilter>();
					this.mushroomHealingWard = this.mushroomWardGameObject.GetComponent<HealingWard>();
					NetworkServer.Spawn(this.mushroomWardGameObject);
				}
				else
				{
					UnityEngine.Object.Destroy(this.mushroomWardGameObject);
					this.mushroomWardGameObject = null;
				}
			}
			if (this.mushroomHealingWard)
			{
				this.mushroomHealingWard.interval = 0.25f;
				this.mushroomHealingWard.healFraction = (0.045f + 0.0225f * (float)(stack - 1)) * this.mushroomHealingWard.interval;
				this.mushroomHealingWard.healPoints = 0f;
				this.mushroomHealingWard.Networkradius = networkradius;
			}
			if (this.mushroomWardTeamFilter)
			{
				this.mushroomWardTeamFilter.teamIndex = base.body.teamComponent.teamIndex;
			}
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x0011F2BC File Offset: 0x0011D4BC
		private void OnDisable()
		{
			if (this.mushroomWardGameObject)
			{
				UnityEngine.Object.Destroy(this.mushroomWardGameObject);
			}
		}

		// Token: 0x0400435F RID: 17247
		private static GameObject mushroomWardPrefab;

		// Token: 0x04004360 RID: 17248
		private const float baseHealFractionPerSecond = 0.045f;

		// Token: 0x04004361 RID: 17249
		private const float healFractionPerSecondPerStack = 0.0225f;

		// Token: 0x04004362 RID: 17250
		private GameObject mushroomWardGameObject;

		// Token: 0x04004363 RID: 17251
		private HealingWard mushroomHealingWard;

		// Token: 0x04004364 RID: 17252
		private TeamFilter mushroomWardTeamFilter;
	}
}
