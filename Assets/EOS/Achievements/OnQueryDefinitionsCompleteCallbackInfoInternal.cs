using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200061B RID: 1563
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryDefinitionsCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x00028C17 File Offset: 0x00026E17
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x00028C20 File Offset: 0x00026E20
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00028C3C File Offset: 0x00026E3C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0400121A RID: 4634
		private Result m_ResultCode;

		// Token: 0x0400121B RID: 4635
		private IntPtr m_ClientData;
	}
}
