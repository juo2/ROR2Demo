using System;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x02000190 RID: 400
	public abstract class BaseToolbotPrimarySkillState : BaseSkillState, IToolbotPrimarySkillState, ISkillState
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001E52D File Offset: 0x0001C72D
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0001E535 File Offset: 0x0001C735
		public Transform muzzleTransform { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001E53E File Offset: 0x0001C73E
		public virtual string baseMuzzleName
		{
			get
			{
				return "Muzzle";
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001E545 File Offset: 0x0001C745
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001E54D File Offset: 0x0001C74D
		public bool isInDualWield { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001E556 File Offset: 0x0001C756
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001E55E File Offset: 0x0001C75E
		public int currentHand { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001E567 File Offset: 0x0001C767
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001E56F File Offset: 0x0001C76F
		public string muzzleName { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001E578 File Offset: 0x0001C778
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001E580 File Offset: 0x0001C780
		public SkillDef skillDef { get; set; }

		// Token: 0x060006FF RID: 1791 RVA: 0x0001E589 File Offset: 0x0001C789
		public override void OnEnter()
		{
			base.OnEnter();
			BaseToolbotPrimarySkillStateMethods.OnEnter<BaseToolbotPrimarySkillState>(this, base.gameObject, base.skillLocator, base.GetModelChildLocator());
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}
	}
}
