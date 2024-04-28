using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007AC RID: 1964
	[ExecuteAlways]
	public class MatchTransform : MonoBehaviour
	{
		// Token: 0x0600297D RID: 10621 RVA: 0x000B367B File Offset: 0x000B187B
		public void LateUpdate()
		{
			this.UpdateNow();
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000B3683 File Offset: 0x000B1883
		[ContextMenu("Update Now")]
		public void UpdateNow()
		{
			if (this.targetTransform)
			{
				base.transform.SetPositionAndRotation(this.targetTransform.position, this.targetTransform.rotation);
			}
		}

		// Token: 0x04002CD9 RID: 11481
		public Transform targetTransform;
	}
}
