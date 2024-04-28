using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001BE RID: 446
	public class QueryJoinRoomTokenCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
		public Result ResultCode { get; private set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0000CBDD File Offset: 0x0000ADDD
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0000CBE5 File Offset: 0x0000ADE5
		public object ClientData { get; private set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0000CBEE File Offset: 0x0000ADEE
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0000CBF6 File Offset: 0x0000ADF6
		public string RoomName { get; private set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0000CBFF File Offset: 0x0000ADFF
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0000CC07 File Offset: 0x0000AE07
		public string ClientBaseUrl { get; private set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0000CC10 File Offset: 0x0000AE10
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0000CC18 File Offset: 0x0000AE18
		public uint QueryId { get; private set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0000CC21 File Offset: 0x0000AE21
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0000CC29 File Offset: 0x0000AE29
		public uint TokenCount { get; private set; }

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0000CC32 File Offset: 0x0000AE32
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0000CC40 File Offset: 0x0000AE40
		internal void Set(QueryJoinRoomTokenCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.RoomName = other.Value.RoomName;
				this.ClientBaseUrl = other.Value.ClientBaseUrl;
				this.QueryId = other.Value.QueryId;
				this.TokenCount = other.Value.TokenCount;
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		public void Set(object other)
		{
			this.Set(other as QueryJoinRoomTokenCompleteCallbackInfoInternal?);
		}
	}
}
