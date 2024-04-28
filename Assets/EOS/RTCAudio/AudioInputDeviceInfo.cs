using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000178 RID: 376
	public class AudioInputDeviceInfo : ISettable
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0000B3DC File Offset: 0x000095DC
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool DefaultDevice { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0000B3ED File Offset: 0x000095ED
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x0000B3F5 File Offset: 0x000095F5
		public string DeviceId { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0000B3FE File Offset: 0x000095FE
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x0000B406 File Offset: 0x00009606
		public string DeviceName { get; set; }

		// Token: 0x06000A35 RID: 2613 RVA: 0x0000B410 File Offset: 0x00009610
		internal void Set(AudioInputDeviceInfoInternal? other)
		{
			if (other != null)
			{
				this.DefaultDevice = other.Value.DefaultDevice;
				this.DeviceId = other.Value.DeviceId;
				this.DeviceName = other.Value.DeviceName;
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000B465 File Offset: 0x00009665
		public void Set(object other)
		{
			this.Set(other as AudioInputDeviceInfoInternal?);
		}
	}
}
