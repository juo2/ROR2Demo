using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000596 RID: 1430
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerUseAbilityOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000AC6 RID: 2758
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x00024977 File Offset: 0x00022B77
		public IntPtr PlayerHandle
		{
			set
			{
				this.m_PlayerHandle = value;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (set) Token: 0x060022A2 RID: 8866 RVA: 0x00024980 File Offset: 0x00022B80
		public uint AbilityId
		{
			set
			{
				this.m_AbilityId = value;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x00024989 File Offset: 0x00022B89
		public uint AbilityDurationMs
		{
			set
			{
				this.m_AbilityDurationMs = value;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (set) Token: 0x060022A4 RID: 8868 RVA: 0x00024992 File Offset: 0x00022B92
		public uint AbilityCooldownMs
		{
			set
			{
				this.m_AbilityCooldownMs = value;
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0002499B File Offset: 0x00022B9B
		public void Set(LogPlayerUseAbilityOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PlayerHandle = other.PlayerHandle;
				this.AbilityId = other.AbilityId;
				this.AbilityDurationMs = other.AbilityDurationMs;
				this.AbilityCooldownMs = other.AbilityCooldownMs;
			}
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000249D7 File Offset: 0x00022BD7
		public void Set(object other)
		{
			this.Set(other as LogPlayerUseAbilityOptions);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000249E5 File Offset: 0x00022BE5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlayerHandle);
		}

		// Token: 0x04001059 RID: 4185
		private int m_ApiVersion;

		// Token: 0x0400105A RID: 4186
		private IntPtr m_PlayerHandle;

		// Token: 0x0400105B RID: 4187
		private uint m_AbilityId;

		// Token: 0x0400105C RID: 4188
		private uint m_AbilityDurationMs;

		// Token: 0x0400105D RID: 4189
		private uint m_AbilityCooldownMs;
	}
}
