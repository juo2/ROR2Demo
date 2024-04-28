using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005BB RID: 1467
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B17 RID: 2839
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x00025B26 File Offset: 0x00023D26
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x00025B35 File Offset: 0x00023D35
		public AntiCheatClientMode Mode
		{
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00025B3E File Offset: 0x00023D3E
		public void Set(BeginSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.LocalUserId;
				this.Mode = other.Mode;
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00025B62 File Offset: 0x00023D62
		public void Set(object other)
		{
			this.Set(other as BeginSessionOptions);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00025B70 File Offset: 0x00023D70
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x040010D8 RID: 4312
		private int m_ApiVersion;

		// Token: 0x040010D9 RID: 4313
		private IntPtr m_LocalUserId;

		// Token: 0x040010DA RID: 4314
		private AntiCheatClientMode m_Mode;
	}
}
