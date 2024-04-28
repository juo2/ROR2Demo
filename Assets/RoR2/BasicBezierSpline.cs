using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005ED RID: 1517
	public class BasicBezierSpline : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x06001B98 RID: 7064 RVA: 0x00075994 File Offset: 0x00073B94
		private void BuildKeyFrames()
		{
			this.totalLength = 0f;
			Array.Resize<BasicBezierSpline.KeyFrame>(ref this.keyFrames, this.controlPoints.Length);
			Array.Resize<Vector3>(ref this.samplesBuffer, this.samplesPerSegment);
			int i = 0;
			int num = this.controlPoints.Length - 1;
			while (i < num)
			{
				BasicBezierSplineControlPoint basicBezierSplineControlPoint = this.controlPoints[i];
				BasicBezierSplineControlPoint basicBezierSplineControlPoint2 = this.controlPoints[i + 1];
				if (!basicBezierSplineControlPoint || !basicBezierSplineControlPoint2)
				{
					return;
				}
				CubicBezier3 curveSegment = BasicBezierSpline.GetCurveSegment(basicBezierSplineControlPoint, basicBezierSplineControlPoint2);
				float num2 = curveSegment.ApproximateLength(this.samplesPerSegment);
				this.keyFrames[i] = new BasicBezierSpline.KeyFrame
				{
					curve = curveSegment,
					approximateLength = num2
				};
				this.totalLength += num2;
				i++;
			}
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00075A5C File Offset: 0x00073C5C
		public Vector3 Evaluate(float normalizedPosition)
		{
			if (this.keyFramesDirty)
			{
				if (Application.isPlaying)
				{
					this.keyFramesDirty = false;
				}
				this.BuildKeyFrames();
			}
			float num = normalizedPosition * this.totalLength;
			float num2 = 0f;
			int i = 0;
			int num3 = this.keyFrames.Length;
			while (i < num3)
			{
				ref BasicBezierSpline.KeyFrame ptr = ref this.keyFrames[i];
				float a = num2;
				num2 += ptr.approximateLength;
				if (num2 >= num)
				{
					float t = Mathf.InverseLerp(a, num2, num);
					return this.EvaluateKeyFrame(t, ptr);
				}
				i++;
			}
			if (this.keyFrames.Length != 0)
			{
				return this.keyFrames[this.keyFrames.Length - 1].curve.p1;
			}
			return Vector3.zero;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00075B0C File Offset: 0x00073D0C
		private Vector3 EvaluateKeyFrame(float t, in BasicBezierSpline.KeyFrame keyFrame)
		{
			float num = t * keyFrame.approximateLength;
			CubicBezier3 curve = keyFrame.curve;
			curve.ToVertices(this.samplesBuffer);
			float a = 0f;
			float num2 = 0f;
			Vector3 vector = this.samplesBuffer[0];
			for (int i = 1; i < this.samplesBuffer.Length; i++)
			{
				Vector3 vector2 = this.samplesBuffer[i];
				float num3 = Vector3.Distance(vector2, vector);
				num2 += num3;
				if (num2 >= num)
				{
					return Vector3.Lerp(vector, vector2, Mathf.InverseLerp(a, num2, num));
				}
				vector = vector2;
				a = num2;
			}
			curve = keyFrame.curve;
			return curve.p1;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00075BB0 File Offset: 0x00073DB0
		private void DrawKeyFrame(in BasicBezierSpline.KeyFrame keyFrame)
		{
			Gizmos.color = Color.Lerp(Color.green, Color.black, 0.5f);
			CubicBezier3 curve = keyFrame.curve;
			Vector3 p = curve.p0;
			curve = keyFrame.curve;
			Gizmos.DrawRay(p, curve.v0);
			Gizmos.color = Color.Lerp(Color.red, Color.black, 0.5f);
			curve = keyFrame.curve;
			Vector3 p2 = curve.p1;
			curve = keyFrame.curve;
			Gizmos.DrawRay(p2, curve.v1);
			for (int i = 1; i <= 20; i++)
			{
				float num = (float)i * 0.05f;
				Gizmos.color = Color.Lerp(Color.red, Color.green, num);
				curve = keyFrame.curve;
				Vector3 vector = curve.Evaluate(num - 0.05f);
				curve = keyFrame.curve;
				Vector3 a = curve.Evaluate(num);
				Gizmos.DrawRay(vector, a - vector);
			}
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00075C94 File Offset: 0x00073E94
		public void OnDrawGizmos()
		{
			if (this.keyFramesDirty)
			{
				if (Application.isPlaying)
				{
					this.keyFramesDirty = false;
				}
				this.BuildKeyFrames();
			}
			for (int i = 0; i < this.keyFrames.Length; i++)
			{
				this.DrawKeyFrame(this.keyFrames[i]);
			}
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00075CE4 File Offset: 0x00073EE4
		public static CubicBezier3 GetCurveSegment(BasicBezierSplineControlPoint startControlPoint, BasicBezierSplineControlPoint endControlPoint)
		{
			Transform transform = startControlPoint.transform;
			Transform transform2 = endControlPoint.transform;
			Vector3 position = transform.position;
			Vector3 v = transform.rotation * startControlPoint.forwardVelocity;
			Vector3 position2 = transform2.position;
			Vector3 v2 = transform2.rotation * endControlPoint.backwardVelocity;
			return CubicBezier3.FromVelocities(position, v, position2, v2);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00075D3A File Offset: 0x00073F3A
		public void OnAfterDeserialize()
		{
			this.keyFramesDirty = true;
		}

		// Token: 0x04002175 RID: 8565
		public BasicBezierSplineControlPoint[] controlPoints = Array.Empty<BasicBezierSplineControlPoint>();

		// Token: 0x04002176 RID: 8566
		public int samplesPerSegment = 20;

		// Token: 0x04002177 RID: 8567
		private BasicBezierSpline.KeyFrame[] keyFrames = Array.Empty<BasicBezierSpline.KeyFrame>();

		// Token: 0x04002178 RID: 8568
		private Vector3[] samplesBuffer = Array.Empty<Vector3>();

		// Token: 0x04002179 RID: 8569
		private float totalLength;

		// Token: 0x0400217A RID: 8570
		private bool keyFramesDirty = true;

		// Token: 0x020005EE RID: 1518
		private struct KeyFrame
		{
			// Token: 0x0400217B RID: 8571
			public CubicBezier3 curve;

			// Token: 0x0400217C RID: 8572
			public float approximateLength;
		}
	}
}
