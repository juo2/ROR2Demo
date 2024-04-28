using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000739 RID: 1849
	[RequireComponent(typeof(Rigidbody))]
	public class HoverVehicleMotor : MonoBehaviour
	{
		// Token: 0x06002677 RID: 9847 RVA: 0x000A78AE File Offset: 0x000A5AAE
		private void Start()
		{
			this.inputBank = base.GetComponent<InputBankTest>();
			this.rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000A78C8 File Offset: 0x000A5AC8
		private void ApplyWheelForces(HoverEngine wheel, float gas, bool driveWheel, AnimationCurve slidingWheelTractionCurve)
		{
			if (wheel.isGrounded)
			{
				float d = 0.005f;
				Transform transform = wheel.transform;
				float d2 = 1f;
				Vector3 position = transform.position;
				Vector3 pointVelocity = this.rigidbody.GetPointVelocity(position);
				Vector3 a = Vector3.Project(pointVelocity, transform.right);
				Vector3 a2 = Vector3.Project(pointVelocity, transform.forward);
				Vector3 up = Vector3.up;
				Debug.DrawRay(position, pointVelocity, Color.blue);
				Vector3 a3 = Vector3.zero;
				if (driveWheel)
				{
					a3 = transform.forward * gas * this.motorForce;
					this.rigidbody.AddForceAtPosition(transform.forward * gas * this.motorForce * d2, position);
					Debug.DrawRay(position, a3 * d, Color.yellow);
				}
				Vector3 vector = Vector3.ProjectOnPlane(-a2 * this.rollingFrictionCoefficient * d2, up);
				this.rigidbody.AddForceAtPosition(vector, position);
				Debug.DrawRay(position, vector * d, Color.red);
				Vector3 vector2 = Vector3.ProjectOnPlane(-a * slidingWheelTractionCurve.Evaluate(pointVelocity.magnitude) * this.slidingTractionCoefficient * d2, up);
				this.rigidbody.AddForceAtPosition(vector2, position);
				Debug.DrawRay(position, vector2 * d, Color.red);
				Debug.DrawRay(position, (a3 + vector + vector2) * d, Color.green);
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000A7A49 File Offset: 0x000A5C49
		private void UpdateCenterOfMass()
		{
			this.rigidbody.ResetCenterOfMass();
			this.rigidbody.centerOfMass = this.rigidbody.centerOfMass + this.centerOfMassOffset;
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000A7A78 File Offset: 0x000A5C78
		private void UpdateWheelParameter(HoverEngine wheel, HoverVehicleMotor.WheelLateralAxis wheelLateralAxis, HoverVehicleMotor.WheelLongitudinalAxis wheelLongitudinalAxis)
		{
			wheel.hoverForce = this.hoverForce;
			wheel.hoverDamping = this.hoverDamping;
			wheel.hoverHeight = this.hoverHeight;
			wheel.offsetVector = this.hoverOffsetVector;
			wheel.hoverRadius = this.hoverRadius;
			Vector3 zero = Vector3.zero;
			zero.y = -this.wheelWellDepth;
			if (wheelLateralAxis != HoverVehicleMotor.WheelLateralAxis.Left)
			{
				if (wheelLateralAxis == HoverVehicleMotor.WheelLateralAxis.Right)
				{
					zero.x = this.trackWidth / 2f;
				}
			}
			else
			{
				zero.x = -this.trackWidth / 2f;
			}
			if (wheelLongitudinalAxis != HoverVehicleMotor.WheelLongitudinalAxis.Front)
			{
				if (wheelLongitudinalAxis == HoverVehicleMotor.WheelLongitudinalAxis.Back)
				{
					zero.z = -this.wheelBase / 2f;
				}
			}
			else
			{
				zero.z = this.wheelBase / 2f;
			}
			wheel.transform.localPosition = zero;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000A7B48 File Offset: 0x000A5D48
		private void UpdateAllWheelParameters()
		{
			foreach (HoverVehicleMotor.AxleGroup axleGroup in this.staticAxles)
			{
				HoverEngine leftWheel = axleGroup.leftWheel;
				HoverEngine rightWheel = axleGroup.rightWheel;
				this.UpdateWheelParameter(leftWheel, HoverVehicleMotor.WheelLateralAxis.Left, axleGroup.wheelLongitudinalAxis);
				this.UpdateWheelParameter(rightWheel, HoverVehicleMotor.WheelLateralAxis.Right, axleGroup.wheelLongitudinalAxis);
			}
			foreach (HoverVehicleMotor.AxleGroup axleGroup2 in this.steerAxles)
			{
				HoverEngine leftWheel2 = axleGroup2.leftWheel;
				HoverEngine rightWheel2 = axleGroup2.rightWheel;
				this.UpdateWheelParameter(leftWheel2, HoverVehicleMotor.WheelLateralAxis.Left, axleGroup2.wheelLongitudinalAxis);
				this.UpdateWheelParameter(rightWheel2, HoverVehicleMotor.WheelLateralAxis.Right, axleGroup2.wheelLongitudinalAxis);
			}
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000A7BF0 File Offset: 0x000A5DF0
		private void FixedUpdate()
		{
			this.UpdateCenterOfMass();
			this.UpdateAllWheelParameters();
			if (this.inputBank)
			{
				Vector3 moveVector = this.inputBank.moveVector;
				Vector3 normalized = Vector3.ProjectOnPlane(this.inputBank.aimDirection, base.transform.up).normalized;
				float num = Mathf.Clamp(Util.AngleSigned(base.transform.forward, normalized, base.transform.up), -this.maxSteerAngle, this.maxSteerAngle);
				float magnitude = moveVector.magnitude;
				foreach (HoverVehicleMotor.AxleGroup axleGroup in this.staticAxles)
				{
					HoverEngine leftWheel = axleGroup.leftWheel;
					HoverEngine rightWheel = axleGroup.rightWheel;
					this.ApplyWheelForces(leftWheel, magnitude, axleGroup.isDriven, axleGroup.slidingTractionCurve);
					this.ApplyWheelForces(rightWheel, magnitude, axleGroup.isDriven, axleGroup.slidingTractionCurve);
				}
				foreach (HoverVehicleMotor.AxleGroup axleGroup2 in this.steerAxles)
				{
					HoverEngine leftWheel2 = axleGroup2.leftWheel;
					HoverEngine rightWheel2 = axleGroup2.rightWheel;
					float num2 = this.maxTurningRadius / Mathf.Abs(num / this.maxSteerAngle);
					float num3 = Mathf.Atan(this.wheelBase / (num2 - this.trackWidth / 2f)) * 57.29578f;
					float num4 = Mathf.Atan(this.wheelBase / (num2 + this.trackWidth / 2f)) * 57.29578f;
					Quaternion localRotation = Quaternion.Euler(0f, num3 * Mathf.Sign(num), 0f);
					Quaternion localRotation2 = Quaternion.Euler(0f, num4 * Mathf.Sign(num), 0f);
					if (num <= 0f)
					{
						leftWheel2.transform.localRotation = localRotation;
						rightWheel2.transform.localRotation = localRotation2;
					}
					else
					{
						leftWheel2.transform.localRotation = localRotation2;
						rightWheel2.transform.localRotation = localRotation;
					}
					this.ApplyWheelForces(leftWheel2, magnitude, axleGroup2.isDriven, axleGroup2.slidingTractionCurve);
					this.ApplyWheelForces(rightWheel2, magnitude, axleGroup2.isDriven, axleGroup2.slidingTractionCurve);
				}
				Debug.DrawRay(base.transform.position, normalized * 5f, Color.blue);
			}
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000A7E45 File Offset: 0x000A6045
		private void OnDrawGizmos()
		{
			if (this.rigidbody)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(base.transform.TransformPoint(this.rigidbody.centerOfMass), 0.3f);
			}
		}

		// Token: 0x04002A51 RID: 10833
		[HideInInspector]
		public Vector3 targetSteerVector;

		// Token: 0x04002A52 RID: 10834
		public Vector3 centerOfMassOffset;

		// Token: 0x04002A53 RID: 10835
		public HoverVehicleMotor.AxleGroup[] staticAxles;

		// Token: 0x04002A54 RID: 10836
		public HoverVehicleMotor.AxleGroup[] steerAxles;

		// Token: 0x04002A55 RID: 10837
		public float wheelWellDepth;

		// Token: 0x04002A56 RID: 10838
		public float wheelBase;

		// Token: 0x04002A57 RID: 10839
		public float trackWidth;

		// Token: 0x04002A58 RID: 10840
		public float rollingFrictionCoefficient;

		// Token: 0x04002A59 RID: 10841
		public float slidingTractionCoefficient;

		// Token: 0x04002A5A RID: 10842
		public float motorForce;

		// Token: 0x04002A5B RID: 10843
		public float maxSteerAngle;

		// Token: 0x04002A5C RID: 10844
		public float maxTurningRadius;

		// Token: 0x04002A5D RID: 10845
		public float hoverForce = 33f;

		// Token: 0x04002A5E RID: 10846
		public float hoverHeight = 2f;

		// Token: 0x04002A5F RID: 10847
		public float hoverDamping = 0.5f;

		// Token: 0x04002A60 RID: 10848
		public float hoverRadius = 0.5f;

		// Token: 0x04002A61 RID: 10849
		public Vector3 hoverOffsetVector = Vector3.up;

		// Token: 0x04002A62 RID: 10850
		private InputBankTest inputBank;

		// Token: 0x04002A63 RID: 10851
		private Vector3 steerVector = Vector3.forward;

		// Token: 0x04002A64 RID: 10852
		private Rigidbody rigidbody;

		// Token: 0x0200073A RID: 1850
		private enum WheelLateralAxis
		{
			// Token: 0x04002A66 RID: 10854
			Left,
			// Token: 0x04002A67 RID: 10855
			Right
		}

		// Token: 0x0200073B RID: 1851
		public enum WheelLongitudinalAxis
		{
			// Token: 0x04002A69 RID: 10857
			Front,
			// Token: 0x04002A6A RID: 10858
			Back
		}

		// Token: 0x0200073C RID: 1852
		[Serializable]
		public struct AxleGroup
		{
			// Token: 0x04002A6B RID: 10859
			public HoverEngine leftWheel;

			// Token: 0x04002A6C RID: 10860
			public HoverEngine rightWheel;

			// Token: 0x04002A6D RID: 10861
			public HoverVehicleMotor.WheelLongitudinalAxis wheelLongitudinalAxis;

			// Token: 0x04002A6E RID: 10862
			public bool isDriven;

			// Token: 0x04002A6F RID: 10863
			public AnimationCurve slidingTractionCurve;
		}
	}
}
