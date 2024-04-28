using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200058A RID: 1418
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogGameRoundStartOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A82 RID: 2690
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x0002426A File Offset: 0x0002246A
		public string SessionIdentifier
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionIdentifier, value);
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x00024279 File Offset: 0x00022479
		public string LevelName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LevelName, value);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (set) Token: 0x06002225 RID: 8741 RVA: 0x00024288 File Offset: 0x00022488
		public string ModeName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ModeName, value);
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x00024297 File Offset: 0x00022497
		public uint RoundTimeSeconds
		{
			set
			{
				this.m_RoundTimeSeconds = value;
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000242A0 File Offset: 0x000224A0
		public void Set(LogGameRoundStartOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionIdentifier = other.SessionIdentifier;
				this.LevelName = other.LevelName;
				this.ModeName = other.ModeName;
				this.RoundTimeSeconds = other.RoundTimeSeconds;
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000242DC File Offset: 0x000224DC
		public void Set(object other)
		{
			this.Set(other as LogGameRoundStartOptions);
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000242EA File Offset: 0x000224EA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionIdentifier);
			Helper.TryMarshalDispose(ref this.m_LevelName);
			Helper.TryMarshalDispose(ref this.m_ModeName);
		}

		// Token: 0x0400100F RID: 4111
		private int m_ApiVersion;

		// Token: 0x04001010 RID: 4112
		private IntPtr m_SessionIdentifier;

		// Token: 0x04001011 RID: 4113
		private IntPtr m_LevelName;

		// Token: 0x04001012 RID: 4114
		private IntPtr m_ModeName;

		// Token: 0x04001013 RID: 4115
		private uint m_RoundTimeSeconds;
	}
}
