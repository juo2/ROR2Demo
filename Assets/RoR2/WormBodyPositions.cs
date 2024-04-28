using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000907 RID: 2311
	public class WormBodyPositions : MonoBehaviour
	{
		// Token: 0x0600342C RID: 13356 RVA: 0x000DBD44 File Offset: 0x000D9F44
		private void Start()
		{
			this.positionHistory.Add(new WormBodyPositions.Keyframe
			{
				rotation = this.segments[0].rotation,
				position = this.segments[0].position,
				fromPreviousNormal = Vector3.zero,
				fromPreviousLength = 0f
			});
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000DBDA8 File Offset: 0x000D9FA8
		private void FixedUpdate()
		{
			Vector3 position = this.segments[0].position;
			Vector3 a = position - this.positionHistory[this.positionHistory.Count - 1].position;
			float magnitude = a.magnitude;
			if (magnitude != 0f)
			{
				Quaternion rotation = this.segments[0].rotation;
				this.segments[0].up = -a;
				Quaternion rotation2 = this.segments[0].rotation;
				this.segments[0].rotation = Quaternion.RotateTowards(rotation, rotation2, 360f * Time.fixedDeltaTime);
				this.positionHistory.Add(new WormBodyPositions.Keyframe
				{
					rotation = this.segments[0].rotation,
					position = position,
					fromPreviousNormal = a * (1f / magnitude),
					fromPreviousLength = magnitude
				});
			}
			float num = this.segmentRadius * 2f;
			float num2 = num;
			Vector3 a2 = position;
			int num3 = 1;
			for (int i = this.positionHistory.Count - 1; i >= 1; i--)
			{
				Vector3 position2 = this.positionHistory[i - 1].position;
				float fromPreviousLength = this.positionHistory[i].fromPreviousLength;
				if (num2 < fromPreviousLength)
				{
					float t = num2 / fromPreviousLength;
					this.segments[num3].position = Vector3.Lerp(a2, position2, t);
					num3++;
					if (num3 >= this.segments.Length)
					{
						this.positionHistory.RemoveRange(0, i - 1);
						break;
					}
					num2 += num;
				}
				num2 -= fromPreviousLength;
				a2 = position2;
			}
			if (this.segments.Length > 1)
			{
				Quaternion rotation3 = this.segments[0].rotation;
				Vector3 b = this.segments[0].position;
				Vector3 vector = this.segments[1].position;
				for (int j = 1; j < this.segments.Length - 1; j++)
				{
					Vector3 position3 = this.segments[j + 1].position;
					Vector3 vector2 = position3 - b;
					if (vector2 != Vector3.zero)
					{
						this.segments[j].rotation = rotation3;
						this.segments[j].up = vector2;
					}
					b = vector;
					vector = position3;
				}
			}
		}

		// Token: 0x04003527 RID: 13607
		public Vector3 headVelocity = Vector3.zero;

		// Token: 0x04003528 RID: 13608
		public Transform[] segments;

		// Token: 0x04003529 RID: 13609
		public float segmentRadius = 1f;

		// Token: 0x0400352A RID: 13610
		private List<WormBodyPositions.Keyframe> positionHistory = new List<WormBodyPositions.Keyframe>();

		// Token: 0x02000908 RID: 2312
		private struct Keyframe
		{
			// Token: 0x0400352B RID: 13611
			public Vector3 position;

			// Token: 0x0400352C RID: 13612
			public Quaternion rotation;

			// Token: 0x0400352D RID: 13613
			public Vector3 fromPreviousNormal;

			// Token: 0x0400352E RID: 13614
			public float fromPreviousLength;
		}
	}
}
