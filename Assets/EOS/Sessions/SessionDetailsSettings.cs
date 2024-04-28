using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000114 RID: 276
	public class SessionDetailsSettings : ISettable
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x000088DB File Offset: 0x00006ADB
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x000088E3 File Offset: 0x00006AE3
		public string BucketId { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x000088EC File Offset: 0x00006AEC
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x000088F4 File Offset: 0x00006AF4
		public uint NumPublicConnections { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000088FD File Offset: 0x00006AFD
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x00008905 File Offset: 0x00006B05
		public bool AllowJoinInProgress { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0000890E File Offset: 0x00006B0E
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00008916 File Offset: 0x00006B16
		public OnlineSessionPermissionLevel PermissionLevel { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0000891F File Offset: 0x00006B1F
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x00008927 File Offset: 0x00006B27
		public bool InvitesAllowed { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00008930 File Offset: 0x00006B30
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00008938 File Offset: 0x00006B38
		public bool SanctionsEnabled { get; set; }

		// Token: 0x060007F3 RID: 2035 RVA: 0x00008944 File Offset: 0x00006B44
		internal void Set(SessionDetailsSettingsInternal? other)
		{
			if (other != null)
			{
				this.BucketId = other.Value.BucketId;
				this.NumPublicConnections = other.Value.NumPublicConnections;
				this.AllowJoinInProgress = other.Value.AllowJoinInProgress;
				this.PermissionLevel = other.Value.PermissionLevel;
				this.InvitesAllowed = other.Value.InvitesAllowed;
				this.SanctionsEnabled = other.Value.SanctionsEnabled;
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000089D8 File Offset: 0x00006BD8
		public void Set(object other)
		{
			this.Set(other as SessionDetailsSettingsInternal?);
		}
	}
}
