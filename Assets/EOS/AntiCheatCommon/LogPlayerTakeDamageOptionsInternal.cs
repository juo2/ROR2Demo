using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000592 RID: 1426
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerTakeDamageOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000AA4 RID: 2724
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x000245AA File Offset: 0x000227AA
		public IntPtr VictimPlayerHandle
		{
			set
			{
				this.m_VictimPlayerHandle = value;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (set) Token: 0x0600226E RID: 8814 RVA: 0x000245B3 File Offset: 0x000227B3
		public Vec3f VictimPlayerPosition
		{
			set
			{
				Helper.TryMarshalSet<Vec3fInternal, Vec3f>(ref this.m_VictimPlayerPosition, value);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x000245C2 File Offset: 0x000227C2
		public Quat VictimPlayerViewRotation
		{
			set
			{
				Helper.TryMarshalSet<QuatInternal, Quat>(ref this.m_VictimPlayerViewRotation, value);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x000245D1 File Offset: 0x000227D1
		public IntPtr AttackerPlayerHandle
		{
			set
			{
				this.m_AttackerPlayerHandle = value;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x000245DA File Offset: 0x000227DA
		public Vec3f AttackerPlayerPosition
		{
			set
			{
				Helper.TryMarshalSet<Vec3fInternal, Vec3f>(ref this.m_AttackerPlayerPosition, value);
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (set) Token: 0x06002272 RID: 8818 RVA: 0x000245E9 File Offset: 0x000227E9
		public Quat AttackerPlayerViewRotation
		{
			set
			{
				Helper.TryMarshalSet<QuatInternal, Quat>(ref this.m_AttackerPlayerViewRotation, value);
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x000245F8 File Offset: 0x000227F8
		public bool IsHitscanAttack
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IsHitscanAttack, value);
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x00024607 File Offset: 0x00022807
		public bool HasLineOfSight
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_HasLineOfSight, value);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x00024616 File Offset: 0x00022816
		public bool IsCriticalHit
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IsCriticalHit, value);
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (set) Token: 0x06002276 RID: 8822 RVA: 0x00024625 File Offset: 0x00022825
		public uint HitBoneId_DEPRECATED
		{
			set
			{
				this.m_HitBoneId_DEPRECATED = value;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x0002462E File Offset: 0x0002282E
		public float DamageTaken
		{
			set
			{
				this.m_DamageTaken = value;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x00024637 File Offset: 0x00022837
		public float HealthRemaining
		{
			set
			{
				this.m_HealthRemaining = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x00024640 File Offset: 0x00022840
		public AntiCheatCommonPlayerTakeDamageSource DamageSource
		{
			set
			{
				this.m_DamageSource = value;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x00024649 File Offset: 0x00022849
		public AntiCheatCommonPlayerTakeDamageType DamageType
		{
			set
			{
				this.m_DamageType = value;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x00024652 File Offset: 0x00022852
		public AntiCheatCommonPlayerTakeDamageResult DamageResult
		{
			set
			{
				this.m_DamageResult = value;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x0002465B File Offset: 0x0002285B
		public LogPlayerUseWeaponData PlayerUseWeaponData
		{
			set
			{
				Helper.TryMarshalSet<LogPlayerUseWeaponDataInternal, LogPlayerUseWeaponData>(ref this.m_PlayerUseWeaponData, value);
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (set) Token: 0x0600227D RID: 8829 RVA: 0x0002466A File Offset: 0x0002286A
		public uint TimeSincePlayerUseWeaponMs
		{
			set
			{
				this.m_TimeSincePlayerUseWeaponMs = value;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (set) Token: 0x0600227E RID: 8830 RVA: 0x00024673 File Offset: 0x00022873
		public Vec3f DamagePosition
		{
			set
			{
				Helper.TryMarshalSet<Vec3fInternal, Vec3f>(ref this.m_DamagePosition, value);
			}
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00024684 File Offset: 0x00022884
		public void Set(LogPlayerTakeDamageOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.VictimPlayerHandle = other.VictimPlayerHandle;
				this.VictimPlayerPosition = other.VictimPlayerPosition;
				this.VictimPlayerViewRotation = other.VictimPlayerViewRotation;
				this.AttackerPlayerHandle = other.AttackerPlayerHandle;
				this.AttackerPlayerPosition = other.AttackerPlayerPosition;
				this.AttackerPlayerViewRotation = other.AttackerPlayerViewRotation;
				this.IsHitscanAttack = other.IsHitscanAttack;
				this.HasLineOfSight = other.HasLineOfSight;
				this.IsCriticalHit = other.IsCriticalHit;
				this.HitBoneId_DEPRECATED = other.HitBoneId_DEPRECATED;
				this.DamageTaken = other.DamageTaken;
				this.HealthRemaining = other.HealthRemaining;
				this.DamageSource = other.DamageSource;
				this.DamageType = other.DamageType;
				this.DamageResult = other.DamageResult;
				this.PlayerUseWeaponData = other.PlayerUseWeaponData;
				this.TimeSincePlayerUseWeaponMs = other.TimeSincePlayerUseWeaponMs;
				this.DamagePosition = other.DamagePosition;
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00024776 File Offset: 0x00022976
		public void Set(object other)
		{
			this.Set(other as LogPlayerTakeDamageOptions);
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x00024784 File Offset: 0x00022984
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_VictimPlayerHandle);
			Helper.TryMarshalDispose(ref this.m_VictimPlayerPosition);
			Helper.TryMarshalDispose(ref this.m_VictimPlayerViewRotation);
			Helper.TryMarshalDispose(ref this.m_AttackerPlayerHandle);
			Helper.TryMarshalDispose(ref this.m_AttackerPlayerPosition);
			Helper.TryMarshalDispose(ref this.m_AttackerPlayerViewRotation);
			Helper.TryMarshalDispose(ref this.m_PlayerUseWeaponData);
			Helper.TryMarshalDispose(ref this.m_DamagePosition);
		}

		// Token: 0x04001035 RID: 4149
		private int m_ApiVersion;

		// Token: 0x04001036 RID: 4150
		private IntPtr m_VictimPlayerHandle;

		// Token: 0x04001037 RID: 4151
		private IntPtr m_VictimPlayerPosition;

		// Token: 0x04001038 RID: 4152
		private IntPtr m_VictimPlayerViewRotation;

		// Token: 0x04001039 RID: 4153
		private IntPtr m_AttackerPlayerHandle;

		// Token: 0x0400103A RID: 4154
		private IntPtr m_AttackerPlayerPosition;

		// Token: 0x0400103B RID: 4155
		private IntPtr m_AttackerPlayerViewRotation;

		// Token: 0x0400103C RID: 4156
		private int m_IsHitscanAttack;

		// Token: 0x0400103D RID: 4157
		private int m_HasLineOfSight;

		// Token: 0x0400103E RID: 4158
		private int m_IsCriticalHit;

		// Token: 0x0400103F RID: 4159
		private uint m_HitBoneId_DEPRECATED;

		// Token: 0x04001040 RID: 4160
		private float m_DamageTaken;

		// Token: 0x04001041 RID: 4161
		private float m_HealthRemaining;

		// Token: 0x04001042 RID: 4162
		private AntiCheatCommonPlayerTakeDamageSource m_DamageSource;

		// Token: 0x04001043 RID: 4163
		private AntiCheatCommonPlayerTakeDamageType m_DamageType;

		// Token: 0x04001044 RID: 4164
		private AntiCheatCommonPlayerTakeDamageResult m_DamageResult;

		// Token: 0x04001045 RID: 4165
		private IntPtr m_PlayerUseWeaponData;

		// Token: 0x04001046 RID: 4166
		private uint m_TimeSincePlayerUseWeaponMs;

		// Token: 0x04001047 RID: 4167
		private IntPtr m_DamagePosition;
	}
}
