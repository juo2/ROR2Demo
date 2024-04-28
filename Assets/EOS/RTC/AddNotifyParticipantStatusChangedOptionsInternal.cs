using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001CC RID: 460
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyParticipantStatusChangedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000329 RID: 809
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0000D37D File Offset: 0x0000B57D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700032A RID: 810
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x0000D38C File Offset: 0x0000B58C
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0000D39B File Offset: 0x0000B59B
		public void Set(AddNotifyParticipantStatusChangedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0000D3BF File Offset: 0x0000B5BF
		public void Set(object other)
		{
			this.Set(other as AddNotifyParticipantStatusChangedOptions);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0000D3CD File Offset: 0x0000B5CD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x040005C4 RID: 1476
		private int m_ApiVersion;

		// Token: 0x040005C5 RID: 1477
		private IntPtr m_LocalUserId;

		// Token: 0x040005C6 RID: 1478
		private IntPtr m_RoomName;
	}
}
