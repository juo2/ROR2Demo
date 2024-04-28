using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002BD RID: 701
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyModInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000519 RID: 1305
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00012E89 File Offset: 0x00011089
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x00012E98 File Offset: 0x00011098
		public ModEnumerationType Type
		{
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00012EA1 File Offset: 0x000110A1
		public void Set(CopyModInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Type = other.Type;
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00012EC5 File Offset: 0x000110C5
		public void Set(object other)
		{
			this.Set(other as CopyModInfoOptions);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00012ED3 File Offset: 0x000110D3
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400085D RID: 2141
		private int m_ApiVersion;

		// Token: 0x0400085E RID: 2142
		private IntPtr m_LocalUserId;

		// Token: 0x0400085F RID: 2143
		private ModEnumerationType m_Type;
	}
}
