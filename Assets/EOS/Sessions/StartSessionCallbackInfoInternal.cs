using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000142 RID: 322
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StartSessionCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0000A0C7 File Offset: 0x000082C7
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0000A0D0 File Offset: 0x000082D0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0000A0EC File Offset: 0x000082EC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000450 RID: 1104
		private Result m_ResultCode;

		// Token: 0x04000451 RID: 1105
		private IntPtr m_ClientData;
	}
}
