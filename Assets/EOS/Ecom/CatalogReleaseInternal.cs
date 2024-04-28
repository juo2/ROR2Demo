using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000436 RID: 1078
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogReleaseInternal : ISettable, IDisposable
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x0001BC18 File Offset: 0x00019E18
		// (set) Token: 0x06001A4E RID: 6734 RVA: 0x0001BC3B File Offset: 0x00019E3B
		public string[] CompatibleAppIds
		{
			get
			{
				string[] result;
				Helper.TryMarshalGet<string>(this.m_CompatibleAppIds, out result, this.m_CompatibleAppIdCount, true);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CompatibleAppIds, value, out this.m_CompatibleAppIdCount, true);
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0001BC54 File Offset: 0x00019E54
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x0001BC77 File Offset: 0x00019E77
		public string[] CompatiblePlatforms
		{
			get
			{
				string[] result;
				Helper.TryMarshalGet<string>(this.m_CompatiblePlatforms, out result, this.m_CompatiblePlatformCount, true);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CompatiblePlatforms, value, out this.m_CompatiblePlatformCount, true);
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0001BC90 File Offset: 0x00019E90
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x0001BCAC File Offset: 0x00019EAC
		public string ReleaseNote
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ReleaseNote, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ReleaseNote, value);
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0001BCBB File Offset: 0x00019EBB
		public void Set(CatalogRelease other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.CompatibleAppIds = other.CompatibleAppIds;
				this.CompatiblePlatforms = other.CompatiblePlatforms;
				this.ReleaseNote = other.ReleaseNote;
			}
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0001BCEB File Offset: 0x00019EEB
		public void Set(object other)
		{
			this.Set(other as CatalogRelease);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0001BCF9 File Offset: 0x00019EF9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_CompatibleAppIds);
			Helper.TryMarshalDispose(ref this.m_CompatiblePlatforms);
			Helper.TryMarshalDispose(ref this.m_ReleaseNote);
		}

		// Token: 0x04000C33 RID: 3123
		private int m_ApiVersion;

		// Token: 0x04000C34 RID: 3124
		private uint m_CompatibleAppIdCount;

		// Token: 0x04000C35 RID: 3125
		private IntPtr m_CompatibleAppIds;

		// Token: 0x04000C36 RID: 3126
		private uint m_CompatiblePlatformCount;

		// Token: 0x04000C37 RID: 3127
		private IntPtr m_CompatiblePlatforms;

		// Token: 0x04000C38 RID: 3128
		private IntPtr m_ReleaseNote;
	}
}
