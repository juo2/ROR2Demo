using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000591 RID: 1425
	public class LogPlayerTakeDamageOptions
	{
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00024478 File Offset: 0x00022678
		// (set) Token: 0x06002249 RID: 8777 RVA: 0x00024480 File Offset: 0x00022680
		public IntPtr VictimPlayerHandle { get; set; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x00024489 File Offset: 0x00022689
		// (set) Token: 0x0600224B RID: 8779 RVA: 0x00024491 File Offset: 0x00022691
		public Vec3f VictimPlayerPosition { get; set; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0002449A File Offset: 0x0002269A
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x000244A2 File Offset: 0x000226A2
		public Quat VictimPlayerViewRotation { get; set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000244AB File Offset: 0x000226AB
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x000244B3 File Offset: 0x000226B3
		public IntPtr AttackerPlayerHandle { get; set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x000244BC File Offset: 0x000226BC
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x000244C4 File Offset: 0x000226C4
		public Vec3f AttackerPlayerPosition { get; set; }

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x000244CD File Offset: 0x000226CD
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x000244D5 File Offset: 0x000226D5
		public Quat AttackerPlayerViewRotation { get; set; }

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000244DE File Offset: 0x000226DE
		// (set) Token: 0x06002255 RID: 8789 RVA: 0x000244E6 File Offset: 0x000226E6
		public bool IsHitscanAttack { get; set; }

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x000244EF File Offset: 0x000226EF
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x000244F7 File Offset: 0x000226F7
		public bool HasLineOfSight { get; set; }

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x00024500 File Offset: 0x00022700
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x00024508 File Offset: 0x00022708
		public bool IsCriticalHit { get; set; }

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x00024511 File Offset: 0x00022711
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x00024519 File Offset: 0x00022719
		public uint HitBoneId_DEPRECATED { get; set; }

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x00024522 File Offset: 0x00022722
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x0002452A File Offset: 0x0002272A
		public float DamageTaken { get; set; }

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x00024533 File Offset: 0x00022733
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x0002453B File Offset: 0x0002273B
		public float HealthRemaining { get; set; }

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x00024544 File Offset: 0x00022744
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x0002454C File Offset: 0x0002274C
		public AntiCheatCommonPlayerTakeDamageSource DamageSource { get; set; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x00024555 File Offset: 0x00022755
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x0002455D File Offset: 0x0002275D
		public AntiCheatCommonPlayerTakeDamageType DamageType { get; set; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x00024566 File Offset: 0x00022766
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x0002456E File Offset: 0x0002276E
		public AntiCheatCommonPlayerTakeDamageResult DamageResult { get; set; }

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x00024577 File Offset: 0x00022777
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x0002457F File Offset: 0x0002277F
		public LogPlayerUseWeaponData PlayerUseWeaponData { get; set; }

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x00024588 File Offset: 0x00022788
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x00024590 File Offset: 0x00022790
		public uint TimeSincePlayerUseWeaponMs { get; set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x00024599 File Offset: 0x00022799
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x000245A1 File Offset: 0x000227A1
		public Vec3f DamagePosition { get; set; }
	}
}
