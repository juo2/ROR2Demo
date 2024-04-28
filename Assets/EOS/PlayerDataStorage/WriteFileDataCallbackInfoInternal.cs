using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000274 RID: 628
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileDataCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0001146C File Offset: 0x0000F66C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00011488 File Offset: 0x0000F688
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00011490 File Offset: 0x0000F690
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x000114AC File Offset: 0x0000F6AC
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x000114C8 File Offset: 0x0000F6C8
		public uint DataBufferLengthBytes
		{
			get
			{
				return this.m_DataBufferLengthBytes;
			}
		}

		// Token: 0x0400077D RID: 1917
		private IntPtr m_ClientData;

		// Token: 0x0400077E RID: 1918
		private IntPtr m_LocalUserId;

		// Token: 0x0400077F RID: 1919
		private IntPtr m_Filename;

		// Token: 0x04000780 RID: 1920
		private uint m_DataBufferLengthBytes;
	}
}
