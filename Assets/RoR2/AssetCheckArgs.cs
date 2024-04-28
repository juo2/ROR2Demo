using System;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009F3 RID: 2547
	public class AssetCheckArgs
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000F421D File Offset: 0x000F241D
		[CanBeNull]
		public Component assetComponent
		{
			get
			{
				return this.asset as Component;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000F422A File Offset: 0x000F242A
		[CanBeNull]
		public GameObject gameObject
		{
			get
			{
				Component assetComponent = this.assetComponent;
				if (assetComponent == null)
				{
					return null;
				}
				return assetComponent.gameObject;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000F423D File Offset: 0x000F243D
		[CanBeNull]
		public GameObject gameObjectRoot
		{
			get
			{
				GameObject gameObject = this.gameObject;
				if (gameObject == null)
				{
					return null;
				}
				Transform root = gameObject.transform.root;
				if (root == null)
				{
					return null;
				}
				return root.gameObject;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000F4260 File Offset: 0x000F2460
		public bool isPrefab
		{
			get
			{
				return AssetCheckArgs.GameObjectIsPrefab(this.gameObjectRoot);
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x000F426D File Offset: 0x000F246D
		public bool isPrefabVariant
		{
			get
			{
				return AssetCheckArgs.GameObjectIsPrefabVariant(this.gameObjectRoot);
			}
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x0000CF8A File Offset: 0x0000B18A
		private static bool GameObjectIsPrefab(GameObject gameObject)
		{
			return false;
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x0000CF8A File Offset: 0x0000B18A
		private static bool GameObjectIsPrefabVariant(GameObject gameObject)
		{
			return false;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000F427C File Offset: 0x000F247C
		[CanBeNull]
		public GameObject prefabRoot
		{
			get
			{
				GameObject gameObjectRoot = this.gameObjectRoot;
				if (AssetCheckArgs.GameObjectIsPrefab(gameObjectRoot))
				{
					return gameObjectRoot;
				}
				return null;
			}
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000F429B File Offset: 0x000F249B
		public void UpdatePrefab()
		{
			this.prefabRoot;
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000F42A9 File Offset: 0x000F24A9
		public void Log(string str, UnityEngine.Object context = null)
		{
			this.projectIssueChecker.Log(str, context);
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x000F42B8 File Offset: 0x000F24B8
		public void LogError(string str, UnityEngine.Object context = null)
		{
			this.projectIssueChecker.LogError(str, context);
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000F42C7 File Offset: 0x000F24C7
		public void LogFormat(UnityEngine.Object context, string format, params object[] formatArgs)
		{
			this.projectIssueChecker.LogFormat(context, format, formatArgs);
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x000F42D7 File Offset: 0x000F24D7
		public void LogErrorFormat(UnityEngine.Object context, string format, params object[] formatArgs)
		{
			this.projectIssueChecker.LogErrorFormat(context, format, formatArgs);
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000F42E7 File Offset: 0x000F24E7
		public void EnsurePath(string path)
		{
			Directory.Exists(path);
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000F42F0 File Offset: 0x000F24F0
		public T LoadAsset<T>(string fullFilePath) where T : UnityEngine.Object
		{
			File.Exists(fullFilePath);
			return default(T);
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x000026ED File Offset: 0x000008ED
		public static void CreateAsset<T>(string path)
		{
		}

		// Token: 0x040039B5 RID: 14773
		[NotNull]
		public ProjectIssueChecker projectIssueChecker;

		// Token: 0x040039B6 RID: 14774
		[NotNull]
		public UnityEngine.Object asset;
	}
}
