using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000173 RID: 371
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioBeforeSendCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0000B194 File Offset: 0x00009394
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0000B1D4 File Offset: 0x000093D4
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0000B1F0 File Offset: 0x000093F0
		public AudioBuffer Buffer
		{
			get
			{
				AudioBuffer result;
				Helper.TryMarshalGet<AudioBufferInternal, AudioBuffer>(this.m_Buffer, out result);
				return result;
			}
		}

		// Token: 0x040004D7 RID: 1239
		private IntPtr m_ClientData;

		// Token: 0x040004D8 RID: 1240
		private IntPtr m_LocalUserId;

		// Token: 0x040004D9 RID: 1241
		private IntPtr m_RoomName;

		// Token: 0x040004DA RID: 1242
		private IntPtr m_Buffer;
	}
}
