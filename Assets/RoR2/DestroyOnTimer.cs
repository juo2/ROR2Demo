using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200069D RID: 1693
	public class DestroyOnTimer : MonoBehaviour
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x0008DE2A File Offset: 0x0008C02A
		private void FixedUpdate()
		{
			this.age += Time.fixedDeltaTime;
			if (this.age > this.duration)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0008DE57 File Offset: 0x0008C057
		private void OnDisable()
		{
			if (this.resetAgeOnDisable)
			{
				this.age = 0f;
			}
		}

		// Token: 0x04002669 RID: 9833
		public float duration;

		// Token: 0x0400266A RID: 9834
		public bool resetAgeOnDisable;

		// Token: 0x0400266B RID: 9835
		private float age;
	}
}
