using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200030B RID: 779
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005A9 RID: 1449
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x00014B56 File Offset: 0x00012D56
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (set) Token: 0x06001376 RID: 4982 RVA: 0x00014B65 File Offset: 0x00012D65
		public uint MaxLobbyMembers
		{
			set
			{
				this.m_MaxLobbyMembers = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x00014B6E File Offset: 0x00012D6E
		public LobbyPermissionLevel PermissionLevel
		{
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (set) Token: 0x06001378 RID: 4984 RVA: 0x00014B77 File Offset: 0x00012D77
		public bool PresenceEnabled
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceEnabled, value);
			}
		}

		// Token: 0x170005AD RID: 1453
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x00014B86 File Offset: 0x00012D86
		public bool AllowInvites
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowInvites, value);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x00014B95 File Offset: 0x00012D95
		public string BucketId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_BucketId, value);
			}
		}

		// Token: 0x170005AF RID: 1455
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x00014BA4 File Offset: 0x00012DA4
		public bool DisableHostMigration
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DisableHostMigration, value);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x00014BB3 File Offset: 0x00012DB3
		public bool EnableRTCRoom
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EnableRTCRoom, value);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x00014BC2 File Offset: 0x00012DC2
		public LocalRTCOptions LocalRTCOptions
		{
			set
			{
				Helper.TryMarshalSet<LocalRTCOptionsInternal, LocalRTCOptions>(ref this.m_LocalRTCOptions, value);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x00014BD1 File Offset: 0x00012DD1
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00014BE0 File Offset: 0x00012DE0
		public void Set(CreateLobbyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 7;
				this.LocalUserId = other.LocalUserId;
				this.MaxLobbyMembers = other.MaxLobbyMembers;
				this.PermissionLevel = other.PermissionLevel;
				this.PresenceEnabled = other.PresenceEnabled;
				this.AllowInvites = other.AllowInvites;
				this.BucketId = other.BucketId;
				this.DisableHostMigration = other.DisableHostMigration;
				this.EnableRTCRoom = other.EnableRTCRoom;
				this.LocalRTCOptions = other.LocalRTCOptions;
				this.LobbyId = other.LobbyId;
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00014C6F File Offset: 0x00012E6F
		public void Set(object other)
		{
			this.Set(other as CreateLobbyOptions);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00014C7D File Offset: 0x00012E7D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_BucketId);
			Helper.TryMarshalDispose(ref this.m_LocalRTCOptions);
			Helper.TryMarshalDispose(ref this.m_LobbyId);
		}

		// Token: 0x04000941 RID: 2369
		private int m_ApiVersion;

		// Token: 0x04000942 RID: 2370
		private IntPtr m_LocalUserId;

		// Token: 0x04000943 RID: 2371
		private uint m_MaxLobbyMembers;

		// Token: 0x04000944 RID: 2372
		private LobbyPermissionLevel m_PermissionLevel;

		// Token: 0x04000945 RID: 2373
		private int m_PresenceEnabled;

		// Token: 0x04000946 RID: 2374
		private int m_AllowInvites;

		// Token: 0x04000947 RID: 2375
		private IntPtr m_BucketId;

		// Token: 0x04000948 RID: 2376
		private int m_DisableHostMigration;

		// Token: 0x04000949 RID: 2377
		private int m_EnableRTCRoom;

		// Token: 0x0400094A RID: 2378
		private IntPtr m_LocalRTCOptions;

		// Token: 0x0400094B RID: 2379
		private IntPtr m_LobbyId;
	}
}
