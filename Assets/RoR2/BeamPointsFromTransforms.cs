using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
[ExecuteAlways]
public class BeamPointsFromTransforms : MonoBehaviour
{
	// Token: 0x060000E3 RID: 227 RVA: 0x0000509C File Offset: 0x0000329C
	private void Start()
	{
		this.UpdateBeamPositions();
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x0000509C File Offset: 0x0000329C
	private void Update()
	{
		this.UpdateBeamPositions();
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x000050A4 File Offset: 0x000032A4
	private void UpdateBeamPositions()
	{
		if (this.target)
		{
			int num = this.pointTransforms.Length;
			this.target.positionCount = num;
			for (int i = 0; i < num; i++)
			{
				Transform transform = this.pointTransforms[i];
				if (transform)
				{
					this.target.SetPosition(i, transform.position);
				}
			}
		}
	}

	// Token: 0x040000D4 RID: 212
	[Tooltip("Line Renderer to set the positions of.")]
	public LineRenderer target;

	// Token: 0x040000D5 RID: 213
	[SerializeField]
	[Tooltip("Transforms to use as the points for the line renderer.")]
	private Transform[] pointTransforms = Array.Empty<Transform>();
}
