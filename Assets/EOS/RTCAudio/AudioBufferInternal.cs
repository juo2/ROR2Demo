using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000175 RID: 373
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioBufferInternal : ISettable, IDisposable
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0000B2A8 File Offset: 0x000094A8
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0000B2CA File Offset: 0x000094CA
		public short[] Frames
		{
			get
			{
				short[] result;
				Helper.TryMarshalGet<short>(this.m_Frames, out result, this.m_FramesCount);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<short>(ref this.m_Frames, value, out this.m_FramesCount);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0000B2DF File Offset: 0x000094DF
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0000B2E7 File Offset: 0x000094E7
		public uint SampleRate
		{
			get
			{
				return this.m_SampleRate;
			}
			set
			{
				this.m_SampleRate = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0000B2F0 File Offset: 0x000094F0
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public uint Channels
		{
			get
			{
				return this.m_Channels;
			}
			set
			{
				this.m_Channels = value;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000B301 File Offset: 0x00009501
		public void Set(AudioBuffer other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Frames = other.Frames;
				this.SampleRate = other.SampleRate;
				this.Channels = other.Channels;
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0000B331 File Offset: 0x00009531
		public void Set(object other)
		{
			this.Set(other as AudioBuffer);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0000B33F File Offset: 0x0000953F
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Frames);
		}

		// Token: 0x040004DE RID: 1246
		private int m_ApiVersion;

		// Token: 0x040004DF RID: 1247
		private IntPtr m_Frames;

		// Token: 0x040004E0 RID: 1248
		private uint m_FramesCount;

		// Token: 0x040004E1 RID: 1249
		private uint m_SampleRate;

		// Token: 0x040004E2 RID: 1250
		private uint m_Channels;
	}
}
