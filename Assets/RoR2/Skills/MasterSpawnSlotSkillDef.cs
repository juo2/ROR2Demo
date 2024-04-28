using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C0C RID: 3084
	[CreateAssetMenu(menuName = "RoR2/SkillDef/MasterSpawnSlotSkillDef")]
	public class MasterSpawnSlotSkillDef : SkillDef
	{
		// Token: 0x060045CE RID: 17870 RVA: 0x00121D65 File Offset: 0x0011FF65
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			return new MasterSpawnSlotSkillDef.InstanceData
			{
				slotController = skillSlot.GetComponent<MasterSpawnSlotController>()
			};
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x00121D78 File Offset: 0x0011FF78
		private bool IsAnySlotOpen([NotNull] GenericSkill skillSlot)
		{
			MasterSpawnSlotSkillDef.InstanceData instanceData = skillSlot.skillInstanceData as MasterSpawnSlotSkillDef.InstanceData;
			return instanceData != null && instanceData.slotController && instanceData.slotController.openSlotCount > 0;
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x00121DB1 File Offset: 0x0011FFB1
		public override bool IsReady([NotNull] GenericSkill skillSlot)
		{
			return base.IsReady(skillSlot) && this.IsAnySlotOpen(skillSlot);
		}

		// Token: 0x02000C0D RID: 3085
		private class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043D7 RID: 17367
			public MasterSpawnSlotController slotController;
		}
	}
}
