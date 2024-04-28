using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A5A RID: 2650
	[CreateAssetMenu(menuName = "RoR2/SkinDef")]
	public class SkinDef : ScriptableObject
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000FB548 File Offset: 0x000F9748
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x000FB550 File Offset: 0x000F9750
		public SkinIndex skinIndex { get; set; } = SkinIndex.None;

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000FB55C File Offset: 0x000F975C
		private void Bake()
		{
			if (this.runtimeSkin != null)
			{
				return;
			}
			this.runtimeSkin = new SkinDef.RuntimeSkin();
			SkinDef.<>c__DisplayClass23_0 CS$<>8__locals1;
			CS$<>8__locals1.rendererInfoTemplates = new List<SkinDef.RendererInfoTemplate>();
			CS$<>8__locals1.gameObjectActivationTemplates = new List<SkinDef.GameObjectActivationTemplate>();
			CS$<>8__locals1.meshReplacementTemplates = new List<SkinDef.MeshReplacementTemplate>();
			foreach (SkinDef skinDef in this.baseSkins)
			{
				if (skinDef)
				{
					skinDef.Bake();
					if (skinDef.runtimeSkin != null)
					{
						if (skinDef.runtimeSkin.rendererInfoTemplates != null)
						{
							SkinDef.RendererInfoTemplate[] rendererInfoTemplates = skinDef.runtimeSkin.rendererInfoTemplates;
							for (int j = 0; j < rendererInfoTemplates.Length; j++)
							{
								SkinDef.<Bake>g__AddRendererInfoTemplate|23_0(rendererInfoTemplates[j], ref CS$<>8__locals1);
							}
						}
						if (skinDef.runtimeSkin.gameObjectActivationTemplates != null)
						{
							SkinDef.GameObjectActivationTemplate[] gameObjectActivationTemplates = skinDef.runtimeSkin.gameObjectActivationTemplates;
							for (int j = 0; j < gameObjectActivationTemplates.Length; j++)
							{
								SkinDef.<Bake>g__AddGameObjectActivationTemplate|23_1(gameObjectActivationTemplates[j], ref CS$<>8__locals1);
							}
						}
						if (skinDef.runtimeSkin.meshReplacementTemplates != null)
						{
							SkinDef.MeshReplacementTemplate[] meshReplacementTemplates = skinDef.runtimeSkin.meshReplacementTemplates;
							for (int j = 0; j < meshReplacementTemplates.Length; j++)
							{
								SkinDef.<Bake>g__AddMeshReplacementTemplate|23_2(meshReplacementTemplates[j], ref CS$<>8__locals1);
							}
						}
					}
				}
			}
			for (int k = 0; k < this.rendererInfos.Length; k++)
			{
				ref CharacterModel.RendererInfo ptr = ref this.rendererInfos[k];
				if (!ptr.renderer)
				{
					Debug.LogErrorFormat("Skin {0} has an empty renderer field in its rendererInfos.", new object[]
					{
						this
					});
				}
				else
				{
					SkinDef.<Bake>g__AddRendererInfoTemplate|23_0(new SkinDef.RendererInfoTemplate
					{
						data = ptr,
						path = Util.BuildPrefabTransformPath(this.rootObject.transform, ptr.renderer.transform, false, false)
					}, ref CS$<>8__locals1);
				}
			}
			this.runtimeSkin.rendererInfoTemplates = CS$<>8__locals1.rendererInfoTemplates.ToArray();
			for (int l = 0; l < this.gameObjectActivations.Length; l++)
			{
				ref SkinDef.GameObjectActivation ptr2 = ref this.gameObjectActivations[l];
				if (!ptr2.gameObject)
				{
					Debug.LogErrorFormat("Skin {0} has an empty gameObject field in its gameObjectActivations.", new object[]
					{
						this
					});
				}
				else
				{
					SkinDef.<Bake>g__AddGameObjectActivationTemplate|23_1(new SkinDef.GameObjectActivationTemplate
					{
						shouldActivate = ptr2.shouldActivate,
						path = Util.BuildPrefabTransformPath(this.rootObject.transform, ptr2.gameObject.transform, false, false)
					}, ref CS$<>8__locals1);
				}
			}
			this.runtimeSkin.gameObjectActivationTemplates = CS$<>8__locals1.gameObjectActivationTemplates.ToArray();
			for (int m = 0; m < this.meshReplacements.Length; m++)
			{
				ref SkinDef.MeshReplacement ptr3 = ref this.meshReplacements[m];
				if (!ptr3.renderer)
				{
					Debug.LogErrorFormat("Skin {0} has an empty renderer field in its meshReplacements.", new object[]
					{
						this
					});
				}
				else
				{
					SkinDef.<Bake>g__AddMeshReplacementTemplate|23_2(new SkinDef.MeshReplacementTemplate
					{
						mesh = ptr3.mesh,
						path = Util.BuildPrefabTransformPath(this.rootObject.transform, ptr3.renderer.transform, false, false)
					}, ref CS$<>8__locals1);
				}
			}
			this.runtimeSkin.meshReplacementTemplates = CS$<>8__locals1.meshReplacementTemplates.ToArray();
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000FB888 File Offset: 0x000F9A88
		public void Apply(GameObject modelObject)
		{
			this.Bake();
			this.runtimeSkin.Apply(modelObject);
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000FB89C File Offset: 0x000F9A9C
		private void Awake()
		{
			if (Application.IsPlaying(this))
			{
				this.Bake();
			}
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000FB8AC File Offset: 0x000F9AAC
		[ContextMenu("Upgrade unlockableName to unlockableDef")]
		public void UpgradeUnlockableNameToUnlockableDef()
		{
			if (!string.IsNullOrEmpty(this.unlockableName) && !this.unlockableDef)
			{
				UnlockableDef exists = LegacyResourcesAPI.Load<UnlockableDef>("UnlockableDefs/" + this.unlockableName);
				if (exists)
				{
					this.unlockableDef = exists;
					this.unlockableName = null;
				}
			}
			EditorUtil.SetDirty(this);
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000FB95C File Offset: 0x000F9B5C
		[CompilerGenerated]
		internal static void <Bake>g__AddRendererInfoTemplate|23_0(SkinDef.RendererInfoTemplate rendererInfoTemplate, ref SkinDef.<>c__DisplayClass23_0 A_1)
		{
			int i = 0;
			int count = A_1.rendererInfoTemplates.Count;
			while (i < count)
			{
				if (A_1.rendererInfoTemplates[i].path == rendererInfoTemplate.path)
				{
					A_1.rendererInfoTemplates[i] = rendererInfoTemplate;
					return;
				}
				i++;
			}
			A_1.rendererInfoTemplates.Add(rendererInfoTemplate);
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000FB9BC File Offset: 0x000F9BBC
		[CompilerGenerated]
		internal static void <Bake>g__AddGameObjectActivationTemplate|23_1(SkinDef.GameObjectActivationTemplate gameObjectActivationTemplate, ref SkinDef.<>c__DisplayClass23_0 A_1)
		{
			int i = 0;
			int count = A_1.gameObjectActivationTemplates.Count;
			while (i < count)
			{
				if (A_1.gameObjectActivationTemplates[i].path == gameObjectActivationTemplate.path)
				{
					A_1.gameObjectActivationTemplates[i] = gameObjectActivationTemplate;
					return;
				}
				i++;
			}
			A_1.gameObjectActivationTemplates.Add(gameObjectActivationTemplate);
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000FBA1C File Offset: 0x000F9C1C
		[CompilerGenerated]
		internal static void <Bake>g__AddMeshReplacementTemplate|23_2(SkinDef.MeshReplacementTemplate meshReplacementTemplate, ref SkinDef.<>c__DisplayClass23_0 A_1)
		{
			int i = 0;
			int count = A_1.meshReplacementTemplates.Count;
			while (i < count)
			{
				if (A_1.meshReplacementTemplates[i].path == meshReplacementTemplate.path)
				{
					A_1.meshReplacementTemplates[i] = meshReplacementTemplate;
					return;
				}
				i++;
			}
			A_1.meshReplacementTemplates.Add(meshReplacementTemplate);
		}

		// Token: 0x04003C01 RID: 15361
		[Tooltip("The skins which will be applied before this one.")]
		public SkinDef[] baseSkins;

		// Token: 0x04003C02 RID: 15362
		[ShowThumbnail]
		public Sprite icon;

		// Token: 0x04003C03 RID: 15363
		public string nameToken;

		// Token: 0x04003C04 RID: 15364
		[Obsolete("Use 'unlockableDef' instead.")]
		public string unlockableName;

		// Token: 0x04003C05 RID: 15365
		public UnlockableDef unlockableDef;

		// Token: 0x04003C06 RID: 15366
		[PrefabReference]
		public GameObject rootObject;

		// Token: 0x04003C07 RID: 15367
		public CharacterModel.RendererInfo[] rendererInfos = Array.Empty<CharacterModel.RendererInfo>();

		// Token: 0x04003C08 RID: 15368
		public SkinDef.GameObjectActivation[] gameObjectActivations = Array.Empty<SkinDef.GameObjectActivation>();

		// Token: 0x04003C09 RID: 15369
		public SkinDef.MeshReplacement[] meshReplacements = Array.Empty<SkinDef.MeshReplacement>();

		// Token: 0x04003C0A RID: 15370
		public SkinDef.ProjectileGhostReplacement[] projectileGhostReplacements = Array.Empty<SkinDef.ProjectileGhostReplacement>();

		// Token: 0x04003C0B RID: 15371
		public SkinDef.MinionSkinReplacement[] minionSkinReplacements = Array.Empty<SkinDef.MinionSkinReplacement>();

		// Token: 0x04003C0C RID: 15372
		private SkinDef.RuntimeSkin runtimeSkin;

		// Token: 0x02000A5B RID: 2651
		[Serializable]
		public struct GameObjectActivation
		{
			// Token: 0x04003C0D RID: 15373
			[PrefabReference]
			public GameObject gameObject;

			// Token: 0x04003C0E RID: 15374
			public bool shouldActivate;
		}

		// Token: 0x02000A5C RID: 2652
		[Serializable]
		public struct MeshReplacement
		{
			// Token: 0x04003C0F RID: 15375
			[PrefabReference]
			public Renderer renderer;

			// Token: 0x04003C10 RID: 15376
			public Mesh mesh;
		}

		// Token: 0x02000A5D RID: 2653
		[Serializable]
		public struct ProjectileGhostReplacement
		{
			// Token: 0x04003C11 RID: 15377
			public GameObject projectilePrefab;

			// Token: 0x04003C12 RID: 15378
			public GameObject projectileGhostReplacementPrefab;
		}

		// Token: 0x02000A5E RID: 2654
		[Serializable]
		public struct MinionSkinReplacement
		{
			// Token: 0x04003C13 RID: 15379
			public GameObject minionBodyPrefab;

			// Token: 0x04003C14 RID: 15380
			public SkinDef minionSkin;
		}

		// Token: 0x02000A5F RID: 2655
		private struct RendererInfoTemplate
		{
			// Token: 0x04003C15 RID: 15381
			public string path;

			// Token: 0x04003C16 RID: 15382
			public CharacterModel.RendererInfo data;
		}

		// Token: 0x02000A60 RID: 2656
		private struct GameObjectActivationTemplate
		{
			// Token: 0x04003C17 RID: 15383
			public string path;

			// Token: 0x04003C18 RID: 15384
			public bool shouldActivate;
		}

		// Token: 0x02000A61 RID: 2657
		private struct MeshReplacementTemplate
		{
			// Token: 0x04003C19 RID: 15385
			public string path;

			// Token: 0x04003C1A RID: 15386
			public Mesh mesh;
		}

		// Token: 0x02000A62 RID: 2658
		private class RuntimeSkin
		{
			// Token: 0x06003CEE RID: 15598 RVA: 0x000FBA7C File Offset: 0x000F9C7C
			public void Apply(GameObject modelObject)
			{
				Transform transform = modelObject.transform;
				for (int i = 0; i < this.rendererInfoTemplates.Length; i++)
				{
					ref SkinDef.RendererInfoTemplate ptr = ref this.rendererInfoTemplates[i];
					CharacterModel.RendererInfo data = ptr.data;
					Transform transform2 = transform.Find(ptr.path);
					if (transform2)
					{
						Renderer component = transform2.GetComponent<Renderer>();
						if (component)
						{
							data.renderer = component;
							SkinDef.RuntimeSkin.rendererInfoBuffer.Add(data);
						}
						else
						{
							Debug.LogWarningFormat("No renderer at {0}/{1}", new object[]
							{
								transform.name,
								ptr.path
							});
						}
					}
					else
					{
						Debug.LogWarningFormat("Could not find transform \"{0}\" relative to \"{1}\".", new object[]
						{
							ptr.path,
							transform.name
						});
					}
				}
				modelObject.GetComponent<CharacterModel>().baseRendererInfos = SkinDef.RuntimeSkin.rendererInfoBuffer.ToArray();
				SkinDef.RuntimeSkin.rendererInfoBuffer.Clear();
				for (int j = 0; j < this.gameObjectActivationTemplates.Length; j++)
				{
					ref SkinDef.GameObjectActivationTemplate ptr2 = ref this.gameObjectActivationTemplates[j];
					bool shouldActivate = ptr2.shouldActivate;
					transform.Find(ptr2.path).gameObject.SetActive(shouldActivate);
				}
				for (int k = 0; k < this.meshReplacementTemplates.Length; k++)
				{
					ref SkinDef.MeshReplacementTemplate ptr3 = ref this.meshReplacementTemplates[k];
					Mesh mesh = ptr3.mesh;
					Renderer component2 = transform.Find(ptr3.path).GetComponent<Renderer>();
					SkinnedMeshRenderer skinnedMeshRenderer;
					if (component2 is MeshRenderer)
					{
						component2.GetComponent<MeshFilter>().sharedMesh = mesh;
					}
					else if ((skinnedMeshRenderer = (component2 as SkinnedMeshRenderer)) != null)
					{
						skinnedMeshRenderer.sharedMesh = mesh;
					}
				}
			}

			// Token: 0x04003C1B RID: 15387
			public SkinDef.RendererInfoTemplate[] rendererInfoTemplates;

			// Token: 0x04003C1C RID: 15388
			public SkinDef.GameObjectActivationTemplate[] gameObjectActivationTemplates;

			// Token: 0x04003C1D RID: 15389
			public SkinDef.MeshReplacementTemplate[] meshReplacementTemplates;

			// Token: 0x04003C1E RID: 15390
			private static readonly List<CharacterModel.RendererInfo> rendererInfoBuffer = new List<CharacterModel.RendererInfo>();
		}
	}
}
