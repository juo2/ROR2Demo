using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020007DF RID: 2015
	[RequireComponent(typeof(ParticleSystem))]
	[ExecuteAlways]
	public class NormalizeParticleScale : MonoBehaviour
	{
		// Token: 0x06002B8B RID: 11147 RVA: 0x000BAC36 File Offset: 0x000B8E36
		public void OnEnable()
		{
			this.UpdateParticleSystem();
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000BAC40 File Offset: 0x000B8E40
		private void UpdateParticleSystem()
		{
			if (!this.particleSystem)
			{
				this.particleSystem = base.GetComponent<ParticleSystem>();
			}
			ParticleSystem.MainModule main = this.particleSystem.main;
			ParticleSystem.MinMaxCurve startSize = main.startSize;
			Vector3 lossyScale = base.transform.lossyScale;
			if (this.normalizeWithSkinnedMeshRendererInstead)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = this.particleSystem.shape.skinnedMeshRenderer;
				if (skinnedMeshRenderer)
				{
					lossyScale = skinnedMeshRenderer.transform.lossyScale;
				}
			}
			float num = Mathf.Max(new float[]
			{
				lossyScale.x,
				lossyScale.y,
				lossyScale.z
			});
			startSize.constantMin /= num;
			startSize.constantMax /= num;
			main.startSize = startSize;
		}

		// Token: 0x04002DFD RID: 11773
		public bool normalizeWithSkinnedMeshRendererInstead;

		// Token: 0x04002DFE RID: 11774
		private ParticleSystem particleSystem;
	}
}
