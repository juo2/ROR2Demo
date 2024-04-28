using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200006E RID: 110
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileTransferProgressCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00005798 File Offset: 0x00003998
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000057B4 File Offset: 0x000039B4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000057BC File Offset: 0x000039BC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000057D8 File Offset: 0x000039D8
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000057F4 File Offset: 0x000039F4
		public uint BytesTransferred
		{
			get
			{
				return this.m_BytesTransferred;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000057FC File Offset: 0x000039FC
		public uint TotalFileSizeBytes
		{
			get
			{
				return this.m_TotalFileSizeBytes;
			}
		}

		// Token: 0x0400025A RID: 602
		private IntPtr m_ClientData;

		// Token: 0x0400025B RID: 603
		private IntPtr m_LocalUserId;

		// Token: 0x0400025C RID: 604
		private IntPtr m_Filename;

		// Token: 0x0400025D RID: 605
		private uint m_BytesTransferred;

		// Token: 0x0400025E RID: 606
		private uint m_TotalFileSizeBytes;
	}
}
