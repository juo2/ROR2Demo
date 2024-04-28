using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000066 RID: 102
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataByFilenameOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000095 RID: 149
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00005344 File Offset: 0x00003544
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00005353 File Offset: 0x00003553
		public string Filename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00005362 File Offset: 0x00003562
		public void Set(CopyFileMetadataByFilenameOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Filename = other.Filename;
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00005386 File Offset: 0x00003586
		public void Set(object other)
		{
			this.Set(other as CopyFileMetadataByFilenameOptions);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00005394 File Offset: 0x00003594
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Filename);
		}

		// Token: 0x04000240 RID: 576
		private int m_ApiVersion;

		// Token: 0x04000241 RID: 577
		private IntPtr m_LocalUserId;

		// Token: 0x04000242 RID: 578
		private IntPtr m_Filename;
	}
}
