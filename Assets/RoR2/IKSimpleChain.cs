using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200075B RID: 1883
	public class IKSimpleChain : MonoBehaviour
	{
		// Token: 0x060026F6 RID: 9974 RVA: 0x000A8FBE File Offset: 0x000A71BE
		private void Start()
		{
			this.ikTarget = base.GetComponent<IIKTargetBehavior>();
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000A8FCC File Offset: 0x000A71CC
		private void LateUpdate()
		{
			if (this.firstRun)
			{
				this.tmpBone = this.boneList[this.startBone];
			}
			if (this.ikTarget != null)
			{
				this.ikTarget.UpdateIKTargetPosition();
			}
			this.targetPosition = base.transform.position;
			this.legLength = this.CalculateLegLength(this.boneList);
			this.Solve(this.boneList, this.targetPosition);
			this.firstRun = false;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000A9044 File Offset: 0x000A7244
		public bool LegTooShort(float legScale = 1f)
		{
			bool result = false;
			if ((this.targetPosition - this.boneList[0].transform.position).sqrMagnitude >= this.legLength * this.legLength * legScale * legScale)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000A9090 File Offset: 0x000A7290
		private float CalculateLegLength(Transform[] bones)
		{
			float[] array = new float[bones.Length - 1];
			float num = 0f;
			for (int i = this.startBone; i < bones.Length - 1; i++)
			{
				array[i] = (bones[i + 1].position - bones[i].position).magnitude;
				num += array[i];
			}
			return num;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000A90EC File Offset: 0x000A72EC
		public void Solve(Transform[] bones, Vector3 target)
		{
			Transform transform = bones[bones.Length - 1];
			Vector3[] array = new Vector3[bones.Length - 2];
			float[] array2 = new float[bones.Length - 2];
			Quaternion[] array3 = new Quaternion[bones.Length - 2];
			for (int i = this.startBone; i < bones.Length - 2; i++)
			{
				array[i] = Vector3.Cross(bones[i + 1].position - bones[i].position, bones[i + 2].position - bones[i + 1].position);
				array[i] = Quaternion.Inverse(bones[i].rotation) * array[i];
				array[i] = array[i].normalized;
				array2[i] = Vector3.Angle(bones[i + 1].position - bones[i].position, bones[i + 1].position - bones[i + 2].position);
				array3[i] = bones[i + 1].localRotation;
			}
			this.positionAccuracy = this.legLength * this.posAccuracy;
			float magnitude = (transform.position - bones[this.startBone].position).magnitude;
			float magnitude2 = (target - bones[this.startBone].position).magnitude;
			this.minIsFound = false;
			this.bendMore = false;
			if (magnitude2 >= magnitude)
			{
				this.minIsFound = true;
				this.bendingHigh = 1f;
				this.bendingLow = 0f;
			}
			else
			{
				this.bendMore = true;
				this.bendingHigh = 1f;
				this.bendingLow = 0f;
			}
			int num = array3.Length;
			int num2 = 0;
			while (Mathf.Abs(magnitude - magnitude2) > this.positionAccuracy && num2 < this.maxIterations)
			{
				num2++;
				float num3;
				if (!this.minIsFound)
				{
					num3 = this.bendingHigh;
				}
				else
				{
					num3 = (this.bendingLow + this.bendingHigh) / 2f;
				}
				for (int j = this.startBone; j < bones.Length - 2; j++)
				{
					float num4;
					if (!this.bendMore)
					{
						num4 = Mathf.Lerp(180f, array2[j], num3);
					}
					else
					{
						num4 = array2[j] * (1f - num3) + (array2[j] - 30f) * num3;
					}
					Quaternion localRotation = Quaternion.AngleAxis(array2[j] - num4, array[j]) * array3[j];
					bones[j + 1].localRotation = localRotation;
				}
				magnitude = (transform.position - bones[this.startBone].position).magnitude;
				if (magnitude2 > magnitude)
				{
					this.minIsFound = true;
				}
				if (this.minIsFound)
				{
					if (magnitude2 > magnitude)
					{
						this.bendingHigh = num3;
					}
					else
					{
						this.bendingLow = num3;
					}
					if (this.bendingHigh < 0.01f)
					{
						break;
					}
				}
				else
				{
					this.bendingLow = this.bendingHigh;
					this.bendingHigh += 1f;
				}
			}
			if (this.firstRun)
			{
				this.tmpBone.rotation = bones[this.startBone].rotation;
			}
			bones[this.startBone].rotation = Quaternion.AngleAxis(Vector3.Angle(transform.position - bones[this.startBone].position, target - bones[this.startBone].position), Vector3.Cross(transform.position - bones[this.startBone].position, target - bones[this.startBone].position)) * bones[this.startBone].rotation;
			if (this.ikPole)
			{
				Vector3 position = bones[this.startBone].position;
				Vector3 up = bones[this.startBone].transform.up;
				Vector3 position2 = transform.position;
				Vector3 position3 = this.ikPole.position;
				Vector3 vector = Vector3.Cross(position2 - position, position3 - position);
				Vector3 vector2 = Vector3.Cross(vector, up);
				Vector3 vecU = Vector3.zero;
				switch (this.innerAxis)
				{
				case IKSimpleChain.InnerAxis.Left:
					vecU = -bones[this.startBone].transform.right;
					break;
				case IKSimpleChain.InnerAxis.Right:
					vecU = bones[this.startBone].transform.right;
					break;
				case IKSimpleChain.InnerAxis.Forward:
					vecU = bones[this.startBone].transform.forward;
					break;
				case IKSimpleChain.InnerAxis.Backward:
					vecU = -bones[this.startBone].transform.forward;
					break;
				}
				float num5 = this.SignedAngle(vecU, vector2, up);
				num5 += this.poleAngle;
				bones[this.startBone].rotation = Quaternion.AngleAxis(num5, transform.position - bones[this.startBone].position) * bones[this.startBone].rotation;
				Debug.DrawLine(transform.position, bones[this.startBone].position, Color.red);
				Debug.DrawRay(bones[this.startBone].position, vector, Color.blue);
				Debug.DrawRay(bones[this.startBone].position, vector2, Color.yellow);
			}
			this.tmpBone = bones[this.startBone];
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000A9650 File Offset: 0x000A7850
		private float SignedAngle(Vector3 vecU, Vector3 vecV, Vector3 normal)
		{
			float num = Vector3.Angle(vecU, vecV);
			if (Vector3.Angle(Vector3.Cross(vecU, vecV), normal) < 1f)
			{
				num *= -1f;
			}
			return -num;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000A9684 File Offset: 0x000A7884
		private float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
		{
			float num = Vector3.Dot(Vector3.Cross(fwd, targetDir), up);
			if (num > 0f)
			{
				return 1f;
			}
			if (num < 0f)
			{
				return -1f;
			}
			return 0f;
		}

		// Token: 0x04002ACB RID: 10955
		public float scale = 1f;

		// Token: 0x04002ACC RID: 10956
		public int maxIterations = 100;

		// Token: 0x04002ACD RID: 10957
		public float positionAccuracy = 0.001f;

		// Token: 0x04002ACE RID: 10958
		private float posAccuracy = 0.001f;

		// Token: 0x04002ACF RID: 10959
		public float bendingLow;

		// Token: 0x04002AD0 RID: 10960
		public float bendingHigh;

		// Token: 0x04002AD1 RID: 10961
		public int chainResolution;

		// Token: 0x04002AD2 RID: 10962
		private int startBone;

		// Token: 0x04002AD3 RID: 10963
		private bool minIsFound;

		// Token: 0x04002AD4 RID: 10964
		private bool bendMore;

		// Token: 0x04002AD5 RID: 10965
		private Vector3 targetPosition;

		// Token: 0x04002AD6 RID: 10966
		public float legLength;

		// Token: 0x04002AD7 RID: 10967
		public float poleAngle;

		// Token: 0x04002AD8 RID: 10968
		public IKSimpleChain.InnerAxis innerAxis = IKSimpleChain.InnerAxis.Right;

		// Token: 0x04002AD9 RID: 10969
		private Transform tmpBone;

		// Token: 0x04002ADA RID: 10970
		public Transform ikPole;

		// Token: 0x04002ADB RID: 10971
		public Transform[] boneList;

		// Token: 0x04002ADC RID: 10972
		private bool firstRun = true;

		// Token: 0x04002ADD RID: 10973
		private IIKTargetBehavior ikTarget;

		// Token: 0x0200075C RID: 1884
		public enum InnerAxis
		{
			// Token: 0x04002ADF RID: 10975
			Left,
			// Token: 0x04002AE0 RID: 10976
			Right,
			// Token: 0x04002AE1 RID: 10977
			Forward,
			// Token: 0x04002AE2 RID: 10978
			Backward
		}
	}
}
