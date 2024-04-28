using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
public class ForcePose : MonoBehaviour
{
	// Token: 0x0600012D RID: 301 RVA: 0x000026ED File Offset: 0x000008ED
	private void Start()
	{
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00007033 File Offset: 0x00005233
	private void Update()
	{
		if (this.clip)
		{
			this.clip.SampleAnimation(base.gameObject, this.cycle * this.clip.length);
		}
	}

	// Token: 0x04000141 RID: 321
	[Tooltip("The animation clip to force.")]
	public AnimationClip clip;

	// Token: 0x04000142 RID: 322
	[Range(0f, 1f)]
	[Tooltip("The moment in the cycle to force.")]
	public float cycle;
}
