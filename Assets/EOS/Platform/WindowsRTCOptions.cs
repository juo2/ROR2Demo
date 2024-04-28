using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005ED RID: 1517
	public class WindowsRTCOptions : ISettable
	{
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x0002746D File Offset: 0x0002566D
		// (set) Token: 0x06002513 RID: 9491 RVA: 0x00027475 File Offset: 0x00025675
		public WindowsRTCOptionsPlatformSpecificOptions PlatformSpecificOptions { get; set; }

		// Token: 0x06002514 RID: 9492 RVA: 0x00027480 File Offset: 0x00025680
		internal void Set(WindowsRTCOptionsInternal? other)
		{
			if (other != null)
			{
				this.PlatformSpecificOptions = other.Value.PlatformSpecificOptions;
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000274AB File Offset: 0x000256AB
		public void Set(object other)
		{
			this.Set(other as WindowsRTCOptionsInternal?);
		}
	}
}
