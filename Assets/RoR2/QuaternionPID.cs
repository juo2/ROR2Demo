using System;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x02000052 RID: 82
public class QuaternionPID : MonoBehaviour
{
	// Token: 0x06000166 RID: 358 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00007A45 File Offset: 0x00005C45
	private void Update()
	{
		this.timer += Time.deltaTime;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00007A5C File Offset: 0x00005C5C
	public Vector3 UpdatePID()
	{
		float num = this.timer - this.lastTimer;
		this.lastTimer = this.timer;
		if (num != 0f)
		{
			Quaternion quaternion = this.targetQuat * Quaternion.Inverse(this.inputQuat);
			if (quaternion.w < 0f)
			{
				quaternion.x *= -1f;
				quaternion.y *= -1f;
				quaternion.z *= -1f;
				quaternion.w *= -1f;
			}
			Vector3 a;
			a.x = quaternion.x;
			a.y = quaternion.y;
			a.z = quaternion.z;
			this.errorSum += a * num;
			this.deltaError = (a - this.lastError) / num;
			this.lastError = a;
			this.outputVector = a * this.PID.x + this.errorSum * this.PID.y + this.deltaError * this.PID.z;
			return this.outputVector * this.gain;
		}
		return Vector3.zero;
	}

	// Token: 0x04000171 RID: 369
	[FormerlySerializedAs("name")]
	[Tooltip("Just a field for user naming. Doesn't do anything.")]
	public string customName;

	// Token: 0x04000172 RID: 370
	[Tooltip("PID Constants.")]
	public Vector3 PID = new Vector3(1f, 0f, 0f);

	// Token: 0x04000173 RID: 371
	[Tooltip("The quaternion we are currently at.")]
	public Quaternion inputQuat = Quaternion.identity;

	// Token: 0x04000174 RID: 372
	[Tooltip("The quaternion we want to be at.")]
	public Quaternion targetQuat = Quaternion.identity;

	// Token: 0x04000175 RID: 373
	[Tooltip("Vector output from PID controller; what we read.")]
	[HideInInspector]
	public Vector3 outputVector = Vector3.zero;

	// Token: 0x04000176 RID: 374
	public float gain = 1f;

	// Token: 0x04000177 RID: 375
	private Vector3 errorSum = Vector3.zero;

	// Token: 0x04000178 RID: 376
	private Vector3 deltaError = Vector3.zero;

	// Token: 0x04000179 RID: 377
	private Vector3 lastError = Vector3.zero;

	// Token: 0x0400017A RID: 378
	private float lastTimer;

	// Token: 0x0400017B RID: 379
	private float timer;
}
