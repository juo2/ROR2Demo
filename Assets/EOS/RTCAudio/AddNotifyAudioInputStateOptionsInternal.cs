using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200016B RID: 363
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioInputStateOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700025A RID: 602
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0000AD9F File Offset: 0x00008F9F
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700025B RID: 603
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0000ADAE File Offset: 0x00008FAE
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000ADBD File Offset: 0x00008FBD
		public void Set(AddNotifyAudioInputStateOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000ADE1 File Offset: 0x00008FE1
		public void Set(object other)
		{
			this.Set(other as AddNotifyAudioInputStateOptions);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0000ADEF File Offset: 0x00008FEF
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x040004BC RID: 1212
		private int m_ApiVersion;

		// Token: 0x040004BD RID: 1213
		private IntPtr m_LocalUserId;

		// Token: 0x040004BE RID: 1214
		private IntPtr m_RoomName;
	}
}
