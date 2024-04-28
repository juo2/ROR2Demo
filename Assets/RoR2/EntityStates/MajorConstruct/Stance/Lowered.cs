using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.MajorConstruct.Stance
{
	// Token: 0x0200028E RID: 654
	public class Lowered : BaseState
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x0003029C File Offset: 0x0002E49C
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active && this.attachmentPrefab)
			{
				this.attachment = UnityEngine.Object.Instantiate<GameObject>(this.attachmentPrefab).GetComponent<NetworkedBodyAttachment>();
				this.attachment.AttachToGameObjectAndSpawn(base.characterBody.gameObject, null);
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000302F0 File Offset: 0x0002E4F0
		public override void OnExit()
		{
			if (this.attachment)
			{
				EntityState.Destroy(this.attachment.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x04000DA5 RID: 3493
		[SerializeField]
		public GameObject attachmentPrefab;

		// Token: 0x04000DA6 RID: 3494
		private NetworkedBodyAttachment attachment;
	}
}
