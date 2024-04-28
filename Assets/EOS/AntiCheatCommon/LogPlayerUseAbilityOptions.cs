using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000595 RID: 1429
	public class LogPlayerUseAbilityOptions
	{
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x00024933 File Offset: 0x00022B33
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x0002493B File Offset: 0x00022B3B
		public IntPtr PlayerHandle { get; set; }

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x00024944 File Offset: 0x00022B44
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x0002494C File Offset: 0x00022B4C
		public uint AbilityId { get; set; }

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x00024955 File Offset: 0x00022B55
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x0002495D File Offset: 0x00022B5D
		public uint AbilityDurationMs { get; set; }

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x00024966 File Offset: 0x00022B66
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x0002496E File Offset: 0x00022B6E
		public uint AbilityCooldownMs { get; set; }
	}
}
