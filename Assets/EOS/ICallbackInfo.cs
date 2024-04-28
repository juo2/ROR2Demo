using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200000C RID: 12
	internal interface ICallbackInfo
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000074 RID: 116
		object ClientData { get; }

		// Token: 0x06000075 RID: 117
		Result? GetResultCode();
	}
}
