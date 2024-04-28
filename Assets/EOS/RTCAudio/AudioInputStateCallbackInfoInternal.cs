using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200017B RID: 379
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioInputStateCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0000B630 File Offset: 0x00009830
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0000B64C File Offset: 0x0000984C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0000B654 File Offset: 0x00009854
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x0000B670 File Offset: 0x00009870
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0000B68C File Offset: 0x0000988C
		public RTCAudioInputStatus Status
		{
			get
			{
				return this.m_Status;
			}
		}

		// Token: 0x040004F0 RID: 1264
		private IntPtr m_ClientData;

		// Token: 0x040004F1 RID: 1265
		private IntPtr m_LocalUserId;

		// Token: 0x040004F2 RID: 1266
		private IntPtr m_RoomName;

		// Token: 0x040004F3 RID: 1267
		private RTCAudioInputStatus m_Status;
	}
}
