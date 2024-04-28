using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005A8 RID: 1448
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetClientDetailsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B08 RID: 2824
		// (set) Token: 0x06002338 RID: 9016 RVA: 0x00025368 File Offset: 0x00023568
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (set) Token: 0x06002339 RID: 9017 RVA: 0x00025371 File Offset: 0x00023571
		public AntiCheatCommonClientFlags ClientFlags
		{
			set
			{
				this.m_ClientFlags = value;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x0002537A File Offset: 0x0002357A
		public AntiCheatCommonClientInput ClientInputMethod
		{
			set
			{
				this.m_ClientInputMethod = value;
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x00025383 File Offset: 0x00023583
		public void Set(SetClientDetailsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.ClientHandle;
				this.ClientFlags = other.ClientFlags;
				this.ClientInputMethod = other.ClientInputMethod;
			}
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000253B3 File Offset: 0x000235B3
		public void Set(object other)
		{
			this.Set(other as SetClientDetailsOptions);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000253C1 File Offset: 0x000235C1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientHandle);
		}

		// Token: 0x0400109D RID: 4253
		private int m_ApiVersion;

		// Token: 0x0400109E RID: 4254
		private IntPtr m_ClientHandle;

		// Token: 0x0400109F RID: 4255
		private AntiCheatCommonClientFlags m_ClientFlags;

		// Token: 0x040010A0 RID: 4256
		private AntiCheatCommonClientInput m_ClientInputMethod;
	}
}
