using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class BezierCurveLine : MonoBehaviour
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060000E9 RID: 233 RVA: 0x000051BB File Offset: 0x000033BB
	// (set) Token: 0x060000EA RID: 234 RVA: 0x000051C3 File Offset: 0x000033C3
	public LineRenderer lineRenderer { get; private set; }

	// Token: 0x060000EB RID: 235 RVA: 0x000051CC File Offset: 0x000033CC
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.windPhaseShift = UnityEngine.Random.insideUnitSphere * 360f;
		Array.Resize<Vector3>(ref this.vertexList, this.lineRenderer.positionCount + 1);
		this.UpdateBezier(0f);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x0000521D File Offset: 0x0000341D
	public void OnEnable()
	{
		Array.Resize<Vector3>(ref this.vertexList, this.lineRenderer.positionCount + 1);
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00005237 File Offset: 0x00003437
	private void LateUpdate()
	{
		this.UpdateBezier(Time.deltaTime);
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00005244 File Offset: 0x00003444
	public void UpdateBezier(float deltaTime)
	{
		this.windTime += deltaTime;
		this.p0 = base.transform.position;
		if (this.endTransform)
		{
			this.p1 = this.endTransform.position;
		}
		if (this.animateBezierWind)
		{
			this.finalv0 = this.v0 + new Vector3(Mathf.Sin(0.017453292f * (this.windTime * 360f + this.windPhaseShift.x) * this.windFrequency.x) * this.windMagnitude.x, Mathf.Sin(0.017453292f * (this.windTime * 360f + this.windPhaseShift.y) * this.windFrequency.y) * this.windMagnitude.y, Mathf.Sin(0.017453292f * (this.windTime * 360f + this.windPhaseShift.z) * this.windFrequency.z) * this.windMagnitude.z);
			this.finalv1 = this.v1 + new Vector3(Mathf.Sin(0.017453292f * (this.windTime * 360f + this.windPhaseShift.x + this.p1.x) * this.windFrequency.x) * this.windMagnitude.x, Mathf.Sin(0.017453292f * (this.windTime * 360f + this.windPhaseShift.y + this.p1.z) * this.windFrequency.y) * this.windMagnitude.y, Mathf.Sin(0.017453292f * (this.windTime * 360f + this.windPhaseShift.z + this.p1.y) * this.windFrequency.z) * this.windMagnitude.z);
		}
		else
		{
			this.finalv0 = this.v0;
			this.finalv1 = this.v1;
		}
		for (int i = 0; i < this.vertexList.Length; i++)
		{
			float t = (float)i / (float)(this.vertexList.Length - 2);
			this.vertexList[i] = this.EvaluateBezier(t);
		}
		this.lineRenderer.SetPositions(this.vertexList);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x000054AC File Offset: 0x000036AC
	private Vector3 EvaluateBezier(float t)
	{
		Vector3 a = Vector3.Lerp(this.p0, this.p0 + this.finalv0, t);
		Vector3 b = Vector3.Lerp(this.p1, this.p1 + this.finalv1, 1f - t);
		return Vector3.Lerp(a, b, t);
	}

	// Token: 0x040000DB RID: 219
	private Vector3[] vertexList = Array.Empty<Vector3>();

	// Token: 0x040000DC RID: 220
	private Vector3 p0 = Vector3.zero;

	// Token: 0x040000DD RID: 221
	public Vector3 v0 = Vector3.zero;

	// Token: 0x040000DE RID: 222
	public Vector3 p1 = Vector3.zero;

	// Token: 0x040000DF RID: 223
	public Vector3 v1 = Vector3.zero;

	// Token: 0x040000E0 RID: 224
	public Transform endTransform;

	// Token: 0x040000E1 RID: 225
	public bool animateBezierWind;

	// Token: 0x040000E2 RID: 226
	public Vector3 windMagnitude;

	// Token: 0x040000E3 RID: 227
	public Vector3 windFrequency;

	// Token: 0x040000E4 RID: 228
	private Vector3 windPhaseShift;

	// Token: 0x040000E5 RID: 229
	private Vector3 lastWind;

	// Token: 0x040000E6 RID: 230
	private Vector3 finalv0;

	// Token: 0x040000E7 RID: 231
	private Vector3 finalv1;

	// Token: 0x040000E8 RID: 232
	private float windTime;
}
