using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000928 RID: 2344
	public static class HGPhysics
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x000E03AC File Offset: 0x000DE5AC
		// (set) Token: 0x060034F4 RID: 13556 RVA: 0x000E03B4 File Offset: 0x000DE5B4
		public static int sharedCollidersBufferEntriesCount
		{
			get
			{
				return HGPhysics._sharedCollidersBufferEntriesCount;
			}
			private set
			{
				int num = HGPhysics.sharedCollidersBufferEntriesCount - value;
				if (num > 0)
				{
					Array.Clear(HGPhysics.sharedCollidersBuffer, HGPhysics.sharedCollidersBufferEntriesCount, num);
				}
				HGPhysics._sharedCollidersBufferEntriesCount = value;
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000E03E3 File Offset: 0x000DE5E3
		public static int OverlapBoxNonAllocShared(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return HGPhysics.sharedCollidersBufferEntriesCount = Physics.OverlapBoxNonAlloc(center, halfExtents, HGPhysics.sharedCollidersBuffer, orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000E03FB File Offset: 0x000DE5FB
		public static int OverlapSphereNonAllocShared(Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return HGPhysics.sharedCollidersBufferEntriesCount = Physics.OverlapSphereNonAlloc(position, radius, HGPhysics.sharedCollidersBuffer, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000E0411 File Offset: 0x000DE611
		public static float CalculateDistance(float initialVelocity, float acceleration, float time)
		{
			return initialVelocity * time + 0.5f * acceleration * time * time;
		}

		// Token: 0x040035DE RID: 13790
		public static readonly Collider[] sharedCollidersBuffer = new Collider[65536];

		// Token: 0x040035DF RID: 13791
		private static int _sharedCollidersBufferEntriesCount = 0;
	}
}
