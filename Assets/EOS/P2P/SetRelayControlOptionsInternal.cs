using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B9 RID: 697
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetRelayControlOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000514 RID: 1300
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x00012D94 File Offset: 0x00010F94
		public RelayControl RelayControl
		{
			set
			{
				this.m_RelayControl = value;
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00012D9D File Offset: 0x00010F9D
		public void Set(SetRelayControlOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.RelayControl = other.RelayControl;
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00012DB5 File Offset: 0x00010FB5
		public void Set(object other)
		{
			this.Set(other as SetRelayControlOptions);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000856 RID: 2134
		private int m_ApiVersion;

		// Token: 0x04000857 RID: 2135
		private RelayControl m_RelayControl;
	}
}
