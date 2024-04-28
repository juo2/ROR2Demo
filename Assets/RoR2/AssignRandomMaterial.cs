using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005E2 RID: 1506
	public class AssignRandomMaterial : MonoBehaviour
	{
		// Token: 0x06001B5A RID: 7002 RVA: 0x00075217 File Offset: 0x00073417
		private void Awake()
		{
			this.rend.material = this.materials[UnityEngine.Random.Range(0, this.materials.Length)];
		}

		// Token: 0x0400215D RID: 8541
		public Renderer rend;

		// Token: 0x0400215E RID: 8542
		public Material[] materials;
	}
}
