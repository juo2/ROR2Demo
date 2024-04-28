using System;
using RoR2.Navigation;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000551 RID: 1361
	public class MasterCopySpawnCard : CharacterSpawnCard
	{
		// Token: 0x060018B6 RID: 6326 RVA: 0x0006B98A File Offset: 0x00069B8A
		public static MasterCopySpawnCard FromMaster(CharacterMaster srcCharacterMaster, bool copyItems, bool copyEquipment, Action<CharacterMaster> onPreSpawnSetup = null)
		{
			if (!srcCharacterMaster || !srcCharacterMaster.GetBody())
			{
				return null;
			}
			MasterCopySpawnCard masterCopySpawnCard = ScriptableObject.CreateInstance<MasterCopySpawnCard>();
			masterCopySpawnCard.onPreSpawnSetup = onPreSpawnSetup;
			MasterCopySpawnCard.CopyDataFromMaster(masterCopySpawnCard, srcCharacterMaster, copyItems, copyEquipment);
			return masterCopySpawnCard;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0006B9B8 File Offset: 0x00069BB8
		protected static void CopyDataFromMaster(MasterCopySpawnCard spawnCard, CharacterMaster srcCharacterMaster, bool copyItems, bool copyEquipment)
		{
			spawnCard.srcItemStacks = ItemCatalog.RequestItemStackArray();
			spawnCard.srcEquipment = Array.Empty<EquipmentIndex>();
			if (srcCharacterMaster)
			{
				spawnCard.sendOverNetwork = true;
				spawnCard.runtimeLoadout = new Loadout();
				spawnCard.srcCharacterMaster = srcCharacterMaster;
				spawnCard.srcCharacterMaster.loadout.Copy(spawnCard.runtimeLoadout);
				if (copyItems)
				{
					srcCharacterMaster.inventory.WriteItemStacks(spawnCard.srcItemStacks);
				}
				if (copyEquipment)
				{
					spawnCard.srcEquipment = new EquipmentIndex[srcCharacterMaster.inventory.GetEquipmentSlotCount()];
					uint num = 0U;
					while ((ulong)num < (ulong)((long)spawnCard.srcEquipment.Length))
					{
						spawnCard.srcEquipment[(int)num] = srcCharacterMaster.inventory.GetEquipment(num).equipmentIndex;
						num += 1U;
					}
				}
				CharacterBody body = srcCharacterMaster.GetBody();
				if (body)
				{
					spawnCard.hullSize = body.hullClassification;
					spawnCard.nodeGraphType = (body.isFlying ? MapNodeGroup.GraphType.Air : MapNodeGroup.GraphType.Ground);
					spawnCard.prefab = MasterCatalog.GetMasterPrefab(MasterCatalog.FindAiMasterIndexForBody(body.bodyIndex));
				}
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0006BAB3 File Offset: 0x00069CB3
		public void GiveItem(ItemIndex itemIndex, int count = 1)
		{
			this.srcItemStacks[(int)itemIndex] += count;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0006BAC6 File Offset: 0x00069CC6
		public void GiveItem(ItemDef itemDef, int count = 1)
		{
			this.GiveItem((itemDef != null) ? itemDef.itemIndex : ItemIndex.None, count);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0006BADB File Offset: 0x00069CDB
		protected override Loadout GetRuntimeLoadout()
		{
			return this.srcCharacterMaster.loadout;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0006BAE8 File Offset: 0x00069CE8
		protected override Action<CharacterMaster> GetPreSpawnSetupCallback()
		{
			MasterCopySpawnCard.<>c__DisplayClass9_0 CS$<>8__locals1 = new MasterCopySpawnCard.<>c__DisplayClass9_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.baseCallback = base.GetPreSpawnSetupCallback();
			return new Action<CharacterMaster>(CS$<>8__locals1.<GetPreSpawnSetupCallback>g__Callback|0);
		}

		// Token: 0x04001E58 RID: 7768
		protected CharacterMaster srcCharacterMaster;

		// Token: 0x04001E59 RID: 7769
		protected int[] srcItemStacks;

		// Token: 0x04001E5A RID: 7770
		protected EquipmentIndex[] srcEquipment;

		// Token: 0x04001E5B RID: 7771
		protected Action<CharacterMaster> onPreSpawnSetup;
	}
}
