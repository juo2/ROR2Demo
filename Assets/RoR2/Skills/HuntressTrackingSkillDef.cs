﻿using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C04 RID: 3076
	[CreateAssetMenu(menuName = "RoR2/SkillDef/HuntressTrackingSkillDef")]
	public class HuntressTrackingSkillDef : SkillDef
	{
		// Token: 0x060045B1 RID: 17841 RVA: 0x00121A8E File Offset: 0x0011FC8E
		public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return new HuntressTrackingSkillDef.InstanceData
			{
				huntressTracker = skillSlot.GetComponent<HuntressTracker>()
			};
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x00121AA1 File Offset: 0x0011FCA1
		private static bool HasTarget([NotNull] GenericSkill skillSlot)
		{
			HuntressTracker huntressTracker = ((HuntressTrackingSkillDef.InstanceData)skillSlot.skillInstanceData).huntressTracker;
			return (huntressTracker != null) ? huntressTracker.GetTrackingTarget() : null;
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x00121AC9 File Offset: 0x0011FCC9
		public override bool CanExecute([NotNull] GenericSkill skillSlot)
		{
			return HuntressTrackingSkillDef.HasTarget(skillSlot) && base.CanExecute(skillSlot);
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x00121ADC File Offset: 0x0011FCDC
		public override bool IsReady([NotNull] GenericSkill skillSlot)
		{
			return base.IsReady(skillSlot) && HuntressTrackingSkillDef.HasTarget(skillSlot);
		}

		// Token: 0x02000C05 RID: 3077
		protected class InstanceData : SkillDef.BaseSkillInstanceData
		{
			// Token: 0x040043D1 RID: 17361
			public HuntressTracker huntressTracker;
		}
	}
}
