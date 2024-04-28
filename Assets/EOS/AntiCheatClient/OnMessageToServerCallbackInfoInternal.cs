using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005C5 RID: 1477
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnMessageToServerCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x00025C68 File Offset: 0x00023E68
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00025C84 File Offset: 0x00023E84
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x00025C8C File Offset: 0x00023E8C
		public byte[] MessageData
		{
			get
			{
				byte[] result;
				Helper.TryMarshalGet<byte>(this.m_MessageData, out result, this.m_MessageDataSizeBytes);
				return result;
			}
		}

		// Token: 0x040010E1 RID: 4321
		private IntPtr m_ClientData;

		// Token: 0x040010E2 RID: 4322
		private IntPtr m_MessageData;

		// Token: 0x040010E3 RID: 4323
		private uint m_MessageDataSizeBytes;
	}
}
