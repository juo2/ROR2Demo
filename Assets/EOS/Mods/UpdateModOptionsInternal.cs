using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002DB RID: 731
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateModOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700055D RID: 1373
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x00013B0A File Offset: 0x00011D0A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700055E RID: 1374
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00013B19 File Offset: 0x00011D19
		public ModIdentifier Mod
		{
			set
			{
				Helper.TryMarshalSet<ModIdentifierInternal, ModIdentifier>(ref this.m_Mod, value);
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00013B28 File Offset: 0x00011D28
		public void Set(UpdateModOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Mod = other.Mod;
			}
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00013B4C File Offset: 0x00011D4C
		public void Set(object other)
		{
			this.Set(other as UpdateModOptions);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00013B5A File Offset: 0x00011D5A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Mod);
		}

		// Token: 0x040008AE RID: 2222
		private int m_ApiVersion;

		// Token: 0x040008AF RID: 2223
		private IntPtr m_LocalUserId;

		// Token: 0x040008B0 RID: 2224
		private IntPtr m_Mod;
	}
}
