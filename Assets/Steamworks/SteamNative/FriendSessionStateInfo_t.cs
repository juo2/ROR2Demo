using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x0200007B RID: 123
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FriendSessionStateInfo_t
	{
		// Token: 0x06000378 RID: 888 RVA: 0x0000A6D9 File Offset: 0x000088D9
		internal static FriendSessionStateInfo_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FriendSessionStateInfo_t.PackSmall)Marshal.PtrToStructure(p, typeof(FriendSessionStateInfo_t.PackSmall));
			}
			return (FriendSessionStateInfo_t)Marshal.PtrToStructure(p, typeof(FriendSessionStateInfo_t));
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000A712 File Offset: 0x00008912
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FriendSessionStateInfo_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FriendSessionStateInfo_t));
		}

		// Token: 0x040004B5 RID: 1205
		internal uint IOnlineSessionInstances;

		// Token: 0x040004B6 RID: 1206
		internal byte IPublishedToFriendsSessionInstance;

		// Token: 0x0200019F RID: 415
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CDB RID: 7387 RVA: 0x00061268 File Offset: 0x0005F468
			public static implicit operator FriendSessionStateInfo_t(FriendSessionStateInfo_t.PackSmall d)
			{
				return new FriendSessionStateInfo_t
				{
					IOnlineSessionInstances = d.IOnlineSessionInstances,
					IPublishedToFriendsSessionInstance = d.IPublishedToFriendsSessionInstance
				};
			}

			// Token: 0x0400095A RID: 2394
			internal uint IOnlineSessionInstances;

			// Token: 0x0400095B RID: 2395
			internal byte IPublishedToFriendsSessionInstance;
		}
	}
}
