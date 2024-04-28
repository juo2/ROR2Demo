using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class InterpolationController : MonoBehaviour
{
	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000142 RID: 322 RVA: 0x00007327 File Offset: 0x00005527
	public static float InterpolationFactor
	{
		get
		{
			return InterpolationController.m_interpolationFactor;
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000732E File Offset: 0x0000552E
	public void Start()
	{
		this.m_lastFixedUpdateTimes = new float[2];
		this.m_newTimeIndex = 0;
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00007343 File Offset: 0x00005543
	public void FixedUpdate()
	{
		this.m_newTimeIndex = this.OldTimeIndex();
		this.m_lastFixedUpdateTimes[this.m_newTimeIndex] = Time.fixedTime;
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00007364 File Offset: 0x00005564
	public void Update()
	{
		float num = this.m_lastFixedUpdateTimes[this.m_newTimeIndex];
		float num2 = this.m_lastFixedUpdateTimes[this.OldTimeIndex()];
		if (num != num2)
		{
			InterpolationController.m_interpolationFactor = (Time.time - num) / (num - num2);
			return;
		}
		InterpolationController.m_interpolationFactor = 1f;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x000073AC File Offset: 0x000055AC
	private int OldTimeIndex()
	{
		if (this.m_newTimeIndex != 0)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x04000152 RID: 338
	private float[] m_lastFixedUpdateTimes;

	// Token: 0x04000153 RID: 339
	private int m_newTimeIndex;

	// Token: 0x04000154 RID: 340
	private static float m_interpolationFactor;
}
