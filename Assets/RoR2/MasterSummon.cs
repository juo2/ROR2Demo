using System;
using JetBrains.Annotations;
using RoR2.CharacterAI;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000967 RID: 2407
	public class MasterSummon
	{
		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x060036BA RID: 14010 RVA: 0x000E6F2C File Offset: 0x000E512C
		// (remove) Token: 0x060036BB RID: 14011 RVA: 0x000E6F60 File Offset: 0x000E5160
		public static event Action<MasterSummon.MasterSummonReport> onServerMasterSummonGlobal;

		// Token: 0x060036BC RID: 14012 RVA: 0x000E6F94 File Offset: 0x000E5194
		public CharacterMaster Perform()
		{
			Debug.Log("CharacterMaster Perform this.masterPrefab:" + this.masterPrefab.name);
			TeamIndex teamIndex;
			if (this.teamIndexOverride != null)
			{
				teamIndex = this.teamIndexOverride.Value;
			}
			else
			{
				if (!this.summonerBodyObject)
				{
					Debug.LogErrorFormat("Cannot spawn master {0}: No team specified.", new object[]
					{
						this.masterPrefab
					});
					return null;
				}
				teamIndex = TeamComponent.GetObjectTeam(this.summonerBodyObject);
			}
			if (!this.ignoreTeamMemberLimit)
			{
				TeamDef teamDef = TeamCatalog.GetTeamDef(teamIndex);
				if (teamDef == null)
				{
					Debug.LogErrorFormat("Attempting to spawn master {0} on TeamIndex.None. Is this intentional?", new object[]
					{
						this.masterPrefab
					});
					return null;
				}
				if (teamDef != null && teamDef.softCharacterLimit <= TeamComponent.GetTeamMembers(teamIndex).Count)
				{
					return null;
				}
			}
			CharacterBody characterBody = null;
			CharacterMaster characterMaster = null;
			SkinDef skinDef = null;
			if (this.summonerBodyObject)
			{
				characterBody = this.summonerBodyObject.GetComponent<CharacterBody>();
				skinDef = SkinCatalog.FindCurrentSkinDefForBodyInstance(this.summonerBodyObject);
			}
			if (characterBody)
			{
				characterMaster = characterBody.master;
			}
			Inventory inventory = (characterMaster != null) ? characterMaster.inventory : null;
			GameObject masterParent = GameObject.Find("masterParent");
			if (masterParent == null)
			{
				masterParent = new GameObject("masterParent");
				UnityEngine.Object.DontDestroyOnLoad(masterParent);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.masterPrefab, this.position, this.rotation, masterParent.transform);
			CharacterMaster component = gameObject.GetComponent<CharacterMaster>();
			component.teamIndex = teamIndex;
			Loadout loadout = Loadout.RequestInstance();
			Loadout loadout2 = this.loadout;
			if (loadout2 != null)
			{
				loadout2.Copy(loadout);
			}
			if (skinDef)
			{
				SkinDef.MinionSkinReplacement[] minionSkinReplacements = skinDef.minionSkinReplacements;
				if (minionSkinReplacements.Length != 0)
				{
					for (int i = 0; i < minionSkinReplacements.Length; i++)
					{
						BodyIndex bodyIndex = BodyCatalog.FindBodyIndex(minionSkinReplacements[i].minionBodyPrefab);
						int num = SkinCatalog.FindLocalSkinIndexForBody(bodyIndex, minionSkinReplacements[i].minionSkin);
						if (num != -1)
						{
							loadout.bodyLoadoutManager.SetSkinIndex(bodyIndex, (uint)num);
						}
					}
				}
			}
			component.SetLoadoutServer(loadout);
			Loadout.ReturnInstance(loadout);
			CharacterMaster characterMaster2 = characterMaster;
			if (characterMaster2 && characterMaster2.minionOwnership.ownerMaster)
			{
				characterMaster2 = characterMaster2.minionOwnership.ownerMaster;
			}
			component.minionOwnership.SetOwner(characterMaster2);
			if (this.summonerBodyObject)
			{
				AIOwnership component2 = gameObject.GetComponent<AIOwnership>();
				if (component2)
				{
					if (characterMaster)
					{
						component2.ownerMaster = characterMaster;
					}
					CharacterBody component3 = this.summonerBodyObject.GetComponent<CharacterBody>();
					if (component3)
					{
						CharacterMaster master = component3.master;
						if (master)
						{
							component2.ownerMaster = master;
						}
					}
				}
				BaseAI component4 = gameObject.GetComponent<BaseAI>();
				if (component4)
				{
					component4.leader.gameObject = this.summonerBodyObject;
				}
			}
			if (this.inventoryToCopy)
			{
				component.inventory.CopyEquipmentFrom(this.inventoryToCopy);
				component.inventory.CopyItemsFrom(this.inventoryToCopy, this.inventoryItemCopyFilter ?? Inventory.defaultItemCopyFilterDelegate);
			}
			MasterSummon.IInventorySetupCallback inventorySetupCallback = this.inventorySetupCallback;
			if (inventorySetupCallback != null)
			{
				inventorySetupCallback.SetupSummonedInventory(this, component.inventory);
			}
			bool flag = false;
			if (this.useAmbientLevel == null)
			{
				if (inventory && inventory.GetItemCount(RoR2Content.Items.UseAmbientLevel) > 0)
				{
					flag = true;
				}
			}
			else
			{
				flag = this.useAmbientLevel.Value;
			}
			if (flag)
			{
				component.inventory.GiveItem(RoR2Content.Items.UseAmbientLevel, 1);
			}
			if (inventory)
			{
				bool? flag2 = this.useAmbientLevel;
				bool flag3 = false;
				if (!(flag2.GetValueOrDefault() == flag3 & flag2 != null))
				{
					component.inventory.GiveItem(RoR2Content.Items.UseAmbientLevel, inventory.GetItemCount(RoR2Content.Items.UseAmbientLevel));
				}
			}
			Action<CharacterMaster> action = this.preSpawnSetupCallback;
			if (action != null)
			{
				action(component);
			}
			NetworkServer.Spawn(gameObject);
			component.Respawn(this.position, this.rotation);
			Action<MasterSummon.MasterSummonReport> action2 = MasterSummon.onServerMasterSummonGlobal;
			if (action2 != null)
			{
				action2(new MasterSummon.MasterSummonReport
				{
					leaderMasterInstance = characterMaster2,
					summonMasterInstance = component
				});
			}
			return component;
		}

		// Token: 0x04003721 RID: 14113
		public GameObject masterPrefab;

		// Token: 0x04003722 RID: 14114
		public Vector3 position;

		// Token: 0x04003723 RID: 14115
		public Quaternion rotation;

		// Token: 0x04003724 RID: 14116
		public GameObject summonerBodyObject;

		// Token: 0x04003725 RID: 14117
		public TeamIndex? teamIndexOverride;

		// Token: 0x04003726 RID: 14118
		public bool ignoreTeamMemberLimit;

		// Token: 0x04003727 RID: 14119
		public Action<CharacterMaster> preSpawnSetupCallback;

		// Token: 0x04003728 RID: 14120
		public Loadout loadout;

		// Token: 0x04003729 RID: 14121
		public bool? useAmbientLevel;

		// Token: 0x0400372B RID: 14123
		[CanBeNull]
		public Inventory inventoryToCopy;

		// Token: 0x0400372C RID: 14124
		[CanBeNull]
		public Func<ItemIndex, bool> inventoryItemCopyFilter;

		// Token: 0x0400372D RID: 14125
		public MasterSummon.IInventorySetupCallback inventorySetupCallback;

		// Token: 0x02000968 RID: 2408
		public struct MasterSummonReport
		{
			// Token: 0x0400372E RID: 14126
			public CharacterMaster summonMasterInstance;

			// Token: 0x0400372F RID: 14127
			public CharacterMaster leaderMasterInstance;
		}

		// Token: 0x02000969 RID: 2409
		public interface IInventorySetupCallback
		{
			// Token: 0x060036BE RID: 14014
			void SetupSummonedInventory([NotNull] MasterSummon masterSummon, [NotNull] Inventory summonedInventory);
		}
	}
}
