using System;

namespace SteamNative
{
	// Token: 0x0200000B RID: 11
	internal enum VoiceResult
	{
		// Token: 0x040000D3 RID: 211
		OK,
		// Token: 0x040000D4 RID: 212
		NotInitialized,
		// Token: 0x040000D5 RID: 213
		NotRecording,
		// Token: 0x040000D6 RID: 214
		NoData,
		// Token: 0x040000D7 RID: 215
		BufferTooSmall,
		// Token: 0x040000D8 RID: 216
		DataCorrupted,
		// Token: 0x040000D9 RID: 217
		Restricted,
		// Token: 0x040000DA RID: 218
		UnsupportedCodec,
		// Token: 0x040000DB RID: 219
		ReceiverOutOfDate,
		// Token: 0x040000DC RID: 220
		ReceiverDidNotAnswer
	}
}
