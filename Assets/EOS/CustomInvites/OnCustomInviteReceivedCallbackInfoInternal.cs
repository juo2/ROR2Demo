using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A1 RID: 1185
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnCustomInviteReceivedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0001E6EC File Offset: 0x0001C8EC
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0001E708 File Offset: 0x0001C908
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0001E724 File Offset: 0x0001C924
		public string CustomInviteId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_CustomInviteId, out result);
				return result;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0001E740 File Offset: 0x0001C940
		public string Payload
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Payload, out result);
				return result;
			}
		}

		// Token: 0x04000D6C RID: 3436
		private IntPtr m_ClientData;

		// Token: 0x04000D6D RID: 3437
		private IntPtr m_TargetUserId;

		// Token: 0x04000D6E RID: 3438
		private IntPtr m_LocalUserId;

		// Token: 0x04000D6F RID: 3439
		private IntPtr m_CustomInviteId;

		// Token: 0x04000D70 RID: 3440
		private IntPtr m_Payload;
	}
}
