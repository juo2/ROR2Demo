using System;
using System.Reflection;
using HG;
using HG.AssetManagement;
using HG.Coroutines;
using UnityEngine;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E12 RID: 3602
	public class ContentLoadHelper
	{
		// Token: 0x06005281 RID: 21121 RVA: 0x001569EF File Offset: 0x00154BEF
		public ContentLoadHelper()
		{
			this.progress = new ReadableProgress<float>();
			this.coroutine = new ParallelProgressCoroutine(this.progress);
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00156A14 File Offset: 0x00154C14
		public void DispatchLoadConvert<TAssetSrc, TAssetDest>(string folderPath, Action<TAssetDest[]> onComplete, Func<TAssetSrc, TAssetDest> selector = null) where TAssetSrc : UnityEngine.Object
		{
			ContentLoadHelper.<>c__DisplayClass6_0<TAssetSrc, TAssetDest> CS$<>8__locals1 = new ContentLoadHelper.<>c__DisplayClass6_0<TAssetSrc, TAssetDest>();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.folderPath = folderPath;
			CS$<>8__locals1.onComplete = onComplete;
			CS$<>8__locals1.selector = selector;
			CS$<>8__locals1.progressReceiver = new ReadableProgress<float>();
			this.coroutine.Add(CS$<>8__locals1.<DispatchLoadConvert>g__Coroutine|0(), CS$<>8__locals1.progressReceiver);
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00156A65 File Offset: 0x00154C65
		public void DispatchLoad<TAsset>(string folderPath, Action<TAsset[]> onComplete) where TAsset : UnityEngine.Object
		{
			this.DispatchLoadConvert<TAsset, TAsset>(folderPath, onComplete, null);
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00156A70 File Offset: 0x00154C70
		public static void PopulateTypeFields<TAsset>(Type typeToPopulate, NamedAssetCollection<TAsset> assets, Func<string, string> fieldNameToAssetNameConverter = null) where TAsset : UnityEngine.Object
		{
			string[] array = new string[assets.Length];
			for (int i = 0; i < assets.Length; i++)
			{
				array[i] = assets.GetAssetName(assets[i]);
			}
			foreach (FieldInfo fieldInfo in typeToPopulate.GetFields(BindingFlags.Static | BindingFlags.Public))
			{
				if (fieldInfo.FieldType == typeof(TAsset))
				{
					TargetAssetNameAttribute customAttribute = fieldInfo.GetCustomAttribute<TargetAssetNameAttribute>();
					string text;
					if (customAttribute != null)
					{
						text = customAttribute.targetAssetName;
					}
					else if (fieldNameToAssetNameConverter != null)
					{
						text = fieldNameToAssetNameConverter(fieldInfo.Name);
					}
					else
					{
						text = fieldInfo.Name;
					}
					TAsset tasset = assets.Find(text);
					if (tasset != null)
					{
						fieldInfo.SetValue(null, tasset);
					}
					else
					{
						Debug.LogWarning(string.Concat(new string[]
						{
							"Failed to assign ",
							fieldInfo.DeclaringType.Name,
							".",
							fieldInfo.Name,
							": Asset \"",
							text,
							"\" not found."
						}));
					}
				}
			}
		}

		// Token: 0x04004ED6 RID: 20182
		public IAssetRepository assetRepository;

		// Token: 0x04004ED7 RID: 20183
		public ReadableProgress<float> progress;

		// Token: 0x04004ED8 RID: 20184
		public ParallelProgressCoroutine coroutine;

		// Token: 0x02000E13 RID: 3603
		// (Invoke) Token: 0x06005286 RID: 21126
		public unsafe delegate T* GetRefDelegate<T>();

		// Token: 0x02000E14 RID: 3604
		// (Invoke) Token: 0x0600528A RID: 21130
		public delegate void AcceptResultDelegate<T>(T result);
	}
}
