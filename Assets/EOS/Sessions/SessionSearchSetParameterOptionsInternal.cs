using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200013B RID: 315
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetParameterOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001FF RID: 511
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00009652 File Offset: 0x00007852
		public AttributeData Parameter
		{
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal, AttributeData>(ref this.m_Parameter, value);
			}
		}

		// Token: 0x17000200 RID: 512
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00009661 File Offset: 0x00007861
		public ComparisonOp ComparisonOp
		{
			set
			{
				this.m_ComparisonOp = value;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0000966A File Offset: 0x0000786A
		public void Set(SessionSearchSetParameterOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Parameter = other.Parameter;
				this.ComparisonOp = other.ComparisonOp;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0000968E File Offset: 0x0000788E
		public void Set(object other)
		{
			this.Set(other as SessionSearchSetParameterOptions);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0000969C File Offset: 0x0000789C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Parameter);
		}

		// Token: 0x04000423 RID: 1059
		private int m_ApiVersion;

		// Token: 0x04000424 RID: 1060
		private IntPtr m_Parameter;

		// Token: 0x04000425 RID: 1061
		private ComparisonOp m_ComparisonOp;
	}
}
