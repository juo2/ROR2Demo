using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200083F RID: 2111
	public class RandomizeSplatBias : MonoBehaviour
	{
		// Token: 0x06002E12 RID: 11794 RVA: 0x000C42BF File Offset: 0x000C24BF
		private void Start()
		{
			this.materialsList = new List<Material>();
			this.rendererList = new List<Renderer>();
			this.printShader = LegacyResourcesAPI.Load<Shader>("Shaders/Deferred/HGStandard");
			this.Setup();
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000C42F0 File Offset: 0x000C24F0
		private void Setup()
		{
			this.characterModel = base.GetComponent<CharacterModel>();
			if (this.characterModel)
			{
				for (int i = 0; i < this.characterModel.baseRendererInfos.Length; i++)
				{
					CharacterModel.RendererInfo rendererInfo = this.characterModel.baseRendererInfos[i];
					Material material = UnityEngine.Object.Instantiate<Material>(rendererInfo.defaultMaterial);
					if (material.shader == this.printShader)
					{
						this.materialsList.Add(material);
						this.rendererList.Add(rendererInfo.renderer);
						rendererInfo.defaultMaterial = material;
						this.characterModel.baseRendererInfos[i] = rendererInfo;
					}
					Renderer renderer = this.rendererList[i];
					this._propBlock = new MaterialPropertyBlock();
					renderer.GetPropertyBlock(this._propBlock);
					this._propBlock.SetFloat("_RedChannelBias", UnityEngine.Random.Range(this.minRedBias, this.maxRedBias));
					this._propBlock.SetFloat("_BlueChannelBias", UnityEngine.Random.Range(this.minBlueBias, this.maxBlueBias));
					this._propBlock.SetFloat("_GreenChannelBias", UnityEngine.Random.Range(this.minGreenBias, this.maxGreenBias));
					renderer.SetPropertyBlock(this._propBlock);
				}
				return;
			}
			Renderer componentInChildren = base.GetComponentInChildren<Renderer>();
			Material material2 = UnityEngine.Object.Instantiate<Material>(componentInChildren.material);
			this.materialsList.Add(material2);
			componentInChildren.material = material2;
			this._propBlock = new MaterialPropertyBlock();
			componentInChildren.GetPropertyBlock(this._propBlock);
			this._propBlock.SetFloat("_RedChannelBias", UnityEngine.Random.Range(this.minRedBias, this.maxRedBias));
			this._propBlock.SetFloat("_BlueChannelBias", UnityEngine.Random.Range(this.minBlueBias, this.maxBlueBias));
			this._propBlock.SetFloat("_GreenChannelBias", UnityEngine.Random.Range(this.minGreenBias, this.maxGreenBias));
			componentInChildren.SetPropertyBlock(this._propBlock);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000C44DC File Offset: 0x000C26DC
		private void OnDestroy()
		{
			if (this.materialsList != null)
			{
				for (int i = 0; i < this.materialsList.Count; i++)
				{
					UnityEngine.Object.Destroy(this.materialsList[i]);
				}
			}
		}

		// Token: 0x04002FFD RID: 12285
		public float minRedBias;

		// Token: 0x04002FFE RID: 12286
		public float maxRedBias;

		// Token: 0x04002FFF RID: 12287
		public float minGreenBias;

		// Token: 0x04003000 RID: 12288
		public float maxGreenBias;

		// Token: 0x04003001 RID: 12289
		public float minBlueBias;

		// Token: 0x04003002 RID: 12290
		public float maxBlueBias;

		// Token: 0x04003003 RID: 12291
		private MaterialPropertyBlock _propBlock;

		// Token: 0x04003004 RID: 12292
		private CharacterModel characterModel;

		// Token: 0x04003005 RID: 12293
		private List<Material> materialsList;

		// Token: 0x04003006 RID: 12294
		private List<Renderer> rendererList;

		// Token: 0x04003007 RID: 12295
		private Shader printShader;
	}
}
