using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000288 RID: 648
	public enum ConnectionClosedReason
	{
		// Token: 0x040007BE RID: 1982
		Unknown,
		// Token: 0x040007BF RID: 1983
		ClosedByLocalUser,
		// Token: 0x040007C0 RID: 1984
		ClosedByPeer,
		// Token: 0x040007C1 RID: 1985
		TimedOut,
		// Token: 0x040007C2 RID: 1986
		TooManyConnections,
		// Token: 0x040007C3 RID: 1987
		InvalidMessage,
		// Token: 0x040007C4 RID: 1988
		InvalidData,
		// Token: 0x040007C5 RID: 1989
		ConnectionFailed,
		// Token: 0x040007C6 RID: 1990
		ConnectionClosed,
		// Token: 0x040007C7 RID: 1991
		NegotiationFailed,
		// Token: 0x040007C8 RID: 1992
		UnexpectedError
	}
}
