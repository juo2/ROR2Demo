using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000605 RID: 1541
	public class BurnEffectController : MonoBehaviour
	{
		// Token: 0x06001C40 RID: 7232 RVA: 0x0007823C File Offset: 0x0007643C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Init()
		{
			BurnEffectController.normalEffect = new BurnEffectController.EffectParams
			{
				startSound = "Play_item_proc_igniteOnKill_Loop",
				stopSound = "Stop_item_proc_igniteOnKill_Loop",
				overlayMaterial = LegacyResourcesAPI.Load<Material>("Materials/matOnFire"),
				fireEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/FireEffect")
			};
			BurnEffectController.helfireEffect = new BurnEffectController.EffectParams
			{
				startSound = "Play_item_proc_igniteOnKill_Loop",
				stopSound = "Stop_item_proc_igniteOnKill_Loop",
				overlayMaterial = LegacyResourcesAPI.Load<Material>("Materials/matOnHelfire"),
				fireEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/HelfireEffect")
			};
			BurnEffectController.poisonEffect = new BurnEffectController.EffectParams
			{
				overlayMaterial = LegacyResourcesAPI.Load<Material>("Materials/matPoisoned"),
				fireEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/PoisonEffect")
			};
			BurnEffectController.blightEffect = new BurnEffectController.EffectParams
			{
				overlayMaterial = LegacyResourcesAPI.Load<Material>("Materials/matBlighted"),
				fireEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/BlightEffect")
			};
			BurnEffectController.strongerBurnEffect = new BurnEffectController.EffectParams
			{
				overlayMaterial = LegacyResourcesAPI.Load<Material>("Materials/matStrongerBurn"),
				fireEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/StrongerBurnEffect")
			};
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00078348 File Offset: 0x00076548
		private void Start()
		{
			if (this.effectType == null)
			{
				Debug.LogError("BurnEffectController on " + base.gameObject.name + " has no effect type!");
				return;
			}
			Util.PlaySound(this.effectType.startSound, base.gameObject);
			this.burnEffectInstances = new List<GameObject>();
			if (this.effectType.overlayMaterial != null)
			{
				this.temporaryOverlay = base.gameObject.AddComponent<TemporaryOverlay>();
				this.temporaryOverlay.originalMaterial = this.effectType.overlayMaterial;
			}
			if (this.target)
			{
				CharacterModel component = this.target.GetComponent<CharacterModel>();
				if (component)
				{
					if (this.temporaryOverlay)
					{
						this.temporaryOverlay.AddToCharacerModel(component);
					}
					CharacterBody body = component.body;
					CharacterModel.RendererInfo[] baseRendererInfos = component.baseRendererInfos;
					if (body)
					{
						for (int i = 0; i < baseRendererInfos.Length; i++)
						{
							if (!baseRendererInfos[i].ignoreOverlays)
							{
								GameObject gameObject = this.AddFireParticles(baseRendererInfos[i].renderer, body.coreTransform);
								if (gameObject)
								{
									this.burnEffectInstances.Add(gameObject);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00078478 File Offset: 0x00076678
		private void OnDestroy()
		{
			Util.PlaySound(this.effectType.stopSound, base.gameObject);
			if (this.temporaryOverlay)
			{
				UnityEngine.Object.Destroy(this.temporaryOverlay);
			}
			for (int i = 0; i < this.burnEffectInstances.Count; i++)
			{
				if (this.burnEffectInstances[i])
				{
					this.burnEffectInstances[i].GetComponent<ParticleSystem>().emission.enabled = false;
					DestroyOnTimer component = this.burnEffectInstances[i].GetComponent<DestroyOnTimer>();
					LightIntensityCurve component2 = this.burnEffectInstances[i].GetComponent<LightIntensityCurve>();
					if (component)
					{
						component.enabled = true;
					}
					if (component2)
					{
						component2.enabled = true;
					}
				}
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00078544 File Offset: 0x00076744
		private GameObject AddFireParticles(Renderer modelRenderer, Transform targetParentTransform)
		{
			if (modelRenderer is MeshRenderer || modelRenderer is SkinnedMeshRenderer)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.effectType.fireEffectPrefab, targetParentTransform);
				ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
				ParticleSystem.ShapeModule shape = component.shape;
				if (modelRenderer)
				{
					if (modelRenderer is MeshRenderer)
					{
						shape.shapeType = ParticleSystemShapeType.MeshRenderer;
						shape.meshRenderer = (MeshRenderer)modelRenderer;
					}
					else if (modelRenderer is SkinnedMeshRenderer)
					{
						shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
						shape.skinnedMeshRenderer = (SkinnedMeshRenderer)modelRenderer;
					}
				}
				ParticleSystem.MainModule main = component.main;
				component.gameObject.SetActive(true);
				BoneParticleController component2 = gameObject.GetComponent<BoneParticleController>();
				if (component2 && modelRenderer is SkinnedMeshRenderer)
				{
					component2.skinnedMeshRenderer = (SkinnedMeshRenderer)modelRenderer;
				}
				return gameObject;
			}
			return null;
		}

		// Token: 0x04002207 RID: 8711
		private List<GameObject> burnEffectInstances;

		// Token: 0x04002208 RID: 8712
		public GameObject target;

		// Token: 0x04002209 RID: 8713
		private TemporaryOverlay temporaryOverlay;

		// Token: 0x0400220A RID: 8714
		private int soundID;

		// Token: 0x0400220B RID: 8715
		public BurnEffectController.EffectParams effectType = BurnEffectController.normalEffect;

		// Token: 0x0400220C RID: 8716
		public static BurnEffectController.EffectParams normalEffect;

		// Token: 0x0400220D RID: 8717
		public static BurnEffectController.EffectParams helfireEffect;

		// Token: 0x0400220E RID: 8718
		public static BurnEffectController.EffectParams poisonEffect;

		// Token: 0x0400220F RID: 8719
		public static BurnEffectController.EffectParams blightEffect;

		// Token: 0x04002210 RID: 8720
		public static BurnEffectController.EffectParams strongerBurnEffect;

		// Token: 0x04002211 RID: 8721
		public float fireParticleSize = 5f;

		// Token: 0x02000606 RID: 1542
		public class EffectParams
		{
			// Token: 0x04002212 RID: 8722
			public string startSound;

			// Token: 0x04002213 RID: 8723
			public string stopSound;

			// Token: 0x04002214 RID: 8724
			public Material overlayMaterial;

			// Token: 0x04002215 RID: 8725
			public GameObject fireEffectPrefab;
		}
	}
}
