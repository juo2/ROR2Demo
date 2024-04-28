using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000550 RID: 1360
	public class IOSCredentialsSystemAuthCredentialsOptions : ISettable
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x00022DAB File Offset: 0x00020FAB
		// (set) Token: 0x06002111 RID: 8465 RVA: 0x00022DB3 File Offset: 0x00020FB3
		public IntPtr PresentationContextProviding { get; set; }

		// Token: 0x06002112 RID: 8466 RVA: 0x00022DBC File Offset: 0x00020FBC
		internal void Set(IOSCredentialsSystemAuthCredentialsOptionsInternal? other)
		{
			if (other != null)
			{
				this.PresentationContextProviding = other.Value.PresentationContextProviding;
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x00022DE7 File Offset: 0x00020FE7
		public void Set(object other)
		{
			this.Set(other as IOSCredentialsSystemAuthCredentialsOptionsInternal?);
		}
	}
}
