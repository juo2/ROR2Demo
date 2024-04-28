using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace RoR2.Artifacts
{
	// Token: 0x02000E6A RID: 3690
	public static class EnigmaArtifactManager
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x0015C3C7 File Offset: 0x0015A5C7
		private static ArtifactDef myArtifact
		{
			get
			{
				return RoR2Content.Artifacts.enigmaArtifactDef;
			}
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x0015C3D0 File Offset: 0x0015A5D0
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog),
			typeof(BodyCatalog)
		})]
		private static void Init()
		{
			RunArtifactManager.onArtifactEnabledGlobal += EnigmaArtifactManager.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal += EnigmaArtifactManager.OnArtifactDisabled;
			Run.onRunStartGlobal += EnigmaArtifactManager.OnRunStartGlobal;
			EnigmaArtifactManager.toolbotBodyIndex = BodyCatalog.FindBodyIndex("ToolbotBody");
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x0015C420 File Offset: 0x0015A620
		private static void OnRunStartGlobal(Run run)
		{
			if (NetworkServer.active)
			{
				EnigmaArtifactManager.serverInitialEquipmentRng.ResetSeed(run.seed);
				EnigmaArtifactManager.serverActivationEquipmentRng.ResetSeed(EnigmaArtifactManager.serverInitialEquipmentRng.nextUlong);
				EnigmaArtifactManager.validEquipment.Clear();
				foreach (EquipmentIndex equipmentIndex in EquipmentCatalog.enigmaEquipmentList)
				{
					EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
					if (equipmentDef && (!equipmentDef.requiredExpansion || run.IsExpansionEnabled(equipmentDef.requiredExpansion)))
					{
						EnigmaArtifactManager.validEquipment.Add(equipmentIndex);
					}
				}
			}
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x0015C4D8 File Offset: 0x0015A6D8
		private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != EnigmaArtifactManager.myArtifact)
			{
				return;
			}
			if (NetworkServer.active)
			{
				CharacterBody.onBodyStartGlobal += EnigmaArtifactManager.OnBodyStartGlobalServer;
				EquipmentSlot.onServerEquipmentActivated += EnigmaArtifactManager.OnServerEquipmentActivated;
			}
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0015C511 File Offset: 0x0015A711
		private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != EnigmaArtifactManager.myArtifact)
			{
				return;
			}
			EquipmentSlot.onServerEquipmentActivated -= EnigmaArtifactManager.OnServerEquipmentActivated;
			CharacterBody.onBodyStartGlobal -= EnigmaArtifactManager.OnBodyStartGlobalServer;
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x0015C543 File Offset: 0x0015A743
		private static void OnBodyStartGlobalServer(CharacterBody characterBody)
		{
			if (characterBody.isPlayerControlled)
			{
				EnigmaArtifactManager.OnPlayerCharacterBodyStartServer(characterBody);
			}
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x0015C554 File Offset: 0x0015A754
		private static void OnPlayerCharacterBodyStartServer(CharacterBody characterBody)
		{
			Inventory inventory = characterBody.inventory;
			int val = (characterBody.bodyIndex == EnigmaArtifactManager.toolbotBodyIndex) ? 2 : 1;
			int num = Math.Max(inventory.GetEquipmentSlotCount(), val);
			for (int i = 0; i < num; i++)
			{
				if (inventory.GetEquipment((uint)i).equipmentIndex == EquipmentIndex.None)
				{
					EquipmentIndex randomEquipment = EnigmaArtifactManager.GetRandomEquipment(EnigmaArtifactManager.serverInitialEquipmentRng, (int)(i + characterBody.bodyIndex));
					EquipmentState equipmentState = new EquipmentState(randomEquipment, Run.FixedTimeStamp.negativeInfinity, 1);
					inventory.SetEquipment(equipmentState, (uint)i);
				}
			}
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x0015C5D0 File Offset: 0x0015A7D0
		private static void OnServerEquipmentActivated(EquipmentSlot equipmentSlot, EquipmentIndex equipmentIndex)
		{
			EquipmentIndex randomEquipment = EnigmaArtifactManager.GetRandomEquipment(EnigmaArtifactManager.serverActivationEquipmentRng, (int)equipmentIndex);
			equipmentSlot.characterBody.inventory.SetEquipmentIndex(randomEquipment);
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x0015C5FC File Offset: 0x0015A7FC
		private static EquipmentIndex GetRandomEquipment(Xoroshiro128Plus rng, int offset)
		{
			int count = EnigmaArtifactManager.validEquipment.Count;
			int num = rng.RangeInt(0, count);
			num += offset;
			num %= count;
			return EnigmaArtifactManager.validEquipment[num];
		}

		// Token: 0x04005028 RID: 20520
		private static readonly Xoroshiro128Plus serverInitialEquipmentRng = new Xoroshiro128Plus(0UL);

		// Token: 0x04005029 RID: 20521
		private static readonly Xoroshiro128Plus serverActivationEquipmentRng = new Xoroshiro128Plus(0UL);

		// Token: 0x0400502A RID: 20522
		private static List<EquipmentIndex> validEquipment = new List<EquipmentIndex>();

		// Token: 0x0400502B RID: 20523
		private static BodyIndex toolbotBodyIndex = BodyIndex.None;
	}
}
