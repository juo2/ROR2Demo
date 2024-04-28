using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000523 RID: 1315
	[CreateAssetMenu(menuName = "RoR2/AssetCollection")]
	public class AssetCollection : ScriptableObject
	{
		// Token: 0x060017EA RID: 6122 RVA: 0x0006922C File Offset: 0x0006742C
		[ContextMenu("Add selected assets.")]
		private void AddSelectedAssets()
		{
			UnityEngine.Object[] additionalAssets = Array.Empty<UnityEngine.Object>();
			this.AddAssets(additionalAssets);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00069248 File Offset: 0x00067448
		public void AddAssets(UnityEngine.Object[] additionalAssets)
		{
			int num = this.assets.Length;
			Array.Resize<UnityEngine.Object>(ref this.assets, this.assets.Length + additionalAssets.Length);
			for (int i = 0; i < additionalAssets.Length; i++)
			{
				this.assets[num + i] = additionalAssets[i];
			}
		}

		// Token: 0x04001D8F RID: 7567
		public UnityEngine.Object[] assets = Array.Empty<UnityEngine.Object>();
	}
}
