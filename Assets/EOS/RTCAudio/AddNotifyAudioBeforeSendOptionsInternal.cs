using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000167 RID: 359
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioBeforeSendOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000256 RID: 598
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000257 RID: 599
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x0000AD08 File Offset: 0x00008F08
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0000AD17 File Offset: 0x00008F17
		public void Set(AddNotifyAudioBeforeSendOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000AD3B File Offset: 0x00008F3B
		public void Set(object other)
		{
			this.Set(other as AddNotifyAudioBeforeSendOptions);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0000AD49 File Offset: 0x00008F49
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x040004B6 RID: 1206
		private int m_ApiVersion;

		// Token: 0x040004B7 RID: 1207
		private IntPtr m_LocalUserId;

		// Token: 0x040004B8 RID: 1208
		private IntPtr m_RoomName;
	}
}
