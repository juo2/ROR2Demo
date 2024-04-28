using System;
using EntityStates;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C14 RID: 3092
	[CreateAssetMenu(menuName = "RoR2/SkillDef/ReloadSkillDef")]
	public class ReloadSkillDef : SkillDef
	{
		// Token: 0x060045EA RID: 17898 RVA: 0x00122180 File Offset: 0x00120380
		public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return new ReloadSkillDef.InstanceData();
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00121DCC File Offset: 0x0011FFCC
		public override void OnUnassigned([NotNull] GenericSkill skillSlot)
		{
			base.OnUnassigned(skillSlot);
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00122188 File Offset: 0x00120388
		public override void OnFixedUpdate([NotNull] GenericSkill skillSlot)
		{
			base.OnFixedUpdate(skillSlot);
			ReloadSkillDef.InstanceData instanceData = (ReloadSkillDef.InstanceData)skillSlot.skillInstanceData;
			instanceData.currentStock = skillSlot.stock;
			if (instanceData.currentStock < this.GetMaxStock(skillSlot))
			{
				if (skillSlot.stateMachine && !skillSlot.stateMachine.HasPendingState() && skillSlot.stateMachine.CanInterruptState(this.reloadInterruptPriority))
				{
					instanceData.graceStopwatch += Time.fixedDeltaTime;
					if (instanceData.graceStopwatch >= this.graceDuration || instanceData.currentStock == 0)
					{
						skillSlot.stateMachine.SetNextState(EntityStateCatalog.InstantiateState(this.reloadState));
						return;
					}
				}
				else
				{
					instanceData.graceStopwatch = 0f;
				}
			}
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x0012223A File Offset: 0x0012043A
		public override void OnExecute([NotNull] GenericSkill skillSlot)
		{
			base.OnExecute(skillSlot);
			((ReloadSkillDef.InstanceData)skillSlot.skillInstanceData).currentStock = skillSlot.stock;
		}

		// Token: 0x040043E7 RID: 17383
		[Header("Reload Parameters")]
		[Tooltip("The reload state to go into, when stock is less than max.")]
		public SerializableEntityStateType reloadState;

		// Token: 0x040043E8 RID: 17384
		[Tooltip("The priority of this reload state.")]
		public InterruptPriority reloadInterruptPriority = InterruptPriority.Skill;

		// Token: 0x040043E9 RID: 17385
		[Tooltip("The amount of time to wait between when we COULD reload, and when we actually start")]
		public float graceDuration;

		// Token: 0x02000C15 RID: 3093
		protected class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043EA RID: 17386
			public int currentStock;

			// Token: 0x040043EB RID: 17387
			public float graceStopwatch;
		}
	}
}
