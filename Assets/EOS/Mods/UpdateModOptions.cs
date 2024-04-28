using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002DA RID: 730
	public class UpdateModOptions
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00013AE8 File Offset: 0x00011CE8
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x00013AF0 File Offset: 0x00011CF0
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00013AF9 File Offset: 0x00011CF9
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x00013B01 File Offset: 0x00011D01
		public ModIdentifier Mod { get; set; }
	}
}
