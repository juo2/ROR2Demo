using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[RequireComponent(typeof(Animator))]
public class RootMotionAccumulator : MonoBehaviour
{
	// Token: 0x0600016F RID: 367 RVA: 0x00007C60 File Offset: 0x00005E60
	public Vector3 ExtractRootMotion()
	{
		Vector3 result = this.accumulatedRootMotion;
		this.accumulatedRootMotion = Vector3.zero;
		return result;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00007C73 File Offset: 0x00005E73
	public Quaternion ExtractRootRotation()
	{
		Quaternion result = this.accumulatedRootRotation;
		this.accumulatedRootRotation = Quaternion.identity;
		return result;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00007C86 File Offset: 0x00005E86
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
		this.accumulatedRootRotation = Quaternion.identity;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00007CA0 File Offset: 0x00005EA0
	private void OnAnimatorMove()
	{
		this.accumulatedRootMotion += this.animator.deltaPosition;
		if (this.accumulateRotation)
		{
			this.accumulatedRootRotation *= this.animator.deltaRotation;
		}
	}

	// Token: 0x0400017E RID: 382
	private Animator animator;

	// Token: 0x0400017F RID: 383
	[NonSerialized]
	public Vector3 accumulatedRootMotion;

	// Token: 0x04000180 RID: 384
	public Quaternion accumulatedRootRotation;

	// Token: 0x04000181 RID: 385
	public bool accumulateRotation;
}
