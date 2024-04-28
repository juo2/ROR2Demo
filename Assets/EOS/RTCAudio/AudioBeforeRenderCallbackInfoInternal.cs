using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioBeforeRenderCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0000B024 File Offset: 0x00009224
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0000B040 File Offset: 0x00009240
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0000B048 File Offset: 0x00009248
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0000B064 File Offset: 0x00009264
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0000B080 File Offset: 0x00009280
		public AudioBuffer Buffer
		{
			get
			{
				AudioBuffer result;
				Helper.TryMarshalGet<AudioBufferInternal, AudioBuffer>(this.m_Buffer, out result);
				return result;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0000B09C File Offset: 0x0000929C
		public ProductUserId ParticipantId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_ParticipantId, out result);
				return result;
			}
		}

		// Token: 0x040004CE RID: 1230
		private IntPtr m_ClientData;

		// Token: 0x040004CF RID: 1231
		private IntPtr m_LocalUserId;

		// Token: 0x040004D0 RID: 1232
		private IntPtr m_RoomName;

		// Token: 0x040004D1 RID: 1233
		private IntPtr m_Buffer;

		// Token: 0x040004D2 RID: 1234
		private IntPtr m_ParticipantId;
	}
}
