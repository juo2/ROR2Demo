using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D5 RID: 213
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionAcceptedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00007C90 File Offset: 0x00005E90
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00007CAC File Offset: 0x00005EAC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public ulong UiEventId
		{
			get
			{
				return this.m_UiEventId;
			}
		}

		// Token: 0x04000357 RID: 855
		private IntPtr m_ClientData;

		// Token: 0x04000358 RID: 856
		private IntPtr m_LocalUserId;

		// Token: 0x04000359 RID: 857
		private ulong m_UiEventId;
	}
}
