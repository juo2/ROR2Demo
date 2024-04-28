using System;
using System.Collections;
using System.Collections.Generic;
using HG;
using JetBrains.Annotations;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E31 RID: 3633
	public class NamedAssetCollection<TAsset> : NamedAssetCollection, IEquatable<NamedAssetCollection<TAsset>>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06005358 RID: 21336 RVA: 0x00158EB6 File Offset: 0x001570B6
		public NamedAssetCollection([NotNull] Func<TAsset, string> nameProvider)
		{
			this.nameProvider = nameProvider;
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x00158EE6 File Offset: 0x001570E6
		public int Length
		{
			get
			{
				return this.assetInfos.Length;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600535A RID: 21338 RVA: 0x00158EF0 File Offset: 0x001570F0
		public int Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600535B RID: 21339 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007BF RID: 1983
		public TAsset this[int i]
		{
			get
			{
				return this.assetInfos[i].asset;
			}
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x00158F0C File Offset: 0x0015710C
		public void Add([NotNull] TAsset[] newAssets)
		{
			string[] array = new string[newAssets.Length];
			for (int i = 0; i < newAssets.Length; i++)
			{
				array[i] = this.nameProvider(newAssets[i]);
			}
			for (int j = 0; j < newAssets.Length; j++)
			{
				TAsset tasset = newAssets[j];
				string text = array[j];
				if (this.assetToName.ContainsKey(tasset))
				{
					throw new ArgumentException(string.Format("Asset {0} is already registered!", tasset));
				}
				if (this.nameToAsset.ContainsKey(text))
				{
					throw new ArgumentException("Asset name " + text + " is already registered!");
				}
			}
			int num = this.assetInfos.Length;
			int newSize = num + newAssets.Length;
			Array.Resize<NamedAssetCollection<TAsset>.AssetInfo>(ref this.assetInfos, newSize);
			for (int k = 0; k < newAssets.Length; k++)
			{
				TAsset tasset2 = newAssets[k];
				string text2 = array[k];
				this.assetInfos[num + k] = new NamedAssetCollection<TAsset>.AssetInfo
				{
					asset = tasset2,
					assetName = text2
				};
				this.nameToAsset[text2] = tasset2;
				this.assetToName[tasset2] = text2;
			}
			Array.Sort<NamedAssetCollection<TAsset>.AssetInfo>(this.assetInfos);
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x00159044 File Offset: 0x00157244
		[CanBeNull]
		public TAsset Find([NotNull] string assetName)
		{
			TAsset result;
			if (!this.nameToAsset.TryGetValue(assetName, out result))
			{
				return default(TAsset);
			}
			return result;
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0015906C File Offset: 0x0015726C
		[CanBeNull]
		public override bool Find([NotNull] string assetName, out object result)
		{
			TAsset tasset;
			bool result2 = this.nameToAsset.TryGetValue(assetName, out tasset);
			result = tasset;
			return result2;
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x00159090 File Offset: 0x00157290
		[CanBeNull]
		public string GetAssetName([NotNull] TAsset asset)
		{
			string result;
			if (!this.assetToName.TryGetValue(asset, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x001590B0 File Offset: 0x001572B0
		public bool Contains([NotNull] TAsset asset)
		{
			return this.assetToName.ContainsKey(asset);
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x001590BE File Offset: 0x001572BE
		public void Clear()
		{
			this.assetToName.Clear();
			this.nameToAsset.Clear();
			this.assetInfos = Array.Empty<NamedAssetCollection<TAsset>.AssetInfo>();
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x001590E4 File Offset: 0x001572E4
		public override bool Equals(object obj)
		{
			NamedAssetCollection<TAsset> other;
			return (other = (obj as NamedAssetCollection<TAsset>)) != null && this.Equals(other);
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x00159104 File Offset: 0x00157304
		public bool Equals(NamedAssetCollection<TAsset> other)
		{
			if (this == other)
			{
				return true;
			}
			if (this.assetInfos.Length != other.assetInfos.Length)
			{
				return false;
			}
			int i = 0;
			int num = this.assetInfos.Length;
			while (i < num)
			{
				if (!this.assetInfos[i].Equals(other.assetInfos[i]))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x00159162 File Offset: 0x00157362
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0015916C File Offset: 0x0015736C
		public static void Copy(NamedAssetCollection<TAsset> src, NamedAssetCollection<TAsset> dest)
		{
			dest.assetInfos = ArrayUtils.Clone<NamedAssetCollection<TAsset>.AssetInfo>(src.assetInfos);
			dest.assetToName = new Dictionary<TAsset, string>(src.assetToName);
			dest.nameToAsset = new Dictionary<string, TAsset>(src.nameToAsset);
			dest.nameProvider = src.nameProvider;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x001591B8 File Offset: 0x001573B8
		public void CopyTo(NamedAssetCollection<TAsset> dest)
		{
			NamedAssetCollection<TAsset>.Copy(this, dest);
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x001591C1 File Offset: 0x001573C1
		public NamedAssetCollection<TAsset>.AssetEnumerator GetEnumerator()
		{
			return new NamedAssetCollection<TAsset>.AssetEnumerator(this);
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x001591C9 File Offset: 0x001573C9
		IEnumerator<TAsset> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x001591C9 File Offset: 0x001573C9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004F7D RID: 20349
		private Dictionary<TAsset, string> assetToName = new Dictionary<TAsset, string>();

		// Token: 0x04004F7E RID: 20350
		private Dictionary<string, TAsset> nameToAsset = new Dictionary<string, TAsset>();

		// Token: 0x04004F7F RID: 20351
		private NamedAssetCollection<TAsset>.AssetInfo[] assetInfos = Array.Empty<NamedAssetCollection<TAsset>.AssetInfo>();

		// Token: 0x04004F80 RID: 20352
		private Func<TAsset, string> nameProvider;

		// Token: 0x02000E32 RID: 3634
		private struct AssetInfo : IComparable<NamedAssetCollection<TAsset>.AssetInfo>, IEquatable<NamedAssetCollection<TAsset>.AssetInfo>
		{
			// Token: 0x0600536B RID: 21355 RVA: 0x001591D6 File Offset: 0x001573D6
			public int CompareTo(NamedAssetCollection<TAsset>.AssetInfo other)
			{
				return string.Compare(this.assetName, other.assetName, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0600536C RID: 21356 RVA: 0x001591EA File Offset: 0x001573EA
			public bool Equals(NamedAssetCollection<TAsset>.AssetInfo other)
			{
				return object.Equals(this.asset, other.asset) && string.Equals(this.assetName, other.assetName, StringComparison.Ordinal);
			}

			// Token: 0x04004F81 RID: 20353
			public TAsset asset;

			// Token: 0x04004F82 RID: 20354
			public string assetName;
		}

		// Token: 0x02000E33 RID: 3635
		public struct AssetEnumerator : IEnumerator<TAsset>, IEnumerator, IDisposable
		{
			// Token: 0x0600536D RID: 21357 RVA: 0x0015921D File Offset: 0x0015741D
			public AssetEnumerator(NamedAssetCollection<TAsset> src)
			{
				this.src = src;
				this.i = -1;
			}

			// Token: 0x170007C0 RID: 1984
			// (get) Token: 0x0600536E RID: 21358 RVA: 0x0015922D File Offset: 0x0015742D
			public TAsset Current
			{
				get
				{
					return this.src.assetInfos[this.i].asset;
				}
			}

			// Token: 0x170007C1 RID: 1985
			// (get) Token: 0x0600536F RID: 21359 RVA: 0x0015924A File Offset: 0x0015744A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06005370 RID: 21360 RVA: 0x000026ED File Offset: 0x000008ED
			public void Dispose()
			{
			}

			// Token: 0x06005371 RID: 21361 RVA: 0x00159257 File Offset: 0x00157457
			public bool MoveNext()
			{
				this.i++;
				return this.i < this.src.Length;
			}

			// Token: 0x06005372 RID: 21362 RVA: 0x0015927A File Offset: 0x0015747A
			public void Reset()
			{
				this.i = -1;
			}

			// Token: 0x04004F83 RID: 20355
			private int i;

			// Token: 0x04004F84 RID: 20356
			private NamedAssetCollection<TAsset> src;
		}
	}
}
