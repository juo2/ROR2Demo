using System;

namespace RoR2.Achievements.Merc
{
	// Token: 0x02000EE0 RID: 3808
	[RegisterAchievement("MercXSkillsInYSeconds", "Skills.Merc.FocusedAssault", "CompleteUnknownEnding", null)]
	public class MercXSkillsInYSecondsAchievement : BaseAchievement
	{
		// Token: 0x060056B5 RID: 22197 RVA: 0x001602A3 File Offset: 0x0015E4A3
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MercBody");
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060056B6 RID: 22198 RVA: 0x001605E9 File Offset: 0x0015E7E9
		// (set) Token: 0x060056B7 RID: 22199 RVA: 0x001605F4 File Offset: 0x0015E7F4
		private CharacterBody trackedBody
		{
			get
			{
				return this._trackedBody;
			}
			set
			{
				if (this._trackedBody == value)
				{
					return;
				}
				if (this._trackedBody != null)
				{
					this._trackedBody.onSkillActivatedAuthority -= this.OnSkillActivated;
				}
				this._trackedBody = value;
				if (this._trackedBody != null)
				{
					this._trackedBody.onSkillActivatedAuthority += this.OnSkillActivated;
				}
			}
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x00160650 File Offset: 0x0015E850
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			this.trackedBody = base.localUser.cachedBody;
			base.localUser.onBodyChanged += this.OnBodyChanged;
			this.tracker.Clear();
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x0016068C File Offset: 0x0015E88C
		protected override void OnBodyRequirementBroken()
		{
			if (base.localUser != null)
			{
				base.localUser.onBodyChanged -= this.OnBodyChanged;
			}
			this.trackedBody = null;
			base.OnBodyRequirementBroken();
			if (this.tracker != null)
			{
				this.tracker.Clear();
			}
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x001606D8 File Offset: 0x0015E8D8
		private void OnBodyChanged()
		{
			this.trackedBody = base.localUser.cachedBody;
			this.tracker.Clear();
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x001606F6 File Offset: 0x0015E8F6
		public override void OnInstall()
		{
			base.OnInstall();
			this.tracker = new DoXInYSecondsTracker(MercXSkillsInYSecondsAchievement.requiredSkillCount, MercXSkillsInYSecondsAchievement.windowSecconds);
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x00160713 File Offset: 0x0015E913
		public override void OnUninstall()
		{
			this.tracker = null;
			base.OnUninstall();
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x00160722 File Offset: 0x0015E922
		private void OnSkillActivated(GenericSkill skill)
		{
			if (this.tracker.Push(Run.FixedTimeStamp.now.t))
			{
				base.Grant();
			}
		}

		// Token: 0x040050A2 RID: 20642
		private static readonly int requiredSkillCount = 20;

		// Token: 0x040050A3 RID: 20643
		private static readonly float windowSecconds = 10f;

		// Token: 0x040050A4 RID: 20644
		private DoXInYSecondsTracker tracker;

		// Token: 0x040050A5 RID: 20645
		private CharacterBody _trackedBody;
	}
}
