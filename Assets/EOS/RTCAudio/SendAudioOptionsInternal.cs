using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A1 RID: 417
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendAudioOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002B5 RID: 693
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0000C27B File Offset: 0x0000A47B
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0000C28A File Offset: 0x0000A48A
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0000C299 File Offset: 0x0000A499
		public AudioBuffer Buffer
		{
			set
			{
				Helper.TryMarshalSet<AudioBufferInternal, AudioBuffer>(ref this.m_Buffer, value);
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
		public void Set(SendAudioOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
				this.Buffer = other.Buffer;
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		public void Set(object other)
		{
			this.Set(other as SendAudioOptions);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0000C2E6 File Offset: 0x0000A4E6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
			Helper.TryMarshalDispose(ref this.m_Buffer);
		}

		// Token: 0x04000541 RID: 1345
		private int m_ApiVersion;

		// Token: 0x04000542 RID: 1346
		private IntPtr m_LocalUserId;

		// Token: 0x04000543 RID: 1347
		private IntPtr m_RoomName;

		// Token: 0x04000544 RID: 1348
		private IntPtr m_Buffer;
	}
}
