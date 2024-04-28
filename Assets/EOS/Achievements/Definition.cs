using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000606 RID: 1542
	public class Definition : ISettable
	{
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x00027EEC File Offset: 0x000260EC
		// (set) Token: 0x06002591 RID: 9617 RVA: 0x00027EF4 File Offset: 0x000260F4
		public string AchievementId { get; set; }

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x00027EFD File Offset: 0x000260FD
		// (set) Token: 0x06002593 RID: 9619 RVA: 0x00027F05 File Offset: 0x00026105
		public string DisplayName { get; set; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x00027F0E File Offset: 0x0002610E
		// (set) Token: 0x06002595 RID: 9621 RVA: 0x00027F16 File Offset: 0x00026116
		public string Description { get; set; }

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x00027F1F File Offset: 0x0002611F
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x00027F27 File Offset: 0x00026127
		public string LockedDisplayName { get; set; }

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x00027F30 File Offset: 0x00026130
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x00027F38 File Offset: 0x00026138
		public string LockedDescription { get; set; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x00027F41 File Offset: 0x00026141
		// (set) Token: 0x0600259B RID: 9627 RVA: 0x00027F49 File Offset: 0x00026149
		public string HiddenDescription { get; set; }

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x00027F52 File Offset: 0x00026152
		// (set) Token: 0x0600259D RID: 9629 RVA: 0x00027F5A File Offset: 0x0002615A
		public string CompletionDescription { get; set; }

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x00027F63 File Offset: 0x00026163
		// (set) Token: 0x0600259F RID: 9631 RVA: 0x00027F6B File Offset: 0x0002616B
		public string UnlockedIconId { get; set; }

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x00027F74 File Offset: 0x00026174
		// (set) Token: 0x060025A1 RID: 9633 RVA: 0x00027F7C File Offset: 0x0002617C
		public string LockedIconId { get; set; }

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x00027F85 File Offset: 0x00026185
		// (set) Token: 0x060025A3 RID: 9635 RVA: 0x00027F8D File Offset: 0x0002618D
		public bool IsHidden { get; set; }

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00027F96 File Offset: 0x00026196
		// (set) Token: 0x060025A5 RID: 9637 RVA: 0x00027F9E File Offset: 0x0002619E
		public StatThresholds[] StatThresholds { get; set; }

		// Token: 0x060025A6 RID: 9638 RVA: 0x00027FA8 File Offset: 0x000261A8
		internal void Set(DefinitionInternal? other)
		{
			if (other != null)
			{
				this.AchievementId = other.Value.AchievementId;
				this.DisplayName = other.Value.DisplayName;
				this.Description = other.Value.Description;
				this.LockedDisplayName = other.Value.LockedDisplayName;
				this.LockedDescription = other.Value.LockedDescription;
				this.HiddenDescription = other.Value.HiddenDescription;
				this.CompletionDescription = other.Value.CompletionDescription;
				this.UnlockedIconId = other.Value.UnlockedIconId;
				this.LockedIconId = other.Value.LockedIconId;
				this.IsHidden = other.Value.IsHidden;
				this.StatThresholds = other.Value.StatThresholds;
			}
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000280A8 File Offset: 0x000262A8
		public void Set(object other)
		{
			this.Set(other as DefinitionInternal?);
		}
	}
}
