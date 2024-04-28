using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026B RID: 619
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00010EB5 File Offset: 0x0000F0B5
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00010EC0 File Offset: 0x0000F0C0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00010EDC File Offset: 0x0000F0DC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00010F00 File Offset: 0x0000F100
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
		}

		// Token: 0x0400074F RID: 1871
		private Result m_ResultCode;

		// Token: 0x04000750 RID: 1872
		private IntPtr m_ClientData;

		// Token: 0x04000751 RID: 1873
		private IntPtr m_LocalUserId;

		// Token: 0x04000752 RID: 1874
		private IntPtr m_Filename;
	}
}
