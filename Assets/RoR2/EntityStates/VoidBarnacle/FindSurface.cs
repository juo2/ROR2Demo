using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidBarnacle
{
	// Token: 0x02000161 RID: 353
	public class FindSurface : NoCastSpawn
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		public override void OnEnter()
		{
			base.OnEnter();
			RaycastHit raycastHit = default(RaycastHit);
			Vector3 origin = new Vector3(base.characterBody.corePosition.x, base.characterBody.corePosition.y + FindSurface.raycastSphereYOffset, base.characterBody.corePosition.z);
			if (base.isAuthority)
			{
				FindSurface.raycastMinimumAngle = Mathf.Clamp(FindSurface.raycastMinimumAngle, 0f, FindSurface.raycastMaximumAngle);
				FindSurface.raycastMaximumAngle = Mathf.Clamp(FindSurface.raycastMaximumAngle, FindSurface.raycastMinimumAngle, 90f);
				FindSurface.raycastCount = Mathf.Abs(FindSurface.raycastCount);
				float num = 360f / (float)FindSurface.raycastCount;
				for (int i = 0; i < FindSurface.raycastCount; i++)
				{
					float min = num * (float)i;
					float max = num * (float)(i + 1) - 1f;
					float f = UnityEngine.Random.Range(min, max) * 0.017453292f;
					float f2 = UnityEngine.Random.Range(FindSurface.raycastMinimumAngle, FindSurface.raycastMaximumAngle) * 0.017453292f;
					float x = Mathf.Cos(f);
					float y = Mathf.Sin(f2);
					float z = Mathf.Sin(f);
					Vector3 direction = new Vector3(x, y, z);
					if (Physics.Raycast(origin, direction, out raycastHit, FindSurface.maxRaycastLength, LayerIndex.world.mask))
					{
						base.transform.position = raycastHit.point;
						base.transform.up = raycastHit.normal;
						return;
					}
				}
			}
		}

		// Token: 0x0400078D RID: 1933
		public static int raycastCount;

		// Token: 0x0400078E RID: 1934
		public static float maxRaycastLength;

		// Token: 0x0400078F RID: 1935
		public static float raycastSphereYOffset;

		// Token: 0x04000790 RID: 1936
		public static float raycastMinimumAngle;

		// Token: 0x04000791 RID: 1937
		public static float raycastMaximumAngle;

		// Token: 0x04000792 RID: 1938
		private const float _cRadianConversionCoeficcient = 0.017453292f;
	}
}
