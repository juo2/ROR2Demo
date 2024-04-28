using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A4 RID: 676
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryNATTypeCompleteInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x000120D8 File Offset: 0x000102D8
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x000120E0 File Offset: 0x000102E0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x000120FC File Offset: 0x000102FC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00012104 File Offset: 0x00010304
		public NATType NATType
		{
			get
			{
				return this.m_NATType;
			}
		}

		// Token: 0x040007FB RID: 2043
		private Result m_ResultCode;

		// Token: 0x040007FC RID: 2044
		private IntPtr m_ClientData;

		// Token: 0x040007FD RID: 2045
		private NATType m_NATType;
	}
}
