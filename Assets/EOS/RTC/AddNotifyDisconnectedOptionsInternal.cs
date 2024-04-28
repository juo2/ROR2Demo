using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001CA RID: 458
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyDisconnectedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000325 RID: 805
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0000D2F1 File Offset: 0x0000B4F1
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000326 RID: 806
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0000D300 File Offset: 0x0000B500
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0000D30F File Offset: 0x0000B50F
		public void Set(AddNotifyDisconnectedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0000D333 File Offset: 0x0000B533
		public void Set(object other)
		{
			this.Set(other as AddNotifyDisconnectedOptions);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0000D341 File Offset: 0x0000B541
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x040005BF RID: 1471
		private int m_ApiVersion;

		// Token: 0x040005C0 RID: 1472
		private IntPtr m_LocalUserId;

		// Token: 0x040005C1 RID: 1473
		private IntPtr m_RoomName;
	}
}
