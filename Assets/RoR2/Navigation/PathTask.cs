using System;

namespace RoR2.Navigation
{
	// Token: 0x02000B42 RID: 2882
	public class PathTask
	{
		// Token: 0x0600419F RID: 16799 RVA: 0x0010FF16 File Offset: 0x0010E116
		public PathTask(Path path)
		{
			this.path = path;
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x000026ED File Offset: 0x000008ED
		public void Wait()
		{
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x0010FF25 File Offset: 0x0010E125
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x0010FF2D File Offset: 0x0010E12D
		public Path path { get; private set; }

		// Token: 0x04003FED RID: 16365
		public PathTask.TaskStatus status;

		// Token: 0x04003FEE RID: 16366
		public bool wasReachable;

		// Token: 0x02000B43 RID: 2883
		public enum TaskStatus
		{
			// Token: 0x04003FF1 RID: 16369
			NotStarted,
			// Token: 0x04003FF2 RID: 16370
			Running,
			// Token: 0x04003FF3 RID: 16371
			Complete
		}
	}
}
