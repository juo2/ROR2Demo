using System;
using System.Collections.Generic;
using HG;
using RoR2.ContentManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000942 RID: 2370
	public static class ItemTierCatalog
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x000E2516 File Offset: 0x000E0716
		public static ReadOnlyArray<ItemTierDef> allItemTierDefs
		{
			get
			{
				return ItemTierCatalog.itemTierDefs;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000E2522 File Offset: 0x000E0722
		public static int itemCount
		{
			get
			{
				return ItemTierCatalog.itemTierDefs.Length;
			}
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000E252C File Offset: 0x000E072C
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			ArrayUtils.CloneTo<ItemTierDef>(ContentManager.itemTierDefs, ref ItemTierCatalog.itemTierDefs);
			ItemTierCatalog.itemTierToDef.Clear();
			int num = 0;
			foreach (ItemTierDef itemTierDef in ItemTierCatalog.itemTierDefs)
			{
				if (itemTierDef.tier == ItemTier.AssignedAtRuntime)
				{
					itemTierDef.tier = ++num + ItemTier.AssignedAtRuntime;
				}
				if (ItemTierCatalog.itemTierToDef.ContainsKey(itemTierDef.tier))
				{
					Debug.LogError(string.Format("Duplicate TierDef for tier {0}", itemTierDef.tier));
				}
				else
				{
					ItemTierCatalog.itemTierToDef.Add(itemTierDef.tier, itemTierDef);
				}
			}
			ItemTierCatalog.availability.MakeAvailable();
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000E25D0 File Offset: 0x000E07D0
		public static ItemTierDef GetItemTierDef(ItemTier itemTier)
		{
			ItemTierDef result;
			if (ItemTierCatalog.itemTierToDef.TryGetValue(itemTier, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000E25F0 File Offset: 0x000E07F0
		public static ItemTierDef FindTierDef(string tierName)
		{
			foreach (ItemTierDef itemTierDef in ItemTierCatalog.itemTierDefs)
			{
				if (itemTierDef.name == tierName)
				{
					return itemTierDef;
				}
			}
			return null;
		}

		// Token: 0x04003661 RID: 13921
		private static ItemTierDef[] itemTierDefs = Array.Empty<ItemTierDef>();

		// Token: 0x04003662 RID: 13922
		public static ResourceAvailability availability = default(ResourceAvailability);

		// Token: 0x04003663 RID: 13923
		private static readonly Dictionary<ItemTier, ItemTierDef> itemTierToDef = new Dictionary<ItemTier, ItemTierDef>();
	}
}
