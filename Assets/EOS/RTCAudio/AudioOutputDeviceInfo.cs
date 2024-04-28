using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200017C RID: 380
	public class AudioOutputDeviceInfo : ISettable
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0000B694 File Offset: 0x00009894
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0000B69C File Offset: 0x0000989C
		public bool DefaultDevice { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0000B6A5 File Offset: 0x000098A5
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0000B6AD File Offset: 0x000098AD
		public string DeviceId { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0000B6B6 File Offset: 0x000098B6
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0000B6BE File Offset: 0x000098BE
		public string DeviceName { get; set; }

		// Token: 0x06000A58 RID: 2648 RVA: 0x0000B6C8 File Offset: 0x000098C8
		internal void Set(AudioOutputDeviceInfoInternal? other)
		{
			if (other != null)
			{
				this.DefaultDevice = other.Value.DefaultDevice;
				this.DeviceId = other.Value.DeviceId;
				this.DeviceName = other.Value.DeviceName;
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0000B71D File Offset: 0x0000991D
		public void Set(object other)
		{
			this.Set(other as AudioOutputDeviceInfoInternal?);
		}
	}
}
