using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200070B RID: 1803
	[ExecuteAlways]
	public class GlassesSize : MonoBehaviour
	{
		// Token: 0x06002525 RID: 9509 RVA: 0x000026ED File Offset: 0x000008ED
		private void Start()
		{
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x0009DD47 File Offset: 0x0009BF47
		private void Update()
		{
			this.UpdateGlasses();
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0009DD50 File Offset: 0x0009BF50
		private void UpdateGlasses()
		{
			Vector3 localScale = base.transform.localScale;
			float num = Mathf.Max(localScale.y, localScale.z);
			Vector3 localScale2 = new Vector3(1f / localScale.x * num, 1f / localScale.y * num, 1f / localScale.z * num);
			if (this.glassesModelBase)
			{
				this.glassesModelBase.transform.localScale = localScale2;
			}
			if (this.glassesBridgeLeft && this.glassesBridgeRight)
			{
				float num2 = (localScale.x / num - 1f) * this.bridgeOffsetScale;
				this.glassesBridgeLeft.transform.localPosition = this.offsetVector * -num2;
				this.glassesBridgeRight.transform.localPosition = this.offsetVector * num2;
			}
		}

		// Token: 0x04002906 RID: 10502
		public Transform glassesModelBase;

		// Token: 0x04002907 RID: 10503
		public Transform glassesBridgeLeft;

		// Token: 0x04002908 RID: 10504
		public Transform glassesBridgeRight;

		// Token: 0x04002909 RID: 10505
		public float bridgeOffsetScale;

		// Token: 0x0400290A RID: 10506
		public Vector3 offsetVector = Vector3.right;
	}
}
