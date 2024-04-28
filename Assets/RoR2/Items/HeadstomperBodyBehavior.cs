using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BD8 RID: 3032
	public class HeadstomperBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044DB RID: 17627 RVA: 0x0011EBDC File Offset: 0x0011CDDC
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.FallBoots;
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x0011EBE3 File Offset: 0x0011CDE3
		private void OnEnable()
		{
			this.headstompersControllerObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/HeadstompersController"));
			this.headstompersControllerObject.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.body.gameObject, null);
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x0011EC16 File Offset: 0x0011CE16
		private void OnDisable()
		{
			if (this.headstompersControllerObject)
			{
				UnityEngine.Object.Destroy(this.headstompersControllerObject);
				this.headstompersControllerObject = null;
			}
		}

		// Token: 0x04004353 RID: 17235
		private GameObject headstompersControllerObject;
	}
}
