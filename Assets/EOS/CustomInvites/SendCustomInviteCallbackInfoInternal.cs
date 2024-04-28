using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A5 RID: 1189
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendCustomInviteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0001E82D File Offset: 0x0001CA2D
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0001E838 File Offset: 0x0001CA38
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x0001E854 File Offset: 0x0001CA54
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0001E85C File Offset: 0x0001CA5C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0001E878 File Offset: 0x0001CA78
		public ProductUserId[] TargetUserIds
		{
			get
			{
				ProductUserId[] result;
				Helper.TryMarshalGetHandle<ProductUserId>(this.m_TargetUserIds, out result, this.m_TargetUserIdsCount);
				return result;
			}
		}

		// Token: 0x04000D75 RID: 3445
		private Result m_ResultCode;

		// Token: 0x04000D76 RID: 3446
		private IntPtr m_ClientData;

		// Token: 0x04000D77 RID: 3447
		private IntPtr m_LocalUserId;

		// Token: 0x04000D78 RID: 3448
		private IntPtr m_TargetUserIds;

		// Token: 0x04000D79 RID: 3449
		private uint m_TargetUserIdsCount;
	}
}
