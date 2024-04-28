using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000593 RID: 1427
	public class LogPlayerTickOptions
	{
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000247F1 File Offset: 0x000229F1
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x000247F9 File Offset: 0x000229F9
		public IntPtr PlayerHandle { get; set; }

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x00024802 File Offset: 0x00022A02
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x0002480A File Offset: 0x00022A0A
		public Vec3f PlayerPosition { get; set; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x00024813 File Offset: 0x00022A13
		// (set) Token: 0x06002287 RID: 8839 RVA: 0x0002481B File Offset: 0x00022A1B
		public Quat PlayerViewRotation { get; set; }

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x00024824 File Offset: 0x00022A24
		// (set) Token: 0x06002289 RID: 8841 RVA: 0x0002482C File Offset: 0x00022A2C
		public bool IsPlayerViewZoomed { get; set; }

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x00024835 File Offset: 0x00022A35
		// (set) Token: 0x0600228B RID: 8843 RVA: 0x0002483D File Offset: 0x00022A3D
		public float PlayerHealth { get; set; }

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x00024846 File Offset: 0x00022A46
		// (set) Token: 0x0600228D RID: 8845 RVA: 0x0002484E File Offset: 0x00022A4E
		public AntiCheatCommonPlayerMovementState PlayerMovementState { get; set; }
	}
}
