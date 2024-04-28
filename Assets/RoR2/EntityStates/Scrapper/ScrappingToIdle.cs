using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Scrapper
{
	// Token: 0x020001D0 RID: 464
	public class ScrappingToIdle : ScrapperBaseState
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool enableInteraction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00023394 File Offset: 0x00021594
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(ScrappingToIdle.enterSoundString, base.gameObject);
			base.PlayAnimation("Base", "ScrappingToIdle", "Scrapping.playbackRate", ScrappingToIdle.duration);
			if (ScrappingToIdle.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(ScrappingToIdle.muzzleflashEffectPrefab, base.gameObject, ScrappingToIdle.muzzleString, false);
			}
			if (!NetworkServer.active)
			{
				return;
			}
			this.foundValidScrap = false;
			PickupIndex pickupIndex = PickupIndex.none;
			ItemDef itemDef = ItemCatalog.GetItemDef(this.scrapperController.lastScrappedItemIndex);
			if (itemDef != null)
			{
				switch (itemDef.tier)
				{
				case ItemTier.Tier1:
					pickupIndex = PickupCatalog.FindPickupIndex("ItemIndex.ScrapWhite");
					break;
				case ItemTier.Tier2:
					pickupIndex = PickupCatalog.FindPickupIndex("ItemIndex.ScrapGreen");
					break;
				case ItemTier.Tier3:
					pickupIndex = PickupCatalog.FindPickupIndex("ItemIndex.ScrapRed");
					break;
				case ItemTier.Boss:
					pickupIndex = PickupCatalog.FindPickupIndex("ItemIndex.ScrapYellow");
					break;
				}
			}
			if (pickupIndex != PickupIndex.none)
			{
				this.foundValidScrap = true;
				Transform transform = base.FindModelChild(ScrappingToIdle.muzzleString);
				PickupDropletController.CreatePickupDroplet(pickupIndex, transform.position, Vector3.up * ScrappingToIdle.dropUpVelocityStrength + transform.forward * ScrappingToIdle.dropForwardVelocityStrength);
				ScrapperController scrapperController = this.scrapperController;
				int itemsEaten = scrapperController.itemsEaten;
				scrapperController.itemsEaten = itemsEaten - 1;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000234DF File Offset: 0x000216DF
		public override void OnExit()
		{
			Util.PlaySound(ScrappingToIdle.exitSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000234F8 File Offset: 0x000216F8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.foundValidScrap && this.scrapperController.itemsEaten > 0 && base.fixedAge > ScrappingToIdle.duration / 2f)
			{
				this.outer.SetNextState(new ScrappingToIdle());
				return;
			}
			if (base.fixedAge > ScrappingToIdle.duration)
			{
				this.outer.SetNextState(new Idle());
			}
		}

		// Token: 0x040009C3 RID: 2499
		public static string enterSoundString;

		// Token: 0x040009C4 RID: 2500
		public static string exitSoundString;

		// Token: 0x040009C5 RID: 2501
		public static float duration;

		// Token: 0x040009C6 RID: 2502
		public static float dropUpVelocityStrength;

		// Token: 0x040009C7 RID: 2503
		public static float dropForwardVelocityStrength;

		// Token: 0x040009C8 RID: 2504
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x040009C9 RID: 2505
		public static string muzzleString;

		// Token: 0x040009CA RID: 2506
		private bool foundValidScrap;
	}
}
