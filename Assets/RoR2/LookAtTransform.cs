using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200079A RID: 1946
	[ExecuteAlways]
	public class LookAtTransform : MonoBehaviour
	{
		// Token: 0x0600291D RID: 10525 RVA: 0x000B21A8 File Offset: 0x000B03A8
		private void LateUpdate()
		{
			if (!this.target)
			{
				return;
			}
			Vector3 vector = this.target.position - base.transform.position;
			if (vector == Vector3.zero)
			{
				return;
			}
			switch (this.axis)
			{
			case LookAtTransform.Axis.Right:
				base.transform.right = vector;
				return;
			case LookAtTransform.Axis.Left:
				base.transform.right = -vector;
				return;
			case LookAtTransform.Axis.Up:
				base.transform.up = vector;
				return;
			case LookAtTransform.Axis.Down:
				base.transform.right = -vector;
				return;
			case LookAtTransform.Axis.Forward:
				base.transform.forward = vector;
				return;
			case LookAtTransform.Axis.Backward:
				base.transform.forward = -vector;
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x04002C8A RID: 11402
		public Transform target;

		// Token: 0x04002C8B RID: 11403
		public LookAtTransform.Axis axis = LookAtTransform.Axis.Forward;

		// Token: 0x0200079B RID: 1947
		public enum Axis
		{
			// Token: 0x04002C8D RID: 11405
			Right,
			// Token: 0x04002C8E RID: 11406
			Left,
			// Token: 0x04002C8F RID: 11407
			Up,
			// Token: 0x04002C90 RID: 11408
			Down,
			// Token: 0x04002C91 RID: 11409
			Forward,
			// Token: 0x04002C92 RID: 11410
			Backward
		}
	}
}
