using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace RoR2
{
	// Token: 0x02000997 RID: 2455
	public class AddressablesTextDataManager : TextDataManager
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x000EA836 File Offset: 0x000E8A36
		public override bool InitializedConfigFiles
		{
			get
			{
				return this.configInitialized;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000EA83E File Offset: 0x000E8A3E
		public override bool InitializedLocFiles
		{
			get
			{
				return this.locInitialized;
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000EA848 File Offset: 0x000E8A48
		public AddressablesTextDataManager()
		{
			Addressables.LoadResourceLocationsAsync("Config", null).Completed += delegate(AsyncOperationHandle<IList<IResourceLocation>> addressesResult)
			{
				Addressables.LoadAssetsAsync<TextAsset>(addressesResult.Result, delegate(TextAsset conf)
				{
					this.configFiles.Add(conf);
				}).Completed += delegate(AsyncOperationHandle<IList<TextAsset>> assetsResult)
				{
					this.configInitialized = true;
				};
			};
			this.locInitialized = true;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000EA88C File Offset: 0x000E8A8C
		public override string GetConfFile(string fileName, string path)
		{
			string fileNameLower = fileName.ToLower();
			TextAsset textAsset = this.configFiles.Find((TextAsset x) => x.name.ToLower().Contains(fileNameLower));
			if (textAsset != null)
			{
				return textAsset.text;
			}
			return null;
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000EA8D4 File Offset: 0x000E8AD4
		public override void GetLocFiles(string folderPath, Action<string[]> callback)
		{
			Addressables.LoadResourceLocationsAsync("Localization", null).Completed += delegate(AsyncOperationHandle<IList<IResourceLocation>> addressesResult)
			{
				List<IResourceLocation> list = new List<IResourceLocation>();
				for (int i = addressesResult.Result.Count - 1; i >= 0; i--)
				{
					if (addressesResult.Result[i].PrimaryKey.StartsWith(folderPath))
					{
						list.Add(addressesResult.Result[i]);
					}
				}
				List<TextAsset> textAssetResults = new List<TextAsset>(list.Count);
				Addressables.LoadAssetsAsync<TextAsset>(list, delegate(TextAsset x)
				{
					if (x != null)
					{
						textAssetResults.Add(x);
					}
				}).Completed += delegate(AsyncOperationHandle<IList<TextAsset>> x)
				{
					int count = textAssetResults.Count;
					string[] array = new string[count];
					for (int j = 0; j < count; j++)
					{
						array[j] = textAssetResults[j].text;
					}
					Action<string[]> callback2 = callback;
					if (callback2 == null)
					{
						return;
					}
					callback2(array);
				};
			};
		}

		// Token: 0x040037F9 RID: 14329
		private bool configInitialized;

		// Token: 0x040037FA RID: 14330
		private bool locInitialized;

		// Token: 0x040037FB RID: 14331
		private List<TextAsset> configFiles = new List<TextAsset>();
	}
}
