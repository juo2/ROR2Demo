using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class ScaleProjector : MonoBehaviour
{
	// Token: 0x0600017E RID: 382 RVA: 0x00007EB7 File Offset: 0x000060B7
	private void Start()
	{
		this.projector = base.GetComponent<Projector>();
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00007EC5 File Offset: 0x000060C5
	private void Update()
	{
		if (this.projector)
		{
			this.projector.orthographicSize = base.transform.lossyScale.x;
		}
	}

	// Token: 0x0400018B RID: 395
	private Projector projector;
}
