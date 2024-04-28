using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BE8 RID: 3048
	public class SiphonOnLowHealthItemBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x06004522 RID: 17698 RVA: 0x0011FD5D File Offset: 0x0011DF5D
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.SiphonOnLowHealth;
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x0011FD64 File Offset: 0x0011DF64
		private void OnEnable()
		{
			this.attachment = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BodyAttachments/SiphonNearbyBodyAttachment")).GetComponent<NetworkedBodyAttachment>();
			this.attachment.AttachToGameObjectAndSpawn(base.body.gameObject, null);
			this.siphonNearbyController = this.attachment.GetComponent<SiphonNearbyController>();
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x0011FDB3 File Offset: 0x0011DFB3
		private void OnDisable()
		{
			this.DestroyAttachment();
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x0011FDBB File Offset: 0x0011DFBB
		private void FixedUpdate()
		{
			this.siphonNearbyController.NetworkmaxTargets = (base.body.healthComponent.alive ? this.stack : 0);
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0011FDE3 File Offset: 0x0011DFE3
		private void DestroyAttachment()
		{
			if (this.attachment)
			{
				UnityEngine.Object.Destroy(this.attachment.gameObject);
			}
			this.attachment = null;
			this.siphonNearbyController = null;
		}

		// Token: 0x04004384 RID: 17284
		private NetworkedBodyAttachment attachment;

		// Token: 0x04004385 RID: 17285
		private SiphonNearbyController siphonNearbyController;
	}
}
