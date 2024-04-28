using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200016F RID: 367
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyParticipantUpdatedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000262 RID: 610
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0000AEB7 File Offset: 0x000090B7
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000263 RID: 611
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x0000AEC6 File Offset: 0x000090C6
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0000AED5 File Offset: 0x000090D5
		public void Set(AddNotifyParticipantUpdatedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0000AEF9 File Offset: 0x000090F9
		public void Set(object other)
		{
			this.Set(other as AddNotifyParticipantUpdatedOptions);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000AF07 File Offset: 0x00009107
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
		}

		// Token: 0x040004C6 RID: 1222
		private int m_ApiVersion;

		// Token: 0x040004C7 RID: 1223
		private IntPtr m_LocalUserId;

		// Token: 0x040004C8 RID: 1224
		private IntPtr m_RoomName;
	}
}
