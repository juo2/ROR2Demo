using System;
using EntityStates;
using HG;

namespace RoR2.Skills
{
	// Token: 0x02000BFC RID: 3068
	public class ComboSkillDef : SkillDef
	{
		// Token: 0x0600459F RID: 17823 RVA: 0x0012189A File Offset: 0x0011FA9A
		protected SerializableEntityStateType GetNextStateType(GenericSkill skillSlot)
		{
			return ArrayUtils.GetSafe<ComboSkillDef.Combo>(this.comboList, ((ComboSkillDef.InstanceData)skillSlot.skillInstanceData).comboCounter).activationStateType;
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x001218BC File Offset: 0x0011FABC
		protected override EntityState InstantiateNextState(GenericSkill skillSlot)
		{
			return EntityStateCatalog.InstantiateState(this.GetNextStateType(skillSlot));
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x001218CC File Offset: 0x0011FACC
		public override void OnExecute(GenericSkill skillSlot)
		{
			base.OnExecute(skillSlot);
			ComboSkillDef.InstanceData instanceData = (ComboSkillDef.InstanceData)skillSlot.skillInstanceData;
			instanceData.comboCounter++;
			if (instanceData.comboCounter >= this.comboList.Length)
			{
				instanceData.comboCounter = 0;
			}
		}

		// Token: 0x040043C9 RID: 17353
		public ComboSkillDef.Combo[] comboList;

		// Token: 0x02000BFD RID: 3069
		[Serializable]
		public struct Combo
		{
			// Token: 0x040043CA RID: 17354
			public SerializableEntityStateType activationStateType;
		}

		// Token: 0x02000BFE RID: 3070
		protected class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043CB RID: 17355
			public int comboCounter;
		}
	}
}
