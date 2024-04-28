using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x020002DD RID: 733
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginPlayerSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000564 RID: 1380
		// (set) Token: 0x06001292 RID: 4754 RVA: 0x00013BC9 File Offset: 0x00011DC9
		public BeginPlayerSessionOptionsAccountId AccountId
		{
			set
			{
				Helper.TryMarshalSet<BeginPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId, value);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x00013BD8 File Offset: 0x00011DD8
		public string DisplayName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x17000566 RID: 1382
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x00013BE7 File Offset: 0x00011DE7
		public UserControllerType ControllerType
		{
			set
			{
				this.m_ControllerType = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public string ServerIp
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ServerIp, value);
			}
		}

		// Token: 0x17000568 RID: 1384
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x00013BFF File Offset: 0x00011DFF
		public string GameSessionId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_GameSessionId, value);
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00013C10 File Offset: 0x00011E10
		public void Set(BeginPlayerSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AccountId = other.AccountId;
				this.DisplayName = other.DisplayName;
				this.ControllerType = other.ControllerType;
				this.ServerIp = other.ServerIp;
				this.GameSessionId = other.GameSessionId;
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00013C63 File Offset: 0x00011E63
		public void Set(object other)
		{
			this.Set(other as BeginPlayerSessionOptions);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00013C71 File Offset: 0x00011E71
		public void Dispose()
		{
			Helper.TryMarshalDispose<BeginPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId);
			Helper.TryMarshalDispose(ref this.m_DisplayName);
			Helper.TryMarshalDispose(ref this.m_ServerIp);
			Helper.TryMarshalDispose(ref this.m_GameSessionId);
		}

		// Token: 0x040008B6 RID: 2230
		private int m_ApiVersion;

		// Token: 0x040008B7 RID: 2231
		private BeginPlayerSessionOptionsAccountIdInternal m_AccountId;

		// Token: 0x040008B8 RID: 2232
		private IntPtr m_DisplayName;

		// Token: 0x040008B9 RID: 2233
		private UserControllerType m_ControllerType;

		// Token: 0x040008BA RID: 2234
		private IntPtr m_ServerIp;

		// Token: 0x040008BB RID: 2235
		private IntPtr m_GameSessionId;
	}
}
