using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BD2 RID: 3026
	public class CaptainDefenseMatrixBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044C2 RID: 17602 RVA: 0x0011E5B8 File Offset: 0x0011C7B8
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.CaptainDefenseMatrix;
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x0011E5BF File Offset: 0x0011C7BF
		private void OnDisable()
		{
			this.attachmentActive = false;
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x0011E5C8 File Offset: 0x0011C7C8
		private void FixedUpdate()
		{
			this.attachmentActive = base.body.healthComponent.alive;
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060044C5 RID: 17605 RVA: 0x0011E5E0 File Offset: 0x0011C7E0
		// (set) Token: 0x060044C6 RID: 17606 RVA: 0x0011E5EC File Offset: 0x0011C7EC
		private bool attachmentActive
		{
			get
			{
				return this.attachment != null;
			}
			set
			{
				if (value == this.attachmentActive)
				{
					return;
				}
				if (value)
				{
					this.attachmentGameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BodyAttachments/CaptainDefenseMatrixItemBodyAttachment"));
					this.attachment = this.attachmentGameObject.GetComponent<NetworkedBodyAttachment>();
					this.attachment.AttachToGameObjectAndSpawn(base.body.gameObject, null);
					return;
				}
				UnityEngine.Object.Destroy(this.attachmentGameObject);
				this.attachmentGameObject = null;
				this.attachment = null;
			}
		}

		// Token: 0x04004340 RID: 17216
		private GameObject attachmentGameObject;

		// Token: 0x04004341 RID: 17217
		private NetworkedBodyAttachment attachment;
	}
}
