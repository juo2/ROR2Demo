using System;

namespace SteamNative
{
	// Token: 0x02000028 RID: 40
	internal enum CheckFileSignature
	{
		// Token: 0x040001FC RID: 508
		InvalidSignature,
		// Token: 0x040001FD RID: 509
		ValidSignature,
		// Token: 0x040001FE RID: 510
		FileNotFound,
		// Token: 0x040001FF RID: 511
		NoSignaturesFoundForThisApp,
		// Token: 0x04000200 RID: 512
		NoSignaturesFoundForThisFile
	}
}
