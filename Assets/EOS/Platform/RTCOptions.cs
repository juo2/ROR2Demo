using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005E7 RID: 1511
	public class RTCOptions : ISettable
	{
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000270F9 File Offset: 0x000252F9
		// (set) Token: 0x060024D6 RID: 9430 RVA: 0x00027101 File Offset: 0x00025301
		public IntPtr PlatformSpecificOptions { get; set; }

		// Token: 0x060024D7 RID: 9431 RVA: 0x0002710C File Offset: 0x0002530C
		internal void Set(RTCOptionsInternal? other)
		{
			if (other != null)
			{
				this.PlatformSpecificOptions = other.Value.PlatformSpecificOptions;
			}
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00027137 File Offset: 0x00025337
		public void Set(object other)
		{
			this.Set(other as RTCOptionsInternal?);
		}
	}
}
