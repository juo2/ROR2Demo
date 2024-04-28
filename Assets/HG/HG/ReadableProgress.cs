using System;

namespace HG
{
	// Token: 0x02000014 RID: 20
	public class ReadableProgress<T> : IProgress<T>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004415 File Offset: 0x00002615
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000441D File Offset: 0x0000261D
		public T value { get; private set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000D8 RID: 216 RVA: 0x00004428 File Offset: 0x00002628
		// (remove) Token: 0x060000D9 RID: 217 RVA: 0x00004460 File Offset: 0x00002660
		public event Action<T> onReport;

		// Token: 0x060000DA RID: 218 RVA: 0x000020A1 File Offset: 0x000002A1
		public ReadableProgress()
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004495 File Offset: 0x00002695
		public ReadableProgress(Action<T> onReport)
		{
			this.onReport += onReport;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000044A4 File Offset: 0x000026A4
		public void Report(T value)
		{
			this.value = value;
			Action<T> action = this.onReport;
			if (action == null)
			{
				return;
			}
			action(value);
		}
	}
}
