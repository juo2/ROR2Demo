using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034E RID: 846
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationAddMemberAttributeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000648 RID: 1608
		// (set) Token: 0x0600152E RID: 5422 RVA: 0x0001714C File Offset: 0x0001534C
		public AttributeData Attribute
		{
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal, AttributeData>(ref this.m_Attribute, value);
			}
		}

		// Token: 0x17000649 RID: 1609
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x0001715B File Offset: 0x0001535B
		public LobbyAttributeVisibility Visibility
		{
			set
			{
				this.m_Visibility = value;
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00017164 File Offset: 0x00015364
		public void Set(LobbyModificationAddMemberAttributeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Attribute = other.Attribute;
				this.Visibility = other.Visibility;
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00017188 File Offset: 0x00015388
		public void Set(object other)
		{
			this.Set(other as LobbyModificationAddMemberAttributeOptions);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00017196 File Offset: 0x00015396
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Attribute);
		}

		// Token: 0x04000A32 RID: 2610
		private int m_ApiVersion;

		// Token: 0x04000A33 RID: 2611
		private IntPtr m_Attribute;

		// Token: 0x04000A34 RID: 2612
		private LobbyAttributeVisibility m_Visibility;
	}
}
