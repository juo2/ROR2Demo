using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002CA RID: 714
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ModInfoInternal : ISettable, IDisposable
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0001354C File Offset: 0x0001174C
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x0001356E File Offset: 0x0001176E
		public ModIdentifier[] Mods
		{
			get
			{
				ModIdentifier[] result;
				Helper.TryMarshalGet<ModIdentifierInternal, ModIdentifier>(this.m_Mods, out result, this.m_ModsCount);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<ModIdentifierInternal, ModIdentifier>(ref this.m_Mods, value, out this.m_ModsCount);
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00013583 File Offset: 0x00011783
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x0001358B File Offset: 0x0001178B
		public ModEnumerationType Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00013594 File Offset: 0x00011794
		public void Set(ModInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Mods = other.Mods;
				this.Type = other.Type;
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000135B8 File Offset: 0x000117B8
		public void Set(object other)
		{
			this.Set(other as ModInfo);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x000135C6 File Offset: 0x000117C6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Mods);
		}

		// Token: 0x0400088C RID: 2188
		private int m_ApiVersion;

		// Token: 0x0400088D RID: 2189
		private int m_ModsCount;

		// Token: 0x0400088E RID: 2190
		private IntPtr m_Mods;

		// Token: 0x0400088F RID: 2191
		private ModEnumerationType m_Type;
	}
}
