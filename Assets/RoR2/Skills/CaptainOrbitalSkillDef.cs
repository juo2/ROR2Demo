using System;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000BF9 RID: 3065
	public class CaptainOrbitalSkillDef : SkillDef
	{
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x00121633 File Offset: 0x0011F833
		private bool isAvailable
		{
			get
			{
				return !SceneCatalog.mostRecentSceneDef.blockOrbitalSkills;
			}
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x00121642 File Offset: 0x0011F842
		public override Sprite GetCurrentIcon(GenericSkill skillSlot)
		{
			if (!this.isAvailable)
			{
				return this.disabledIcon;
			}
			return base.GetCurrentIcon(skillSlot);
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x0012165A File Offset: 0x0011F85A
		public override string GetCurrentNameToken(GenericSkill skillSlot)
		{
			if (!this.isAvailable)
			{
				return this.disabledNameToken;
			}
			return base.GetCurrentNameToken(skillSlot);
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x00121672 File Offset: 0x0011F872
		public override string GetCurrentDescriptionToken(GenericSkill skillSlot)
		{
			if (!this.isAvailable)
			{
				return this.disabledDescriptionToken;
			}
			return base.GetCurrentDescriptionToken(skillSlot);
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x0012168A File Offset: 0x0011F88A
		public override bool CanExecute(GenericSkill skillSlot)
		{
			return this.isAvailable && base.CanExecute(skillSlot);
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x0012169D File Offset: 0x0011F89D
		public override bool IsReady(GenericSkill skillSlot)
		{
			return this.isAvailable && base.IsReady(skillSlot);
		}

		// Token: 0x040043BE RID: 17342
		public Sprite disabledIcon;

		// Token: 0x040043BF RID: 17343
		public string disabledNameToken;

		// Token: 0x040043C0 RID: 17344
		public string disabledDescriptionToken;
	}
}
