using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C1F RID: 3103
	[CreateAssetMenu(menuName = "RoR2/SkillDef/VoidRaidCrabBodySkillDef")]
	public class VoidRaidCrabBodySkillDef : SkillDef
	{
		// Token: 0x06004626 RID: 17958 RVA: 0x0012296C File Offset: 0x00120B6C
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			return new VoidRaidCrabBodySkillDef.InstanceData
			{
				weaponStateMachine = EntityStateMachine.FindByCustomName(skillSlot.gameObject, "Weapon")
			};
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x00122989 File Offset: 0x00120B89
		public override bool IsReady([NotNull] GenericSkill skillSlot)
		{
			return base.IsReady(skillSlot) && !this.IsWeaponBusy(skillSlot);
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x001229A0 File Offset: 0x00120BA0
		private bool IsWeaponBusy([NotNull] GenericSkill skillSlot)
		{
			VoidRaidCrabBodySkillDef.InstanceData instanceData = skillSlot.skillInstanceData as VoidRaidCrabBodySkillDef.InstanceData;
			return instanceData != null && instanceData.weaponStateMachine && !instanceData.weaponStateMachine.IsInMainState();
		}

		// Token: 0x0400441C RID: 17436
		private const string weaponStateMachineName = "Weapon";

		// Token: 0x02000C20 RID: 3104
		private class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x0400441D RID: 17437
			public EntityStateMachine weaponStateMachine;
		}
	}
}
