using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200059A RID: 1434
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerUseWeaponOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000AD7 RID: 2775
		// (set) Token: 0x060022C9 RID: 8905 RVA: 0x00024C99 File Offset: 0x00022E99
		public LogPlayerUseWeaponData UseWeaponData
		{
			set
			{
				Helper.TryMarshalSet<LogPlayerUseWeaponDataInternal, LogPlayerUseWeaponData>(ref this.m_UseWeaponData, value);
			}
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x00024CA8 File Offset: 0x00022EA8
		public void Set(LogPlayerUseWeaponOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.UseWeaponData = other.UseWeaponData;
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x00024CC0 File Offset: 0x00022EC0
		public void Set(object other)
		{
			this.Set(other as LogPlayerUseWeaponOptions);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x00024CCE File Offset: 0x00022ECE
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UseWeaponData);
		}

		// Token: 0x0400106B RID: 4203
		private int m_ApiVersion;

		// Token: 0x0400106C RID: 4204
		private IntPtr m_UseWeaponData;
	}
}
