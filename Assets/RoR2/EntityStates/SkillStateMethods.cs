using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000CC RID: 204
	public static class SkillStateMethods
	{
		// Token: 0x060003BB RID: 955 RVA: 0x0000F74C File Offset: 0x0000D94C
		public static void Serialize(this ISkillState skillState, SkillLocator skillLocator, NetworkWriter writer)
		{
			int num = -1;
			if (skillLocator != null)
			{
				num = skillLocator.GetSkillSlotIndex(skillState.activatorSkillSlot);
			}
			writer.Write((sbyte)num);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000F774 File Offset: 0x0000D974
		public static void Deserialize(this ISkillState skillState, SkillLocator skillLocator, NetworkReader reader)
		{
			int index = (int)reader.ReadSByte();
			if (skillLocator != null)
			{
				skillState.activatorSkillSlot = skillLocator.GetSkillAtIndex(index);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000F798 File Offset: 0x0000D998
		public static bool IsKeyDownAuthority(this ISkillState skillState, SkillLocator skillLocator, InputBankTest inputBank)
		{
			GenericSkill activatorSkillSlot = skillState.activatorSkillSlot;
			if (skillLocator == null || activatorSkillSlot == null || inputBank == null)
			{
				return false;
			}
			switch (skillLocator.FindSkillSlot(activatorSkillSlot))
			{
			case SkillSlot.None:
				return false;
			case SkillSlot.Primary:
				return inputBank.skill1.down;
			case SkillSlot.Secondary:
				return inputBank.skill2.down;
			case SkillSlot.Utility:
				return inputBank.skill3.down;
			case SkillSlot.Special:
				return inputBank.skill4.down;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}
