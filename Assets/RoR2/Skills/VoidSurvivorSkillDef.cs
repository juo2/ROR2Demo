using System;
using JetBrains.Annotations;

namespace RoR2.Skills
{
	// Token: 0x02000C24 RID: 3108
	public class VoidSurvivorSkillDef : SkillDef
	{
		// Token: 0x06004633 RID: 17971 RVA: 0x00122B38 File Offset: 0x00120D38
		public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return new VoidSurvivorSkillDef.InstanceData
			{
				voidSurvivorController = skillSlot.GetComponent<VoidSurvivorController>()
			};
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x00122B4B File Offset: 0x00120D4B
		public override bool IsReady([NotNull] GenericSkill skillSlot)
		{
			return base.IsReady(skillSlot) && this.HasRequiredCorruption(skillSlot);
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x00122B60 File Offset: 0x00120D60
		public bool HasRequiredCorruption([NotNull] GenericSkill skillSlot)
		{
			VoidSurvivorSkillDef.InstanceData instanceData = (VoidSurvivorSkillDef.InstanceData)skillSlot.skillInstanceData;
			return instanceData.voidSurvivorController && instanceData.voidSurvivorController.corruption >= this.minimumCorruption && instanceData.voidSurvivorController.corruption < this.maximumCorruption;
		}

		// Token: 0x04004423 RID: 17443
		public float minimumCorruption;

		// Token: 0x04004424 RID: 17444
		public float maximumCorruption;

		// Token: 0x02000C25 RID: 3109
		protected class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x04004425 RID: 17445
			public VoidSurvivorController voidSurvivorController;
		}
	}
}
