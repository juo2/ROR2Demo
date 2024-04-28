using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000598 RID: 1432
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerUseWeaponDataInternal : ISettable, IDisposable
	{
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00024B03 File Offset: 0x00022D03
		// (set) Token: 0x060022B8 RID: 8888 RVA: 0x00024B0B File Offset: 0x00022D0B
		public IntPtr PlayerHandle
		{
			get
			{
				return this.m_PlayerHandle;
			}
			set
			{
				this.m_PlayerHandle = value;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x00024B14 File Offset: 0x00022D14
		// (set) Token: 0x060022BA RID: 8890 RVA: 0x00024B30 File Offset: 0x00022D30
		public Vec3f PlayerPosition
		{
			get
			{
				Vec3f result;
				Helper.TryMarshalGet<Vec3fInternal, Vec3f>(this.m_PlayerPosition, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<Vec3fInternal, Vec3f>(ref this.m_PlayerPosition, value);
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x00024B40 File Offset: 0x00022D40
		// (set) Token: 0x060022BC RID: 8892 RVA: 0x00024B5C File Offset: 0x00022D5C
		public Quat PlayerViewRotation
		{
			get
			{
				Quat result;
				Helper.TryMarshalGet<QuatInternal, Quat>(this.m_PlayerViewRotation, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<QuatInternal, Quat>(ref this.m_PlayerViewRotation, value);
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x00024B6C File Offset: 0x00022D6C
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x00024B88 File Offset: 0x00022D88
		public bool IsPlayerViewZoomed
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsPlayerViewZoomed, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_IsPlayerViewZoomed, value);
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00024B98 File Offset: 0x00022D98
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x00024BB4 File Offset: 0x00022DB4
		public bool IsMeleeAttack
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_IsMeleeAttack, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_IsMeleeAttack, value);
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x00024BC4 File Offset: 0x00022DC4
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x00024BE0 File Offset: 0x00022DE0
		public string WeaponName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_WeaponName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_WeaponName, value);
			}
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x00024BF0 File Offset: 0x00022DF0
		public void Set(LogPlayerUseWeaponData other)
		{
			if (other != null)
			{
				this.PlayerHandle = other.PlayerHandle;
				this.PlayerPosition = other.PlayerPosition;
				this.PlayerViewRotation = other.PlayerViewRotation;
				this.IsPlayerViewZoomed = other.IsPlayerViewZoomed;
				this.IsMeleeAttack = other.IsMeleeAttack;
				this.WeaponName = other.WeaponName;
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x00024C48 File Offset: 0x00022E48
		public void Set(object other)
		{
			this.Set(other as LogPlayerUseWeaponData);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x00024C56 File Offset: 0x00022E56
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PlayerHandle);
			Helper.TryMarshalDispose(ref this.m_PlayerPosition);
			Helper.TryMarshalDispose(ref this.m_PlayerViewRotation);
			Helper.TryMarshalDispose(ref this.m_WeaponName);
		}

		// Token: 0x04001064 RID: 4196
		private IntPtr m_PlayerHandle;

		// Token: 0x04001065 RID: 4197
		private IntPtr m_PlayerPosition;

		// Token: 0x04001066 RID: 4198
		private IntPtr m_PlayerViewRotation;

		// Token: 0x04001067 RID: 4199
		private int m_IsPlayerViewZoomed;

		// Token: 0x04001068 RID: 4200
		private int m_IsMeleeAttack;

		// Token: 0x04001069 RID: 4201
		private IntPtr m_WeaponName;
	}
}
