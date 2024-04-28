using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
public class MoveCurve : MonoBehaviour
{
	// Token: 0x0600003A RID: 58 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00002E68 File Offset: 0x00001068
	private void Update()
	{
		if (this.animateX)
		{
			this.xValue = this.moveCurve.Evaluate(Time.time % (float)this.moveCurve.length) * this.curveScale;
		}
		else
		{
			this.xValue = base.transform.localPosition.x;
		}
		if (this.animateY)
		{
			this.yValue = this.moveCurve.Evaluate(Time.time % (float)this.moveCurve.length) * this.curveScale;
		}
		else
		{
			this.yValue = base.transform.localPosition.y;
		}
		if (this.animateZ)
		{
			this.zValue = this.moveCurve.Evaluate(Time.time % (float)this.moveCurve.length) * this.curveScale;
		}
		else
		{
			this.zValue = base.transform.localPosition.z;
		}
		base.transform.localPosition = new Vector3(this.xValue, this.yValue, this.zValue);
	}

	// Token: 0x04000044 RID: 68
	public bool animateX;

	// Token: 0x04000045 RID: 69
	public bool animateY;

	// Token: 0x04000046 RID: 70
	public bool animateZ;

	// Token: 0x04000047 RID: 71
	public float curveScale = 1f;

	// Token: 0x04000048 RID: 72
	public AnimationCurve moveCurve;

	// Token: 0x04000049 RID: 73
	private float xValue;

	// Token: 0x0400004A RID: 74
	private float yValue;

	// Token: 0x0400004B RID: 75
	private float zValue;
}
