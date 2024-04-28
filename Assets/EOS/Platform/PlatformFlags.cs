using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E6 RID: 1510
	[Flags]
	public enum PlatformFlags : ulong
	{
		// Token: 0x04001168 RID: 4456
		None = 0UL,
		// Token: 0x04001169 RID: 4457
		LoadingInEditor = 1UL,
		// Token: 0x0400116A RID: 4458
		DisableOverlay = 2UL,
		// Token: 0x0400116B RID: 4459
		DisableSocialOverlay = 4UL,
		// Token: 0x0400116C RID: 4460
		Reserved1 = 8UL,
		// Token: 0x0400116D RID: 4461
		WindowsEnableOverlayD3D9 = 16UL,
		// Token: 0x0400116E RID: 4462
		WindowsEnableOverlayD3D10 = 32UL,
		// Token: 0x0400116F RID: 4463
		WindowsEnableOverlayOpengl = 64UL
	}
}
