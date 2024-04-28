using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B7 RID: 439
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170002FD RID: 765
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0000CB62 File Offset: 0x0000AD62
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x170002FE RID: 766
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0000CB71 File Offset: 0x0000AD71
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0000CB80 File Offset: 0x0000AD80
		public void Set(KickOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.RoomName = other.RoomName;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public void Set(object other)
		{
			this.Set(other as KickOptions);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0000CBB2 File Offset: 0x0000ADB2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_RoomName);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400058E RID: 1422
		private int m_ApiVersion;

		// Token: 0x0400058F RID: 1423
		private IntPtr m_RoomName;

		// Token: 0x04000590 RID: 1424
		private IntPtr m_TargetUserId;
	}
}
