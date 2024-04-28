using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000331 RID: 817
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyMemberAttributeByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000602 RID: 1538
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x000159AD File Offset: 0x00013BAD
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x000159BC File Offset: 0x00013BBC
		public uint AttrIndex
		{
			set
			{
				this.m_AttrIndex = value;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000159C5 File Offset: 0x00013BC5
		public void Set(LobbyDetailsCopyMemberAttributeByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.AttrIndex = other.AttrIndex;
			}
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000159E9 File Offset: 0x00013BE9
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsCopyMemberAttributeByIndexOptions);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x000159F7 File Offset: 0x00013BF7
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x040009B0 RID: 2480
		private int m_ApiVersion;

		// Token: 0x040009B1 RID: 2481
		private IntPtr m_TargetUserId;

		// Token: 0x040009B2 RID: 2482
		private uint m_AttrIndex;
	}
}
