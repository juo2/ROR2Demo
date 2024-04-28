using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E34 RID: 3636
	public readonly struct ReadOnlyNamedAssetCollection<TAsset> : IEnumerable<!0>, IEnumerable, IEquatable<ReadOnlyNamedAssetCollection<TAsset>>
	{
		// Token: 0x06005373 RID: 21363 RVA: 0x00159283 File Offset: 0x00157483
		public ReadOnlyNamedAssetCollection(NamedAssetCollection<TAsset> src)
		{
			this.src = src;
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06005374 RID: 21364 RVA: 0x0015928C File Offset: 0x0015748C
		public int Length
		{
			get
			{
				return this.src.Length;
			}
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x00159299 File Offset: 0x00157499
		public TAsset Find(string assetName)
		{
			return this.src.Find(assetName);
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x001592A7 File Offset: 0x001574A7
		public bool Find(string assetName, out object result)
		{
			return this.src.Find(assetName, out result);
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x001592B6 File Offset: 0x001574B6
		public string GetAssetName(TAsset asset)
		{
			return this.src.GetAssetName(asset);
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x001592C4 File Offset: 0x001574C4
		public bool Contains([NotNull] TAsset asset)
		{
			return this.src.Contains(asset);
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x001592D2 File Offset: 0x001574D2
		public static void Copy(ReadOnlyNamedAssetCollection<TAsset> src, NamedAssetCollection<TAsset> dest)
		{
			NamedAssetCollection<TAsset>.Copy(src.src, dest);
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x001592E0 File Offset: 0x001574E0
		public void CopyTo(NamedAssetCollection<TAsset> dest)
		{
			this.src.CopyTo(dest);
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x001592F0 File Offset: 0x001574F0
		public override bool Equals(object obj)
		{
			NamedAssetCollection<TAsset> namedAssetCollection;
			return (namedAssetCollection = (obj as NamedAssetCollection<TAsset>)) != null && this.Equals(namedAssetCollection);
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x00159315 File Offset: 0x00157515
		public override int GetHashCode()
		{
			return this.src.GetHashCode();
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00159322 File Offset: 0x00157522
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0015932F File Offset: 0x0015752F
		public NamedAssetCollection<TAsset>.AssetEnumerator GetEnumerator()
		{
			return this.src.GetEnumerator();
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x00159322 File Offset: 0x00157522
		IEnumerator<TAsset> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x0015933C File Offset: 0x0015753C
		public bool Equals(ReadOnlyNamedAssetCollection<TAsset> other)
		{
			return this.src.Equals(other.src);
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x0015934F File Offset: 0x0015754F
		public static implicit operator ReadOnlyNamedAssetCollection<TAsset>(NamedAssetCollection<TAsset> src)
		{
			return new ReadOnlyNamedAssetCollection<TAsset>(src);
		}

		// Token: 0x04004F85 RID: 20357
		private readonly NamedAssetCollection<TAsset> src;
	}
}
