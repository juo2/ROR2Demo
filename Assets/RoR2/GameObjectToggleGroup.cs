using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200087C RID: 2172
	[Serializable]
	public struct GameObjectToggleGroup
	{
		// Token: 0x0400315C RID: 12636
		public GameObject[] objects;

		// Token: 0x0400315D RID: 12637
		public int minEnabled;

		// Token: 0x0400315E RID: 12638
		public int maxEnabled;
	}
}
