using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A7 RID: 1447
	public class SetClientDetailsOptions
	{
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00025335 File Offset: 0x00023535
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x0002533D File Offset: 0x0002353D
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x00025346 File Offset: 0x00023546
		// (set) Token: 0x06002334 RID: 9012 RVA: 0x0002534E File Offset: 0x0002354E
		public AntiCheatCommonClientFlags ClientFlags { get; set; }

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x00025357 File Offset: 0x00023557
		// (set) Token: 0x06002336 RID: 9014 RVA: 0x0002535F File Offset: 0x0002355F
		public AntiCheatCommonClientInput ClientInputMethod { get; set; }
	}
}
