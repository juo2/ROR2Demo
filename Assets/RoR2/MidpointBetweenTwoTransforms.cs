using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007B0 RID: 1968
	[ExecuteAlways]
	public class MidpointBetweenTwoTransforms : MonoBehaviour
	{
		// Token: 0x06002989 RID: 10633 RVA: 0x000B3BF6 File Offset: 0x000B1DF6
		public void Update()
		{
			base.transform.position = Vector3.Lerp(this.transform1.position, this.transform2.position, 0.5f);
		}

		// Token: 0x04002CF7 RID: 11511
		public Transform transform1;

		// Token: 0x04002CF8 RID: 11512
		public Transform transform2;
	}
}
