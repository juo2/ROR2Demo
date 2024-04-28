using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class PhysicsController : MonoBehaviour
{
	// Token: 0x0600015C RID: 348 RVA: 0x000076F2 File Offset: 0x000058F2
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(base.transform.TransformPoint(this.centerOfMass), 0.5f);
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00007719 File Offset: 0x00005919
	private void Awake()
	{
		this.carRigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000026ED File Offset: 0x000008ED
	private void Update()
	{
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00007728 File Offset: 0x00005928
	private void FixedUpdate()
	{
		if (!this.turnOnInput || Input.GetAxis("Vertical") > 0f || Input.GetAxis("Vertical") > 0f)
		{
			this.desiredHeading = this.cameraTransform.forward;
			this.desiredHeading = Vector3.Project(this.desiredHeading, base.transform.forward);
			this.desiredHeading = this.cameraTransform.forward - this.desiredHeading;
			Debug.DrawRay(base.transform.position, this.desiredHeading * 15f, Color.magenta);
		}
		Vector3 vector = -base.transform.up;
		Debug.DrawRay(base.transform.position, vector * 15f, Color.blue);
		Vector3 a = Vector3.Cross(vector, this.desiredHeading);
		Debug.DrawRay(base.transform.position, a * 15f, Color.red);
		a.x = 0f;
		a.z = 0f;
		this.errorSum += a * Time.fixedDeltaTime;
		this.deltaError = (a - this.lastError) / Time.fixedDeltaTime;
		this.lastError = a;
		this.carRigidbody.AddTorque(a * this.PID.x + this.errorSum * this.PID.y + this.deltaError * this.PID.z, ForceMode.Acceleration);
	}

	// Token: 0x04000163 RID: 355
	public Vector3 centerOfMass = Vector3.zero;

	// Token: 0x04000164 RID: 356
	private Rigidbody carRigidbody;

	// Token: 0x04000165 RID: 357
	public Transform cameraTransform;

	// Token: 0x04000166 RID: 358
	public Vector3 PID = new Vector3(1f, 0f, 0f);

	// Token: 0x04000167 RID: 359
	public bool turnOnInput;

	// Token: 0x04000168 RID: 360
	private Vector3 errorSum = Vector3.zero;

	// Token: 0x04000169 RID: 361
	private Vector3 deltaError = Vector3.zero;

	// Token: 0x0400016A RID: 362
	private Vector3 lastError = Vector3.zero;

	// Token: 0x0400016B RID: 363
	private Vector3 desiredHeading;
}
