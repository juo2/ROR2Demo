using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000174 RID: 372
	public class AudioBuffer : ISettable
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0000B20C File Offset: 0x0000940C
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0000B214 File Offset: 0x00009414
		public short[] Frames { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0000B21D File Offset: 0x0000941D
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0000B225 File Offset: 0x00009425
		public uint SampleRate { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0000B22E File Offset: 0x0000942E
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x0000B236 File Offset: 0x00009436
		public uint Channels { get; set; }

		// Token: 0x06000A1B RID: 2587 RVA: 0x0000B240 File Offset: 0x00009440
		internal void Set(AudioBufferInternal? other)
		{
			if (other != null)
			{
				this.Frames = other.Value.Frames;
				this.SampleRate = other.Value.SampleRate;
				this.Channels = other.Value.Channels;
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0000B295 File Offset: 0x00009495
		public void Set(object other)
		{
			this.Set(other as AudioBufferInternal?);
		}
	}
}
