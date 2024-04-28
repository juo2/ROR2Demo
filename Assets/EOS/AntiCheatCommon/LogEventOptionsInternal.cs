using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000582 RID: 1410
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogEventOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A66 RID: 2662
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x00023A80 File Offset: 0x00021C80
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x00023A89 File Offset: 0x00021C89
		public uint EventId
		{
			set
			{
				this.m_EventId = value;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (set) Token: 0x060021D5 RID: 8661 RVA: 0x00023A92 File Offset: 0x00021C92
		public LogEventParamPair[] Params
		{
			set
			{
				Helper.TryMarshalSet<LogEventParamPairInternal, LogEventParamPair>(ref this.m_Params, value, out this.m_ParamsCount);
			}
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x00023AA7 File Offset: 0x00021CA7
		public void Set(LogEventOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.ClientHandle;
				this.EventId = other.EventId;
				this.Params = other.Params;
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00023AD7 File Offset: 0x00021CD7
		public void Set(object other)
		{
			this.Set(other as LogEventOptions);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00023AE5 File Offset: 0x00021CE5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientHandle);
			Helper.TryMarshalDispose(ref this.m_Params);
		}

		// Token: 0x04000FEF RID: 4079
		private int m_ApiVersion;

		// Token: 0x04000FF0 RID: 4080
		private IntPtr m_ClientHandle;

		// Token: 0x04000FF1 RID: 4081
		private uint m_EventId;

		// Token: 0x04000FF2 RID: 4082
		private uint m_ParamsCount;

		// Token: 0x04000FF3 RID: 4083
		private IntPtr m_Params;
	}
}
