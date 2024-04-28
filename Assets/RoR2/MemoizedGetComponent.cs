using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200096A RID: 2410
	public struct MemoizedGetComponent<TComponent> where TComponent : Component
	{
		// Token: 0x060036BF RID: 14015 RVA: 0x000E7350 File Offset: 0x000E5550
		public TComponent Get(GameObject gameObject)
		{
			if (this.cachedGameObject != gameObject)
			{
				this.cachedGameObject = gameObject;
				this.cachedValue = (gameObject ? gameObject.GetComponent<TComponent>() : default(TComponent));
			}
			return this.cachedValue;
		}

		// Token: 0x04003730 RID: 14128
		private GameObject cachedGameObject;

		// Token: 0x04003731 RID: 14129
		private TComponent cachedValue;
	}
}
