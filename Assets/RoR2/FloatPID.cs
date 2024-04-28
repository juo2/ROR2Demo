using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class FloatPID : MonoBehaviour
{
	// Token: 0x06000129 RID: 297 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00006F1C File Offset: 0x0000511C
	private void FixedUpdate()
	{
		this.timer += Time.fixedDeltaTime;
		if (this.automaticallyUpdate && this.timer > this.timeBetweenUpdates)
		{
			this.timer -= this.timeBetweenUpdates;
			this.outputFloat = this.UpdatePID();
		}
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00006F70 File Offset: 0x00005170
	public float UpdatePID()
	{
		float num = this.timer - this.lastTimer;
		this.lastTimer = this.timer;
		float num2 = this.targetFloat - this.inputFloat;
		this.errorSum += num2 * num;
		this.deltaError = (num2 - this.lastError) / num;
		this.lastError = num2;
		return (num2 * this.PID.x + this.errorSum * this.PID.y + this.deltaError * this.PID.z) * this.gain;
	}

	// Token: 0x04000135 RID: 309
	[Tooltip("PID Constants.")]
	public Vector3 PID = new Vector3(1f, 0f, 0f);

	// Token: 0x04000136 RID: 310
	public float gain = 1f;

	// Token: 0x04000137 RID: 311
	[HideInInspector]
	[Tooltip("The value we are currently at.")]
	public float inputFloat;

	// Token: 0x04000138 RID: 312
	[Tooltip("The value we want to be at.")]
	[HideInInspector]
	public float targetFloat;

	// Token: 0x04000139 RID: 313
	[HideInInspector]
	[Tooltip("Value output from PID controller; what we read.")]
	public float outputFloat;

	// Token: 0x0400013A RID: 314
	public float timeBetweenUpdates;

	// Token: 0x0400013B RID: 315
	private float timer;

	// Token: 0x0400013C RID: 316
	private float errorSum;

	// Token: 0x0400013D RID: 317
	private float deltaError;

	// Token: 0x0400013E RID: 318
	private float lastError;

	// Token: 0x0400013F RID: 319
	private float lastTimer;

	// Token: 0x04000140 RID: 320
	public bool automaticallyUpdate;
}
