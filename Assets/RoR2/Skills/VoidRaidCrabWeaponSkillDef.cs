using System;
using EntityStates;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C21 RID: 3105
	[CreateAssetMenu(menuName = "RoR2/SkillDef/VoidRaidCrabWeaponSkillDef")]
	public class VoidRaidCrabWeaponSkillDef : SkillDef
	{
		// Token: 0x0600462B RID: 17963 RVA: 0x001229D9 File Offset: 0x00120BD9
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			return new VoidRaidCrabWeaponSkillDef.InstanceData
			{
				bodyStateMachine = EntityStateMachine.FindByCustomName(skillSlot.gameObject, "Body"),
				weaponStateMachine = EntityStateMachine.FindByCustomName(skillSlot.gameObject, "Weapon")
			};
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x00122A0C File Offset: 0x00120C0C
		public override bool IsReady([NotNull] GenericSkill skillSlot)
		{
			return base.IsReady(skillSlot) && this.CanReceiveInput(skillSlot) && !this.IsWeaponBusy(skillSlot);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x00122A2C File Offset: 0x00120C2C
		private bool CanReceiveInput([NotNull] GenericSkill skillSlot)
		{
			VoidRaidCrabWeaponSkillDef.InstanceData instanceData = skillSlot.skillInstanceData as VoidRaidCrabWeaponSkillDef.InstanceData;
			return instanceData != null && instanceData.bodyStateMachine && instanceData.bodyStateMachine.state is GenericCharacterMain;
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x00122A6C File Offset: 0x00120C6C
		private bool IsWeaponBusy([NotNull] GenericSkill skillSlot)
		{
			VoidRaidCrabWeaponSkillDef.InstanceData instanceData = skillSlot.skillInstanceData as VoidRaidCrabWeaponSkillDef.InstanceData;
			return instanceData != null && instanceData.weaponStateMachine && !instanceData.weaponStateMachine.IsInMainState();
		}

		// Token: 0x0400441E RID: 17438
		private const string bodyStateMachineName = "Body";

		// Token: 0x0400441F RID: 17439
		private const string weaponStateMachineName = "Weapon";

		// Token: 0x02000C22 RID: 3106
		private class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x04004420 RID: 17440
			public EntityStateMachine bodyStateMachine;

			// Token: 0x04004421 RID: 17441
			public EntityStateMachine weaponStateMachine;
		}
	}
}
