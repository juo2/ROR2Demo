using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001D8 RID: 472
	public class FindItem : BaseState
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x00023B40 File Offset: 0x00021D40
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FindItem.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Body", "SitRummage", "Sit.playbackRate", this.duration, 0.1f);
			Util.PlaySound(FindItem.sound, base.gameObject);
			if (base.isAuthority)
			{
				WeightedSelection<List<PickupIndex>> weightedSelection = new WeightedSelection<List<PickupIndex>>(8);
				weightedSelection.AddChoice(Run.instance.availableTier1DropList.Where(new Func<PickupIndex, bool>(this.PickupIsNonBlacklistedItem)).ToList<PickupIndex>(), FindItem.tier1Chance);
				weightedSelection.AddChoice(Run.instance.availableTier2DropList.Where(new Func<PickupIndex, bool>(this.PickupIsNonBlacklistedItem)).ToList<PickupIndex>(), FindItem.tier2Chance);
				weightedSelection.AddChoice(Run.instance.availableTier3DropList.Where(new Func<PickupIndex, bool>(this.PickupIsNonBlacklistedItem)).ToList<PickupIndex>(), FindItem.tier3Chance);
				List<PickupIndex> list = weightedSelection.Evaluate(UnityEngine.Random.value);
				this.dropPickup = list[UnityEngine.Random.Range(0, list.Count)];
				PickupDef pickupDef = PickupCatalog.GetPickupDef(this.dropPickup);
				if (pickupDef != null)
				{
					ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
					if (itemDef != null)
					{
						this.itemsToGrant = 0;
						switch (itemDef.tier)
						{
						case ItemTier.Tier1:
							this.itemsToGrant = FindItem.tier1Count;
							break;
						case ItemTier.Tier2:
							this.itemsToGrant = FindItem.tier2Count;
							break;
						case ItemTier.Tier3:
							this.itemsToGrant = FindItem.tier3Count;
							break;
						default:
							this.itemsToGrant = 1;
							break;
						}
					}
				}
			}
			Transform transform = base.FindModelChild("PickupDisplay");
			this.pickupDisplay = transform.GetComponent<PickupDisplay>();
			this.pickupDisplay.SetPickupIndex(this.dropPickup, false);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00023CEB File Offset: 0x00021EEB
		public override void OnExit()
		{
			this.pickupDisplay.SetPickupIndex(PickupIndex.none, false);
			base.OnExit();
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00023D04 File Offset: 0x00021F04
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextState(new GrantItem
				{
					dropPickup = this.dropPickup,
					itemsToGrant = this.itemsToGrant
				});
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00023D55 File Offset: 0x00021F55
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.dropPickup);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00023D6A File Offset: 0x00021F6A
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.dropPickup = reader.ReadPickupIndex();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00023D80 File Offset: 0x00021F80
		private bool PickupIsNonBlacklistedItem(PickupIndex pickupIndex)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (pickupDef == null)
			{
				return false;
			}
			ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
			return !(itemDef == null) && itemDef.DoesNotContainTag(ItemTag.AIBlacklist);
		}

		// Token: 0x040009EC RID: 2540
		public static float baseDuration;

		// Token: 0x040009ED RID: 2541
		public static float tier1Chance;

		// Token: 0x040009EE RID: 2542
		public static int tier1Count;

		// Token: 0x040009EF RID: 2543
		public static float tier2Chance;

		// Token: 0x040009F0 RID: 2544
		public static int tier2Count;

		// Token: 0x040009F1 RID: 2545
		public static float tier3Chance;

		// Token: 0x040009F2 RID: 2546
		public static int tier3Count;

		// Token: 0x040009F3 RID: 2547
		public static string sound;

		// Token: 0x040009F4 RID: 2548
		private float duration;

		// Token: 0x040009F5 RID: 2549
		private PickupIndex dropPickup;

		// Token: 0x040009F6 RID: 2550
		private int itemsToGrant;

		// Token: 0x040009F7 RID: 2551
		private PickupDisplay pickupDisplay;
	}
}
