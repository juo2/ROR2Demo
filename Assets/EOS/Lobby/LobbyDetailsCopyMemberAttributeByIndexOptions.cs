using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000330 RID: 816
	public class LobbyDetailsCopyMemberAttributeByIndexOptions
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0001598B File Offset: 0x00013B8B
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00015993 File Offset: 0x00013B93
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0001599C File Offset: 0x00013B9C
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x000159A4 File Offset: 0x00013BA4
		public uint AttrIndex { get; set; }
	}
}
