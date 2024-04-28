using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002BF RID: 703
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EnumerateModsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00012FB1 File Offset: 0x000111B1
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00012FBC File Offset: 0x000111BC
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00012FD8 File Offset: 0x000111D8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00012FF4 File Offset: 0x000111F4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00012FFC File Offset: 0x000111FC
		public ModEnumerationType Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x04000864 RID: 2148
		private Result m_ResultCode;

		// Token: 0x04000865 RID: 2149
		private IntPtr m_LocalUserId;

		// Token: 0x04000866 RID: 2150
		private IntPtr m_ClientData;

		// Token: 0x04000867 RID: 2151
		private ModEnumerationType m_Type;
	}
}
