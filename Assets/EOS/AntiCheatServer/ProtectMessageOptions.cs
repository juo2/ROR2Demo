using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000567 RID: 1383
	public class ProtectMessageOptions
	{
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x00023680 File Offset: 0x00021880
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x00023688 File Offset: 0x00021888
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x00023691 File Offset: 0x00021891
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x00023699 File Offset: 0x00021899
		public byte[] Data { get; set; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000236A2 File Offset: 0x000218A2
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x000236AA File Offset: 0x000218AA
		public uint OutBufferSizeBytes { get; set; }
	}
}
