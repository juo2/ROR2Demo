using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000483 RID: 1155
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0001DB35 File Offset: 0x0001BD35
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0001DB40 File Offset: 0x0001BD40
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0001DB5C File Offset: 0x0001BD5C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0001DB64 File Offset: 0x0001BD64
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0001DB80 File Offset: 0x0001BD80
		public ItemOwnership[] ItemOwnership
		{
			get
			{
				ItemOwnership[] result;
				Helper.TryMarshalGet<ItemOwnershipInternal, ItemOwnership>(this.m_ItemOwnership, out result, this.m_ItemOwnershipCount);
				return result;
			}
		}

		// Token: 0x04000D1D RID: 3357
		private Result m_ResultCode;

		// Token: 0x04000D1E RID: 3358
		private IntPtr m_ClientData;

		// Token: 0x04000D1F RID: 3359
		private IntPtr m_LocalUserId;

		// Token: 0x04000D20 RID: 3360
		private IntPtr m_ItemOwnership;

		// Token: 0x04000D21 RID: 3361
		private uint m_ItemOwnershipCount;
	}
}
