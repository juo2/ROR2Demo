using System;
using JetBrains.Annotations;

namespace RoR2.Skills
{
	// Token: 0x02000C02 RID: 3074
	public class GroundedSkillDef : SkillDef
	{
		// Token: 0x060045AC RID: 17836 RVA: 0x00121A2E File Offset: 0x0011FC2E
		public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return new GroundedSkillDef.InstanceData
			{
				characterMotor = skillSlot.GetComponent<CharacterMotor>()
			};
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x00121A44 File Offset: 0x0011FC44
		public bool IsGrounded([NotNull] GenericSkill skillSlot)
		{
			GroundedSkillDef.InstanceData instanceData = (GroundedSkillDef.InstanceData)skillSlot.skillInstanceData;
			return instanceData.characterMotor && instanceData.characterMotor.isGrounded;
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x00121A7A File Offset: 0x0011FC7A
		public override bool IsReady([NotNull] GenericSkill skillSlot)
		{
			return base.HasRequiredStockAndDelay(skillSlot) && this.IsGrounded(skillSlot);
		}

		// Token: 0x02000C03 RID: 3075
		protected class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043D0 RID: 17360
			public CharacterMotor characterMotor;
		}
	}
}
