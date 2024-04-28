using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034C RID: 844
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationAddAttributeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000644 RID: 1604
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x000170D2 File Offset: 0x000152D2
		public AttributeData Attribute
		{
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal, AttributeData>(ref this.m_Attribute, value);
			}
		}

		// Token: 0x17000645 RID: 1605
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x000170E1 File Offset: 0x000152E1
		public LobbyAttributeVisibility Visibility
		{
			set
			{
				this.m_Visibility = value;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x000170EA File Offset: 0x000152EA
		public void Set(LobbyModificationAddAttributeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Attribute = other.Attribute;
				this.Visibility = other.Visibility;
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0001710E File Offset: 0x0001530E
		public void Set(object other)
		{
			this.Set(other as LobbyModificationAddAttributeOptions);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0001711C File Offset: 0x0001531C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Attribute);
		}

		// Token: 0x04000A2D RID: 2605
		private int m_ApiVersion;

		// Token: 0x04000A2E RID: 2606
		private IntPtr m_Attribute;

		// Token: 0x04000A2F RID: 2607
		private LobbyAttributeVisibility m_Visibility;
	}
}
