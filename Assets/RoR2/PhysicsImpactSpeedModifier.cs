using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[RequireComponent(typeof(Rigidbody))]
public class PhysicsImpactSpeedModifier : MonoBehaviour
{
	// Token: 0x06000163 RID: 355 RVA: 0x000079D1 File Offset: 0x00005BD1
	private void Awake()
	{
		this.rigid = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000079E0 File Offset: 0x00005BE0
	private void OnCollisionEnter(Collision collision)
	{
		Vector3 normal = collision.contacts[0].normal;
		Vector3 velocity = this.rigid.velocity;
		Vector3 vector = Vector3.Project(velocity, normal);
		Vector3 vector2 = velocity - vector;
		vector *= this.normalSpeedModifier;
		vector2 *= this.perpendicularSpeedModifier;
		this.rigid.velocity = vector + vector2;
	}

	// Token: 0x0400016E RID: 366
	public float normalSpeedModifier;

	// Token: 0x0400016F RID: 367
	public float perpendicularSpeedModifier;

	// Token: 0x04000170 RID: 368
	private Rigidbody rigid;
}
