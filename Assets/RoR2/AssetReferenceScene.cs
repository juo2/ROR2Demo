using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoR2
{
	// Token: 0x02000524 RID: 1316
	[Serializable]
	public class AssetReferenceScene : AssetReference
	{
		// Token: 0x060017ED RID: 6125 RVA: 0x000692A3 File Offset: 0x000674A3
		public AssetReferenceScene(string guid) : base(guid)
		{
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000692AC File Offset: 0x000674AC
		public override bool ValidateAsset(string path)
		{
			return base.ValidateAsset(path) && path.EndsWith(".unity", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x000692C8 File Offset: 0x000674C8
		public override bool ValidateAsset(UnityEngine.Object obj)
		{
			bool flag = base.ValidateAsset(obj);
			bool flag2 = false;
			return flag && flag2;
		}
	}
}
