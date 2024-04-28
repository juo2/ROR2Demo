using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BDF RID: 3039
	public class NovaOnLowHealthBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044FB RID: 17659 RVA: 0x0011F367 File Offset: 0x0011D567
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.NovaOnLowHealth;
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x0011F36E File Offset: 0x0011D56E
		private void Start()
		{
			this.attachment = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BodyAttachments/VagrantNovaItemBodyAttachment")).GetComponent<NetworkedBodyAttachment>();
			this.attachment.AttachToGameObjectAndSpawn(base.body.gameObject, null);
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x0011F3A1 File Offset: 0x0011D5A1
		private void FixedUpdate()
		{
			if (!base.body.healthComponent.alive)
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x0011F3BB File Offset: 0x0011D5BB
		private void OnDestroy()
		{
			if (this.attachment)
			{
				UnityEngine.Object.Destroy(this.attachment.gameObject);
				this.attachment = null;
			}
		}

		// Token: 0x04004366 RID: 17254
		private NetworkedBodyAttachment attachment;
	}
}
