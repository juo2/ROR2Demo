using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000590 RID: 1424
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerSpawnOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A8F RID: 2703
		// (set) Token: 0x06002242 RID: 8770 RVA: 0x00024411 File Offset: 0x00022611
		public IntPtr SpawnedPlayerHandle
		{
			set
			{
				this.m_SpawnedPlayerHandle = value;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x0002441A File Offset: 0x0002261A
		public uint TeamId
		{
			set
			{
				this.m_TeamId = value;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (set) Token: 0x06002244 RID: 8772 RVA: 0x00024423 File Offset: 0x00022623
		public uint CharacterId
		{
			set
			{
				this.m_CharacterId = value;
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0002442C File Offset: 0x0002262C
		public void Set(LogPlayerSpawnOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SpawnedPlayerHandle = other.SpawnedPlayerHandle;
				this.TeamId = other.TeamId;
				this.CharacterId = other.CharacterId;
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0002445C File Offset: 0x0002265C
		public void Set(object other)
		{
			this.Set(other as LogPlayerSpawnOptions);
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0002446A File Offset: 0x0002266A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SpawnedPlayerHandle);
		}

		// Token: 0x0400101F RID: 4127
		private int m_ApiVersion;

		// Token: 0x04001020 RID: 4128
		private IntPtr m_SpawnedPlayerHandle;

		// Token: 0x04001021 RID: 4129
		private uint m_TeamId;

		// Token: 0x04001022 RID: 4130
		private uint m_CharacterId;
	}
}
