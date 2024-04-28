using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001BF RID: 447
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryJoinRoomTokenCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0000CCE7 File Offset: 0x0000AEE7
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0000CD14 File Offset: 0x0000AF14
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0000CD30 File Offset: 0x0000AF30
		public string ClientBaseUrl
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ClientBaseUrl, out result);
				return result;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public uint QueryId
		{
			get
			{
				return this.m_QueryId;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0000CD54 File Offset: 0x0000AF54
		public uint TokenCount
		{
			get
			{
				return this.m_TokenCount;
			}
		}

		// Token: 0x04000597 RID: 1431
		private Result m_ResultCode;

		// Token: 0x04000598 RID: 1432
		private IntPtr m_ClientData;

		// Token: 0x04000599 RID: 1433
		private IntPtr m_RoomName;

		// Token: 0x0400059A RID: 1434
		private IntPtr m_ClientBaseUrl;

		// Token: 0x0400059B RID: 1435
		private uint m_QueryId;

		// Token: 0x0400059C RID: 1436
		private uint m_TokenCount;
	}
}
