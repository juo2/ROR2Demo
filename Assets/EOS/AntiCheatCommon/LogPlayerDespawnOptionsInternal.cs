using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200058C RID: 1420
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerDespawnOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A87 RID: 2695
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x00024321 File Offset: 0x00022521
		public IntPtr DespawnedPlayerHandle
		{
			set
			{
				this.m_DespawnedPlayerHandle = value;
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0002432A File Offset: 0x0002252A
		public void Set(LogPlayerDespawnOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DespawnedPlayerHandle = other.DespawnedPlayerHandle;
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00024342 File Offset: 0x00022542
		public void Set(object other)
		{
			this.Set(other as LogPlayerDespawnOptions);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00024350 File Offset: 0x00022550
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_DespawnedPlayerHandle);
		}

		// Token: 0x04001015 RID: 4117
		private int m_ApiVersion;

		// Token: 0x04001016 RID: 4118
		private IntPtr m_DespawnedPlayerHandle;
	}
}
