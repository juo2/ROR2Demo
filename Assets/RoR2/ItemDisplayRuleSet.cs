using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000544 RID: 1348
	[CreateAssetMenu(menuName = "RoR2/ItemDisplayRuleSet")]
	public class ItemDisplayRuleSet : ScriptableObject
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0006B1F0 File Offset: 0x000693F0
		private bool hasObsoleteNamedRuleGroups
		{
			get
			{
				return this.namedItemRuleGroups.Length != 0 || this.namedEquipmentRuleGroups.Length != 0;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0006B207 File Offset: 0x00069407
		public bool isEmpty
		{
			get
			{
				return this.keyAssetRuleGroups.Length == 0;
			}
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0006B214 File Offset: 0x00069414
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog),
			typeof(EquipmentCatalog)
		})]
		private static void Init()
		{
			ItemDisplayRuleSet.runtimeDependenciesReady = true;
			for (int i = 0; i < ItemDisplayRuleSet.instancesList.Count; i++)
			{
				ItemDisplayRuleSet.instancesList[i].GenerateRuntimeValues();
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0006B24C File Offset: 0x0006944C
		private void OnEnable()
		{
			ItemDisplayRuleSet.instancesList.Add(this);
			if (ItemDisplayRuleSet.runtimeDependenciesReady)
			{
				this.GenerateRuntimeValues();
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0006B266 File Offset: 0x00069466
		private void OnDisable()
		{
			ItemDisplayRuleSet.instancesList.Remove(this);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0006B274 File Offset: 0x00069474
		private void OnValidate()
		{
			if (this.hasObsoleteNamedRuleGroups)
			{
				Debug.LogWarningFormat(this, "ItemDisplayRuleSet \"{0}\" still defines one or more entries in an obsolete format. Run the upgrade from the inspector context menu.", new object[]
				{
					this
				});
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0006B294 File Offset: 0x00069494
		public void GenerateRuntimeValues()
		{
			this.runtimeItemRuleGroups = ItemCatalog.GetPerItemBuffer<DisplayRuleGroup>();
			this.runtimeEquipmentRuleGroups = EquipmentCatalog.GetPerEquipmentBuffer<DisplayRuleGroup>();
			ArrayUtils.SetAll<DisplayRuleGroup>(this.runtimeItemRuleGroups, DisplayRuleGroup.empty);
			ArrayUtils.SetAll<DisplayRuleGroup>(this.runtimeEquipmentRuleGroups, DisplayRuleGroup.empty);
			for (int i = 0; i < this.keyAssetRuleGroups.Length; i++)
			{
				ref ItemDisplayRuleSet.KeyAssetRuleGroup ptr = ref this.keyAssetRuleGroups[i];
				UnityEngine.Object keyAsset = ptr.keyAsset;
				if (keyAsset != null)
				{
					ItemDef itemDef;
					if ((itemDef = (keyAsset as ItemDef)) == null)
					{
						EquipmentDef equipmentDef;
						if ((equipmentDef = (keyAsset as EquipmentDef)) != null)
						{
							EquipmentDef equipmentDef2 = equipmentDef;
							if (equipmentDef2.equipmentIndex != EquipmentIndex.None)
							{
								this.runtimeEquipmentRuleGroups[(int)equipmentDef2.equipmentIndex] = ptr.displayRuleGroup;
							}
						}
					}
					else
					{
						ItemDef itemDef2 = itemDef;
						if (itemDef2.itemIndex != ItemIndex.None)
						{
							this.runtimeItemRuleGroups[(int)itemDef2.itemIndex] = ptr.displayRuleGroup;
						}
					}
				}
			}
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0006B368 File Offset: 0x00069568
		public DisplayRuleGroup FindDisplayRuleGroup(UnityEngine.Object keyAsset)
		{
			if (keyAsset)
			{
				for (int i = 0; i < this.keyAssetRuleGroups.Length; i++)
				{
					ref ItemDisplayRuleSet.KeyAssetRuleGroup ptr = ref this.keyAssetRuleGroups[i];
					if (ptr.keyAsset == keyAsset)
					{
						return ptr.displayRuleGroup;
					}
				}
			}
			return DisplayRuleGroup.empty;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0006B3B4 File Offset: 0x000695B4
		public void SetDisplayRuleGroup(UnityEngine.Object keyAsset, DisplayRuleGroup displayRuleGroup)
		{
			if (!keyAsset)
			{
				return;
			}
			for (int i = 0; i < this.keyAssetRuleGroups.Length; i++)
			{
				ref ItemDisplayRuleSet.KeyAssetRuleGroup ptr = ref this.keyAssetRuleGroups[i];
				if (ptr.keyAsset == keyAsset)
				{
					ptr.displayRuleGroup = displayRuleGroup;
					return;
				}
			}
			ItemDisplayRuleSet.KeyAssetRuleGroup keyAssetRuleGroup = default(ItemDisplayRuleSet.KeyAssetRuleGroup);
			keyAssetRuleGroup.keyAsset = keyAsset;
			keyAssetRuleGroup.displayRuleGroup = displayRuleGroup;
			ArrayUtils.ArrayAppend<ItemDisplayRuleSet.KeyAssetRuleGroup>(ref this.keyAssetRuleGroups, keyAssetRuleGroup);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0006B420 File Offset: 0x00069620
		public DisplayRuleGroup GetItemDisplayRuleGroup(ItemIndex itemIndex)
		{
			return ArrayUtils.GetSafe<DisplayRuleGroup>(this.runtimeItemRuleGroups, (int)itemIndex, DisplayRuleGroup.empty);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0006B433 File Offset: 0x00069633
		public DisplayRuleGroup GetEquipmentDisplayRuleGroup(EquipmentIndex equipmentIndex)
		{
			return ArrayUtils.GetSafe<DisplayRuleGroup>(this.runtimeEquipmentRuleGroups, (int)equipmentIndex, DisplayRuleGroup.empty);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0006B446 File Offset: 0x00069646
		public void Reset()
		{
			this.keyAssetRuleGroups = Array.Empty<ItemDisplayRuleSet.KeyAssetRuleGroup>();
			this.runtimeItemRuleGroups = Array.Empty<DisplayRuleGroup>();
			this.runtimeEquipmentRuleGroups = Array.Empty<DisplayRuleGroup>();
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0006B46C File Offset: 0x0006966C
		[ContextMenu("Upgrade to keying by asset")]
		public void UpdgradeToAssetKeying()
		{
			if (!Application.isPlaying)
			{
				Debug.Log("Cannot run upgrade outside play mode, where catalogs are unavailable.");
			}
			if (!this.hasObsoleteNamedRuleGroups)
			{
				return;
			}
			List<string> list = new List<string>();
			this.UpgradeNamedRuleGroups(ref this.namedItemRuleGroups, "ItemDef", (string assetName) => ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex(assetName)), list);
			this.UpgradeNamedRuleGroups(ref this.namedEquipmentRuleGroups, "EquipmentDef", (string assetName) => EquipmentCatalog.GetEquipmentDef(EquipmentCatalog.FindEquipmentIndex(assetName)), list);
			if (list.Count > 0)
			{
				Debug.LogWarningFormat("Encountered {0} errors attempting to upgrade ItemDisplayRuleSet \"{1}\":\n{2}", new object[]
				{
					list.Count,
					base.name,
					string.Join("\n", list)
				});
			}
			EditorUtil.SetDirty(this);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0006B540 File Offset: 0x00069740
		private void UpgradeNamedRuleGroups(ref ItemDisplayRuleSet.NamedRuleGroup[] namedRuleGroups, string assetTypeName, Func<string, UnityEngine.Object> assetLookupMethod, List<string> failureMessagesList)
		{
			int num = namedRuleGroups.Length;
			for (int i = 0; i < num; i++)
			{
				ItemDisplayRuleSet.NamedRuleGroup namedRuleGroup = namedRuleGroups[i];
				UnityEngine.Object @object = assetLookupMethod(namedRuleGroup.name);
				string text = null;
				if (!namedRuleGroup.displayRuleGroup.isEmpty)
				{
					if (@object)
					{
						if (this.FindDisplayRuleGroup(@object).isEmpty)
						{
							this.SetDisplayRuleGroup(@object, namedRuleGroup.displayRuleGroup);
						}
						else
						{
							text = "Conflicts with existing rule group.";
						}
					}
					else
					{
						text = "Named asset not found.";
					}
				}
				if (text != null)
				{
					failureMessagesList.Add(string.Concat(new string[]
					{
						assetTypeName,
						" \"",
						namedRuleGroup.name,
						"\": ",
						text
					}));
				}
				else
				{
					ArrayUtils.ArrayRemoveAt<ItemDisplayRuleSet.NamedRuleGroup>(namedRuleGroups, ref num, i, 1);
					i--;
				}
			}
			Array.Resize<ItemDisplayRuleSet.NamedRuleGroup>(ref namedRuleGroups, num);
		}

		// Token: 0x04001E25 RID: 7717
		[SerializeField]
		[Obsolete("Use .assetRuleGroups instead.")]
		private ItemDisplayRuleSet.NamedRuleGroup[] namedItemRuleGroups = Array.Empty<ItemDisplayRuleSet.NamedRuleGroup>();

		// Token: 0x04001E26 RID: 7718
		[SerializeField]
		[Obsolete("Use .assetRuleGroups instead.")]
		private ItemDisplayRuleSet.NamedRuleGroup[] namedEquipmentRuleGroups = Array.Empty<ItemDisplayRuleSet.NamedRuleGroup>();

		// Token: 0x04001E27 RID: 7719
		public ItemDisplayRuleSet.KeyAssetRuleGroup[] keyAssetRuleGroups = Array.Empty<ItemDisplayRuleSet.KeyAssetRuleGroup>();

		// Token: 0x04001E28 RID: 7720
		private DisplayRuleGroup[] runtimeItemRuleGroups;

		// Token: 0x04001E29 RID: 7721
		private DisplayRuleGroup[] runtimeEquipmentRuleGroups;

		// Token: 0x04001E2A RID: 7722
		private static readonly List<ItemDisplayRuleSet> instancesList = new List<ItemDisplayRuleSet>();

		// Token: 0x04001E2B RID: 7723
		private static bool runtimeDependenciesReady = false;

		// Token: 0x02000545 RID: 1349
		[Obsolete]
		[Serializable]
		public struct NamedRuleGroup
		{
			// Token: 0x04001E2C RID: 7724
			public string name;

			// Token: 0x04001E2D RID: 7725
			public DisplayRuleGroup displayRuleGroup;
		}

		// Token: 0x02000546 RID: 1350
		[Serializable]
		public struct KeyAssetRuleGroup
		{
			// Token: 0x04001E2E RID: 7726
			public UnityEngine.Object keyAsset;

			// Token: 0x04001E2F RID: 7727
			public DisplayRuleGroup displayRuleGroup;
		}
	}
}
