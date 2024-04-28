using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000002 RID: 2
	internal sealed class BoxedData
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public object Data { get; private set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public BoxedData(object data)
		{
			this.Data = data;
		}
	}
}
