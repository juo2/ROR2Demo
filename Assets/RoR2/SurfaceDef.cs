using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000563 RID: 1379
	[CreateAssetMenu(menuName = "RoR2/SurfaceDef")]
	public class SurfaceDef : ScriptableObject
	{
		// Token: 0x060018EB RID: 6379 RVA: 0x0006C103 File Offset: 0x0006A303
		private void OnValidate()
		{
			if (!Application.isPlaying && this.surfaceDefIndex != SurfaceDefIndex.Invalid)
			{
				this.surfaceDefIndex = SurfaceDefIndex.Invalid;
			}
		}

		// Token: 0x04001EB6 RID: 7862
		[HideInInspector]
		public SurfaceDefIndex surfaceDefIndex = SurfaceDefIndex.Invalid;

		// Token: 0x04001EB7 RID: 7863
		public Color approximateColor;

		// Token: 0x04001EB8 RID: 7864
		public GameObject impactEffectPrefab;

		// Token: 0x04001EB9 RID: 7865
		public GameObject footstepEffectPrefab;

		// Token: 0x04001EBA RID: 7866
		public string impactSoundString;

		// Token: 0x04001EBB RID: 7867
		public string materialSwitchString;

		// Token: 0x04001EBC RID: 7868
		public bool isSlippery;
	}
}
