using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008C6 RID: 2246
	[RequireComponent(typeof(BezierCurveLine))]
	public class TetherVfx : MonoBehaviour
	{
		// Token: 0x06003249 RID: 12873 RVA: 0x000D45F2 File Offset: 0x000D27F2
		public void Update()
		{
			if (this.tetherTargetTransform && this.tetherEndTransform)
			{
				this.tetherEndTransform.position = this.tetherTargetTransform.position;
			}
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000D4624 File Offset: 0x000D2824
		public void Terminate()
		{
			if (this.fadeOut)
			{
				this.fadeOut.enabled = true;
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0400336F RID: 13167
		public AnimateShaderAlpha fadeOut;

		// Token: 0x04003370 RID: 13168
		[Tooltip("The transform to position at the target.")]
		public Transform tetherEndTransform;

		// Token: 0x04003371 RID: 13169
		[Tooltip("The transform to position the end to.")]
		public Transform tetherTargetTransform;
	}
}
