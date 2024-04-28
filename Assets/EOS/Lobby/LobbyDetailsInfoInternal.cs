using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033F RID: 831
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsInfoInternal : ISettable, IDisposable
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x00015CF8 File Offset: 0x00013EF8
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00015D14 File Offset: 0x00013F14
		public string LobbyId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LobbyId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x00015D24 File Offset: 0x00013F24
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00015D40 File Offset: 0x00013F40
		public ProductUserId LobbyOwnerUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LobbyOwnerUserId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyOwnerUserId, value);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00015D4F File Offset: 0x00013F4F
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x00015D57 File Offset: 0x00013F57
		public LobbyPermissionLevel PermissionLevel
		{
			get
			{
				return this.m_PermissionLevel;
			}
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x00015D60 File Offset: 0x00013F60
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x00015D68 File Offset: 0x00013F68
		public uint AvailableSlots
		{
			get
			{
				return this.m_AvailableSlots;
			}
			set
			{
				this.m_AvailableSlots = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00015D71 File Offset: 0x00013F71
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x00015D79 File Offset: 0x00013F79
		public uint MaxMembers
		{
			get
			{
				return this.m_MaxMembers;
			}
			set
			{
				this.m_MaxMembers = value;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x00015D84 File Offset: 0x00013F84
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x00015DA0 File Offset: 0x00013FA0
		public bool AllowInvites
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_AllowInvites, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowInvites, value);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x00015DB0 File Offset: 0x00013FB0
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x00015DCC File Offset: 0x00013FCC
		public string BucketId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_BucketId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_BucketId, value);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00015DDC File Offset: 0x00013FDC
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x00015DF8 File Offset: 0x00013FF8
		public bool AllowHostMigration
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_AllowHostMigration, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowHostMigration, value);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00015E08 File Offset: 0x00014008
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x00015E24 File Offset: 0x00014024
		public bool RTCRoomEnabled
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_RTCRoomEnabled, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_RTCRoomEnabled, value);
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00015E34 File Offset: 0x00014034
		public void Set(LobbyDetailsInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
				this.LobbyOwnerUserId = other.LobbyOwnerUserId;
				this.PermissionLevel = other.PermissionLevel;
				this.AvailableSlots = other.AvailableSlots;
				this.MaxMembers = other.MaxMembers;
				this.AllowInvites = other.AllowInvites;
				this.BucketId = other.BucketId;
				this.AllowHostMigration = other.AllowHostMigration;
				this.RTCRoomEnabled = other.RTCRoomEnabled;
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00015EB7 File Offset: 0x000140B7
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsInfo);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00015EC5 File Offset: 0x000140C5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
			Helper.TryMarshalDispose(ref this.m_LobbyOwnerUserId);
			Helper.TryMarshalDispose(ref this.m_BucketId);
		}

		// Token: 0x040009CA RID: 2506
		private int m_ApiVersion;

		// Token: 0x040009CB RID: 2507
		private IntPtr m_LobbyId;

		// Token: 0x040009CC RID: 2508
		private IntPtr m_LobbyOwnerUserId;

		// Token: 0x040009CD RID: 2509
		private LobbyPermissionLevel m_PermissionLevel;

		// Token: 0x040009CE RID: 2510
		private uint m_AvailableSlots;

		// Token: 0x040009CF RID: 2511
		private uint m_MaxMembers;

		// Token: 0x040009D0 RID: 2512
		private int m_AllowInvites;

		// Token: 0x040009D1 RID: 2513
		private IntPtr m_BucketId;

		// Token: 0x040009D2 RID: 2514
		private int m_AllowHostMigration;

		// Token: 0x040009D3 RID: 2515
		private int m_RTCRoomEnabled;
	}
}
