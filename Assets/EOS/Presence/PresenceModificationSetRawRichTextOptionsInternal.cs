using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000228 RID: 552
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetRawRichTextOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003E5 RID: 997
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x0000F894 File Offset: 0x0000DA94
		public string RichText
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RichText, value);
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0000F8A3 File Offset: 0x0000DAA3
		public void Set(PresenceModificationSetRawRichTextOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.RichText = other.RichText;
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0000F8BB File Offset: 0x0000DABB
		public void Set(object other)
		{
			this.Set(other as PresenceModificationSetRawRichTextOptions);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0000F8C9 File Offset: 0x0000DAC9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_RichText);
		}

		// Token: 0x040006C2 RID: 1730
		private int m_ApiVersion;

		// Token: 0x040006C3 RID: 1731
		private IntPtr m_RichText;
	}
}
