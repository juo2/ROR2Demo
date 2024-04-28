using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000113 RID: 275
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsInfoInternal : ISettable, IDisposable
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000087D8 File Offset: 0x000069D8
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x000087F4 File Offset: 0x000069F4
		public string SessionId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_SessionId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionId, value);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00008804 File Offset: 0x00006A04
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00008820 File Offset: 0x00006A20
		public string HostAddress
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_HostAddress, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_HostAddress, value);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0000882F File Offset: 0x00006A2F
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00008837 File Offset: 0x00006A37
		public uint NumOpenPublicConnections
		{
			get
			{
				return this.m_NumOpenPublicConnections;
			}
			set
			{
				this.m_NumOpenPublicConnections = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00008840 File Offset: 0x00006A40
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0000885C File Offset: 0x00006A5C
		public SessionDetailsSettings Settings
		{
			get
			{
				SessionDetailsSettings result;
				Helper.TryMarshalGet<SessionDetailsSettingsInternal, SessionDetailsSettings>(this.m_Settings, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<SessionDetailsSettingsInternal, SessionDetailsSettings>(ref this.m_Settings, value);
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0000886B File Offset: 0x00006A6B
		public void Set(SessionDetailsInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionId = other.SessionId;
				this.HostAddress = other.HostAddress;
				this.NumOpenPublicConnections = other.NumOpenPublicConnections;
				this.Settings = other.Settings;
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000088A7 File Offset: 0x00006AA7
		public void Set(object other)
		{
			this.Set(other as SessionDetailsInfo);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000088B5 File Offset: 0x00006AB5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionId);
			Helper.TryMarshalDispose(ref this.m_HostAddress);
			Helper.TryMarshalDispose(ref this.m_Settings);
		}

		// Token: 0x040003BC RID: 956
		private int m_ApiVersion;

		// Token: 0x040003BD RID: 957
		private IntPtr m_SessionId;

		// Token: 0x040003BE RID: 958
		private IntPtr m_HostAddress;

		// Token: 0x040003BF RID: 959
		private uint m_NumOpenPublicConnections;

		// Token: 0x040003C0 RID: 960
		private IntPtr m_Settings;
	}
}
