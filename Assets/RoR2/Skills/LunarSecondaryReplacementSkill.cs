using System;

namespace RoR2.Skills
{
	// Token: 0x02000C0A RID: 3082
	public class LunarSecondaryReplacementSkill : SkillDef
	{
		// Token: 0x060045C8 RID: 17864 RVA: 0x00121CE4 File Offset: 0x0011FEE4
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			LunarSecondaryReplacementSkill.InstanceData instanceData = new LunarSecondaryReplacementSkill.InstanceData();
			instanceData.skillSlot = skillSlot;
			skillSlot.characterBody.onInventoryChanged += instanceData.OnInventoryChanged;
			return instanceData;
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x00121D16 File Offset: 0x0011FF16
		public override void OnUnassigned(GenericSkill skillSlot)
		{
			skillSlot.characterBody.onInventoryChanged -= ((LunarSecondaryReplacementSkill.InstanceData)skillSlot.skillInstanceData).OnInventoryChanged;
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x00121D39 File Offset: 0x0011FF39
		public override float GetRechargeInterval(GenericSkill skillSlot)
		{
			return (float)skillSlot.characterBody.inventory.GetItemCount(RoR2Content.Items.LunarSecondaryReplacement) * this.baseRechargeInterval;
		}

		// Token: 0x02000C0B RID: 3083
		private class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x060045CC RID: 17868 RVA: 0x00121D58 File Offset: 0x0011FF58
			public void OnInventoryChanged()
			{
				this.skillSlot.RecalculateValues();
			}

			// Token: 0x040043D6 RID: 17366
			public GenericSkill skillSlot;
		}
	}
}
