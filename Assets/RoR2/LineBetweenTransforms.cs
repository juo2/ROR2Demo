using System;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x0200004B RID: 75
[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class LineBetweenTransforms : MonoBehaviour
{
	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600014B RID: 331 RVA: 0x000074DD File Offset: 0x000056DD
	// (set) Token: 0x0600014C RID: 332 RVA: 0x000074E5 File Offset: 0x000056E5
	public Transform[] transformNodes
	{
		get
		{
			return this._transformNodes;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._transformNodes = value;
			this.UpdateVertexBufferSize();
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00007504 File Offset: 0x00005704
	private void PushPositionsToLineRenderer()
	{
		Vector3[] array = this.vertexList;
		Transform[] transformNodes = this.transformNodes;
		for (int i = 0; i < array.Length; i++)
		{
			Transform transform = transformNodes[i];
			if (transform)
			{
				array[i] = transform.position;
			}
		}
		this.lineRenderer.SetPositions(array);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00007552 File Offset: 0x00005752
	private void UpdateVertexBufferSize()
	{
		Array.Resize<Vector3>(ref this.vertexList, this.transformNodes.Length);
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00007567 File Offset: 0x00005767
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.UpdateVertexBufferSize();
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000757B File Offset: 0x0000577B
	private void LateUpdate()
	{
		this.PushPositionsToLineRenderer();
	}

	// Token: 0x0400015D RID: 349
	[SerializeField]
	[Tooltip("The list of transforms whose positions will drive the vertex positions of the sibling LineRenderer component.")]
	[FormerlySerializedAs("transformNodes")]
	private Transform[] _transformNodes = Array.Empty<Transform>();

	// Token: 0x0400015E RID: 350
	private LineRenderer lineRenderer;

	// Token: 0x0400015F RID: 351
	private Vector3[] vertexList = Array.Empty<Vector3>();
}
