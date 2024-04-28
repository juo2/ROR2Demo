using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000315 RID: 789
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteIdByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005C4 RID: 1476
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x00014EE2 File Offset: 0x000130E2
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00014EF1 File Offset: 0x000130F1
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00014EFA File Offset: 0x000130FA
		public void Set(GetInviteIdByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Index = other.Index;
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00014F1E File Offset: 0x0001311E
		public void Set(object other)
		{
			this.Set(other as GetInviteIdByIndexOptions);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00014F2C File Offset: 0x0001312C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400095F RID: 2399
		private int m_ApiVersion;

		// Token: 0x04000960 RID: 2400
		private IntPtr m_LocalUserId;

		// Token: 0x04000961 RID: 2401
		private uint m_Index;
	}
}
