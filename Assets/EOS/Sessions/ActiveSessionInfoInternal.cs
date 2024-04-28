using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000AD RID: 173
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionInfoInternal : ISettable, IDisposable
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00006F84 File Offset: 0x00005184
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00006FA0 File Offset: 0x000051A0
		public string SessionName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_SessionName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00006FB0 File Offset: 0x000051B0
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00006FCC File Offset: 0x000051CC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00006FDB File Offset: 0x000051DB
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00006FE3 File Offset: 0x000051E3
		public OnlineSessionState State
		{
			get
			{
				return this.m_State;
			}
			set
			{
				this.m_State = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00006FEC File Offset: 0x000051EC
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00007008 File Offset: 0x00005208
		public SessionDetailsInfo SessionDetails
		{
			get
			{
				SessionDetailsInfo result;
				Helper.TryMarshalGet<SessionDetailsInfoInternal, SessionDetailsInfo>(this.m_SessionDetails, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<SessionDetailsInfoInternal, SessionDetailsInfo>(ref this.m_SessionDetails, value);
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00007017 File Offset: 0x00005217
		public void Set(ActiveSessionInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
				this.LocalUserId = other.LocalUserId;
				this.State = other.State;
				this.SessionDetails = other.SessionDetails;
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00007053 File Offset: 0x00005253
		public void Set(object other)
		{
			this.Set(other as ActiveSessionInfo);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00007061 File Offset: 0x00005261
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_SessionDetails);
		}

		// Token: 0x04000301 RID: 769
		private int m_ApiVersion;

		// Token: 0x04000302 RID: 770
		private IntPtr m_SessionName;

		// Token: 0x04000303 RID: 771
		private IntPtr m_LocalUserId;

		// Token: 0x04000304 RID: 772
		private OnlineSessionState m_State;

		// Token: 0x04000305 RID: 773
		private IntPtr m_SessionDetails;
	}
}
