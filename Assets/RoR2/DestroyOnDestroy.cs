using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200069C RID: 1692
	public class DestroyOnDestroy : MonoBehaviour
	{
		// Token: 0x06002111 RID: 8465 RVA: 0x0008DE1D File Offset: 0x0008C01D
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.target);
		}

		// Token: 0x04002668 RID: 9832
		[Tooltip("The GameObject to destroy when this object is destroyed.")]
		public GameObject target;
	}
}
