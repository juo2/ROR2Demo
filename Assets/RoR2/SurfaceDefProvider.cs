using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008AF RID: 2223
	public class SurfaceDefProvider : MonoBehaviour
	{
		// Token: 0x06003152 RID: 12626 RVA: 0x000D1804 File Offset: 0x000CFA04
		public static SurfaceDef GetObjectSurfaceDef(Collider collider, Vector3 position)
		{
			SurfaceDefProvider component = collider.GetComponent<SurfaceDefProvider>();
			if (!component)
			{
				return null;
			}
			return component.surfaceDef;
		}

		// Token: 0x040032E8 RID: 13032
		[Tooltip("The primary surface definition. Use this when not tying to a splatmap.")]
		public SurfaceDef surfaceDef;
	}
}
