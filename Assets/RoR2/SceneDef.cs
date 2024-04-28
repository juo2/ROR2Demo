using System;
using System.Collections.Generic;
using HG;
using RoR2.ExpansionManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x0200055B RID: 1371
	[CreateAssetMenu(menuName = "RoR2/SceneDef")]
	public class SceneDef : ScriptableObject
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0006BEAC File Offset: 0x0006A0AC
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x0006BEB4 File Offset: 0x0006A0B4
		public SceneIndex sceneDefIndex { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0006BEBD File Offset: 0x0006A0BD
		public string baseSceneName
		{
			get
			{
				if (string.IsNullOrEmpty(this.baseSceneNameOverride))
				{
					return this.cachedName;
				}
				return this.baseSceneNameOverride;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0006BED9 File Offset: 0x0006A0D9
		public bool isFinalStage
		{
			get
			{
				return this.sceneType == SceneType.Stage && !this.hasAnyDestinations;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0006BEEF File Offset: 0x0006A0EF
		public bool hasAnyDestinations
		{
			get
			{
				return this.destinations.Length != 0 || (this.destinationsGroup && !this.destinationsGroup.isEmpty);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x00062756 File Offset: 0x00060956
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x00062756 File Offset: 0x00060956
		[Obsolete(".name should not be used. Use .cachedName instead. If retrieving the value from the engine is absolutely necessary, cast to ScriptableObject first.", true)]
		public new string name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0006BF19 File Offset: 0x0006A119
		// (set) Token: 0x060018DD RID: 6365 RVA: 0x0006BF21 File Offset: 0x0006A121
		public string cachedName
		{
			get
			{
				return this._cachedName;
			}
			set
			{
				base.name = value;
				this._cachedName = value;
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0006BF31 File Offset: 0x0006A131
		private void Awake()
		{
			this._cachedName = base.name;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0006BF3F File Offset: 0x0006A13F
		private void OnValidate()
		{
			this._cachedName = base.name;
			AssetReferenceScene assetReferenceScene = this.sceneAddress;
			if (string.IsNullOrEmpty((assetReferenceScene != null) ? assetReferenceScene.AssetGUID : null))
			{
				this.AutoAssignSceneAddress();
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000026ED File Offset: 0x000008ED
		[ContextMenu("Auto-assign scene address")]
		private void AutoAssignSceneAddress()
		{
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0006BF6C File Offset: 0x0006A16C
		public void AddDestinationsToWeightedSelection(WeightedSelection<SceneDef> dest, Func<SceneDef, bool> canAdd = null)
		{
			if (this.destinationsGroup)
			{
				this.destinationsGroup.AddToWeightedSelection(dest, canAdd);
			}
			foreach (SceneDef value in this.destinations)
			{
				dest.AddChoice(value, 1f);
			}
		}

		// Token: 0x04001E85 RID: 7813
		[Header("Scene")]
		[Tooltip("The address of the associated scene. There is a name-based fallback systems for mods to use if the address isn't provided, but all official scenes must provide this.")]
		public AssetReferenceScene sceneAddress;

		// Token: 0x04001E86 RID: 7814
		[Tooltip("The \"base\" name used for things like unlockables and stat tracking associated with this scene. If empty, the name of this asset will be used instead.")]
		public string baseSceneNameOverride;

		// Token: 0x04001E87 RID: 7815
		[Header("Classification")]
		public SceneType sceneType;

		// Token: 0x04001E88 RID: 7816
		public bool isOfflineScene;

		// Token: 0x04001E89 RID: 7817
		public int stageOrder;

		// Token: 0x04001E8A RID: 7818
		public ExpansionDef requiredExpansion;

		// Token: 0x04001E8B RID: 7819
		[Header("User-Facing Name")]
		public string nameToken;

		// Token: 0x04001E8C RID: 7820
		public string subtitleToken;

		// Token: 0x04001E8D RID: 7821
		public Texture previewTexture;

		// Token: 0x04001E8E RID: 7822
		[Header("Bazaar")]
		public Material portalMaterial;

		// Token: 0x04001E8F RID: 7823
		public string portalSelectionMessageString;

		// Token: 0x04001E90 RID: 7824
		[Header("Logbook")]
		public bool shouldIncludeInLogbook = true;

		// Token: 0x04001E91 RID: 7825
		[Tooltip("The logbook text for this scene. If empty, this scene will not be represented in the logbook.")]
		public string loreToken;

		// Token: 0x04001E92 RID: 7826
		public GameObject dioramaPrefab;

		// Token: 0x04001E93 RID: 7827
		[Header("Music")]
		[FormerlySerializedAs("song")]
		public MusicTrackDef mainTrack;

		// Token: 0x04001E94 RID: 7828
		[FormerlySerializedAs("bossSong")]
		public MusicTrackDef bossTrack;

		// Token: 0x04001E95 RID: 7829
		[Header("Behavior")]
		[Tooltip("Prevents players from spawning into the scene. This is usually for cutscenes.")]
		public bool suppressPlayerEntry;

		// Token: 0x04001E96 RID: 7830
		[Tooltip("Prevents persistent NPCs (like drones) from spawning into the scene. This is usually for cutscenes, or areas that get them killed them due to hazards they're not smart enough to avoid.")]
		public bool suppressNpcEntry;

		// Token: 0x04001E97 RID: 7831
		[Tooltip("Prevents Captain from using orbital skills.")]
		public bool blockOrbitalSkills;

		// Token: 0x04001E98 RID: 7832
		[Tooltip("Is this stage allowed to be selected when using a random stage order (e.g., in Prismatic Trials?)")]
		public bool validForRandomSelection = true;

		// Token: 0x04001E99 RID: 7833
		[Header("Destinations")]
		[Tooltip("A collection of stages that can be destinations of the teleporter.")]
		public SceneCollection destinationsGroup;

		// Token: 0x04001E9A RID: 7834
		[Obsolete("Use destinationsGroup instead.")]
		[ShowFieldObsolete]
		[Tooltip("Stages that can be destinations of the teleporter.")]
		public SceneDef[] destinations = Array.Empty<SceneDef>();

		// Token: 0x04001E9B RID: 7835
		[Header("Portal Appearance")]
		public GameObject preferredPortalPrefab;

		// Token: 0x04001E9C RID: 7836
		private string _cachedName;

		// Token: 0x04001E9D RID: 7837
		[Obsolete]
		[HideInInspector]
		[NonSerialized]
		public List<string> sceneNameOverrides;
	}
}
