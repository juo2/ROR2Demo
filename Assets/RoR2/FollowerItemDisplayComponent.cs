using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006DF RID: 1759
	public class FollowerItemDisplayComponent : MonoBehaviour
	{
		// Token: 0x060022C2 RID: 8898 RVA: 0x00096283 File Offset: 0x00094483
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x00096294 File Offset: 0x00094494
		private void LateUpdate()
		{
			if (!this.target)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			Quaternion rotation = this.target.rotation;
			this.transform.position = this.target.position + rotation * this.localPosition;
			this.transform.rotation = rotation * this.localRotation;
			this.transform.localScale = this.localScale;
		}

		// Token: 0x040027DB RID: 10203
		public Transform target;

		// Token: 0x040027DC RID: 10204
		public Vector3 localPosition;

		// Token: 0x040027DD RID: 10205
		public Quaternion localRotation;

		// Token: 0x040027DE RID: 10206
		public Vector3 localScale;

		// Token: 0x040027DF RID: 10207
		private new Transform transform;
	}
}
