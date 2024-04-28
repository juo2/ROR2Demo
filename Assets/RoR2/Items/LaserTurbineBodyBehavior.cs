using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BDA RID: 3034
	public class LaserTurbineBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044E5 RID: 17637 RVA: 0x0011ED07 File Offset: 0x0011CF07
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.LaserTurbine;
		}

		// Token: 0x060044E6 RID: 17638 RVA: 0x0011ED10 File Offset: 0x0011CF10
		private void OnEnable()
		{
			this.laserTurbineControllerInstance = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/LaserTurbineController"), base.body.corePosition, Quaternion.identity);
			this.laserTurbineControllerInstance.GetComponent<GenericOwnership>().ownerObject = base.gameObject;
			this.laserTurbineControllerInstance.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.gameObject, null);
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x0011ED6F File Offset: 0x0011CF6F
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.laserTurbineControllerInstance);
			this.laserTurbineControllerInstance = null;
		}

		// Token: 0x04004356 RID: 17238
		private GameObject laserTurbineControllerInstance;
	}
}
