using System;
using RoR2.Skills;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000688 RID: 1672
	public class CrocoDamageTypeController : MonoBehaviour
	{
		// Token: 0x060020A7 RID: 8359 RVA: 0x0008C458 File Offset: 0x0008A658
		public DamageType GetDamageType()
		{
			if (this.passiveSkillSlot)
			{
				if (this.passiveSkillSlot.skillDef == this.poisonSkillDef)
				{
					return DamageType.PoisonOnHit;
				}
				if (this.passiveSkillSlot.skillDef == this.blightSkillDef)
				{
					return DamageType.BlightOnHit;
				}
			}
			return DamageType.Generic;
		}

		// Token: 0x040025E3 RID: 9699
		public SkillDef poisonSkillDef;

		// Token: 0x040025E4 RID: 9700
		public SkillDef blightSkillDef;

		// Token: 0x040025E5 RID: 9701
		public GenericSkill passiveSkillSlot;
	}
}
