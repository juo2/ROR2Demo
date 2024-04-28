using System;
using RoR2.Stats;

namespace RoR2.Achievements.Commando
{
	// Token: 0x02000F01 RID: 3841
	[RegisterAchievement("CommandoNonLunarEndurance", "Skills.Commando.ThrowGrenade", null, null)]
	public class CommandoNonLunarEnduranceAchievement : BaseAchievement
	{
		// Token: 0x06005762 RID: 22370 RVA: 0x001613DC File Offset: 0x0015F5DC
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CommandoBody");
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x001614D0 File Offset: 0x0015F6D0
		private static bool EverPickedUpLunarItems(StatSheet statSheet)
		{
			foreach (ItemIndex key in ItemCatalog.lunarItemList)
			{
				if (statSheet.GetStatValueULong(PerItemStatDef.totalCollected.FindStatDef(key)) > 0UL)
				{
					return true;
				}
			}
			foreach (EquipmentIndex equipmentIndex in EquipmentCatalog.equipmentList)
			{
				if (EquipmentCatalog.GetEquipmentDef(equipmentIndex).isLunar && statSheet.GetStatValueDouble(PerEquipmentStatDef.totalTimeHeld.FindStatDef(equipmentIndex)) > 0.0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x001615A4 File Offset: 0x0015F7A4
		private void OnMasterChanged()
		{
			if (base.localUser != null)
			{
				this.SetMasterController(base.localUser.cachedMasterController);
			}
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x001615C0 File Offset: 0x0015F7C0
		private void SetMasterController(PlayerCharacterMasterController playerCharacterMasterController)
		{
			if (base.localUser.cachedMasterController == this.cachedMasterController)
			{
				return;
			}
			bool flag = this.cachedStatsComponent != null;
			this.cachedMasterController = base.localUser.cachedMasterController;
			this.cachedStatsComponent = (this.cachedMasterController ? this.cachedMasterController.GetComponent<PlayerStatsComponent>() : null);
			bool flag2 = this.cachedStatsComponent != null;
			if (flag != flag2 && base.userProfile != null)
			{
				if (flag2)
				{
					UserProfile userProfile = base.userProfile;
					userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.OnStatsChanged));
					return;
				}
				UserProfile userProfile2 = base.userProfile;
				userProfile2.onStatsReceived = (Action)Delegate.Remove(userProfile2.onStatsReceived, new Action(this.OnStatsChanged));
			}
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x00161684 File Offset: 0x0015F884
		private void OnStatsChanged()
		{
			if (this.cachedStatsComponent == null)
			{
				return;
			}
			if (CommandoNonLunarEnduranceAchievement.requirement <= this.cachedStatsComponent.currentStats.GetStatValueULong(StatDef.totalStagesCompleted))
			{
				if (CommandoNonLunarEnduranceAchievement.EverPickedUpLunarItems(this.cachedStatsComponent.currentStats))
				{
					this.SetMasterController(null);
					return;
				}
				base.Grant();
			}
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x001616D6 File Offset: 0x0015F8D6
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.localUser.onMasterChanged += this.OnMasterChanged;
			this.OnMasterChanged();
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x001616FB File Offset: 0x0015F8FB
		protected override void OnBodyRequirementBroken()
		{
			base.localUser.onMasterChanged -= this.OnMasterChanged;
			this.SetMasterController(null);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050C5 RID: 20677
		private static readonly ulong requirement = 20UL;

		// Token: 0x040050C6 RID: 20678
		private PlayerCharacterMasterController cachedMasterController;

		// Token: 0x040050C7 RID: 20679
		private PlayerStatsComponent cachedStatsComponent;
	}
}
