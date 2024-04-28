using System;
using EntityStates.Railgunner.Backpack;
using EntityStates.Railgunner.Reload;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C12 RID: 3090
	[CreateAssetMenu(menuName = "RoR2/SkillDef/RailgunSkillDef")]
	public class RailgunSkillDef : SkillDef
	{
		// Token: 0x060045E2 RID: 17890 RVA: 0x00122076 File Offset: 0x00120276
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			return new RailgunSkillDef.InstanceData
			{
				backpackStateMachine = EntityStateMachine.FindByCustomName(skillSlot.gameObject, "Backpack"),
				reloadStateMachine = EntityStateMachine.FindByCustomName(skillSlot.gameObject, "Reload")
			};
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x001220AC File Offset: 0x001202AC
		private bool IsBackpackOffline([NotNull] GenericSkill skillSlot)
		{
			RailgunSkillDef.InstanceData instanceData = skillSlot.skillInstanceData as RailgunSkillDef.InstanceData;
			return instanceData != null && instanceData.backpackStateMachine && instanceData.backpackStateMachine.state is Offline;
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x001220EC File Offset: 0x001202EC
		private bool IsReloading([NotNull] GenericSkill skillSlot)
		{
			RailgunSkillDef.InstanceData instanceData = skillSlot.skillInstanceData as RailgunSkillDef.InstanceData;
			return instanceData != null && instanceData.reloadStateMachine && instanceData.reloadStateMachine.state is Reloading;
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x0012212A File Offset: 0x0012032A
		public override bool IsReady([NotNull] GenericSkill skillSlot)
		{
			return base.IsReady(skillSlot) && !this.IsBackpackOffline(skillSlot) && !this.IsReloading(skillSlot);
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x0012214A File Offset: 0x0012034A
		public override void OnFixedUpdate([NotNull] GenericSkill skillSlot)
		{
			base.OnFixedUpdate(skillSlot);
			if (this.IsBackpackOffline(skillSlot))
			{
				skillSlot.rechargeStopwatch = 0f;
			}
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x00122167 File Offset: 0x00120367
		public override Sprite GetCurrentIcon([NotNull] GenericSkill skillSlot)
		{
			if (this.IsBackpackOffline(skillSlot))
			{
				return this.offlineIcon;
			}
			return base.GetCurrentIcon(skillSlot);
		}

		// Token: 0x040043E1 RID: 17377
		private const string backpackStateMachineName = "Backpack";

		// Token: 0x040043E2 RID: 17378
		private const string reloadStateMachineName = "Reload";

		// Token: 0x040043E3 RID: 17379
		[SerializeField]
		public Sprite offlineIcon;

		// Token: 0x040043E4 RID: 17380
		[SerializeField]
		public bool restockOnReload;

		// Token: 0x02000C13 RID: 3091
		private class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043E5 RID: 17381
			public EntityStateMachine backpackStateMachine;

			// Token: 0x040043E6 RID: 17382
			public EntityStateMachine reloadStateMachine;
		}
	}
}
