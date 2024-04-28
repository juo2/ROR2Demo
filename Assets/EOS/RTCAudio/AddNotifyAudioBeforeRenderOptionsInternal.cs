using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000165 RID: 357
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioBeforeRenderOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000251 RID: 593
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x0000AC52 File Offset: 0x00008E52
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000252 RID: 594
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x0000AC61 File Offset: 0x00008E61
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x17000253 RID: 595
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x0000AC70 File Offset: 0x00008E70
		public bool UnmixedAudio
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UnmixedAudio, value);
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0000AC7F File Offset: 0x00008E7F
		public void Set(AddNotifyAudioBeforeRenderOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
				this.UnmixedAudio = other.UnmixedAudio;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000ACAF File Offset: 0x00008EAF
		public void Set(object other)
		{
			this.Set(other as AddNotifyAudioBeforeRenderOptions);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0000ACBD File Offset: 0x00008EBD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x040004B0 RID: 1200
		private int m_ApiVersion;

		// Token: 0x040004B1 RID: 1201
		private IntPtr m_LocalUserId;

		// Token: 0x040004B2 RID: 1202
		private IntPtr m_RoomName;

		// Token: 0x040004B3 RID: 1203
		private int m_UnmixedAudio;
	}
}
