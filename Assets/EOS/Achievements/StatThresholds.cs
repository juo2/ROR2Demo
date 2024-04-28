using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200062C RID: 1580
	public class StatThresholds : ISettable
	{
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0002946C File Offset: 0x0002766C
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x00029474 File Offset: 0x00027674
		public string Name { get; set; }

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0002947D File Offset: 0x0002767D
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x00029485 File Offset: 0x00027685
		public int Threshold { get; set; }

		// Token: 0x060026C6 RID: 9926 RVA: 0x00029490 File Offset: 0x00027690
		internal void Set(StatThresholdsInternal? other)
		{
			if (other != null)
			{
				this.Name = other.Value.Name;
				this.Threshold = other.Value.Threshold;
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000294D0 File Offset: 0x000276D0
		public void Set(object other)
		{
			this.Set(other as StatThresholdsInternal?);
		}
	}
}
