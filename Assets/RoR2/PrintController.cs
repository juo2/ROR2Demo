using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200082D RID: 2093
	public class PrintController : MonoBehaviour
	{
		// Token: 0x06002D85 RID: 11653 RVA: 0x000C1FCC File Offset: 0x000C01CC
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			PrintController.printShader = LegacyResourcesAPI.Load<Shader>("Shaders/Deferred/HGStandard");
			PrintController.sliceHeightShaderPropertyId = Shader.PropertyToID("_SliceHeight");
			PrintController.printBiasShaderPropertyId = Shader.PropertyToID("_PrintBias");
			PrintController.flowHeightPowerShaderPropertyId = Shader.PropertyToID("_FlowHeightPower");
			PrintController.printOnPropertyId = Shader.PropertyToID("_PrintOn");
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000C2024 File Offset: 0x000C0224
		private void Awake()
		{
			this.characterModel = base.GetComponent<CharacterModel>();
			this._propBlock = new MaterialPropertyBlock();
			this.SetupPrint();
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000C2043 File Offset: 0x000C0243
		private void OnDisable()
		{
			this.SetMaterialPrintCutoffEnabled(false);
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000C204C File Offset: 0x000C024C
		private void OnEnable()
		{
			this.SetMaterialPrintCutoffEnabled(true);
			this.age = 0f;
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000C2060 File Offset: 0x000C0260
		private void Update()
		{
			this.UpdatePrint(Time.deltaTime);
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000C2070 File Offset: 0x000C0270
		private void OnDestroy()
		{
			for (int i = this.rendererMaterialPairs.Length - 1; i > 0; i--)
			{
				UnityEngine.Object.Destroy(this.rendererMaterialPairs[i].material);
				this.rendererMaterialPairs[i] = new PrintController.RendererMaterialPair(null, null);
			}
			this.rendererMaterialPairs = Array.Empty<PrintController.RendererMaterialPair>();
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000C20C8 File Offset: 0x000C02C8
		private void OnValidate()
		{
			if (this.printCurve == null)
			{
				this.printCurve = new AnimationCurve();
			}
			Keyframe[] keys = this.printCurve.keys;
			for (int i = 1; i < keys.Length; i++)
			{
				Keyframe[] array = keys;
				int num = i - 1;
				ref Keyframe ptr = ref keys[i];
				if (array[num].time >= ptr.time)
				{
					Debug.LogErrorFormat("Animation curve error on object {0}", new object[]
					{
						base.gameObject.name
					});
					break;
				}
			}
			if (this.printTime == 0f)
			{
				Debug.LogErrorFormat("printTime==0f on object {0}", new object[]
				{
					base.gameObject.name
				});
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000C216B File Offset: 0x000C036B
		public void SetPaused(bool newPaused)
		{
			this.paused = newPaused;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000C2174 File Offset: 0x000C0374
		private void UpdatePrint(float deltaTime)
		{
			if (this.printCurve == null)
			{
				return;
			}
			if (!this.paused)
			{
				this.age += deltaTime;
			}
			float printThreshold = this.printCurve.Evaluate(this.age / this.printTime);
			this.SetPrintThreshold(printThreshold);
			if (this.age >= this.printTime && this.disableWhenFinished)
			{
				base.enabled = false;
				this.age = 0f;
			}
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000C21E8 File Offset: 0x000C03E8
		private void SetPrintThreshold(float sample)
		{
			float num = 1f - sample;
			float value = sample * this.maxPrintHeight + num * this.startingPrintHeight;
			float value2 = sample * this.maxPrintBias + num * this.startingPrintBias;
			float value3 = sample * this.maxFlowmapPower + num * this.startingFlowmapPower;
			for (int i = 0; i < this.rendererMaterialPairs.Length; i++)
			{
				PrintController.RendererMaterialPair[] array = this.rendererMaterialPairs;
				int num2 = i;
				array[num2].renderer.GetPropertyBlock(this._propBlock);
				this._propBlock.SetFloat(PrintController.sliceHeightShaderPropertyId, value);
				this._propBlock.SetFloat(PrintController.printBiasShaderPropertyId, value2);
				if (this.animateFlowmapPower)
				{
					this._propBlock.SetFloat(PrintController.flowHeightPowerShaderPropertyId, value3);
				}
				array[num2].renderer.SetPropertyBlock(this._propBlock);
			}
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000C22B4 File Offset: 0x000C04B4
		private void SetupPrint()
		{
			if (this.hasSetupOnce)
			{
				return;
			}
			this.hasSetupOnce = true;
			if (this.characterModel)
			{
				CharacterModel.RendererInfo[] baseRendererInfos = this.characterModel.baseRendererInfos;
				int num = 0;
				for (int i = 0; i < baseRendererInfos.Length; i++)
				{
					Material defaultMaterial = baseRendererInfos[i].defaultMaterial;
					if (!(((defaultMaterial != null) ? defaultMaterial.shader : null) != PrintController.printShader))
					{
						num++;
					}
				}
				Array.Resize<PrintController.RendererMaterialPair>(ref this.rendererMaterialPairs, num);
				int j = 0;
				int num2 = 0;
				while (j < baseRendererInfos.Length)
				{
					ref CharacterModel.RendererInfo ptr = ref baseRendererInfos[j];
					Material defaultMaterial2 = ptr.defaultMaterial;
					if (!(((defaultMaterial2 != null) ? defaultMaterial2.shader : null) != PrintController.printShader))
					{
						Material material = UnityEngine.Object.Instantiate<Material>(ptr.defaultMaterial);
						ptr.defaultMaterial = material;
						this.rendererMaterialPairs[num2++] = new PrintController.RendererMaterialPair(ptr.renderer, material);
					}
					j++;
				}
			}
			else
			{
				List<Renderer> gameObjectComponentsInChildren = GetComponentsCache<Renderer>.GetGameObjectComponentsInChildren(base.gameObject, true);
				Array.Resize<PrintController.RendererMaterialPair>(ref this.rendererMaterialPairs, gameObjectComponentsInChildren.Count);
				int k = 0;
				int count = gameObjectComponentsInChildren.Count;
				while (k < count)
				{
					Renderer renderer = gameObjectComponentsInChildren[k];
					Material material2 = renderer.material;
					this.rendererMaterialPairs[k] = new PrintController.RendererMaterialPair(renderer, material2);
					k++;
				}
				GetComponentsCache<Renderer>.ReturnBuffer(gameObjectComponentsInChildren);
			}
			this.SetMaterialPrintCutoffEnabled(false);
			this.age = 0f;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000C2420 File Offset: 0x000C0620
		private void SetMaterialPrintCutoffEnabled(bool shouldEnable)
		{
			if (shouldEnable)
			{
				this.EnableMaterialPrintCutoff();
				return;
			}
			this.DisableMaterialPrintCutoff();
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000C2434 File Offset: 0x000C0634
		private void EnableMaterialPrintCutoff()
		{
			for (int i = 0; i < this.rendererMaterialPairs.Length; i++)
			{
				Material material = this.rendererMaterialPairs[i].material;
				material.EnableKeyword("PRINT_CUTOFF");
				material.SetInt(PrintController.printOnPropertyId, 1);
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000C247C File Offset: 0x000C067C
		private void DisableMaterialPrintCutoff()
		{
			for (int i = 0; i < this.rendererMaterialPairs.Length; i++)
			{
				Material material = this.rendererMaterialPairs[i].material;
				material.DisableKeyword("PRINT_CUTOFF");
				material.SetInt(PrintController.printOnPropertyId, 0);
			}
			this.SetPrintThreshold(1f);
		}

		// Token: 0x04002F84 RID: 12164
		[Header("Print Time and Behaviors")]
		public float printTime;

		// Token: 0x04002F85 RID: 12165
		public AnimationCurve printCurve;

		// Token: 0x04002F86 RID: 12166
		public float age;

		// Token: 0x04002F87 RID: 12167
		public bool disableWhenFinished = true;

		// Token: 0x04002F88 RID: 12168
		public bool paused;

		// Token: 0x04002F89 RID: 12169
		[Header("Print Start/End Values")]
		public float startingPrintHeight;

		// Token: 0x04002F8A RID: 12170
		public float maxPrintHeight;

		// Token: 0x04002F8B RID: 12171
		public float startingPrintBias;

		// Token: 0x04002F8C RID: 12172
		public float maxPrintBias;

		// Token: 0x04002F8D RID: 12173
		public bool animateFlowmapPower;

		// Token: 0x04002F8E RID: 12174
		public float startingFlowmapPower;

		// Token: 0x04002F8F RID: 12175
		public float maxFlowmapPower;

		// Token: 0x04002F90 RID: 12176
		private CharacterModel characterModel;

		// Token: 0x04002F91 RID: 12177
		private MaterialPropertyBlock _propBlock;

		// Token: 0x04002F92 RID: 12178
		private PrintController.RendererMaterialPair[] rendererMaterialPairs = Array.Empty<PrintController.RendererMaterialPair>();

		// Token: 0x04002F93 RID: 12179
		private bool hasSetupOnce;

		// Token: 0x04002F94 RID: 12180
		private static Shader printShader;

		// Token: 0x04002F95 RID: 12181
		private static int sliceHeightShaderPropertyId;

		// Token: 0x04002F96 RID: 12182
		private static int printBiasShaderPropertyId;

		// Token: 0x04002F97 RID: 12183
		private static int flowHeightPowerShaderPropertyId;

		// Token: 0x04002F98 RID: 12184
		private static int printOnPropertyId;

		// Token: 0x0200082E RID: 2094
		private struct RendererMaterialPair
		{
			// Token: 0x06002D94 RID: 11668 RVA: 0x000C24E8 File Offset: 0x000C06E8
			public RendererMaterialPair(Renderer renderer, Material material)
			{
				this.renderer = renderer;
				this.material = material;
			}

			// Token: 0x04002F99 RID: 12185
			public readonly Renderer renderer;

			// Token: 0x04002F9A RID: 12186
			public readonly Material material;
		}
	}
}
