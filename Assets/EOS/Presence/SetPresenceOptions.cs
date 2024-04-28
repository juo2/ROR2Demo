using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000233 RID: 563
	public class SetPresenceOptions
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0000FBD9 File Offset: 0x0000DDD9
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x0000FBE1 File Offset: 0x0000DDE1
		public PresenceModification PresenceModificationHandle { get; set; }
	}
}
