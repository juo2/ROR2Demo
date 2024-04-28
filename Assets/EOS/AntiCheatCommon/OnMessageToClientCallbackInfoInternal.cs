using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A0 RID: 1440
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnMessageToClientCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x00024FD4 File Offset: 0x000231D4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x00024FF0 File Offset: 0x000231F0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x00024FF8 File Offset: 0x000231F8
		public IntPtr ClientHandle
		{
			get
			{
				return this.m_ClientHandle;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x00025000 File Offset: 0x00023200
		public byte[] MessageData
		{
			get
			{
				byte[] result;
				Helper.TryMarshalGet<byte>(this.m_MessageData, out result, this.m_MessageDataSizeBytes);
				return result;
			}
		}

		// Token: 0x04001080 RID: 4224
		private IntPtr m_ClientData;

		// Token: 0x04001081 RID: 4225
		private IntPtr m_ClientHandle;

		// Token: 0x04001082 RID: 4226
		private IntPtr m_MessageData;

		// Token: 0x04001083 RID: 4227
		private uint m_MessageDataSizeBytes;
	}
}
