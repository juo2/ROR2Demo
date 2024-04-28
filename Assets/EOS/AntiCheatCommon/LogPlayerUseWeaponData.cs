using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000597 RID: 1431
	public class LogPlayerUseWeaponData : ISettable
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000249F3 File Offset: 0x00022BF3
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x000249FB File Offset: 0x00022BFB
		public IntPtr PlayerHandle { get; set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x00024A04 File Offset: 0x00022C04
		// (set) Token: 0x060022AB RID: 8875 RVA: 0x00024A0C File Offset: 0x00022C0C
		public Vec3f PlayerPosition { get; set; }

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x00024A15 File Offset: 0x00022C15
		// (set) Token: 0x060022AD RID: 8877 RVA: 0x00024A1D File Offset: 0x00022C1D
		public Quat PlayerViewRotation { get; set; }

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x00024A26 File Offset: 0x00022C26
		// (set) Token: 0x060022AF RID: 8879 RVA: 0x00024A2E File Offset: 0x00022C2E
		public bool IsPlayerViewZoomed { get; set; }

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00024A37 File Offset: 0x00022C37
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x00024A3F File Offset: 0x00022C3F
		public bool IsMeleeAttack { get; set; }

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x00024A48 File Offset: 0x00022C48
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x00024A50 File Offset: 0x00022C50
		public string WeaponName { get; set; }

		// Token: 0x060022B4 RID: 8884 RVA: 0x00024A5C File Offset: 0x00022C5C
		internal void Set(LogPlayerUseWeaponDataInternal? other)
		{
			if (other != null)
			{
				this.PlayerHandle = other.Value.PlayerHandle;
				this.PlayerPosition = other.Value.PlayerPosition;
				this.PlayerViewRotation = other.Value.PlayerViewRotation;
				this.IsPlayerViewZoomed = other.Value.IsPlayerViewZoomed;
				this.IsMeleeAttack = other.Value.IsMeleeAttack;
				this.WeaponName = other.Value.WeaponName;
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00024AF0 File Offset: 0x00022CF0
		public void Set(object other)
		{
			this.Set(other as LogPlayerUseWeaponDataInternal?);
		}
	}
}
