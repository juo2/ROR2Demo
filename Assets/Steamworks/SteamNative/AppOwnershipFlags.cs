using System;

namespace SteamNative
{
	// Token: 0x02000012 RID: 18
	internal enum AppOwnershipFlags
	{
		// Token: 0x04000118 RID: 280
		None,
		// Token: 0x04000119 RID: 281
		OwnsLicense,
		// Token: 0x0400011A RID: 282
		FreeLicense,
		// Token: 0x0400011B RID: 283
		RegionRestricted = 4,
		// Token: 0x0400011C RID: 284
		LowViolence = 8,
		// Token: 0x0400011D RID: 285
		InvalidPlatform = 16,
		// Token: 0x0400011E RID: 286
		SharedLicense = 32,
		// Token: 0x0400011F RID: 287
		FreeWeekend = 64,
		// Token: 0x04000120 RID: 288
		RetailLicense = 128,
		// Token: 0x04000121 RID: 289
		LicenseLocked = 256,
		// Token: 0x04000122 RID: 290
		LicensePending = 512,
		// Token: 0x04000123 RID: 291
		LicenseExpired = 1024,
		// Token: 0x04000124 RID: 292
		LicensePermanent = 2048,
		// Token: 0x04000125 RID: 293
		LicenseRecurring = 4096,
		// Token: 0x04000126 RID: 294
		LicenseCanceled = 8192,
		// Token: 0x04000127 RID: 295
		AutoGrant = 16384,
		// Token: 0x04000128 RID: 296
		PendingGift = 32768,
		// Token: 0x04000129 RID: 297
		RentalNotActivated = 65536,
		// Token: 0x0400012A RID: 298
		Rental = 131072,
		// Token: 0x0400012B RID: 299
		SiteLicense = 262144
	}
}
