using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005EF RID: 1519
	public class WindowsRTCOptionsPlatformSpecificOptions : ISettable
	{
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x0002751F File Offset: 0x0002571F
		// (set) Token: 0x0600251D RID: 9501 RVA: 0x00027527 File Offset: 0x00025727
		public string XAudio29DllPath { get; set; }

		// Token: 0x0600251E RID: 9502 RVA: 0x00027530 File Offset: 0x00025730
		internal void Set(WindowsRTCOptionsPlatformSpecificOptionsInternal? other)
		{
			if (other != null)
			{
				this.XAudio29DllPath = other.Value.XAudio29DllPath;
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0002755B File Offset: 0x0002575B
		public void Set(object other)
		{
			this.Set(other as WindowsRTCOptionsPlatformSpecificOptionsInternal?);
		}
	}
}
