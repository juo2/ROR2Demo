using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000086 RID: 134
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00005C59 File Offset: 0x00003E59
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00005C64 File Offset: 0x00003E64
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00005C80 File Offset: 0x00003E80
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00005C88 File Offset: 0x00003E88
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
		}

		// Token: 0x0400027F RID: 639
		private Result m_ResultCode;

		// Token: 0x04000280 RID: 640
		private IntPtr m_ClientData;

		// Token: 0x04000281 RID: 641
		private IntPtr m_LocalUserId;

		// Token: 0x04000282 RID: 642
		private IntPtr m_Filename;
	}
}
