using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000BB RID: 187
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleByInviteIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700013F RID: 319
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x00007585 File Offset: 0x00005785
		public string InviteId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_InviteId, value);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00007594 File Offset: 0x00005794
		public void Set(CopySessionHandleByInviteIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.InviteId = other.InviteId;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000075AC File Offset: 0x000057AC
		public void Set(object other)
		{
			this.Set(other as CopySessionHandleByInviteIdOptions);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000075BA File Offset: 0x000057BA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_InviteId);
		}

		// Token: 0x0400031C RID: 796
		private int m_ApiVersion;

		// Token: 0x0400031D RID: 797
		private IntPtr m_InviteId;
	}
}
