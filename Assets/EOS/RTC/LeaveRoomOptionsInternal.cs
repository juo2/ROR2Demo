using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001DB RID: 475
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveRoomOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700036D RID: 877
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0000DC16 File Offset: 0x0000BE16
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700036E RID: 878
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0000DC25 File Offset: 0x0000BE25
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0000DC34 File Offset: 0x0000BE34
		public void Set(LeaveRoomOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public void Set(object other)
		{
			this.Set(other as LeaveRoomOptions);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0000DC66 File Offset: 0x0000BE66
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x0400060A RID: 1546
		private int m_ApiVersion;

		// Token: 0x0400060B RID: 1547
		private IntPtr m_LocalUserId;

		// Token: 0x0400060C RID: 1548
		private IntPtr m_RoomName;
	}
}
