using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000494 RID: 1172
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyCustomInviteAcceptedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001C79 RID: 7289 RVA: 0x0001E110 File Offset: 0x0001C310
		public void Set(AddNotifyCustomInviteAcceptedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0001E11C File Offset: 0x0001C31C
		public void Set(object other)
		{
			this.Set(other as AddNotifyCustomInviteAcceptedOptions);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000D4C RID: 3404
		private int m_ApiVersion;
	}
}
