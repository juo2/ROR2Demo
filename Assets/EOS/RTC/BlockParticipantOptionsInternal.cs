using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D0 RID: 464
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BlockParticipantOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700033C RID: 828
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700033D RID: 829
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0000D5F3 File Offset: 0x0000B7F3
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x1700033E RID: 830
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x0000D602 File Offset: 0x0000B802
		public ProductUserId ParticipantId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ParticipantId, value);
			}
		}

		// Token: 0x1700033F RID: 831
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0000D611 File Offset: 0x0000B811
		public bool Blocked
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Blocked, value);
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0000D620 File Offset: 0x0000B820
		public void Set(BlockParticipantOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
				this.ParticipantId = other.ParticipantId;
				this.Blocked = other.Blocked;
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0000D65C File Offset: 0x0000B85C
		public void Set(object other)
		{
			this.Set(other as BlockParticipantOptions);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0000D66A File Offset: 0x0000B86A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
			Helper.TryMarshalDispose(ref this.m_ParticipantId);
		}

		// Token: 0x040005D7 RID: 1495
		private int m_ApiVersion;

		// Token: 0x040005D8 RID: 1496
		private IntPtr m_LocalUserId;

		// Token: 0x040005D9 RID: 1497
		private IntPtr m_RoomName;

		// Token: 0x040005DA RID: 1498
		private IntPtr m_ParticipantId;

		// Token: 0x040005DB RID: 1499
		private int m_Blocked;
	}
}
