using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B7 RID: 695
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPortRangeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000511 RID: 1297
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x00012D3F File Offset: 0x00010F3F
		public ushort Port
		{
			set
			{
				this.m_Port = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00012D48 File Offset: 0x00010F48
		public ushort MaxAdditionalPortsToTry
		{
			set
			{
				this.m_MaxAdditionalPortsToTry = value;
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00012D51 File Offset: 0x00010F51
		public void Set(SetPortRangeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Port = other.Port;
				this.MaxAdditionalPortsToTry = other.MaxAdditionalPortsToTry;
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00012D75 File Offset: 0x00010F75
		public void Set(object other)
		{
			this.Set(other as SetPortRangeOptions);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000852 RID: 2130
		private int m_ApiVersion;

		// Token: 0x04000853 RID: 2131
		private ushort m_Port;

		// Token: 0x04000854 RID: 2132
		private ushort m_MaxAdditionalPortsToTry;
	}
}
