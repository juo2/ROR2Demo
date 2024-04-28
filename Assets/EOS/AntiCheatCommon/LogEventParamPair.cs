using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000583 RID: 1411
	public class LogEventParamPair : ISettable
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00023AFF File Offset: 0x00021CFF
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x00023B07 File Offset: 0x00021D07
		public LogEventParamPairParamValue ParamValue { get; set; }

		// Token: 0x060021DB RID: 8667 RVA: 0x00023B10 File Offset: 0x00021D10
		internal void Set(LogEventParamPairInternal? other)
		{
			if (other != null)
			{
				this.ParamValue = other.Value.ParamValue;
			}
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00023B3B File Offset: 0x00021D3B
		public void Set(object other)
		{
			this.Set(other as LogEventParamPairInternal?);
		}
	}
}
