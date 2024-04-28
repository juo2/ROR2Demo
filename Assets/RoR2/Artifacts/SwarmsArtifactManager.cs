using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Artifacts
{
	// Token: 0x02000E6F RID: 3695
	public static class SwarmsArtifactManager
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06005497 RID: 21655 RVA: 0x0015CBDD File Offset: 0x0015ADDD
		private static ArtifactDef myArtifact
		{
			get
			{
				return RoR2Content.Artifacts.swarmsArtifactDef;
			}
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x0015CBE4 File Offset: 0x0015ADE4
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			RunArtifactManager.onArtifactEnabledGlobal += SwarmsArtifactManager.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal += SwarmsArtifactManager.OnArtifactDisabled;
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x0015CC08 File Offset: 0x0015AE08
		private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != SwarmsArtifactManager.myArtifact)
			{
				return;
			}
			if (!NetworkServer.active)
			{
				return;
			}
			SpawnCard.onSpawnedServerGlobal += SwarmsArtifactManager.OnSpawnCardOnSpawnedServerGlobal;
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0015CC31 File Offset: 0x0015AE31
		private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != SwarmsArtifactManager.myArtifact)
			{
				return;
			}
			SpawnCard.onSpawnedServerGlobal -= SwarmsArtifactManager.OnSpawnCardOnSpawnedServerGlobal;
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x0015CC54 File Offset: 0x0015AE54
		private static void OnSpawnCardOnSpawnedServerGlobal(SpawnCard.SpawnResult result)
		{
			if (!result.success)
			{
				return;
			}
			if (result.spawnRequest.spawnCard as CharacterSpawnCard)
			{
				TeamIndex? teamIndexOverride = result.spawnRequest.teamIndexOverride;
				TeamIndex teamIndex = TeamIndex.Player;
				if (teamIndexOverride.GetValueOrDefault() == teamIndex & teamIndexOverride != null)
				{
					return;
				}
				CharacterMaster component = result.spawnedInstance.gameObject.GetComponent<CharacterMaster>();
				if (component)
				{
					component.inventory.GiveItem(RoR2Content.Items.CutHp, 1);
					GameObject bodyObject = component.GetBodyObject();
					if (bodyObject)
					{
						DeathRewards component2 = bodyObject.GetComponent<DeathRewards>();
						if (component2)
						{
							component2.spawnValue = (int)Mathf.Max(1f, (float)component2.spawnValue / 2f);
							component2.expReward = (uint)Mathf.Ceil(component2.expReward / 2f);
							component2.goldReward = (uint)Mathf.Ceil(component2.goldReward / 2f);
						}
					}
				}
				if (!SwarmsArtifactManager.inSpawn)
				{
					for (int i = 1; i < 2; i++)
					{
						SwarmsArtifactManager.inSpawn = true;
						try
						{
							DirectorCore.instance.TrySpawnObject(result.spawnRequest);
						}
						catch (Exception message)
						{
							Debug.LogError(message);
						}
						SwarmsArtifactManager.inSpawn = false;
					}
				}
			}
		}

		// Token: 0x04005034 RID: 20532
		private const int swarmSpawnCount = 2;

		// Token: 0x04005035 RID: 20533
		private static bool inSpawn;
	}
}
