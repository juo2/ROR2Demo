using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000887 RID: 2183
	public class SetGravity : MonoBehaviour
	{
		// Token: 0x06002FD9 RID: 12249 RVA: 0x000CBABF File Offset: 0x000C9CBF
		private void OnEnable()
		{
			Physics.gravity = new Vector3(0f, this.newGravity, 0f);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000CBADB File Offset: 0x000C9CDB
		private void OnDisable()
		{
			Physics.gravity = new Vector3(0f, Run.baseGravity, 0f);
		}

		// Token: 0x0400319A RID: 12698
		public float newGravity = -30f;
	}
}
