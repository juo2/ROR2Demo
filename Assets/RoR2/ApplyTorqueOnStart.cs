using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005CF RID: 1487
	public class ApplyTorqueOnStart : MonoBehaviour
	{
		// Token: 0x06001AE7 RID: 6887 RVA: 0x00073708 File Offset: 0x00071908
		private void Start()
		{
			Rigidbody component = base.GetComponent<Rigidbody>();
			if (component)
			{
				Vector3 vector = this.localTorque;
				if (this.randomize)
				{
					vector.x = UnityEngine.Random.Range(-vector.x / 2f, vector.x / 2f);
					vector.y = UnityEngine.Random.Range(-vector.y / 2f, vector.y / 2f);
					vector.z = UnityEngine.Random.Range(-vector.z / 2f, vector.z / 2f);
				}
				component.AddRelativeTorque(vector);
			}
		}

		// Token: 0x04002113 RID: 8467
		public Vector3 localTorque;

		// Token: 0x04002114 RID: 8468
		public bool randomize;
	}
}
