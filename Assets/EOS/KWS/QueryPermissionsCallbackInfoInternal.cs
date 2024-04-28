using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000400 RID: 1024
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPermissionsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0001A04F File Offset: 0x0001824F
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0001A058 File Offset: 0x00018258
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0001A074 File Offset: 0x00018274
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0001A07C File Offset: 0x0001827C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0001A098 File Offset: 0x00018298
		public string KWSUserId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_KWSUserId, out result);
				return result;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0001A0B4 File Offset: 0x000182B4
		public string DateOfBirth
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DateOfBirth, out result);
				return result;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0001A0D0 File Offset: 0x000182D0
		public bool IsMinor
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsMinor, out result);
				return result;
			}
		}

		// Token: 0x04000B7F RID: 2943
		private Result m_ResultCode;

		// Token: 0x04000B80 RID: 2944
		private IntPtr m_ClientData;

		// Token: 0x04000B81 RID: 2945
		private IntPtr m_LocalUserId;

		// Token: 0x04000B82 RID: 2946
		private IntPtr m_KWSUserId;

		// Token: 0x04000B83 RID: 2947
		private IntPtr m_DateOfBirth;

		// Token: 0x04000B84 RID: 2948
		private int m_IsMinor;
	}
}
