using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000091 RID: 145
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyStatByNameOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170000F3 RID: 243
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00006417 File Offset: 0x00004617
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00006426 File Offset: 0x00004626
		public string Name
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Name, value);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00006435 File Offset: 0x00004635
		public void Set(CopyStatByNameOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.Name = other.Name;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00006459 File Offset: 0x00004659
		public void Set(object other)
		{
			this.Set(other as CopyStatByNameOptions);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00006467 File Offset: 0x00004667
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_Name);
		}

		// Token: 0x040002B1 RID: 689
		private int m_ApiVersion;

		// Token: 0x040002B2 RID: 690
		private IntPtr m_TargetUserId;

		// Token: 0x040002B3 RID: 691
		private IntPtr m_Name;
	}
}
