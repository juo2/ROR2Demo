using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x02000196 RID: 406
	public class FireGrenadeLauncher : GenericProjectileBaseState, IToolbotPrimarySkillState, ISkillState
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001EE83 File Offset: 0x0001D083
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001EE8B File Offset: 0x0001D08B
		Transform IToolbotPrimarySkillState.muzzleTransform { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001EE94 File Offset: 0x0001D094
		string IToolbotPrimarySkillState.baseMuzzleName
		{
			get
			{
				return this.targetMuzzle;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001EE9C File Offset: 0x0001D09C
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		bool IToolbotPrimarySkillState.isInDualWield { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001EEAD File Offset: 0x0001D0AD
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001EEB5 File Offset: 0x0001D0B5
		int IToolbotPrimarySkillState.currentHand { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001EEBE File Offset: 0x0001D0BE
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001EEC6 File Offset: 0x0001D0C6
		string IToolbotPrimarySkillState.muzzleName { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001EECF File Offset: 0x0001D0CF
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001EED7 File Offset: 0x0001D0D7
		public SkillDef skillDef { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		public GenericSkill activatorSkillSlot { get; set; }

		// Token: 0x0600072F RID: 1839 RVA: 0x0001EEF1 File Offset: 0x0001D0F1
		public override void OnEnter()
		{
			BaseToolbotPrimarySkillStateMethods.OnEnter<FireGrenadeLauncher>(this, base.gameObject, base.skillLocator, base.GetModelChildLocator());
			base.OnEnter();
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001EF11 File Offset: 0x0001D111
		protected override void PlayAnimation(float duration)
		{
			if (!((IToolbotPrimarySkillState)this).isInDualWield)
			{
				base.PlayAnimation("Gesture, Additive", "FireGrenadeLauncher", "FireGrenadeLauncher.playbackRate", duration);
				return;
			}
			BaseToolbotPrimarySkillStateMethods.PlayGenericFireAnim<FireGrenadeLauncher>(this, base.gameObject, base.skillLocator, duration);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001EF48 File Offset: 0x0001D148
		protected override Ray ModifyProjectileAimRay(Ray projectileRay)
		{
			if (((IToolbotPrimarySkillState)this).isInDualWield)
			{
				Transform muzzleTransform = ((IToolbotPrimarySkillState)this).muzzleTransform;
				if (muzzleTransform)
				{
					projectileRay.origin = muzzleTransform.position;
				}
			}
			return projectileRay;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000C08B File Offset: 0x0000A28B
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			this.Serialize(base.skillLocator, writer);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.Deserialize(base.skillLocator, reader);
		}
	}
}
