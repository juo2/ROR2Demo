using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Artifacts
{
	// Token: 0x02000E70 RID: 3696
	public static class TeamDeathArtifactManager
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x0600549D RID: 21661 RVA: 0x0015CD9C File Offset: 0x0015AF9C
		private static ArtifactDef myArtifactDef
		{
			get
			{
				return RoR2Content.Artifacts.teamDeathArtifactDef;
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0015CDA3 File Offset: 0x0015AFA3
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			RunArtifactManager.onArtifactEnabledGlobal += TeamDeathArtifactManager.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal += TeamDeathArtifactManager.OnArtifactDisabled;
			TeamDeathArtifactManager.forceSpectatePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/ForceSpectate");
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0015CDD6 File Offset: 0x0015AFD6
		private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != TeamDeathArtifactManager.myArtifactDef)
			{
				return;
			}
			GlobalEventManager.onCharacterDeathGlobal += TeamDeathArtifactManager.OnServerCharacterDeathGlobal;
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0015CDF7 File Offset: 0x0015AFF7
		private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != TeamDeathArtifactManager.myArtifactDef)
			{
				return;
			}
			GlobalEventManager.onCharacterDeathGlobal -= TeamDeathArtifactManager.OnServerCharacterDeathGlobal;
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0015CE18 File Offset: 0x0015B018
		private static void OnServerCharacterDeathGlobal(DamageReport damageReport)
		{
			if (TeamDeathArtifactManager.inTeamKill)
			{
				return;
			}
			if (damageReport.victimMaster && damageReport.victimMaster.playerCharacterMasterController)
			{
				TeamDeathArtifactManager.<>c__DisplayClass7_0 CS$<>8__locals1 = new TeamDeathArtifactManager.<>c__DisplayClass7_0();
				if (damageReport.victimMaster.inventory.GetItemCount(RoR2Content.Items.ExtraLife) > 0 || damageReport.victimMaster.inventory.GetItemCount(DLC1Content.Items.ExtraLifeVoid) > 0)
				{
					return;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(TeamDeathArtifactManager.forceSpectatePrefab);
				gameObject.GetComponent<ForceSpectate>().Networktarget = damageReport.victimBody.gameObject;
				NetworkServer.Spawn(gameObject);
				CS$<>8__locals1.teamIndex = damageReport.victimTeamIndex;
				PlayerCharacterMasterController playerCharacterMasterController = damageReport.victimMaster.playerCharacterMasterController;
				CS$<>8__locals1.victimNetworkUser = (playerCharacterMasterController ? playerCharacterMasterController.networkUser : null);
				RoR2Application.onNextUpdate += CS$<>8__locals1.<OnServerCharacterDeathGlobal>g__KillTeam|0;
			}
		}

		// Token: 0x04005036 RID: 20534
		private static GameObject forceSpectatePrefab;

		// Token: 0x04005037 RID: 20535
		private static bool inTeamKill;
	}
}
