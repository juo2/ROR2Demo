using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000626 RID: 1574
	public class PlayerStatInfo : ISettable
	{
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000291E1 File Offset: 0x000273E1
		// (set) Token: 0x0600269A RID: 9882 RVA: 0x000291E9 File Offset: 0x000273E9
		public string Name { get; set; }

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x000291F2 File Offset: 0x000273F2
		// (set) Token: 0x0600269C RID: 9884 RVA: 0x000291FA File Offset: 0x000273FA
		public int CurrentValue { get; set; }

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x00029203 File Offset: 0x00027403
		// (set) Token: 0x0600269E RID: 9886 RVA: 0x0002920B File Offset: 0x0002740B
		public int ThresholdValue { get; set; }

		// Token: 0x0600269F RID: 9887 RVA: 0x00029214 File Offset: 0x00027414
		internal void Set(PlayerStatInfoInternal? other)
		{
			if (other != null)
			{
				this.Name = other.Value.Name;
				this.CurrentValue = other.Value.CurrentValue;
				this.ThresholdValue = other.Value.ThresholdValue;
			}
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x00029269 File Offset: 0x00027469
		public void Set(object other)
		{
			this.Set(other as PlayerStatInfoInternal?);
		}
	}
}
