using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E92 RID: 3730
	[RegisterAchievement("Discover5Equipment", "Items.EquipmentMagazine", null, null)]
	public class Discover5EquipmentAchievement : BaseAchievement
	{
		// Token: 0x06005548 RID: 21832 RVA: 0x0015E03C File Offset: 0x0015C23C
		public override void OnInstall()
		{
			base.OnInstall();
			UserProfile userProfile = base.userProfile;
			userProfile.onPickupDiscovered = (Action<PickupIndex>)Delegate.Combine(userProfile.onPickupDiscovered, new Action<PickupIndex>(this.OnPickupDiscovered));
			this.Check();
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x0015E071 File Offset: 0x0015C271
		public override void OnUninstall()
		{
			UserProfile userProfile = base.userProfile;
			userProfile.onPickupDiscovered = (Action<PickupIndex>)Delegate.Remove(userProfile.onPickupDiscovered, new Action<PickupIndex>(this.OnPickupDiscovered));
			base.OnUninstall();
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x0015E0A0 File Offset: 0x0015C2A0
		public override float ProgressForAchievement()
		{
			return (float)this.EquipmentDiscovered() / 5f;
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x0015E0AF File Offset: 0x0015C2AF
		private void OnPickupDiscovered(PickupIndex pickupIndex)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (((pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None) != EquipmentIndex.None)
			{
				this.Check();
			}
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x0015E0CC File Offset: 0x0015C2CC
		private int EquipmentDiscovered()
		{
			int num = 0;
			EquipmentIndex equipmentIndex = (EquipmentIndex)0;
			EquipmentIndex equipmentCount = (EquipmentIndex)EquipmentCatalog.equipmentCount;
			while (equipmentIndex < equipmentCount)
			{
				if (base.userProfile.HasDiscoveredPickup(PickupCatalog.FindPickupIndex(equipmentIndex)))
				{
					num++;
				}
				equipmentIndex++;
			}
			return num;
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0015E105 File Offset: 0x0015C305
		private void Check()
		{
			if (this.EquipmentDiscovered() >= 5)
			{
				base.Grant();
			}
		}

		// Token: 0x04005057 RID: 20567
		private const int requirement = 5;
	}
}
