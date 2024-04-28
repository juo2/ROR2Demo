using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200058F RID: 1423
	public class LogPlayerSpawnOptions
	{
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000243DE File Offset: 0x000225DE
		// (set) Token: 0x0600223C RID: 8764 RVA: 0x000243E6 File Offset: 0x000225E6
		public IntPtr SpawnedPlayerHandle { get; set; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000243EF File Offset: 0x000225EF
		// (set) Token: 0x0600223E RID: 8766 RVA: 0x000243F7 File Offset: 0x000225F7
		public uint TeamId { get; set; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x00024400 File Offset: 0x00022600
		// (set) Token: 0x06002240 RID: 8768 RVA: 0x00024408 File Offset: 0x00022608
		public uint CharacterId { get; set; }
	}
}
