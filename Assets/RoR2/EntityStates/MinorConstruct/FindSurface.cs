using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MinorConstruct
{
	// Token: 0x02000268 RID: 616
	public class FindSurface : NoCastSpawn
	{
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002C268 File Offset: 0x0002A468
		public override void OnEnter()
		{
			RaycastHit raycastHit = default(RaycastHit);
			Vector3 corePosition = base.characterBody.corePosition;
			if (base.isAuthority)
			{
				MinionOwnership minionOwnership = base.characterBody.master.minionOwnership;
				if (!((minionOwnership != null) ? minionOwnership.ownerMaster : null))
				{
					for (int i = 0; i < this.raycastCount; i++)
					{
						if (Physics.Raycast(corePosition, UnityEngine.Random.onUnitSphere, out raycastHit, this.maxRaycastLength, LayerIndex.world.mask))
						{
							base.transform.position = raycastHit.point;
							base.transform.up = raycastHit.normal;
						}
					}
				}
			}
			base.OnEnter();
		}

		// Token: 0x04000C51 RID: 3153
		[SerializeField]
		public int raycastCount;

		// Token: 0x04000C52 RID: 3154
		[SerializeField]
		public float maxRaycastLength;
	}
}
