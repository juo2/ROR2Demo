using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006BA RID: 1722
	public class EnableOnMecanimFloat : MonoBehaviour
	{
		// Token: 0x06002188 RID: 8584 RVA: 0x0009073C File Offset: 0x0008E93C
		private void Update()
		{
			if (this.animator)
			{
				float @float = this.animator.GetFloat(this.animatorString);
				bool flag = Mathf.Clamp(@float, this.minFloatValue, this.maxFloatValue) == @float;
				if (flag != this.wasWithinRange)
				{
					GameObject[] array = this.objectsToEnable;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].SetActive(flag);
					}
					array = this.objectsToDisable;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].SetActive(!flag);
					}
					this.wasWithinRange = flag;
				}
			}
		}

		// Token: 0x040026EF RID: 9967
		public Animator animator;

		// Token: 0x040026F0 RID: 9968
		[Tooltip("The name of the mecanim variable to compare against")]
		public string animatorString;

		// Token: 0x040026F1 RID: 9969
		[Tooltip("The minimum value at which the objects are enabled")]
		public float minFloatValue;

		// Token: 0x040026F2 RID: 9970
		[Tooltip("The maximum value at which the objects are enabled")]
		public float maxFloatValue;

		// Token: 0x040026F3 RID: 9971
		public GameObject[] objectsToEnable;

		// Token: 0x040026F4 RID: 9972
		public GameObject[] objectsToDisable;

		// Token: 0x040026F5 RID: 9973
		private bool wasWithinRange;
	}
}
