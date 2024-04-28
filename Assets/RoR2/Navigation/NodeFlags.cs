using System;

namespace RoR2.Navigation
{
	// Token: 0x02000B41 RID: 2881
	[Flags]
	public enum NodeFlags : byte
	{
		// Token: 0x04003FE7 RID: 16359
		None = 0,
		// Token: 0x04003FE8 RID: 16360
		NoCeiling = 1,
		// Token: 0x04003FE9 RID: 16361
		TeleporterOK = 2,
		// Token: 0x04003FEA RID: 16362
		NoCharacterSpawn = 4,
		// Token: 0x04003FEB RID: 16363
		NoChestSpawn = 8,
		// Token: 0x04003FEC RID: 16364
		NoShrineSpawn = 16
	}
}
