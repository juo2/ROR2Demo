using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200014E RID: 334
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000223 RID: 547
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0000A46D File Offset: 0x0000866D
		public SessionModification SessionModificationHandle
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionModificationHandle, value);
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0000A47C File Offset: 0x0000867C
		public void Set(UpdateSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionModificationHandle = other.SessionModificationHandle;
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0000A494 File Offset: 0x00008694
		public void Set(object other)
		{
			this.Set(other as UpdateSessionOptions);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0000A4A2 File Offset: 0x000086A2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionModificationHandle);
		}

		// Token: 0x0400046E RID: 1134
		private int m_ApiVersion;

		// Token: 0x0400046F RID: 1135
		private IntPtr m_SessionModificationHandle;
	}
}
