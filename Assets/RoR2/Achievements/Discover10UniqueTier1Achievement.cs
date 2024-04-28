using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E91 RID: 3729
	[RegisterAchievement("Discover10UniqueTier1", "Items.Crowbar", null, null)]
	public class Discover10UniqueTier1Achievement : BaseAchievement
	{
		// Token: 0x06005541 RID: 21825 RVA: 0x0015DF38 File Offset: 0x0015C138
		public override void OnInstall()
		{
			base.OnInstall();
			UserProfile userProfile = base.userProfile;
			userProfile.onPickupDiscovered = (Action<PickupIndex>)Delegate.Combine(userProfile.onPickupDiscovered, new Action<PickupIndex>(this.OnPickupDiscovered));
			this.Check();
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x0015DF6D File Offset: 0x0015C16D
		public override void OnUninstall()
		{
			UserProfile userProfile = base.userProfile;
			userProfile.onPickupDiscovered = (Action<PickupIndex>)Delegate.Remove(userProfile.onPickupDiscovered, new Action<PickupIndex>(this.OnPickupDiscovered));
			base.OnUninstall();
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x0015DF9C File Offset: 0x0015C19C
		public override float ProgressForAchievement()
		{
			return (float)this.UniqueTier1Discovered() / 10f;
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x0015DFAC File Offset: 0x0015C1AC
		private void OnPickupDiscovered(PickupIndex pickupIndex)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			ItemIndex itemIndex = (pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None;
			if (itemIndex != ItemIndex.None && ItemCatalog.GetItemDef(itemIndex).tier == ItemTier.Tier1)
			{
				this.Check();
			}
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x0015DFE4 File Offset: 0x0015C1E4
		private int UniqueTier1Discovered()
		{
			int num = 0;
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				if (ItemCatalog.GetItemDef(itemIndex).tier == ItemTier.Tier1 && base.userProfile.HasDiscoveredPickup(PickupCatalog.FindPickupIndex(itemIndex)))
				{
					num++;
				}
				itemIndex++;
			}
			return num;
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x0015E02A File Offset: 0x0015C22A
		private void Check()
		{
			if (this.UniqueTier1Discovered() >= 10)
			{
				base.Grant();
			}
		}

		// Token: 0x04005056 RID: 20566
		private const int requirement = 10;
	}
}
