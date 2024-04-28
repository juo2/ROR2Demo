using System;

namespace RoR2
{
	// Token: 0x020007DB RID: 2011
	public interface INetworkedBodyAttachmentListener
	{
		// Token: 0x06002B81 RID: 11137
		void OnAttachedBodyDiscovered(NetworkedBodyAttachment networkedBodyAttachment, CharacterBody attachedBody);
	}
}
