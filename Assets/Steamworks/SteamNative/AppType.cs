using System;

namespace SteamNative
{
	// Token: 0x02000013 RID: 19
	internal enum AppType
	{
		// Token: 0x0400012D RID: 301
		Invalid,
		// Token: 0x0400012E RID: 302
		Game,
		// Token: 0x0400012F RID: 303
		Application,
		// Token: 0x04000130 RID: 304
		Tool = 4,
		// Token: 0x04000131 RID: 305
		Demo = 8,
		// Token: 0x04000132 RID: 306
		Media_DEPRECATED = 16,
		// Token: 0x04000133 RID: 307
		DLC = 32,
		// Token: 0x04000134 RID: 308
		Guide = 64,
		// Token: 0x04000135 RID: 309
		Driver = 128,
		// Token: 0x04000136 RID: 310
		Config = 256,
		// Token: 0x04000137 RID: 311
		Hardware = 512,
		// Token: 0x04000138 RID: 312
		Franchise = 1024,
		// Token: 0x04000139 RID: 313
		Video = 2048,
		// Token: 0x0400013A RID: 314
		Plugin = 4096,
		// Token: 0x0400013B RID: 315
		Music = 8192,
		// Token: 0x0400013C RID: 316
		Series = 16384,
		// Token: 0x0400013D RID: 317
		Comic = 32768,
		// Token: 0x0400013E RID: 318
		Shortcut = 1073741824,
		// Token: 0x0400013F RID: 319
		DepotOnly = -2147483648
	}
}
