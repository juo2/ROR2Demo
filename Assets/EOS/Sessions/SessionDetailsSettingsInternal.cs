using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000115 RID: 277
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsSettingsInternal : ISettable, IDisposable
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000089EC File Offset: 0x00006BEC
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00008A08 File Offset: 0x00006C08
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

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00008A17 File Offset: 0x00006C17
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x00008A1F File Offset: 0x00006C1F
		public uint NumPublicConnections
		{
			get
			{
				return this.m_NumPublicConnections;
			}
			set
			{
				this.m_NumPublicConnections = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00008A28 File Offset: 0x00006C28
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00008A44 File Offset: 0x00006C44
		public bool AllowJoinInProgress
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_AllowJoinInProgress, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowJoinInProgress, value);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00008A53 File Offset: 0x00006C53
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00008A5B File Offset: 0x00006C5B
		public OnlineSessionPermissionLevel PermissionLevel
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

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00008A64 File Offset: 0x00006C64
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00008A80 File Offset: 0x00006C80
		public bool InvitesAllowed
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_InvitesAllowed, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_InvitesAllowed, value);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00008A90 File Offset: 0x00006C90
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00008AAC File Offset: 0x00006CAC
		public bool SanctionsEnabled
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_SanctionsEnabled, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_SanctionsEnabled, value);
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00008ABC File Offset: 0x00006CBC
		public void Set(SessionDetailsSettings other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.BucketId = other.BucketId;
				this.NumPublicConnections = other.NumPublicConnections;
				this.AllowJoinInProgress = other.AllowJoinInProgress;
				this.PermissionLevel = other.PermissionLevel;
				this.InvitesAllowed = other.InvitesAllowed;
				this.SanctionsEnabled = other.SanctionsEnabled;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00008B1B File Offset: 0x00006D1B
		public void Set(object other)
		{
			this.Set(other as SessionDetailsSettings);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00008B29 File Offset: 0x00006D29
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_BucketId);
		}

		// Token: 0x040003C7 RID: 967
		private int m_ApiVersion;

		// Token: 0x040003C8 RID: 968
		private IntPtr m_BucketId;

		// Token: 0x040003C9 RID: 969
		private uint m_NumPublicConnections;

		// Token: 0x040003CA RID: 970
		private int m_AllowJoinInProgress;

		// Token: 0x040003CB RID: 971
		private OnlineSessionPermissionLevel m_PermissionLevel;

		// Token: 0x040003CC RID: 972
		private int m_InvitesAllowed;

		// Token: 0x040003CD RID: 973
		private int m_SanctionsEnabled;
	}
}
