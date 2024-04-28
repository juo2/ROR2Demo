using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000370 RID: 880
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetTargetUserIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700066C RID: 1644
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x00017843 File Offset: 0x00015A43
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00017852 File Offset: 0x00015A52
		public void Set(LobbySearchSetTargetUserIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0001786A File Offset: 0x00015A6A
		public void Set(object other)
		{
			this.Set(other as LobbySearchSetTargetUserIdOptions);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00017878 File Offset: 0x00015A78
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000A6F RID: 2671
		private int m_ApiVersion;

		// Token: 0x04000A70 RID: 2672
		private IntPtr m_TargetUserId;
	}
}
