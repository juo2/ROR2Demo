using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000551 RID: 1361
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IOSCredentialsSystemAuthCredentialsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x00022DFA File Offset: 0x00020FFA
		// (set) Token: 0x06002116 RID: 8470 RVA: 0x00022E02 File Offset: 0x00021002
		public IntPtr PresentationContextProviding
		{
			get
			{
				return this.m_PresentationContextProviding;
			}
			set
			{
				this.m_PresentationContextProviding = value;
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00022E0B File Offset: 0x0002100B
		public void Set(IOSCredentialsSystemAuthCredentialsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PresentationContextProviding = other.PresentationContextProviding;
			}
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00022E23 File Offset: 0x00021023
		public void Set(object other)
		{
			this.Set(other as IOSCredentialsSystemAuthCredentialsOptions);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00022E31 File Offset: 0x00021031
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PresentationContextProviding);
		}

		// Token: 0x04000F3F RID: 3903
		private int m_ApiVersion;

		// Token: 0x04000F40 RID: 3904
		private IntPtr m_PresentationContextProviding;
	}
}
