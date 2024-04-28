using System;
using RoR2.ExpansionManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x02000542 RID: 1346
	[CreateAssetMenu(menuName = "RoR2/ItemDef")]
	public class ItemDef : ScriptableObject
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x0006AE45 File Offset: 0x00069045
		public static void AttemptGrant(ref PickupDef.GrantContext context)
		{
			Inventory inventory = context.body.inventory;
			PickupDef pickupDef = PickupCatalog.GetPickupDef(context.controller.pickupIndex);
			inventory.GiveItem((pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None, 1);
			context.shouldDestroy = true;
			context.shouldNotify = true;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0006AE84 File Offset: 0x00069084
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x0006AEF4 File Offset: 0x000690F4
		public ItemIndex itemIndex
		{
			get
			{
				if (this._itemIndex == ItemIndex.None)
				{
					Debug.LogError("ItemDef '" + base.name + "' has an item index of 'None'.  Attempting to fix...");
					this._itemIndex = ItemCatalog.FindItemIndex(base.name);
					if (this._itemIndex != ItemIndex.None)
					{
						Debug.LogError(string.Format("Able to fix ItemDef '{0}' (item index = {1}).  This is probably because the asset is being duplicated across bundles.", base.name, this._itemIndex));
					}
				}
				return this._itemIndex;
			}
			set
			{
				this._itemIndex = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0006AEFD File Offset: 0x000690FD
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x0006AF1E File Offset: 0x0006911E
		public ItemTier tier
		{
			get
			{
				if (this._itemTierDef)
				{
					return this._itemTierDef.tier;
				}
				return this.deprecatedTier;
			}
			set
			{
				this._itemTierDef = ItemTierCatalog.GetItemTierDef(value);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0006AF2C File Offset: 0x0006912C
		[Obsolete("Get isDroppable from the ItemTierDef instead")]
		public bool inDroppableTier
		{
			get
			{
				ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(this.tier);
				return itemTierDef && itemTierDef.isDroppable;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0006AF55 File Offset: 0x00069155
		public Texture pickupIconTexture
		{
			get
			{
				if (!this.pickupIconSprite)
				{
					return null;
				}
				return this.pickupIconSprite.texture;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0006AF71 File Offset: 0x00069171
		[Obsolete("Get bgIconTexture from the ItemTierDef instead")]
		public Texture bgIconTexture
		{
			get
			{
				ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(this.tier);
				if (itemTierDef == null)
				{
					return null;
				}
				return itemTierDef.bgIconTexture;
			}
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0006AF89 File Offset: 0x00069189
		public bool ContainsTag(ItemTag tag)
		{
			return tag == ItemTag.Any || Array.IndexOf<ItemTag>(this.tags, tag) != -1;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0006AFA2 File Offset: 0x000691A2
		public bool DoesNotContainTag(ItemTag tag)
		{
			return Array.IndexOf<ItemTag>(this.tags, tag) == -1;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0006AFB4 File Offset: 0x000691B4
		public virtual PickupDef CreatePickupDef()
		{
			ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(this.tier);
			return new PickupDef
			{
				internalName = "ItemIndex." + base.name,
				itemIndex = this.itemIndex,
				itemTier = this.tier,
				displayPrefab = this.pickupModelPrefab,
				dropletDisplayPrefab = ((itemTierDef != null) ? itemTierDef.dropletDisplayPrefab : null),
				nameToken = this.nameToken,
				baseColor = ColorCatalog.GetColor(this.colorIndex),
				darkColor = ColorCatalog.GetColor(this.darkColorIndex),
				unlockableDef = this.unlockableDef,
				interactContextToken = "ITEM_PICKUP_CONTEXT",
				isLunar = (this.tier == ItemTier.Lunar),
				isBoss = (this.tier == ItemTier.Boss),
				iconTexture = this.pickupIconTexture,
				iconSprite = this.pickupIconSprite,
				attemptGrant = new PickupDef.AttemptGrantDelegate(ItemDef.AttemptGrant)
			};
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0006B0B5 File Offset: 0x000692B5
		[Obsolete("Get colorIndex from the ItemTierDef instead")]
		public ColorCatalog.ColorIndex colorIndex
		{
			get
			{
				ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(this.tier);
				if (itemTierDef == null)
				{
					return ColorCatalog.ColorIndex.Unaffordable;
				}
				return itemTierDef.colorIndex;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0006B0CE File Offset: 0x000692CE
		[Obsolete("Get darkColorIndex from the ItemTierDef instead")]
		public ColorCatalog.ColorIndex darkColorIndex
		{
			get
			{
				ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(this.tier);
				if (itemTierDef == null)
				{
					return ColorCatalog.ColorIndex.Unaffordable;
				}
				return itemTierDef.darkColorIndex;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0006B0E8 File Offset: 0x000692E8
		[ContextMenu("Auto Populate Tokens")]
		public void AutoPopulateTokens()
		{
			string arg = base.name.ToUpperInvariant();
			this.nameToken = string.Format("ITEM_{0}_NAME", arg);
			this.pickupToken = string.Format("ITEM_{0}_PICKUP", arg);
			this.descriptionToken = string.Format("ITEM_{0}_DESC", arg);
			this.loreToken = string.Format("ITEM_{0}_LORE", arg);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0006B148 File Offset: 0x00069348
		[ConCommand(commandName = "items_migrate", flags = ConVarFlags.None, helpText = "Generates ItemDef assets from the existing catalog entries.")]
		private static void CCItemsMigrate(ConCommandArgs args)
		{
			for (ItemIndex itemIndex = (ItemIndex)0; itemIndex < (ItemIndex)ItemCatalog.itemCount; itemIndex++)
			{
				EditorUtil.CopyToScriptableObject<ItemDef, ItemDef>(ItemCatalog.GetItemDef(itemIndex), "Assets/RoR2/Resources/ItemDefs/");
			}
		}

		// Token: 0x04001E15 RID: 7701
		private ItemIndex _itemIndex = ItemIndex.None;

		// Token: 0x04001E16 RID: 7702
		[SerializeField]
		[FormerlySerializedAs("tier")]
		[Obsolete("Replaced by itemTierDef field", false)]
		[Tooltip("Deprecated.  Use itemTierDef instead.")]
		private ItemTier deprecatedTier;

		// Token: 0x04001E17 RID: 7703
		[SerializeField]
		private ItemTierDef _itemTierDef;

		// Token: 0x04001E18 RID: 7704
		public string nameToken;

		// Token: 0x04001E19 RID: 7705
		public string pickupToken;

		// Token: 0x04001E1A RID: 7706
		public string descriptionToken;

		// Token: 0x04001E1B RID: 7707
		public string loreToken;

		// Token: 0x04001E1C RID: 7708
		public UnlockableDef unlockableDef;

		// Token: 0x04001E1D RID: 7709
		public GameObject pickupModelPrefab;

		// Token: 0x04001E1E RID: 7710
		public Sprite pickupIconSprite;

		// Token: 0x04001E1F RID: 7711
		public bool hidden;

		// Token: 0x04001E20 RID: 7712
		public bool canRemove = true;

		// Token: 0x04001E21 RID: 7713
		public ItemTag[] tags = Array.Empty<ItemTag>();

		// Token: 0x04001E22 RID: 7714
		public ExpansionDef requiredExpansion;

		// Token: 0x02000543 RID: 1347
		[Serializable]
		public struct Pair : IEquatable<ItemDef.Pair>
		{
			// Token: 0x0600188E RID: 6286 RVA: 0x0006B196 File Offset: 0x00069396
			public override int GetHashCode()
			{
				return this.itemDef1.GetHashCode() ^ ~this.itemDef2.GetHashCode();
			}

			// Token: 0x0600188F RID: 6287 RVA: 0x0006B1B0 File Offset: 0x000693B0
			public override bool Equals(object obj)
			{
				return obj is ItemDef.Pair && this.Equals((ItemDef.Pair)obj);
			}

			// Token: 0x06001890 RID: 6288 RVA: 0x0006B1C8 File Offset: 0x000693C8
			public bool Equals(ItemDef.Pair other)
			{
				return other.itemDef1 == this.itemDef1 && other.itemDef2 == this.itemDef2;
			}

			// Token: 0x04001E23 RID: 7715
			public ItemDef itemDef1;

			// Token: 0x04001E24 RID: 7716
			public ItemDef itemDef2;
		}
	}
}
