using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005B9 RID: 1465
	public enum AntiCheatClientViolationType
	{
		// Token: 0x040010C6 RID: 4294
		Invalid,
		// Token: 0x040010C7 RID: 4295
		IntegrityCatalogNotFound,
		// Token: 0x040010C8 RID: 4296
		IntegrityCatalogError,
		// Token: 0x040010C9 RID: 4297
		IntegrityCatalogCertificateRevoked,
		// Token: 0x040010CA RID: 4298
		IntegrityCatalogMissingMainExecutable,
		// Token: 0x040010CB RID: 4299
		GameFileMismatch,
		// Token: 0x040010CC RID: 4300
		RequiredGameFileNotFound,
		// Token: 0x040010CD RID: 4301
		UnknownGameFileForbidden,
		// Token: 0x040010CE RID: 4302
		SystemFileUntrusted,
		// Token: 0x040010CF RID: 4303
		ForbiddenModuleLoaded,
		// Token: 0x040010D0 RID: 4304
		CorruptedMemory,
		// Token: 0x040010D1 RID: 4305
		ForbiddenToolDetected,
		// Token: 0x040010D2 RID: 4306
		InternalAntiCheatViolation,
		// Token: 0x040010D3 RID: 4307
		CorruptedNetworkMessageFlow,
		// Token: 0x040010D4 RID: 4308
		VirtualMachineNotAllowed,
		// Token: 0x040010D5 RID: 4309
		ForbiddenSystemConfiguration
	}
}
