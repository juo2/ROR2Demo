using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class RJShroomBounce : MonoBehaviour
{
	// Token: 0x06000045 RID: 69 RVA: 0x0000307C File Offset: 0x0000127C
	public void Start()
	{
		this.shroomAnimator = base.GetComponent<Animator>();
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0000308A File Offset: 0x0000128A
	public void Bounce()
	{
		this.shroomAnimator.Play("Bounce", 0);
	}

	// Token: 0x04000051 RID: 81
	private Animator shroomAnimator;
}
