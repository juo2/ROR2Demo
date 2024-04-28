using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200023F RID: 575
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteFileCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0000FF48 File Offset: 0x0000E148
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0000FF50 File Offset: 0x0000E150
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0000FF74 File Offset: 0x0000E174
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x040006FB RID: 1787
		private Result m_ResultCode;

		// Token: 0x040006FC RID: 1788
		private IntPtr m_ClientData;

		// Token: 0x040006FD RID: 1789
		private IntPtr m_LocalUserId;
	}
}
