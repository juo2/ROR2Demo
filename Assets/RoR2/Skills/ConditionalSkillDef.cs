using System;
using EntityStates;
using JetBrains.Annotations;

namespace RoR2.Skills
{
	// Token: 0x02000BFF RID: 3071
	public class ConditionalSkillDef : SkillDef
	{
		// Token: 0x060045A4 RID: 17828 RVA: 0x00121919 File Offset: 0x0011FB19
		public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return new ConditionalSkillDef.InstanceData
			{
				characterBody = skillSlot.GetComponent<CharacterBody>()
			};
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x0012192C File Offset: 0x0011FB2C
		public bool IsGrounded([NotNull] GenericSkill skillSlot)
		{
			ConditionalSkillDef.InstanceData instanceData = (ConditionalSkillDef.InstanceData)skillSlot.skillInstanceData;
			return instanceData.characterBody.characterMotor && instanceData.characterBody.characterMotor.isGrounded;
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x0012196C File Offset: 0x0011FB6C
		public bool IsSprinting([NotNull] GenericSkill skillSlot)
		{
			ConditionalSkillDef.InstanceData instanceData = (ConditionalSkillDef.InstanceData)skillSlot.skillInstanceData;
			return instanceData.characterBody && instanceData.characterBody.isSprinting;
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x001219A4 File Offset: 0x0011FBA4
		protected override EntityState InstantiateNextState(GenericSkill skillSlot)
		{
			bool flag = this.IsGrounded(skillSlot);
			bool flag2 = this.IsSprinting(skillSlot);
			if (!flag)
			{
				return EntityStateCatalog.InstantiateState(this.jumpStateType);
			}
			if (flag2)
			{
				return EntityStateCatalog.InstantiateState(this.sprintStateType);
			}
			return EntityStateCatalog.InstantiateState(this.baseStateType);
		}

		// Token: 0x040043CC RID: 17356
		public SerializableEntityStateType baseStateType;

		// Token: 0x040043CD RID: 17357
		public SerializableEntityStateType sprintStateType;

		// Token: 0x040043CE RID: 17358
		public SerializableEntityStateType jumpStateType;

		// Token: 0x02000C00 RID: 3072
		protected class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043CF RID: 17359
			public CharacterBody characterBody;
		}
	}
}
