using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200013F RID: 319
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetTargetUserIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000204 RID: 516
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0000970F File Offset: 0x0000790F
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0000971E File Offset: 0x0000791E
		public void Set(SessionSearchSetTargetUserIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00009736 File Offset: 0x00007936
		public void Set(object other)
		{
			this.Set(other as SessionSearchSetTargetUserIdOptions);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00009744 File Offset: 0x00007944
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400042A RID: 1066
		private int m_ApiVersion;

		// Token: 0x0400042B RID: 1067
		private IntPtr m_TargetUserId;
	}
}
