using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Skills
{
	// Token: 0x02000C10 RID: 3088
	[CreateAssetMenu(menuName = "RoR2/SkillDef/PassiveItem")]
	public class PassiveItemSkillDef : SkillDef
	{
		// Token: 0x060045DC RID: 17884 RVA: 0x00121F84 File Offset: 0x00120184
		public override SkillDef.BaseSkillInstanceData OnAssigned(GenericSkill skillSlot)
		{
			PassiveItemSkillDef.InstanceData instanceData = new PassiveItemSkillDef.InstanceData
			{
				hasItemBeenGiven = false
			};
			if (NetworkServer.active)
			{
				this.TryGiveItem(skillSlot, instanceData);
			}
			return instanceData;
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x00121FAE File Offset: 0x001201AE
		public override void OnFixedUpdate(GenericSkill skillSlot)
		{
			if (NetworkServer.active)
			{
				this.TryGiveItem(skillSlot, (PassiveItemSkillDef.InstanceData)skillSlot.skillInstanceData);
			}
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x00121FCC File Offset: 0x001201CC
		public override void OnUnassigned(GenericSkill skillSlot)
		{
			if (NetworkServer.active)
			{
				PassiveItemSkillDef.InstanceData instanceData = (PassiveItemSkillDef.InstanceData)skillSlot.skillInstanceData;
				if (instanceData.hasItemBeenGiven)
				{
					CharacterBody component = skillSlot.GetComponent<CharacterBody>();
					if (component && component.inventory)
					{
						component.inventory.RemoveItem(this.passiveItem, 1);
						instanceData.hasItemBeenGiven = false;
					}
				}
			}
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x0012202C File Offset: 0x0012022C
		private void TryGiveItem(GenericSkill skillSlot, PassiveItemSkillDef.InstanceData data)
		{
			if (!data.hasItemBeenGiven)
			{
				CharacterBody component = skillSlot.GetComponent<CharacterBody>();
				if (component && component.inventory)
				{
					component.inventory.GiveItem(this.passiveItem, 1);
					data.hasItemBeenGiven = true;
				}
			}
		}

		// Token: 0x040043DF RID: 17375
		[SerializeField]
		public ItemDef passiveItem;

		// Token: 0x02000C11 RID: 3089
		private class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043E0 RID: 17376
			public bool hasItemBeenGiven;
		}
	}
}
