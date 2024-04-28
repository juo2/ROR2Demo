using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000082 RID: 130
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170000C7 RID: 199
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00005A8E File Offset: 0x00003C8E
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00005A9D File Offset: 0x00003C9D
		public string[] ListOfTags
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ListOfTags, value, out this.m_ListOfTagsCount);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00005AB2 File Offset: 0x00003CB2
		public void Set(QueryFileListOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ListOfTags = other.ListOfTags;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00005AD6 File Offset: 0x00003CD6
		public void Set(object other)
		{
			this.Set(other as QueryFileListOptions);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ListOfTags);
		}

		// Token: 0x04000272 RID: 626
		private int m_ApiVersion;

		// Token: 0x04000273 RID: 627
		private IntPtr m_LocalUserId;

		// Token: 0x04000274 RID: 628
		private IntPtr m_ListOfTags;

		// Token: 0x04000275 RID: 629
		private uint m_ListOfTagsCount;
	}
}
