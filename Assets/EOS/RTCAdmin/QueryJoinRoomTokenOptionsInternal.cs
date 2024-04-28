using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C1 RID: 449
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryJoinRoomTokenOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000310 RID: 784
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000311 RID: 785
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0000CDAF File Offset: 0x0000AFAF
		public string RoomName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RoomName, value);
			}
		}

		// Token: 0x17000312 RID: 786
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0000CDBE File Offset: 0x0000AFBE
		public ProductUserId[] TargetUserIds
		{
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_TargetUserIds, value, out this.m_TargetUserIdsCount);
			}
		}

		// Token: 0x17000313 RID: 787
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0000CDD3 File Offset: 0x0000AFD3
		public string TargetUserIpAddresses
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserIpAddresses, value);
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0000CDE2 File Offset: 0x0000AFE2
		public void Set(QueryJoinRoomTokenOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.RoomName = other.RoomName;
				this.TargetUserIds = other.TargetUserIds;
				this.TargetUserIpAddresses = other.TargetUserIpAddresses;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0000CE1E File Offset: 0x0000B01E
		public void Set(object other)
		{
			this.Set(other as QueryJoinRoomTokenOptions);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0000CE2C File Offset: 0x0000B02C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RoomName);
			Helper.TryMarshalDispose(ref this.m_TargetUserIds);
			Helper.TryMarshalDispose(ref this.m_TargetUserIpAddresses);
		}

		// Token: 0x040005A1 RID: 1441
		private int m_ApiVersion;

		// Token: 0x040005A2 RID: 1442
		private IntPtr m_LocalUserId;

		// Token: 0x040005A3 RID: 1443
		private IntPtr m_RoomName;

		// Token: 0x040005A4 RID: 1444
		private IntPtr m_TargetUserIds;

		// Token: 0x040005A5 RID: 1445
		private uint m_TargetUserIdsCount;

		// Token: 0x040005A6 RID: 1446
		private IntPtr m_TargetUserIpAddresses;
	}
}
