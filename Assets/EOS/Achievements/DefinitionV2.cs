using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000608 RID: 1544
	public class DefinitionV2 : ISettable
	{
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000283DD File Offset: 0x000265DD
		// (set) Token: 0x060025C3 RID: 9667 RVA: 0x000283E5 File Offset: 0x000265E5
		public string AchievementId { get; set; }

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000283EE File Offset: 0x000265EE
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x000283F6 File Offset: 0x000265F6
		public string UnlockedDisplayName { get; set; }

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x000283FF File Offset: 0x000265FF
		// (set) Token: 0x060025C7 RID: 9671 RVA: 0x00028407 File Offset: 0x00026607
		public string UnlockedDescription { get; set; }

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x00028410 File Offset: 0x00026610
		// (set) Token: 0x060025C9 RID: 9673 RVA: 0x00028418 File Offset: 0x00026618
		public string LockedDisplayName { get; set; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x00028421 File Offset: 0x00026621
		// (set) Token: 0x060025CB RID: 9675 RVA: 0x00028429 File Offset: 0x00026629
		public string LockedDescription { get; set; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x00028432 File Offset: 0x00026632
		// (set) Token: 0x060025CD RID: 9677 RVA: 0x0002843A File Offset: 0x0002663A
		public string FlavorText { get; set; }

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x00028443 File Offset: 0x00026643
		// (set) Token: 0x060025CF RID: 9679 RVA: 0x0002844B File Offset: 0x0002664B
		public string UnlockedIconURL { get; set; }

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x00028454 File Offset: 0x00026654
		// (set) Token: 0x060025D1 RID: 9681 RVA: 0x0002845C File Offset: 0x0002665C
		public string LockedIconURL { get; set; }

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x00028465 File Offset: 0x00026665
		// (set) Token: 0x060025D3 RID: 9683 RVA: 0x0002846D File Offset: 0x0002666D
		public bool IsHidden { get; set; }

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x00028476 File Offset: 0x00026676
		// (set) Token: 0x060025D5 RID: 9685 RVA: 0x0002847E File Offset: 0x0002667E
		public StatThresholds[] StatThresholds { get; set; }

		// Token: 0x060025D6 RID: 9686 RVA: 0x00028488 File Offset: 0x00026688
		internal void Set(DefinitionV2Internal? other)
		{
			if (other != null)
			{
				this.AchievementId = other.Value.AchievementId;
				this.UnlockedDisplayName = other.Value.UnlockedDisplayName;
				this.UnlockedDescription = other.Value.UnlockedDescription;
				this.LockedDisplayName = other.Value.LockedDisplayName;
				this.LockedDescription = other.Value.LockedDescription;
				this.FlavorText = other.Value.FlavorText;
				this.UnlockedIconURL = other.Value.UnlockedIconURL;
				this.LockedIconURL = other.Value.LockedIconURL;
				this.IsHidden = other.Value.IsHidden;
				this.StatThresholds = other.Value.StatThresholds;
			}
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00028573 File Offset: 0x00026773
		public void Set(object other)
		{
			this.Set(other as DefinitionV2Internal?);
		}
	}
}
