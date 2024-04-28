using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002FD RID: 765
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeInternal : ISettable, IDisposable
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x000143D4 File Offset: 0x000125D4
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x000143F0 File Offset: 0x000125F0
		public AttributeData Data
		{
			get
			{
				AttributeData result;
				Helper.TryMarshalGet<AttributeDataInternal, AttributeData>(this.m_Data, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal, AttributeData>(ref this.m_Data, value);
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x000143FF File Offset: 0x000125FF
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x00014407 File Offset: 0x00012607
		public LobbyAttributeVisibility Visibility
		{
			get
			{
				return this.m_Visibility;
			}
			set
			{
				this.m_Visibility = value;
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00014410 File Offset: 0x00012610
		public void Set(Attribute other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Data;
				this.Visibility = other.Visibility;
			}
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00014434 File Offset: 0x00012634
		public void Set(object other)
		{
			this.Set(other as Attribute);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00014442 File Offset: 0x00012642
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04000914 RID: 2324
		private int m_ApiVersion;

		// Token: 0x04000915 RID: 2325
		private IntPtr m_Data;

		// Token: 0x04000916 RID: 2326
		private LobbyAttributeVisibility m_Visibility;
	}
}
