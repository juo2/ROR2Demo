using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C4 RID: 452
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetParticipantHardMuteCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0000D0CF File Offset: 0x0000B2CF
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x040005AF RID: 1455
		private Result m_ResultCode;

		// Token: 0x040005B0 RID: 1456
		private IntPtr m_ClientData;
	}
}
