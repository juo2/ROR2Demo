using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E2 RID: 994
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0001967E File Offset: 0x0001787E
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00019688 File Offset: 0x00017888
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x000196A4 File Offset: 0x000178A4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x000196AC File Offset: 0x000178AC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x000196C8 File Offset: 0x000178C8
		public string KWSUserId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_KWSUserId, out result);
				return result;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x000196E4 File Offset: 0x000178E4
		public bool IsMinor
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsMinor, out result);
				return result;
			}
		}

		// Token: 0x04000B43 RID: 2883
		private Result m_ResultCode;

		// Token: 0x04000B44 RID: 2884
		private IntPtr m_ClientData;

		// Token: 0x04000B45 RID: 2885
		private IntPtr m_LocalUserId;

		// Token: 0x04000B46 RID: 2886
		private IntPtr m_KWSUserId;

		// Token: 0x04000B47 RID: 2887
		private int m_IsMinor;
	}
}
