using System;
using EntityStates;
using EntityStates.VoidSurvivor.Weapon;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C23 RID: 3107
	public class VoidSurvivorBlasterSkillDef : SteppedSkillDef
	{
		// Token: 0x06004631 RID: 17969 RVA: 0x00122AA8 File Offset: 0x00120CA8
		protected override EntityState InstantiateNextState([NotNull] GenericSkill skillSlot)
		{
			EntityState entityState = base.InstantiateNextState(skillSlot);
			VoidSurvivorController component = skillSlot.GetComponent<VoidSurvivorController>();
			if (component)
			{
				float corruptionPercentage = component.corruptionPercentage;
				if (corruptionPercentage >= 75f)
				{
					entityState = new FireBlaster4();
				}
				else if (corruptionPercentage >= 50f)
				{
					entityState = new FireBlaster3();
				}
				else if (corruptionPercentage >= 25f)
				{
					entityState = new FireBlaster2();
				}
				else
				{
					entityState = new FireBlaster1();
				}
			}
			SteppedSkillDef.InstanceData instanceData = (SteppedSkillDef.InstanceData)skillSlot.skillInstanceData;
			SteppedSkillDef.IStepSetter stepSetter;
			if ((stepSetter = (entityState as SteppedSkillDef.IStepSetter)) != null)
			{
				stepSetter.SetStep(instanceData.step);
			}
			return entityState;
		}

		// Token: 0x04004422 RID: 17442
		public Sprite[] icons;
	}
}
