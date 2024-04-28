using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000176 RID: 374
	public class AudioDevicesChangedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0000B34D File Offset: 0x0000954D
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x0000B355 File Offset: 0x00009555
		public object ClientData { get; private set; }

		// Token: 0x06000A29 RID: 2601 RVA: 0x0000B360 File Offset: 0x00009560
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0000B378 File Offset: 0x00009578
		internal void Set(AudioDevicesChangedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000B3A3 File Offset: 0x000095A3
		public void Set(object other)
		{
			this.Set(other as AudioDevicesChangedCallbackInfoInternal?);
		}
	}
}
