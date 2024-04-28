using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200058E RID: 1422
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerReviveOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A8A RID: 2698
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x00024380 File Offset: 0x00022580
		public IntPtr RevivedPlayerHandle
		{
			set
			{
				this.m_RevivedPlayerHandle = value;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (set) Token: 0x06002237 RID: 8759 RVA: 0x00024389 File Offset: 0x00022589
		public IntPtr ReviverPlayerHandle
		{
			set
			{
				this.m_ReviverPlayerHandle = value;
			}
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x00024392 File Offset: 0x00022592
		public void Set(LogPlayerReviveOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.RevivedPlayerHandle = other.RevivedPlayerHandle;
				this.ReviverPlayerHandle = other.ReviverPlayerHandle;
			}
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000243B6 File Offset: 0x000225B6
		public void Set(object other)
		{
			this.Set(other as LogPlayerReviveOptions);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000243C4 File Offset: 0x000225C4
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_RevivedPlayerHandle);
			Helper.TryMarshalDispose(ref this.m_ReviverPlayerHandle);
		}

		// Token: 0x04001019 RID: 4121
		private int m_ApiVersion;

		// Token: 0x0400101A RID: 4122
		private IntPtr m_RevivedPlayerHandle;

		// Token: 0x0400101B RID: 4123
		private IntPtr m_ReviverPlayerHandle;
	}
}
