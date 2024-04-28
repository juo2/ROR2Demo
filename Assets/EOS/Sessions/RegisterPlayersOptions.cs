using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FC RID: 252
	public class RegisterPlayersOptions
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00008102 File Offset: 0x00006302
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x0000810A File Offset: 0x0000630A
		public string SessionName { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00008113 File Offset: 0x00006313
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x0000811B File Offset: 0x0000631B
		public ProductUserId[] PlayersToRegister { get; set; }
	}
}
