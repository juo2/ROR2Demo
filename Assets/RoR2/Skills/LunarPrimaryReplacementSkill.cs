using System;

namespace RoR2.Skills
{
	// Token: 0x02000C08 RID: 3080
	public class LunarPrimaryReplacementSkill : SkillDef
	{
		// Token: 0x060045C0 RID: 17856 RVA: 0x00121C3C File Offset: 0x0011FE3C
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			LunarPrimaryReplacementSkill.InstanceData instanceData = new LunarPrimaryReplacementSkill.InstanceData();
			instanceData.skillSlot = skillSlot;
			skillSlot.characterBody.onInventoryChanged += instanceData.OnInventoryChanged;
			return instanceData;
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x00121C6E File Offset: 0x0011FE6E
		public override void OnUnassigned(GenericSkill skillSlot)
		{
			skillSlot.characterBody.onInventoryChanged -= ((LunarPrimaryReplacementSkill.InstanceData)skillSlot.skillInstanceData).OnInventoryChanged;
		}

		// Token: 0x060045C2 RID: 17858 RVA: 0x00121C91 File Offset: 0x0011FE91
		public override int GetMaxStock(GenericSkill skillSlot)
		{
			return skillSlot.characterBody.inventory.GetItemCount(RoR2Content.Items.LunarPrimaryReplacement) * this.baseMaxStock;
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x00121CAF File Offset: 0x0011FEAF
		public override float GetRechargeInterval(GenericSkill skillSlot)
		{
			return (float)skillSlot.characterBody.inventory.GetItemCount(RoR2Content.Items.LunarPrimaryReplacement) * this.baseRechargeInterval;
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x00121CCE File Offset: 0x0011FECE
		public override int GetRechargeStock(GenericSkill skillSlot)
		{
			return this.GetMaxStock(skillSlot);
		}

		// Token: 0x02000C09 RID: 3081
		private class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x060045C6 RID: 17862 RVA: 0x00121CD7 File Offset: 0x0011FED7
			public void OnInventoryChanged()
			{
				this.skillSlot.RecalculateValues();
			}

			// Token: 0x040043D5 RID: 17365
			public GenericSkill skillSlot;
		}
	}
}
