using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007AB RID: 1963
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	public class MatchCamera : MonoBehaviour
	{
		// Token: 0x0600297A RID: 10618 RVA: 0x000B35B5 File Offset: 0x000B17B5
		private void Awake()
		{
			this.destCamera = base.GetComponent<Camera>();
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000B35C4 File Offset: 0x000B17C4
		private void LateUpdate()
		{
			if (this.srcCamera)
			{
				if (this.matchRect)
				{
					this.destCamera.rect = this.srcCamera.rect;
				}
				if (this.matchFOV)
				{
					this.destCamera.fieldOfView = this.srcCamera.fieldOfView;
				}
				if (this.matchPosition)
				{
					this.destCamera.transform.position = this.srcCamera.transform.position;
					this.destCamera.transform.rotation = this.srcCamera.transform.rotation;
				}
			}
		}

		// Token: 0x04002CD4 RID: 11476
		private Camera destCamera;

		// Token: 0x04002CD5 RID: 11477
		public Camera srcCamera;

		// Token: 0x04002CD6 RID: 11478
		public bool matchFOV = true;

		// Token: 0x04002CD7 RID: 11479
		public bool matchRect = true;

		// Token: 0x04002CD8 RID: 11480
		public bool matchPosition;
	}
}
