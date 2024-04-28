using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200055E RID: 1374
	[CreateAssetMenu(menuName = "RoR2/SimpleSpriteAnimation")]
	public class SimpleSpriteAnimation : ScriptableObject
	{
		// Token: 0x04001E9F RID: 7839
		public float frameRate = 60f;

		// Token: 0x04001EA0 RID: 7840
		public SimpleSpriteAnimation.Frame[] frames = Array.Empty<SimpleSpriteAnimation.Frame>();

		// Token: 0x0200055F RID: 1375
		[Serializable]
		public struct Frame
		{
			// Token: 0x04001EA1 RID: 7841
			public Sprite sprite;

			// Token: 0x04001EA2 RID: 7842
			public int duration;
		}
	}
}
