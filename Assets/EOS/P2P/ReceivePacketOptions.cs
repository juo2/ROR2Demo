using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AF RID: 687
	public class ReceivePacketOptions
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00012A80 File Offset: 0x00010C80
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x00012A88 File Offset: 0x00010C88
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00012A91 File Offset: 0x00010C91
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x00012A99 File Offset: 0x00010C99
		public uint MaxDataSizeBytes { get; set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00012AA2 File Offset: 0x00010CA2
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x00012AAA File Offset: 0x00010CAA
		public byte? RequestedChannel { get; set; }
	}
}
