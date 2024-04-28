using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200055C RID: 1372
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A3D RID: 2621
		// (set) Token: 0x0600215A RID: 8538 RVA: 0x0002358C File Offset: 0x0002178C
		public uint RegisterTimeoutSeconds
		{
			set
			{
				this.m_RegisterTimeoutSeconds = value;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x00023595 File Offset: 0x00021795
		public string ServerName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ServerName, value);
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (set) Token: 0x0600215C RID: 8540 RVA: 0x000235A4 File Offset: 0x000217A4
		public bool EnableGameplayData
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EnableGameplayData, value);
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x000235B3 File Offset: 0x000217B3
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000235C2 File Offset: 0x000217C2
		public void Set(BeginSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.RegisterTimeoutSeconds = other.RegisterTimeoutSeconds;
				this.ServerName = other.ServerName;
				this.EnableGameplayData = other.EnableGameplayData;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000235FE File Offset: 0x000217FE
		public void Set(object other)
		{
			this.Set(other as BeginSessionOptions);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0002360C File Offset: 0x0002180C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ServerName);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000F5B RID: 3931
		private int m_ApiVersion;

		// Token: 0x04000F5C RID: 3932
		private uint m_RegisterTimeoutSeconds;

		// Token: 0x04000F5D RID: 3933
		private IntPtr m_ServerName;

		// Token: 0x04000F5E RID: 3934
		private int m_EnableGameplayData;

		// Token: 0x04000F5F RID: 3935
		private IntPtr m_LocalUserId;
	}
}
