using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000112 RID: 274
	public class SessionDetailsInfo : ISettable
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00008714 File Offset: 0x00006914
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0000871C File Offset: 0x0000691C
		public string SessionId { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00008725 File Offset: 0x00006925
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0000872D File Offset: 0x0000692D
		public string HostAddress { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00008736 File Offset: 0x00006936
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0000873E File Offset: 0x0000693E
		public uint NumOpenPublicConnections { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00008747 File Offset: 0x00006947
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0000874F File Offset: 0x0000694F
		public SessionDetailsSettings Settings { get; set; }

		// Token: 0x060007D9 RID: 2009 RVA: 0x00008758 File Offset: 0x00006958
		internal void Set(SessionDetailsInfoInternal? other)
		{
			if (other != null)
			{
				this.SessionId = other.Value.SessionId;
				this.HostAddress = other.Value.HostAddress;
				this.NumOpenPublicConnections = other.Value.NumOpenPublicConnections;
				this.Settings = other.Value.Settings;
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000087C2 File Offset: 0x000069C2
		public void Set(object other)
		{
			this.Set(other as SessionDetailsInfoInternal?);
		}
	}
}
