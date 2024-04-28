using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x0200049D RID: 1181
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnCustomInviteAcceptedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0001E530 File Offset: 0x0001C730
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001CB3 RID: 7347 RVA: 0x0001E54C File Offset: 0x0001C74C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0001E554 File Offset: 0x0001C754
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x0001E570 File Offset: 0x0001C770
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0001E58C File Offset: 0x0001C78C
		public string CustomInviteId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_CustomInviteId, out result);
				return result;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
		public string Payload
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Payload, out result);
				return result;
			}
		}

		// Token: 0x04000D62 RID: 3426
		private IntPtr m_ClientData;

		// Token: 0x04000D63 RID: 3427
		private IntPtr m_TargetUserId;

		// Token: 0x04000D64 RID: 3428
		private IntPtr m_LocalUserId;

		// Token: 0x04000D65 RID: 3429
		private IntPtr m_CustomInviteId;

		// Token: 0x04000D66 RID: 3430
		private IntPtr m_Payload;
	}
}
