using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C1 RID: 705
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EnumerateModsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000526 RID: 1318
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x00013026 File Offset: 0x00011226
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x00013035 File Offset: 0x00011235
		public ModEnumerationType Type
		{
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0001303E File Offset: 0x0001123E
		public void Set(EnumerateModsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Type = other.Type;
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00013062 File Offset: 0x00011262
		public void Set(object other)
		{
			this.Set(other as EnumerateModsOptions);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00013070 File Offset: 0x00011270
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400086A RID: 2154
		private int m_ApiVersion;

		// Token: 0x0400086B RID: 2155
		private IntPtr m_LocalUserId;

		// Token: 0x0400086C RID: 2156
		private ModEnumerationType m_Type;
	}
}
