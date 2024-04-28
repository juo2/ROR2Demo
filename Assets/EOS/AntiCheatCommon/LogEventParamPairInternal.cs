using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000584 RID: 1412
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogEventParamPairInternal : ISettable, IDisposable
	{
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x00023B50 File Offset: 0x00021D50
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x00023B6C File Offset: 0x00021D6C
		public LogEventParamPairParamValue ParamValue
		{
			get
			{
				LogEventParamPairParamValue result;
				Helper.TryMarshalGet<LogEventParamPairParamValueInternal, LogEventParamPairParamValue>(this.m_ParamValue, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<LogEventParamPairParamValueInternal>(ref this.m_ParamValue, value);
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00023B7B File Offset: 0x00021D7B
		public void Set(LogEventParamPair other)
		{
			if (other != null)
			{
				this.ParamValue = other.ParamValue;
			}
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00023B8C File Offset: 0x00021D8C
		public void Set(object other)
		{
			this.Set(other as LogEventParamPair);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00023B9A File Offset: 0x00021D9A
		public void Dispose()
		{
			Helper.TryMarshalDispose<LogEventParamPairParamValueInternal>(ref this.m_ParamValue);
		}

		// Token: 0x04000FF5 RID: 4085
		private LogEventParamPairParamValueInternal m_ParamValue;
	}
}
