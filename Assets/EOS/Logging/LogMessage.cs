using System;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x020002E9 RID: 745
	public class LogMessage : ISettable
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x000140F4 File Offset: 0x000122F4
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x000140FC File Offset: 0x000122FC
		public string Category { get; private set; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00014105 File Offset: 0x00012305
		// (set) Token: 0x060012CC RID: 4812 RVA: 0x0001410D File Offset: 0x0001230D
		public string Message { get; private set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00014116 File Offset: 0x00012316
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x0001411E File Offset: 0x0001231E
		public LogLevel Level { get; private set; }

		// Token: 0x060012CF RID: 4815 RVA: 0x00014128 File Offset: 0x00012328
		internal void Set(LogMessageInternal? other)
		{
			if (other != null)
			{
				this.Category = other.Value.Category;
				this.Message = other.Value.Message;
				this.Level = other.Value.Level;
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0001417D File Offset: 0x0001237D
		public void Set(object other)
		{
			this.Set(other as LogMessageInternal?);
		}
	}
}
