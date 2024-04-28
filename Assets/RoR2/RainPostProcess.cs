using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200083E RID: 2110
	[ExecuteInEditMode]
	public class RainPostProcess : MonoBehaviour
	{
		// Token: 0x06002E0E RID: 11790 RVA: 0x000C42A2 File Offset: 0x000C24A2
		private void Start()
		{
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000C42B0 File Offset: 0x000C24B0
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination, this.mat);
		}

		// Token: 0x04002FFB RID: 12283
		public Material mat;

		// Token: 0x04002FFC RID: 12284
		private RenderTexture renderTex;
	}
}
