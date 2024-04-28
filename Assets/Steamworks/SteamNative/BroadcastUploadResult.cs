using System;

namespace SteamNative
{
	// Token: 0x0200001A RID: 26
	internal enum BroadcastUploadResult
	{
		// Token: 0x04000174 RID: 372
		None,
		// Token: 0x04000175 RID: 373
		OK,
		// Token: 0x04000176 RID: 374
		InitFailed,
		// Token: 0x04000177 RID: 375
		FrameFailed,
		// Token: 0x04000178 RID: 376
		Timeout,
		// Token: 0x04000179 RID: 377
		BandwidthExceeded,
		// Token: 0x0400017A RID: 378
		LowFPS,
		// Token: 0x0400017B RID: 379
		MissingKeyFrames,
		// Token: 0x0400017C RID: 380
		NoConnection,
		// Token: 0x0400017D RID: 381
		RelayFailed,
		// Token: 0x0400017E RID: 382
		SettingsChanged,
		// Token: 0x0400017F RID: 383
		MissingAudio,
		// Token: 0x04000180 RID: 384
		TooFarBehind,
		// Token: 0x04000181 RID: 385
		TranscodeBehind
	}
}
