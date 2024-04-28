using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008A7 RID: 2215
	public class StageCountObjectFilter : MonoBehaviour
	{
		// Token: 0x0600313B RID: 12603 RVA: 0x000D0E64 File Offset: 0x000CF064
		private void Start()
		{
			if (Run.instance)
			{
				bool active = Run.instance.stageClearCount >= this.minimumStageClearCount;
				for (int i = 0; i < this.gameObjects.Length; i++)
				{
					GameObject gameObject = this.gameObjects[i];
					if (gameObject)
					{
						gameObject.SetActive(active);
					}
				}
			}
		}

		// Token: 0x040032B9 RID: 12985
		[Tooltip("The minimum stage required to start enabling objects.")]
		public int minimumStageClearCount;

		// Token: 0x040032BA RID: 12986
		[Tooltip("The objects to activate or deactivate, depending on the stage count.")]
		public GameObject[] gameObjects;
	}
}
