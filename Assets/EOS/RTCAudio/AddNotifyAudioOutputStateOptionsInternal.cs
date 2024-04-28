using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200016D RID: 365
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioOutputStateOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700025E RID: 606
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x0000AE2B File Offset: 0x0000902B
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700025F RID: 607
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0000AE3A File Offset: 0x0000903A
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0000AE49 File Offset: 0x00009049
		public void Set(AddNotifyAudioOutputStateOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000AE6D File Offset: 0x0000906D
		public void Set(object other)
		{
			this.Set(other as AddNotifyAudioOutputStateOptions);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000AE7B File Offset: 0x0000907B
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x040004C1 RID: 1217
		private int m_ApiVersion;

		// Token: 0x040004C2 RID: 1218
		private IntPtr m_LocalUserId;

		// Token: 0x040004C3 RID: 1219
		private IntPtr m_RoomName;
	}
}
