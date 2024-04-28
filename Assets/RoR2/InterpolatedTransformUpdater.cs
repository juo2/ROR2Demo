using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class InterpolatedTransformUpdater : MonoBehaviour
{
	// Token: 0x0600013F RID: 319 RVA: 0x0000730C File Offset: 0x0000550C
	private void Awake()
	{
		this.m_interpolatedTransform = base.GetComponent<InterpolatedTransform>();
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000731A File Offset: 0x0000551A
	private void FixedUpdate()
	{
		this.m_interpolatedTransform.LateFixedUpdate();
	}

	// Token: 0x04000151 RID: 337
	private InterpolatedTransform m_interpolatedTransform;
}
