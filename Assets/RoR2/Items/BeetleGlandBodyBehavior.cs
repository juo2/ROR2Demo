using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BD0 RID: 3024
	public class BeetleGlandBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044BC RID: 17596 RVA: 0x0011E433 File Offset: 0x0011C633
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.BeetleGland;
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x0011E43C File Offset: 0x0011C63C
		private void FixedUpdate()
		{
			BeetleGlandBodyBehavior.<>c__DisplayClass4_0 CS$<>8__locals1 = new BeetleGlandBodyBehavior.<>c__DisplayClass4_0();
			int stack = this.stack;
			CS$<>8__locals1.bodyMaster = base.body.master;
			if (!CS$<>8__locals1.bodyMaster)
			{
				return;
			}
			int deployableCount = CS$<>8__locals1.bodyMaster.GetDeployableCount(DeployableSlot.BeetleGuardAlly);
			if (deployableCount < stack)
			{
				this.guardResummonCooldown -= Time.fixedDeltaTime;
				if (this.guardResummonCooldown <= 0f)
				{
					DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetleGuardAlly"), new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Approximate,
						minDistance = 3f,
						maxDistance = 40f,
						spawnOnTarget = base.transform
					}, RoR2Application.rng);
					directorSpawnRequest.summonerBodyObject = base.gameObject;
					directorSpawnRequest.onSpawnedServer = new Action<SpawnCard.SpawnResult>(CS$<>8__locals1.<FixedUpdate>g__OnGuardMasterSpawned|0);
					DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
					if (deployableCount < stack)
					{
						this.guardResummonCooldown = BeetleGlandBodyBehavior.timeBetweenGuardRetryResummons;
						return;
					}
					this.guardResummonCooldown = BeetleGlandBodyBehavior.timeBetweenGuardResummons;
				}
			}
		}

		// Token: 0x0400433C RID: 17212
		private static readonly float timeBetweenGuardResummons = 30f;

		// Token: 0x0400433D RID: 17213
		private static readonly float timeBetweenGuardRetryResummons = 1f;

		// Token: 0x0400433E RID: 17214
		private float guardResummonCooldown;
	}
}
