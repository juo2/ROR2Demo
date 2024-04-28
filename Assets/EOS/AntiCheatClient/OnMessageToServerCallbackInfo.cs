using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005C4 RID: 1476
	public class OnMessageToServerCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x00025BD8 File Offset: 0x00023DD8
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x00025BE0 File Offset: 0x00023DE0
		public object ClientData { get; private set; }

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x00025BE9 File Offset: 0x00023DE9
		// (set) Token: 0x060023AF RID: 9135 RVA: 0x00025BF1 File Offset: 0x00023DF1
		public byte[] MessageData { get; private set; }

		// Token: 0x060023B0 RID: 9136 RVA: 0x00025BFC File Offset: 0x00023DFC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00025C14 File Offset: 0x00023E14
		internal void Set(OnMessageToServerCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.MessageData = other.Value.MessageData;
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00025C54 File Offset: 0x00023E54
		public void Set(object other)
		{
			this.Set(other as OnMessageToServerCallbackInfoInternal?);
		}
	}
}
