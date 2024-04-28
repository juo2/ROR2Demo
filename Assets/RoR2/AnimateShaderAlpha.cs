using System;
using ThreeEyedGames;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005C9 RID: 1481
	public class AnimateShaderAlpha : MonoBehaviour
	{
		// Token: 0x06001ACA RID: 6858 RVA: 0x00072FA4 File Offset: 0x000711A4
		private void Start()
		{
			this.targetRenderer = base.GetComponent<Renderer>();
			if (this.targetRenderer)
			{
				this.materials = this.targetRenderer.materials;
			}
			if (this.decal)
			{
				this.initialFade = this.decal.Fade;
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x00072FFC File Offset: 0x000711FC
		private void Update()
		{
			if (!this.pauseTime)
			{
				this.time = Mathf.Min(this.timeMax, this.time + Time.deltaTime);
			}
			float num = this.alphaCurve.Evaluate(this.time / this.timeMax);
			if (this.decal)
			{
				this.decal.Fade = num * this.initialFade;
			}
			else
			{
				foreach (Material material in this.materials)
				{
					this._propBlock = new MaterialPropertyBlock();
					this.targetRenderer.GetPropertyBlock(this._propBlock);
					this._propBlock.SetFloat("_ExternalAlpha", num);
					this.targetRenderer.SetPropertyBlock(this._propBlock);
				}
			}
			if (this.time >= this.timeMax)
			{
				if (this.disableOnEnd)
				{
					base.enabled = false;
				}
				if (this.destroyOnEnd)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x040020F2 RID: 8434
		public AnimationCurve alphaCurve;

		// Token: 0x040020F3 RID: 8435
		private Renderer targetRenderer;

		// Token: 0x040020F4 RID: 8436
		private MaterialPropertyBlock _propBlock;

		// Token: 0x040020F5 RID: 8437
		private Material[] materials;

		// Token: 0x040020F6 RID: 8438
		public float timeMax = 5f;

		// Token: 0x040020F7 RID: 8439
		[Tooltip("Optional field if you want to animate Decal 'Fade' rather than renderer _ExternalAlpha.")]
		public Decal decal;

		// Token: 0x040020F8 RID: 8440
		public bool pauseTime;

		// Token: 0x040020F9 RID: 8441
		public bool destroyOnEnd;

		// Token: 0x040020FA RID: 8442
		public bool disableOnEnd;

		// Token: 0x040020FB RID: 8443
		[HideInInspector]
		public float time;

		// Token: 0x040020FC RID: 8444
		private float initialFade;
	}
}
