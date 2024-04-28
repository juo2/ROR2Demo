using System;
using RoR2;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000046 RID: 70
[RequireComponent(typeof(InterpolatedTransformUpdater))]
public class InterpolatedTransform : MonoBehaviour, ITeleportHandler, IEventSystemHandler
{
	// Token: 0x06000136 RID: 310 RVA: 0x00007141 File Offset: 0x00005341
	private void OnEnable()
	{
		this.ForgetPreviousTransforms();
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000714C File Offset: 0x0000534C
	public void ForgetPreviousTransforms()
	{
		this.m_lastTransforms = new InterpolatedTransform.TransformData[2];
		InterpolatedTransform.TransformData transformData = new InterpolatedTransform.TransformData(base.transform.localPosition, base.transform.localRotation, base.transform.localScale);
		this.m_lastTransforms[0] = transformData;
		this.m_lastTransforms[1] = transformData;
		this.m_newTransformIndex = 0;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x000071B0 File Offset: 0x000053B0
	private void FixedUpdate()
	{
		InterpolatedTransform.TransformData transformData = this.m_lastTransforms[this.m_newTransformIndex];
		base.transform.localPosition = transformData.position;
		base.transform.localRotation = transformData.rotation;
		base.transform.localScale = transformData.scale;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00007204 File Offset: 0x00005404
	public void LateFixedUpdate()
	{
		this.m_newTransformIndex = this.OldTransformIndex();
		this.m_lastTransforms[this.m_newTransformIndex] = new InterpolatedTransform.TransformData(base.transform.localPosition, base.transform.localRotation, base.transform.localScale);
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00007254 File Offset: 0x00005454
	private void Update()
	{
		InterpolatedTransform.TransformData transformData = this.m_lastTransforms[this.m_newTransformIndex];
		InterpolatedTransform.TransformData transformData2 = this.m_lastTransforms[this.OldTransformIndex()];
		base.transform.localPosition = Vector3.Lerp(transformData2.position, transformData.position, InterpolationController.InterpolationFactor);
		base.transform.localRotation = Quaternion.Slerp(transformData2.rotation, transformData.rotation, InterpolationController.InterpolationFactor);
		base.transform.localScale = Vector3.Lerp(transformData2.scale, transformData.scale, InterpolationController.InterpolationFactor);
	}

	// Token: 0x0600013B RID: 315 RVA: 0x000072E8 File Offset: 0x000054E8
	private int OldTransformIndex()
	{
		if (this.m_newTransformIndex != 0)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00007141 File Offset: 0x00005341
	public void OnTeleport(Vector3 oldPosition, Vector3 newPosition)
	{
		this.ForgetPreviousTransforms();
	}

	// Token: 0x0400014C RID: 332
	private InterpolatedTransform.TransformData[] m_lastTransforms;

	// Token: 0x0400014D RID: 333
	private int m_newTransformIndex;

	// Token: 0x02000047 RID: 71
	private struct TransformData
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000072F5 File Offset: 0x000054F5
		public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
		{
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
		}

		// Token: 0x0400014E RID: 334
		public Vector3 position;

		// Token: 0x0400014F RID: 335
		public Quaternion rotation;

		// Token: 0x04000150 RID: 336
		public Vector3 scale;
	}
}
