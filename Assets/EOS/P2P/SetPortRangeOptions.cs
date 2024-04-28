using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B6 RID: 694
	public class SetPortRangeOptions
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00012D1D File Offset: 0x00010F1D
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00012D25 File Offset: 0x00010F25
		public ushort Port { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00012D2E File Offset: 0x00010F2E
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00012D36 File Offset: 0x00010F36
		public ushort MaxAdditionalPortsToTry { get; set; }
	}
}
