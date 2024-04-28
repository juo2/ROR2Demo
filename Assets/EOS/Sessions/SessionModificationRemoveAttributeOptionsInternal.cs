using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011E RID: 286
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationRemoveAttributeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001E1 RID: 481
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0000903B File Offset: 0x0000723B
		public string Key
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0000904A File Offset: 0x0000724A
		public void Set(SessionModificationRemoveAttributeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00009062 File Offset: 0x00007262
		public void Set(object other)
		{
			this.Set(other as SessionModificationRemoveAttributeOptions);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00009070 File Offset: 0x00007270
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
		}

		// Token: 0x040003F2 RID: 1010
		private int m_ApiVersion;

		// Token: 0x040003F3 RID: 1011
		private IntPtr m_Key;
	}
}
