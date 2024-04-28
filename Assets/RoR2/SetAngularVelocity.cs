using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
[RequireComponent(typeof(Rigidbody))]
public class SetAngularVelocity : MonoBehaviour
{
	// Token: 0x06000184 RID: 388 RVA: 0x00007F91 File Offset: 0x00006191
	private void Start()
	{
		this.rigidBody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00007F9F File Offset: 0x0000619F
	private void FixedUpdate()
	{
		this.rigidBody.maxAngularVelocity = this.angularVelocity.magnitude;
		this.rigidBody.angularVelocity = base.transform.TransformVector(this.angularVelocity);
	}

	// Token: 0x0400018F RID: 399
	public Vector3 angularVelocity;

	// Token: 0x04000190 RID: 400
	private Rigidbody rigidBody;
}
