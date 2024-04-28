using System;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x02000065 RID: 101
public class VectorPID : MonoBehaviour
{
	// Token: 0x060001A1 RID: 417 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000086D4 File Offset: 0x000068D4
	private void FixedUpdate()
	{
		this.timer += Time.fixedDeltaTime;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000086E8 File Offset: 0x000068E8
	public Vector3 UpdatePID()
	{
		float num = this.timer - this.lastTimer;
		this.lastTimer = this.timer;
		if (num != 0f)
		{
			Vector3 a;
			if (this.isAngle)
			{
				a = Vector3.zero;
				a.x = Mathf.DeltaAngle(this.inputVector.x, this.targetVector.x);
				a.y = Mathf.DeltaAngle(this.inputVector.y, this.targetVector.y);
				a.z = Mathf.DeltaAngle(this.inputVector.z, this.targetVector.z);
			}
			else
			{
				a = this.targetVector - this.inputVector;
			}
			this.errorSum += a * num;
			this.deltaError = (a - this.lastError) / num;
			this.lastError = a;
			this.outputVector = a * this.PID.x + this.errorSum * this.PID.y + this.deltaError * this.PID.z;
			return this.outputVector * this.gain;
		}
		return Vector3.zero;
	}

	// Token: 0x040001BA RID: 442
	[FormerlySerializedAs("name")]
	[Tooltip("Just a field for user naming. Doesn't do anything.")]
	public string customName;

	// Token: 0x040001BB RID: 443
	[Tooltip("PID Constants.")]
	public Vector3 PID = new Vector3(1f, 0f, 0f);

	// Token: 0x040001BC RID: 444
	[HideInInspector]
	[Tooltip("The vector we are currently at.")]
	public Vector3 inputVector = Vector3.zero;

	// Token: 0x040001BD RID: 445
	[HideInInspector]
	[Tooltip("The vector we want to be at.")]
	public Vector3 targetVector = Vector3.zero;

	// Token: 0x040001BE RID: 446
	[HideInInspector]
	[Tooltip("Vector output from PID controller; what we read.")]
	public Vector3 outputVector = Vector3.zero;

	// Token: 0x040001BF RID: 447
	[Tooltip("This is an euler angle, so we need to wrap correctly")]
	public bool isAngle;

	// Token: 0x040001C0 RID: 448
	public float gain = 1f;

	// Token: 0x040001C1 RID: 449
	private Vector3 errorSum = Vector3.zero;

	// Token: 0x040001C2 RID: 450
	private Vector3 deltaError = Vector3.zero;

	// Token: 0x040001C3 RID: 451
	private Vector3 lastError = Vector3.zero;

	// Token: 0x040001C4 RID: 452
	private float lastTimer;

	// Token: 0x040001C5 RID: 453
	private float timer;
}
