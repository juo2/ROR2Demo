using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000867 RID: 2151
	public class RunFixedTimeRotateObject : MonoBehaviour
	{
		// Token: 0x06002F19 RID: 12057 RVA: 0x000C8E61 File Offset: 0x000C7061
		private void Start()
		{
			if (this.isLocal)
			{
				this.initialRotation = base.transform.localRotation;
				return;
			}
			this.initialRotation = base.transform.rotation;
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000C8E90 File Offset: 0x000C7090
		private void FixedUpdate()
		{
			if (Run.instance)
			{
				Quaternion quaternion = this.initialRotation * Quaternion.Euler(this.eulerVelocity * Run.instance.fixedTime);
				if (this.isLocal)
				{
					base.transform.localRotation = quaternion;
					return;
				}
				base.transform.rotation = quaternion;
			}
		}

		// Token: 0x04003102 RID: 12546
		[SerializeField]
		private Vector3 eulerVelocity;

		// Token: 0x04003103 RID: 12547
		[SerializeField]
		private bool isLocal = true;

		// Token: 0x04003104 RID: 12548
		private Quaternion initialRotation;
	}
}
