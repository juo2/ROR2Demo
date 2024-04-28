using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000795 RID: 1941
	public class LimbMatcher : MonoBehaviour
	{
		// Token: 0x060028FF RID: 10495 RVA: 0x000B1C68 File Offset: 0x000AFE68
		public void SetChildLocator(ChildLocator childLocator)
		{
			this.valid = true;
			for (int i = 0; i < this.limbPairs.Length; i++)
			{
				LimbMatcher.LimbPair limbPair = this.limbPairs[i];
				Transform transform = childLocator.FindChild(limbPair.targetChildLimb);
				if (!transform)
				{
					this.valid = false;
					return;
				}
				this.limbPairs[i].targetTransform = transform;
			}
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000B1CCB File Offset: 0x000AFECB
		private void LateUpdate()
		{
			this.UpdateLimbs();
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000B1CD4 File Offset: 0x000AFED4
		private void UpdateLimbs()
		{
			if (!this.valid)
			{
				return;
			}
			for (int i = 0; i < this.limbPairs.Length; i++)
			{
				LimbMatcher.LimbPair limbPair = this.limbPairs[i];
				Transform targetTransform = limbPair.targetTransform;
				if (targetTransform && limbPair.originalTransform)
				{
					limbPair.originalTransform.position = targetTransform.position;
					limbPair.originalTransform.rotation = targetTransform.rotation;
					if (i < this.limbPairs.Length - 1)
					{
						float num = Vector3.Magnitude(this.limbPairs[i + 1].targetTransform.position - targetTransform.position);
						float originalLimbLength = limbPair.originalLimbLength;
						if (this.scaleLimbs)
						{
							Vector3 localScale = limbPair.originalTransform.localScale;
							localScale.y = num / originalLimbLength;
							limbPair.originalTransform.localScale = localScale;
						}
					}
				}
			}
		}

		// Token: 0x04002C77 RID: 11383
		public bool scaleLimbs = true;

		// Token: 0x04002C78 RID: 11384
		private bool valid;

		// Token: 0x04002C79 RID: 11385
		public LimbMatcher.LimbPair[] limbPairs;

		// Token: 0x02000796 RID: 1942
		[Serializable]
		public struct LimbPair
		{
			// Token: 0x04002C7A RID: 11386
			public Transform originalTransform;

			// Token: 0x04002C7B RID: 11387
			public string targetChildLimb;

			// Token: 0x04002C7C RID: 11388
			public float originalLimbLength;

			// Token: 0x04002C7D RID: 11389
			[NonSerialized]
			public Transform targetTransform;
		}
	}
}
