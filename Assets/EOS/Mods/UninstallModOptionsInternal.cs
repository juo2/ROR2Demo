using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002D7 RID: 727
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UninstallModOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000550 RID: 1360
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x00013946 File Offset: 0x00011B46
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x00013955 File Offset: 0x00011B55
		public ModIdentifier Mod
		{
			set
			{
				Helper.TryMarshalSet<ModIdentifierInternal, ModIdentifier>(ref this.m_Mod, value);
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00013964 File Offset: 0x00011B64
		public void Set(UninstallModOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Mod = other.Mod;
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00013988 File Offset: 0x00011B88
		public void Set(object other)
		{
			this.Set(other as UninstallModOptions);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00013996 File Offset: 0x00011B96
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Mod);
		}

		// Token: 0x040008A1 RID: 2209
		private int m_ApiVersion;

		// Token: 0x040008A2 RID: 2210
		private IntPtr m_LocalUserId;

		// Token: 0x040008A3 RID: 2211
		private IntPtr m_Mod;
	}
}
