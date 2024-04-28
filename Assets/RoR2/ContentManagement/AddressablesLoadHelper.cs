using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HG;
using HG.AsyncOperations;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E00 RID: 3584
	public class AddressablesLoadHelper
	{
		// Token: 0x06005235 RID: 21045 RVA: 0x00155354 File Offset: 0x00153554
		public AddressablesLoadHelper(IReadOnlyList<IResourceLocator> resourceLocators, object[] requiredKeys = null)
		{
			ArrayUtils.CloneTo<IResourceLocator, IReadOnlyList<IResourceLocator>>(resourceLocators, ref this.resourceLocators);
			if (requiredKeys != null)
			{
				ArrayUtils.CloneTo<object>(requiredKeys, ref this.requiredKeys);
			}
			this.progress = new ReadableProgress<float>();
			this.coroutine = this.Coroutine(this.progress);
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x001553F3 File Offset: 0x001535F3
		public AddressablesLoadHelper(IResourceLocator resourceLocator, object[] requiredKeys = null) : this(new IResourceLocator[]
		{
			resourceLocator
		}, requiredKeys)
		{
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x00155406 File Offset: 0x00153606
		public static AddressablesLoadHelper CreateUsingDefaultResourceLocator(object[] requiredKeys = null)
		{
			return new AddressablesLoadHelper(Addressables.ResourceLocators.First<IResourceLocator>(), requiredKeys);
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00155418 File Offset: 0x00153618
		public static AddressablesLoadHelper CreateUsingDefaultResourceLocator(object requiredKey)
		{
			return AddressablesLoadHelper.CreateUsingDefaultResourceLocator(new object[]
			{
				requiredKey
			});
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06005239 RID: 21049 RVA: 0x00155429 File Offset: 0x00153629
		// (set) Token: 0x0600523A RID: 21050 RVA: 0x00155431 File Offset: 0x00153631
		public ReadableProgress<float> progress { get; private set; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x0015543A File Offset: 0x0015363A
		// (set) Token: 0x0600523C RID: 21052 RVA: 0x00155442 File Offset: 0x00153642
		public IEnumerator coroutine { get; private set; }

		// Token: 0x0600523D RID: 21053 RVA: 0x0015544C File Offset: 0x0015364C
		public void AddContentPackLoadOperation(ContentPack contentPack)
		{
			AddressablesLoadHelper.<>c__DisplayClass21_0 CS$<>8__locals1 = new AddressablesLoadHelper.<>c__DisplayClass21_0();
			CS$<>8__locals1.contentPack = contentPack;
			this.AddLoadOperation<GameObject>(AddressablesLabels.characterBody, new Action<GameObject[]>(CS$<>8__locals1.contentPack.bodyPrefabs.Add), 1f);
			this.AddLoadOperation<GameObject>(AddressablesLabels.characterMaster, new Action<GameObject[]>(CS$<>8__locals1.contentPack.masterPrefabs.Add), 1f);
			this.AddLoadOperation<GameObject>(AddressablesLabels.projectile, new Action<GameObject[]>(CS$<>8__locals1.contentPack.projectilePrefabs.Add), 1f);
			this.AddLoadOperation<GameObject>(AddressablesLabels.gameMode, new Action<GameObject[]>(CS$<>8__locals1.contentPack.gameModePrefabs.Add), 1f);
			this.AddLoadOperation<GameObject>(AddressablesLabels.networkedObject, new Action<GameObject[]>(CS$<>8__locals1.contentPack.networkedObjectPrefabs.Add), 1f);
			this.AddLoadOperation<SkillFamily>(AddressablesLabels.skillFamily, new Action<SkillFamily[]>(CS$<>8__locals1.contentPack.skillFamilies.Add), 1f);
			this.AddLoadOperation<SkillDef>(AddressablesLabels.skillDef, new Action<SkillDef[]>(CS$<>8__locals1.contentPack.skillDefs.Add), 1f);
			this.AddLoadOperation<UnlockableDef>(AddressablesLabels.unlockableDef, new Action<UnlockableDef[]>(CS$<>8__locals1.contentPack.unlockableDefs.Add), 1f);
			this.AddLoadOperation<SurfaceDef>(AddressablesLabels.surfaceDef, new Action<SurfaceDef[]>(CS$<>8__locals1.contentPack.surfaceDefs.Add), 1f);
			this.AddLoadOperation<SceneDef>(AddressablesLabels.sceneDef, new Action<SceneDef[]>(CS$<>8__locals1.contentPack.sceneDefs.Add), 1f);
			this.AddLoadOperation<NetworkSoundEventDef>(AddressablesLabels.networkSoundEventDef, new Action<NetworkSoundEventDef[]>(CS$<>8__locals1.contentPack.networkSoundEventDefs.Add), 1f);
			this.AddLoadOperation<MusicTrackDef>(AddressablesLabels.musicTrackDef, new Action<MusicTrackDef[]>(CS$<>8__locals1.contentPack.musicTrackDefs.Add), 1f);
			this.AddLoadOperation<GameEndingDef>(AddressablesLabels.gameEndingDef, new Action<GameEndingDef[]>(CS$<>8__locals1.contentPack.gameEndingDefs.Add), 1f);
			this.AddLoadOperation<ItemDef>(AddressablesLabels.itemDef, new Action<ItemDef[]>(CS$<>8__locals1.contentPack.itemDefs.Add), 1f);
			this.AddLoadOperation<ItemTierDef>(AddressablesLabels.itemTierDef, new Action<ItemTierDef[]>(CS$<>8__locals1.contentPack.itemTierDefs.Add), 1f);
			this.AddLoadOperation<ItemRelationshipProvider>(AddressablesLabels.itemRelationshipProvider, new Action<ItemRelationshipProvider[]>(CS$<>8__locals1.contentPack.itemRelationshipProviders.Add), 1f);
			this.AddLoadOperation<ItemRelationshipType>(AddressablesLabels.itemRelationshipType, new Action<ItemRelationshipType[]>(CS$<>8__locals1.contentPack.itemRelationshipTypes.Add), 1f);
			this.AddLoadOperation<EquipmentDef>(AddressablesLabels.equipmentDef, new Action<EquipmentDef[]>(CS$<>8__locals1.contentPack.equipmentDefs.Add), 1f);
			this.AddLoadOperation<MiscPickupDef>(AddressablesLabels.miscPickupDef, new Action<MiscPickupDef[]>(CS$<>8__locals1.contentPack.miscPickupDefs.Add), 1f);
			this.AddLoadOperation<BuffDef>(AddressablesLabels.buffDef, new Action<BuffDef[]>(CS$<>8__locals1.contentPack.buffDefs.Add), 1f);
			this.AddLoadOperation<EliteDef>(AddressablesLabels.eliteDef, new Action<EliteDef[]>(CS$<>8__locals1.contentPack.eliteDefs.Add), 1f);
			this.AddLoadOperation<SurvivorDef>(AddressablesLabels.survivorDef, new Action<SurvivorDef[]>(CS$<>8__locals1.contentPack.survivorDefs.Add), 1f);
			this.AddLoadOperation<ArtifactDef>(AddressablesLabels.artifactDef, new Action<ArtifactDef[]>(CS$<>8__locals1.contentPack.artifactDefs.Add), 1f);
			this.AddLoadOperation<GameObject, EffectDef>(AddressablesLabels.effect, new Action<EffectDef[]>(CS$<>8__locals1.contentPack.effectDefs.Add), (GameObject asset) => new EffectDef(asset), 1f);
			this.AddLoadOperation<EntityStateConfiguration>(AddressablesLabels.entityStateConfiguration, new Action<EntityStateConfiguration[]>(CS$<>8__locals1.contentPack.entityStateConfigurations.Add), 1f);
			this.AddLoadOperation<ExpansionDef>(AddressablesLabels.expansionDef, new Action<ExpansionDef[]>(CS$<>8__locals1.contentPack.expansionDefs.Add), 1f);
			this.AddLoadOperation<EntitlementDef>(AddressablesLabels.entitlementDef, new Action<EntitlementDef[]>(CS$<>8__locals1.contentPack.entitlementDefs.Add), 1f);
			this.AddGenericOperation(CS$<>8__locals1.<AddContentPackLoadOperation>g__AddEntityStateTypes|1(), 0.05f);
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00155898 File Offset: 0x00153A98
		public void AddLoadOperation<TAssetSrc>(string key, Action<TAssetSrc[]> onComplete, float weight = 1f) where TAssetSrc : UnityEngine.Object
		{
			this.AddLoadOperation<TAssetSrc, TAssetSrc>(key, onComplete, null, weight);
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x001558A4 File Offset: 0x00153AA4
		public void AddLoadOperation<TAssetSrc, TAssetDest>(string key, Action<TAssetDest[]> onComplete, Func<TAssetSrc, TAssetDest> selector = null, float weight = 1f) where TAssetSrc : UnityEngine.Object
		{
			AddressablesLoadHelper.<>c__DisplayClass23_0<TAssetSrc, TAssetDest> CS$<>8__locals1 = new AddressablesLoadHelper.<>c__DisplayClass23_0<TAssetSrc, TAssetDest>();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.key = key;
			CS$<>8__locals1.onComplete = onComplete;
			CS$<>8__locals1.selector = selector;
			CS$<>8__locals1.loadOperation = new AddressablesLoadHelper.Operation
			{
				weight = weight
			};
			CS$<>8__locals1.loadOperation.coroutine = CS$<>8__locals1.<AddLoadOperation>g__Coroutine|0();
			this.allOperations.Add(CS$<>8__locals1.loadOperation);
			this.pendingLoadOperations.Enqueue(CS$<>8__locals1.loadOperation);
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0015591C File Offset: 0x00153B1C
		public void AddGenericOperation(IEnumerator coroutine, float weight = 1f)
		{
			AddressablesLoadHelper.Operation item = new AddressablesLoadHelper.Operation
			{
				weight = weight,
				coroutine = coroutine
			};
			this.allOperations.Add(item);
			this.allGenericOperations.Add(item);
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x00155955 File Offset: 0x00153B55
		public void AddGenericOperation(IEnumerable coroutineProvider, float weight = 1f)
		{
			this.AddGenericOperation(coroutineProvider.GetEnumerator(), weight);
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x00155964 File Offset: 0x00153B64
		public void AddGenericOperation(Func<IEnumerator> coroutineMethod, float weight = 1f)
		{
			this.AddGenericOperation(coroutineMethod(), weight);
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x00155974 File Offset: 0x00153B74
		public void AddGenericOperation(Action action, float weight = 1f)
		{
			AddressablesLoadHelper.<>c__DisplayClass27_0 CS$<>8__locals1 = new AddressablesLoadHelper.<>c__DisplayClass27_0();
			CS$<>8__locals1.action = action;
			this.AddGenericOperation(CS$<>8__locals1.<AddGenericOperation>g__Coroutine|0(), weight);
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x0015599B File Offset: 0x00153B9B
		private IEnumerator Coroutine(IProgress<float> progressReceiver)
		{
			AddressablesLoadHelper.<>c__DisplayClass28_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.progressReceiver = progressReceiver;
			while (this.pendingLoadOperations.Count > 0 || this.runningLoadOperations.Count > 0)
			{
				while (this.pendingLoadOperations.Count > 0 && this.runningLoadOperations.Count < this.maxConcurrentOperations)
				{
					this.runningLoadOperations.Add(this.pendingLoadOperations.Dequeue());
				}
				int num;
				for (int i = 0; i < this.runningLoadOperations.Count; i = num)
				{
					AddressablesLoadHelper.Operation operation = this.runningLoadOperations[i];
					if (operation.coroutine.MoveNext())
					{
						this.<Coroutine>g__UpdateProgress|28_0(ref CS$<>8__locals1);
						yield return operation.coroutine.Current;
					}
					else
					{
						this.runningLoadOperations.RemoveAt(i);
						num = i - 1;
						i = num;
					}
					num = i + 1;
				}
			}
			foreach (AddressablesLoadHelper.Operation genericOperation in this.allGenericOperations)
			{
				while (genericOperation.coroutine.MoveNext())
				{
					this.<Coroutine>g__UpdateProgress|28_0(ref CS$<>8__locals1);
					yield return genericOperation.coroutine.Current;
				}
				genericOperation = null;
			}
			List<AddressablesLoadHelper.Operation>.Enumerator enumerator = default(List<AddressablesLoadHelper.Operation>.Enumerator);
			CS$<>8__locals1.progressReceiver.Report(1f);
			yield break;
			yield break;
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x001559B4 File Offset: 0x00153BB4
		[CompilerGenerated]
		private void <Coroutine>g__UpdateProgress|28_0(ref AddressablesLoadHelper.<>c__DisplayClass28_0 A_1)
		{
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < this.allOperations.Count; i++)
			{
				AddressablesLoadHelper.Operation operation = this.allOperations[i];
				num += operation.weight;
				num2 += operation.progress * operation.weight;
			}
			if (num == 0f)
			{
				num = 1f;
				num2 = 0.5f;
			}
			A_1.progressReceiver.Report(num2 / num);
		}

		// Token: 0x04004E92 RID: 20114
		private readonly IResourceLocator[] resourceLocators = Array.Empty<IResourceLocator>();

		// Token: 0x04004E93 RID: 20115
		private readonly object[] requiredKeys = Array.Empty<object>();

		// Token: 0x04004E96 RID: 20118
		public int maxConcurrentOperations = 2;

		// Token: 0x04004E97 RID: 20119
		public float timeoutDuration = float.PositiveInfinity;

		// Token: 0x04004E98 RID: 20120
		private readonly List<AddressablesLoadHelper.Operation> allOperations = new List<AddressablesLoadHelper.Operation>();

		// Token: 0x04004E99 RID: 20121
		private readonly Queue<AddressablesLoadHelper.Operation> pendingLoadOperations = new Queue<AddressablesLoadHelper.Operation>();

		// Token: 0x04004E9A RID: 20122
		private readonly List<AddressablesLoadHelper.Operation> runningLoadOperations = new List<AddressablesLoadHelper.Operation>();

		// Token: 0x04004E9B RID: 20123
		private readonly List<AddressablesLoadHelper.Operation> allGenericOperations = new List<AddressablesLoadHelper.Operation>();

		// Token: 0x02000E01 RID: 3585
		private class Operation
		{
			// Token: 0x04004E9C RID: 20124
			public float weight;

			// Token: 0x04004E9D RID: 20125
			public IEnumerator coroutine;

			// Token: 0x04004E9E RID: 20126
			public float progress;
		}

		// Token: 0x02000E02 RID: 3586
		public class AddressablesLoadAsyncOperationWrapper<TAsset> : BaseAsyncOperation<TAsset[]> where TAsset : UnityEngine.Object
		{
			// Token: 0x06005247 RID: 21063 RVA: 0x00155A2C File Offset: 0x00153C2C
			public AddressablesLoadAsyncOperationWrapper(IReadOnlyList<AsyncOperationHandle<IList<TAsset>>> handles)
			{
				if (handles.Count == 0)
				{
					this.handles = Array.Empty<AsyncOperationHandle<IList<TAsset>>>();
					base.Complete(Array.Empty<TAsset>());
					return;
				}
				Action<AsyncOperationHandle<IList<TAsset>>> value = new Action<AsyncOperationHandle<IList<TAsset>>>(this.OnChildOperationCompleted);
				this.handles = new AsyncOperationHandle<IList<TAsset>>[handles.Count];
				for (int i = 0; i < handles.Count; i++)
				{
					this.handles[i] = handles[i];
					handles[i].Completed += value;
				}
			}

			// Token: 0x06005248 RID: 21064 RVA: 0x00155AB0 File Offset: 0x00153CB0
			private void OnChildOperationCompleted(AsyncOperationHandle<IList<TAsset>> completedOperationHandle)
			{
				this.completionCount++;
				if (this.completionCount == this.handles.Length)
				{
					List<TAsset> list = new List<TAsset>();
					foreach (AsyncOperationHandle<IList<TAsset>> asyncOperationHandle in this.handles)
					{
						if (asyncOperationHandle.Result != null)
						{
							list.AddRange(asyncOperationHandle.Result);
						}
					}
					base.Complete(list.ToArray());
				}
			}

			// Token: 0x1700076A RID: 1898
			// (get) Token: 0x06005249 RID: 21065 RVA: 0x00155B20 File Offset: 0x00153D20
			public override float progress
			{
				get
				{
					float num = 0f;
					if (this.handles.Length == 0)
					{
						return 0f;
					}
					float num2 = 1f / (float)this.handles.Length;
					for (int i = 0; i < this.handles.Length; i++)
					{
						num += this.handles[i].PercentComplete * num2;
					}
					return num;
				}
			}

			// Token: 0x04004E9F RID: 20127
			private AsyncOperationHandle<IList<TAsset>>[] handles;

			// Token: 0x04004EA0 RID: 20128
			private int completionCount;
		}
	}
}
