using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005AA RID: 1450
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetGameSessionIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B0C RID: 2828
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x000253E0 File Offset: 0x000235E0
		public string GameSessionId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_GameSessionId, value);
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000253EF File Offset: 0x000235EF
		public void Set(SetGameSessionIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.GameSessionId = other.GameSessionId;
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00025407 File Offset: 0x00023607
		public void Set(object other)
		{
			this.Set(other as SetGameSessionIdOptions);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00025415 File Offset: 0x00023615
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_GameSessionId);
		}

		// Token: 0x040010A2 RID: 4258
		private int m_ApiVersion;

		// Token: 0x040010A3 RID: 4259
		private IntPtr m_GameSessionId;
	}
}
