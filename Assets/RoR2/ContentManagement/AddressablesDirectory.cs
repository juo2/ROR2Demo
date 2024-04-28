using System;
using System.Collections.Generic;
using System.Linq;
using HG;
using HG.AssetManagement;
using HG.AsyncOperations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace RoR2.ContentManagement
{
	// Token: 0x02000DFC RID: 3580
	public class AddressablesDirectory : IAssetRepository
	{
		// Token: 0x06005228 RID: 21032 RVA: 0x00154AEC File Offset: 0x00152CEC
		public AddressablesDirectory(string baseDirectory, IEnumerable<IResourceLocator> resourceLocators)
		{
			baseDirectory = (baseDirectory ?? string.Empty);
			if (baseDirectory.Length > 0 && !baseDirectory.EndsWith("/"))
			{
				throw new ArgumentException("'baseDirectory' must be empty or end with '/'. baseDirectory=\"" + baseDirectory + "\"");
			}
			this.baseDirectory = baseDirectory;
			this.resourceLocators = resourceLocators.ToArray<IResourceLocator>();
			List<string> list = new List<string>();
			IResourceLocator[] array = this.resourceLocators;
			for (int i = 0; i < array.Length; i++)
			{
				using (IEnumerator<object> enumerator = array[i].Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text;
						if ((text = (enumerator.Current as string)) != null && text.StartsWith(baseDirectory))
						{
							list.Add(text);
						}
					}
				}
			}
			this.folderTree = FilePathTree.Create<List<string>>(list);
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00154BD4 File Offset: 0x00152DD4
		public BaseAsyncOperation<TAsset[]> LoadAllAsync<TAsset>(string folderPath) where TAsset : UnityEngine.Object
		{
			List<string> list = new List<string>();
			string text = this.baseDirectory + folderPath;
			this.folderTree.GetEntriesInFolder(text, list);
			if (list.Count == 0)
			{
				Debug.Log("No entries for path \"" + text + "\".");
			}
			return this.IssueLoadOperationAsGroup<TAsset>(list);
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00154C28 File Offset: 0x00152E28
		private BaseAsyncOperation<TAsset[]> IssueLoadOperationAsGroup<TAsset>(List<string> entriesToLoad) where TAsset : UnityEngine.Object
		{
			List<IResourceLocation> locations = this.FilterPathsByType<TAsset>(entriesToLoad);
			List<TAsset> results = new List<TAsset>();
			AsyncOperationHandle<IList<TAsset>> loadOperationHandle = Addressables.LoadAssetsAsync<TAsset>(locations, delegate(TAsset result)
			{
				results.Add(result);
			});
			ScriptedAsyncOperation<TAsset[]> resultOperation = new ScriptedAsyncOperation<TAsset[]>(() => loadOperationHandle.PercentComplete);
			loadOperationHandle.Completed += delegate(AsyncOperationHandle<IList<TAsset>> handle)
			{
				resultOperation.Complete(results.ToArray());
			};
			return resultOperation;
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x00154C9C File Offset: 0x00152E9C
		private BaseAsyncOperation<TAsset[]> IssueLoadOperationsIndividually<TAsset>(List<string> entriesToLoad) where TAsset : UnityEngine.Object
		{
			List<IResourceLocation> list = this.FilterPathsByType<TAsset>(entriesToLoad);
			AsyncOperationHandle<TAsset>[] array = new AsyncOperationHandle<TAsset>[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				AsyncOperationHandle<TAsset> asyncOperationHandle = Addressables.LoadAssetAsync<TAsset>(list[i]);
				array[i] = asyncOperationHandle;
			}
			return new AddressablesDirectory.AddressablesLoadAsyncOperationWrapper<TAsset>(array);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x00154CEC File Offset: 0x00152EEC
		private List<IResourceLocation> FilterPathsByType<TAsset>(List<string> paths)
		{
			Type typeFromHandle = typeof(TAsset);
			bool[] array = new bool[paths.Count];
			List<IResourceLocation> list = new List<IResourceLocation>();
			foreach (IResourceLocator resourceLocator in this.resourceLocators)
			{
				for (int j = 0; j < paths.Count; j++)
				{
					IList<IResourceLocation> list2;
					if (!array[j] && resourceLocator.Locate(paths[j], null, out list2))
					{
						for (int k = 0; k < list2.Count; k++)
						{
							IResourceLocation resourceLocation = list2[k];
							if (typeFromHandle.IsAssignableFrom(resourceLocation.ResourceType))
							{
								list.Add(resourceLocation);
								array[j] = true;
								break;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04004E6B RID: 20075
		private string baseDirectory;

		// Token: 0x04004E6C RID: 20076
		private FilePathTree folderTree;

		// Token: 0x04004E6D RID: 20077
		private IResourceLocator[] resourceLocators = Array.Empty<IResourceLocator>();

		// Token: 0x02000DFD RID: 3581
		public class AddressablesLoadAsyncOperationWrapper<TAsset> : BaseAsyncOperation<TAsset[]> where TAsset : UnityEngine.Object
		{
			// Token: 0x0600522D RID: 21037 RVA: 0x00154DA8 File Offset: 0x00152FA8
			public AddressablesLoadAsyncOperationWrapper(IReadOnlyList<AsyncOperationHandle<TAsset>> handles)
			{
				if (handles.Count == 0)
				{
					this.handles = Array.Empty<AsyncOperationHandle<TAsset>>();
					base.Complete(Array.Empty<TAsset>());
					return;
				}
				this.results = new List<TAsset>(handles.Count);
				this.handles = new AsyncOperationHandle<TAsset>[handles.Count];
				Action<AsyncOperationHandle<TAsset>> value = new Action<AsyncOperationHandle<TAsset>>(this.OnChildOperationCompleted);
				for (int i = 0; i < handles.Count; i++)
				{
					this.handles[i] = handles[i];
					handles[i].Completed += value;
				}
			}

			// Token: 0x0600522E RID: 21038 RVA: 0x00154E48 File Offset: 0x00153048
			private void OnChildOperationCompleted(AsyncOperationHandle<TAsset> handle)
			{
				if (handle.Result)
				{
					this.results.Add(handle.Result);
				}
				this.completionCount++;
				Debug.Log(string.Format("{0} ({1}/{2})", handle.Result, this.completionCount, this.handles.Length));
				if (this.completionCount == this.handles.Length)
				{
					base.Complete(this.results.ToArray());
				}
			}

			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x0600522F RID: 21039 RVA: 0x00154EDC File Offset: 0x001530DC
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

			// Token: 0x04004E6E RID: 20078
			private AsyncOperationHandle<TAsset>[] handles;

			// Token: 0x04004E6F RID: 20079
			private int completionCount;

			// Token: 0x04004E70 RID: 20080
			private List<TAsset> results = new List<TAsset>();
		}
	}
}
