using System;
using EntityStates;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C1B RID: 3099
	public class SteppedSkillDef : SkillDef
	{
		// Token: 0x0600461E RID: 17950 RVA: 0x00122873 File Offset: 0x00120A73
		public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return new SteppedSkillDef.InstanceData();
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x0012287C File Offset: 0x00120A7C
		protected override EntityState InstantiateNextState([NotNull] GenericSkill skillSlot)
		{
			EntityState entityState = base.InstantiateNextState(skillSlot);
			SteppedSkillDef.InstanceData instanceData = (SteppedSkillDef.InstanceData)skillSlot.skillInstanceData;
			SteppedSkillDef.IStepSetter stepSetter;
			if ((stepSetter = (entityState as SteppedSkillDef.IStepSetter)) != null)
			{
				stepSetter.SetStep(instanceData.step);
			}
			return entityState;
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x001228B4 File Offset: 0x00120AB4
		public override void OnExecute([NotNull] GenericSkill skillSlot)
		{
			base.OnExecute(skillSlot);
			SteppedSkillDef.InstanceData instanceData = (SteppedSkillDef.InstanceData)skillSlot.skillInstanceData;
			instanceData.step++;
			if (instanceData.step >= this.stepCount)
			{
				instanceData.step = 0;
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x001228F8 File Offset: 0x00120AF8
		public override void OnFixedUpdate([NotNull] GenericSkill skillSlot)
		{
			base.OnFixedUpdate(skillSlot);
			if (skillSlot.CanExecute())
			{
				this.stepResetTimer += Time.fixedDeltaTime;
			}
			else
			{
				this.stepResetTimer = 0f;
			}
			if (this.stepResetTimer > this.stepGraceDuration)
			{
				((SteppedSkillDef.InstanceData)skillSlot.skillInstanceData).step = 0;
			}
		}

		// Token: 0x0400440F RID: 17423
		public int stepCount = 2;

		// Token: 0x04004410 RID: 17424
		[Tooltip("The amount of time a step is 'held' before it resets. Only begins to count down when available to execute.")]
		public float stepGraceDuration = 0.1f;

		// Token: 0x04004411 RID: 17425
		private float stepResetTimer;

		// Token: 0x02000C1C RID: 3100
		public class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x04004412 RID: 17426
			public int step;
		}

		// Token: 0x02000C1D RID: 3101
		public interface IStepSetter
		{
			// Token: 0x06004624 RID: 17956
			void SetStep(int i);
		}
	}
}
