using System;
using RoR2;

namespace EntityStates
{
	// Token: 0x020000AF RID: 175
	public class BaseBodyAttachmentState : EntityState
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B963 File Offset: 0x00009B63
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000B96B File Offset: 0x00009B6B
		private protected NetworkedBodyAttachment bodyAttachment { protected get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000B974 File Offset: 0x00009B74
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000B97C File Offset: 0x00009B7C
		private protected CharacterBody attachedBody { protected get; private set; }

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B985 File Offset: 0x00009B85
		public override void OnEnter()
		{
			base.OnEnter();
			this.bodyAttachment = base.GetComponent<NetworkedBodyAttachment>();
			this.attachedBody = (this.bodyAttachment ? this.bodyAttachment.attachedBody : null);
		}
	}
}
