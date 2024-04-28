using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000214 RID: 532
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinGameAcceptedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0000F108 File Offset: 0x0000D308
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0000F124 File Offset: 0x0000D324
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0000F12C File Offset: 0x0000D32C
		public string JoinInfo
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_JoinInfo, out result);
				return result;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0000F148 File Offset: 0x0000D348
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0000F164 File Offset: 0x0000D364
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0000F180 File Offset: 0x0000D380
		public ulong UiEventId
		{
			get
			{
				return this.m_UiEventId;
			}
		}

		// Token: 0x0400068F RID: 1679
		private IntPtr m_ClientData;

		// Token: 0x04000690 RID: 1680
		private IntPtr m_JoinInfo;

		// Token: 0x04000691 RID: 1681
		private IntPtr m_LocalUserId;

		// Token: 0x04000692 RID: 1682
		private IntPtr m_TargetUserId;

		// Token: 0x04000693 RID: 1683
		private ulong m_UiEventId;
	}
}
