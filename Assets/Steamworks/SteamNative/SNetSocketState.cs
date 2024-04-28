using System;

namespace SteamNative
{
	// Token: 0x0200003D RID: 61
	internal enum SNetSocketState
	{
		// Token: 0x04000277 RID: 631
		Invalid,
		// Token: 0x04000278 RID: 632
		Connected,
		// Token: 0x04000279 RID: 633
		Initiated = 10,
		// Token: 0x0400027A RID: 634
		LocalCandidatesFound,
		// Token: 0x0400027B RID: 635
		ReceivedRemoteCandidates,
		// Token: 0x0400027C RID: 636
		ChallengeHandshake = 15,
		// Token: 0x0400027D RID: 637
		Disconnecting = 21,
		// Token: 0x0400027E RID: 638
		LocalDisconnect,
		// Token: 0x0400027F RID: 639
		TimeoutDuringConnect,
		// Token: 0x04000280 RID: 640
		RemoteEndDisconnected,
		// Token: 0x04000281 RID: 641
		ConnectionBroken
	}
}
