using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200017A RID: 378
	public class AudioInputStateCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0000B553 File Offset: 0x00009753
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0000B55B File Offset: 0x0000975B
		public object ClientData { get; private set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0000B564 File Offset: 0x00009764
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x0000B56C File Offset: 0x0000976C
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0000B575 File Offset: 0x00009775
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0000B57D File Offset: 0x0000977D
		public string RoomName { get; private set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0000B586 File Offset: 0x00009786
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0000B58E File Offset: 0x0000978E
		public RTCAudioInputStatus Status { get; private set; }

		// Token: 0x06000A49 RID: 2633 RVA: 0x0000B598 File Offset: 0x00009798
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0000B5B0 File Offset: 0x000097B0
		internal void Set(AudioInputStateCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Status = other.Value.Status;
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000B61A File Offset: 0x0000981A
		public void Set(object other)
		{
			this.Set(other as AudioInputStateCallbackInfoInternal?);
		}
	}
}
