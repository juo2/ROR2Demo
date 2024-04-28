using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class FocusedConvergenceAnimator : MonoBehaviour
{
	// Token: 0x06000010 RID: 16 RVA: 0x0000230E File Offset: 0x0000050E
	private void Start()
	{
		this.tempScaleTimer = 0f;
		this.tempRotateTimer = 0f;
		this.animCurve = AnimationCurve.EaseInOut(0f, this.scaleMin, 1f, this.scaleMax);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002348 File Offset: 0x00000548
	private void FixedUpdate()
	{
		if (this.isScaling)
		{
			this.tempScaleTimer += Time.deltaTime;
			if (this.tempScaleTimer <= this.scaleTime * 0.333f)
			{
				float t = this.tempScaleTimer / (this.scaleTime * 0.333f);
				this.xScale = Mathf.Lerp(1f, this.scaleMax, t);
				this.yScale = Mathf.Lerp(1f, this.scaleMax, t);
				this.zScale = Mathf.Lerp(1f, this.scaleMax, t);
			}
			if (this.tempScaleTimer > this.scaleTime * 0.333f && this.tempScaleTimer <= this.scaleTime * 0.666f)
			{
				float t = (this.tempScaleTimer - this.scaleTime * 0.333f) / (this.scaleTime * 0.333f);
				this.xScale = Mathf.Lerp(this.scaleMax, this.scaleMin, t);
				this.yScale = Mathf.Lerp(this.scaleMax, this.scaleMin, t);
				this.zScale = Mathf.Lerp(this.scaleMax, this.scaleMin, t);
			}
			if (this.tempScaleTimer > this.scaleTime * 0.666f)
			{
				float t = (this.tempScaleTimer - this.scaleTime * 0.666f) / (this.scaleTime * 0.333f);
				this.xScale = Mathf.Lerp(this.scaleMin, 1f, t);
				this.yScale = Mathf.Lerp(this.scaleMin, 1f, t);
				this.zScale = Mathf.Lerp(this.scaleMin, 1f, t);
			}
			if (this.tempScaleTimer >= this.scaleTime)
			{
				this.isScaling = false;
				this.tempRotateTimer = 0f;
				this.xRotateTop = UnityEngine.Random.Range(0f, 10f);
				this.yRotateTop = UnityEngine.Random.Range(0f, 10f);
				this.zRotateTop = UnityEngine.Random.Range(0f, 10f);
				return;
			}
			base.transform.localScale = new Vector3(this.xScale, this.yScale, this.zScale);
			return;
		}
		else
		{
			this.tempRotateTimer += Time.deltaTime;
			if (this.tempRotateTimer <= this.rotateTime * 0.5f)
			{
				float t2 = this.tempRotateTimer / (this.rotateTime * 0.5f);
				this.xRotate = Mathf.Lerp(0f, this.xRotateTop, t2);
				this.yRotate = Mathf.Lerp(0f, this.yRotateTop, t2);
				this.zRotate = Mathf.Lerp(0f, this.zRotateTop, t2);
			}
			else
			{
				float t2 = (this.tempRotateTimer - this.rotateTime * 0.5f) / (this.rotateTime * 0.5f);
				this.xRotate = Mathf.Lerp(this.xRotateTop, 0f, t2);
				this.yRotate = Mathf.Lerp(this.yRotateTop, 0f, t2);
				this.zRotate = Mathf.Lerp(this.zRotateTop, 0f, t2);
			}
			if (this.tempRotateTimer >= this.rotateTime)
			{
				this.isScaling = true;
				this.tempScaleTimer = 0f;
				return;
			}
			base.transform.Rotate(new Vector3(this.xRotate, this.yRotate, this.zRotate));
			return;
		}
	}

	// Token: 0x0400000C RID: 12
	public bool animateX;

	// Token: 0x0400000D RID: 13
	public bool animateY;

	// Token: 0x0400000E RID: 14
	public bool animateZ;

	// Token: 0x0400000F RID: 15
	private AnimationCurve animCurve;

	// Token: 0x04000010 RID: 16
	private float xScale;

	// Token: 0x04000011 RID: 17
	private float yScale;

	// Token: 0x04000012 RID: 18
	private float zScale;

	// Token: 0x04000013 RID: 19
	public float scaleTime = 4f;

	// Token: 0x04000014 RID: 20
	private float tempScaleTimer = 4f;

	// Token: 0x04000015 RID: 21
	public float rotateTime = 4f;

	// Token: 0x04000016 RID: 22
	private float tempRotateTimer = 4f;

	// Token: 0x04000017 RID: 23
	private float scaleMax = 1.2f;

	// Token: 0x04000018 RID: 24
	private float scaleMin = 0.8f;

	// Token: 0x04000019 RID: 25
	private bool isScaling;

	// Token: 0x0400001A RID: 26
	private float xRotate;

	// Token: 0x0400001B RID: 27
	private float xRotateTop;

	// Token: 0x0400001C RID: 28
	private float yRotate;

	// Token: 0x0400001D RID: 29
	private float yRotateTop;

	// Token: 0x0400001E RID: 30
	private float zRotate;

	// Token: 0x0400001F RID: 31
	private float zRotateTop;
}
