using System;

namespace RoR2
{
	// Token: 0x020009B0 RID: 2480
	public class SaveSystemEOS : SaveSystemSteam
	{
		// Token: 0x060038AF RID: 14511 RVA: 0x000EDB96 File Offset: 0x000EBD96
		public override string GetPlatformUsernameOrDefault(string defaultName)
		{
			return PlatformSystems.userManager.GetUserName();
		}
	}
}
