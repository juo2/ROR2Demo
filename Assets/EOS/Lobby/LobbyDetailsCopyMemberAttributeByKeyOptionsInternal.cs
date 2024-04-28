using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000333 RID: 819
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyMemberAttributeByKeyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000606 RID: 1542
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x00015A27 File Offset: 0x00013C27
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000607 RID: 1543
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x00015A36 File Offset: 0x00013C36
		public string AttrKey
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AttrKey, value);
			}
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00015A45 File Offset: 0x00013C45
		public void Set(LobbyDetailsCopyMemberAttributeByKeyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.AttrKey = other.AttrKey;
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00015A69 File Offset: 0x00013C69
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsCopyMemberAttributeByKeyOptions);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00015A77 File Offset: 0x00013C77
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_AttrKey);
		}

		// Token: 0x040009B5 RID: 2485
		private int m_ApiVersion;

		// Token: 0x040009B6 RID: 2486
		private IntPtr m_TargetUserId;

		// Token: 0x040009B7 RID: 2487
		private IntPtr m_AttrKey;
	}
}
