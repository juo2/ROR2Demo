using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036E RID: 878
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetParameterOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000669 RID: 1641
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x000177DA File Offset: 0x000159DA
		public AttributeData Parameter
		{
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal, AttributeData>(ref this.m_Parameter, value);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x000177E9 File Offset: 0x000159E9
		public ComparisonOp ComparisonOp
		{
			set
			{
				this.m_ComparisonOp = value;
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000177F2 File Offset: 0x000159F2
		public void Set(LobbySearchSetParameterOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Parameter = other.Parameter;
				this.ComparisonOp = other.ComparisonOp;
			}
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00017816 File Offset: 0x00015A16
		public void Set(object other)
		{
			this.Set(other as LobbySearchSetParameterOptions);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00017824 File Offset: 0x00015A24
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Parameter);
		}

		// Token: 0x04000A6B RID: 2667
		private int m_ApiVersion;

		// Token: 0x04000A6C RID: 2668
		private IntPtr m_Parameter;

		// Token: 0x04000A6D RID: 2669
		private ComparisonOp m_ComparisonOp;
	}
}
