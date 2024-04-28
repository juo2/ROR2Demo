using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x02000130 RID: 304
	public abstract class BaseMultiBeamState : BaseState
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00016F96 File Offset: 0x00015196
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00016F9E File Offset: 0x0001519E
		private protected Transform muzzleTransform { protected get; private set; }

		// Token: 0x06000565 RID: 1381 RVA: 0x00016FA7 File Offset: 0x000151A7
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(BaseMultiBeamState.muzzleName);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00016FC0 File Offset: 0x000151C0
		protected void CalcBeamPath(out Ray beamRay, out Vector3 beamEndPos)
		{
			Ray aimRay = base.GetAimRay();
			float num = float.PositiveInfinity;
			RaycastHit[] array = Physics.RaycastAll(aimRay, BaseMultiBeamState.beamMaxDistance, LayerIndex.CommonMasks.bullet, QueryTriggerInteraction.Ignore);
			Transform root = base.GetModelTransform().root;
			for (int i = 0; i < array.Length; i++)
			{
				ref RaycastHit ptr = ref array[i];
				float distance = ptr.distance;
				if (distance < num && ptr.collider.transform.root != root)
				{
					num = distance;
				}
			}
			num = Mathf.Min(num, BaseMultiBeamState.beamMaxDistance);
			beamEndPos = aimRay.GetPoint(num);
			Vector3 position = this.muzzleTransform.position;
			beamRay = new Ray(position, beamEndPos - position);
		}

		// Token: 0x04000649 RID: 1609
		public static float beamMaxDistance = 1000f;

		// Token: 0x0400064A RID: 1610
		public static string muzzleName;
	}
}
