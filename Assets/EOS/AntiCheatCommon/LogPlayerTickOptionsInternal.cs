using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000594 RID: 1428
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerTickOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000ABC RID: 2748
		// (set) Token: 0x0600228F RID: 8847 RVA: 0x00024857 File Offset: 0x00022A57
		public IntPtr PlayerHandle
		{
			set
			{
				this.m_PlayerHandle = value;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x00024860 File Offset: 0x00022A60
		public Vec3f PlayerPosition
		{
			set
			{
				Helper.TryMarshalSet<Vec3fInternal, Vec3f>(ref this.m_PlayerPosition, value);
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (set) Token: 0x06002291 RID: 8849 RVA: 0x0002486F File Offset: 0x00022A6F
		public Quat PlayerViewRotation
		{
			set
			{
				Helper.TryMarshalSet<QuatInternal, Quat>(ref this.m_PlayerViewRotation, value);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (set) Token: 0x06002292 RID: 8850 RVA: 0x0002487E File Offset: 0x00022A7E
		public bool IsPlayerViewZoomed
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IsPlayerViewZoomed, value);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (set) Token: 0x06002293 RID: 8851 RVA: 0x0002488D File Offset: 0x00022A8D
		public float PlayerHealth
		{
			set
			{
				this.m_PlayerHealth = value;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x00024896 File Offset: 0x00022A96
		public AntiCheatCommonPlayerMovementState PlayerMovementState
		{
			set
			{
				this.m_PlayerMovementState = value;
			}
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000248A0 File Offset: 0x00022AA0
		public void Set(LogPlayerTickOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.PlayerHandle = other.PlayerHandle;
				this.PlayerPosition = other.PlayerPosition;
				this.PlayerViewRotation = other.PlayerViewRotation;
				this.IsPlayerViewZoomed = other.IsPlayerViewZoomed;
				this.PlayerHealth = other.PlayerHealth;
				this.PlayerMovementState = other.PlayerMovementState;
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000248FF File Offset: 0x00022AFF
		public void Set(object other)
		{
			this.Set(other as LogPlayerTickOptions);
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0002490D File Offset: 0x00022B0D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlayerHandle);
			Helper.TryMarshalDispose(ref this.m_PlayerPosition);
			Helper.TryMarshalDispose(ref this.m_PlayerViewRotation);
		}

		// Token: 0x0400104E RID: 4174
		private int m_ApiVersion;

		// Token: 0x0400104F RID: 4175
		private IntPtr m_PlayerHandle;

		// Token: 0x04001050 RID: 4176
		private IntPtr m_PlayerPosition;

		// Token: 0x04001051 RID: 4177
		private IntPtr m_PlayerViewRotation;

		// Token: 0x04001052 RID: 4178
		private int m_IsPlayerViewZoomed;

		// Token: 0x04001053 RID: 4179
		private float m_PlayerHealth;

		// Token: 0x04001054 RID: 4180
		private AntiCheatCommonPlayerMovementState m_PlayerMovementState;
	}
}
