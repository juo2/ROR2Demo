using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000249 RID: 585
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileTransferProgressCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x000104DC File Offset: 0x0000E6DC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x000104F8 File Offset: 0x0000E6F8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00010500 File Offset: 0x0000E700
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0001051C File Offset: 0x0000E71C
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00010538 File Offset: 0x0000E738
		public uint BytesTransferred
		{
			get
			{
				return this.m_BytesTransferred;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00010540 File Offset: 0x0000E740
		public uint TotalFileSizeBytes
		{
			get
			{
				return this.m_TotalFileSizeBytes;
			}
		}

		// Token: 0x04000720 RID: 1824
		private IntPtr m_ClientData;

		// Token: 0x04000721 RID: 1825
		private IntPtr m_LocalUserId;

		// Token: 0x04000722 RID: 1826
		private IntPtr m_Filename;

		// Token: 0x04000723 RID: 1827
		private uint m_BytesTransferred;

		// Token: 0x04000724 RID: 1828
		private uint m_TotalFileSizeBytes;
	}
}
