using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200059F RID: 1439
	public class OnMessageToClientCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x00024F20 File Offset: 0x00023120
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x00024F28 File Offset: 0x00023128
		public object ClientData { get; private set; }

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x00024F31 File Offset: 0x00023131
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x00024F39 File Offset: 0x00023139
		public IntPtr ClientHandle { get; private set; }

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x00024F42 File Offset: 0x00023142
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x00024F4A File Offset: 0x0002314A
		public byte[] MessageData { get; private set; }

		// Token: 0x060022F5 RID: 8949 RVA: 0x00024F54 File Offset: 0x00023154
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00024F6C File Offset: 0x0002316C
		internal void Set(OnMessageToClientCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.ClientHandle = other.Value.ClientHandle;
				this.MessageData = other.Value.MessageData;
			}
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x00024FC1 File Offset: 0x000231C1
		public void Set(object other)
		{
			this.Set(other as OnMessageToClientCallbackInfoInternal?);
		}
	}
}
