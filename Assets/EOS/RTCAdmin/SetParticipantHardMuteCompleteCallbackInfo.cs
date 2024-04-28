using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001C3 RID: 451
	public class SetParticipantHardMuteCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0000D04C File Offset: 0x0000B24C
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0000D054 File Offset: 0x0000B254
		public Result ResultCode { get; private set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0000D05D File Offset: 0x0000B25D
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0000D065 File Offset: 0x0000B265
		public object ClientData { get; private set; }

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0000D06E File Offset: 0x0000B26E
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0000D07C File Offset: 0x0000B27C
		internal void Set(SetParticipantHardMuteCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		public void Set(object other)
		{
			this.Set(other as SetParticipantHardMuteCompleteCallbackInfoInternal?);
		}
	}
}
