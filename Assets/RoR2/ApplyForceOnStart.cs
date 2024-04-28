using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005CD RID: 1485
	public class ApplyForceOnStart : MonoBehaviour
	{
		// Token: 0x06001AE3 RID: 6883 RVA: 0x0007366C File Offset: 0x0007186C
		private void Start()
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			if (component)
			{
				component.AddRelativeForce(this.localForce);
			}
		}

		// Token: 0x0400210E RID: 8462
		public Vector3 localForce;
	}
}
