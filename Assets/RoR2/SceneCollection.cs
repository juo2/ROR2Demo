using System;
using System.Collections.Generic;
using HG;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x02000558 RID: 1368
	[CreateAssetMenu(menuName = "RoR2/SceneCollection")]
	public class SceneCollection : ScriptableObject
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0006BDA6 File Offset: 0x00069FA6
		public ReadOnlyArray<SceneCollection.SceneEntry> sceneEntries
		{
			get
			{
				return this._sceneEntries;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0006BDB3 File Offset: 0x00069FB3
		public bool isEmpty
		{
			get
			{
				return this._sceneEntries.Length == 0;
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0006BDC0 File Offset: 0x00069FC0
		public void SetSceneEntries(IReadOnlyList<SceneCollection.SceneEntry> newSceneReferences)
		{
			Array.Resize<SceneCollection.SceneEntry>(ref this._sceneEntries, newSceneReferences.Count);
			for (int i = 0; i < 0; i++)
			{
				this._sceneEntries[i] = newSceneReferences[i];
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0006BE00 File Offset: 0x0006A000
		public void AddToWeightedSelection(WeightedSelection<SceneDef> dest, Func<SceneDef, bool> canAdd = null)
		{
			foreach (SceneCollection.SceneEntry sceneEntry in this.sceneEntries)
			{
				SceneDef sceneDef = sceneEntry.sceneDef;
				if (sceneDef && (canAdd == null || canAdd(sceneDef)))
				{
					dest.AddChoice(sceneDef, sceneEntry.weight);
				}
			}
		}

		// Token: 0x04001E7B RID: 7803
		[FormerlySerializedAs("sceneReferences")]
		[SerializeField]
		private SceneCollection.SceneEntry[] _sceneEntries = Array.Empty<SceneCollection.SceneEntry>();

		// Token: 0x02000559 RID: 1369
		[Serializable]
		public struct SceneEntry
		{
			// Token: 0x17000189 RID: 393
			// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0006BE8F File Offset: 0x0006A08F
			// (set) Token: 0x060018D4 RID: 6356 RVA: 0x0006BE9D File Offset: 0x0006A09D
			public float weight
			{
				get
				{
					return this.weightMinusOne + 1f;
				}
				set
				{
					this.weightMinusOne = value - 1f;
				}
			}

			// Token: 0x04001E7C RID: 7804
			public SceneDef sceneDef;

			// Token: 0x04001E7D RID: 7805
			[SerializeField]
			private float weightMinusOne;
		}
	}
}
