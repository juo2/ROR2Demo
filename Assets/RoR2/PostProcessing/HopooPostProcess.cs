using System;
using UnityEngine;

namespace RoR2.PostProcessing
{
	// Token: 0x02000BBE RID: 3006
	[ExecuteInEditMode]
	public class HopooPostProcess : MonoBehaviour
	{
		// Token: 0x0600447C RID: 17532 RVA: 0x000026ED File Offset: 0x000008ED
		private void Start()
		{
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x0011D24A File Offset: 0x0011B44A
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination, this.mat);
		}

		// Token: 0x040042F1 RID: 17137
		public Material mat;
	}
}
