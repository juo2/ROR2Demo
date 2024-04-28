using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000137 RID: 311
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchRemoveParameterOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001F9 RID: 505
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x00009598 File Offset: 0x00007798
		public string Key
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x170001FA RID: 506
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x000095A7 File Offset: 0x000077A7
		public ComparisonOp ComparisonOp
		{
			set
			{
				this.m_ComparisonOp = value;
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000095B0 File Offset: 0x000077B0
		public void Set(SessionSearchRemoveParameterOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
				this.ComparisonOp = other.ComparisonOp;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000095D4 File Offset: 0x000077D4
		public void Set(object other)
		{
			this.Set(other as SessionSearchRemoveParameterOptions);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000095E2 File Offset: 0x000077E2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
		}

		// Token: 0x0400041B RID: 1051
		private int m_ApiVersion;

		// Token: 0x0400041C RID: 1052
		private IntPtr m_Key;

		// Token: 0x0400041D RID: 1053
		private ComparisonOp m_ComparisonOp;
	}
}
