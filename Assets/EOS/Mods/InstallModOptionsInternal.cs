using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C5 RID: 709
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InstallModOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000534 RID: 1332
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x000131E7 File Offset: 0x000113E7
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x000131F6 File Offset: 0x000113F6
		public ModIdentifier Mod
		{
			set
			{
				Helper.TryMarshalSet<ModIdentifierInternal, ModIdentifier>(ref this.m_Mod, value);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x00013205 File Offset: 0x00011405
		public bool RemoveAfterExit
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RemoveAfterExit, value);
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00013214 File Offset: 0x00011414
		public void Set(InstallModOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Mod = other.Mod;
				this.RemoveAfterExit = other.RemoveAfterExit;
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00013244 File Offset: 0x00011444
		public void Set(object other)
		{
			this.Set(other as InstallModOptions);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00013252 File Offset: 0x00011452
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Mod);
		}

		// Token: 0x04000878 RID: 2168
		private int m_ApiVersion;

		// Token: 0x04000879 RID: 2169
		private IntPtr m_LocalUserId;

		// Token: 0x0400087A RID: 2170
		private IntPtr m_Mod;

		// Token: 0x0400087B RID: 2171
		private int m_RemoveAfterExit;
	}
}
