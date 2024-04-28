using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000902 RID: 2306
	public class WheelVehicleMotor : MonoBehaviour
	{
		// Token: 0x0600341B RID: 13339 RVA: 0x000DB684 File Offset: 0x000D9884
		private void Start()
		{
			this.inputBank = base.GetComponent<InputBankTest>();
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000DB694 File Offset: 0x000D9894
		private void UpdateWheelParameter(WheelCollider wheel)
		{
			wheel.mass = this.wheelMass;
			wheel.radius = this.wheelRadius;
			wheel.suspensionDistance = this.wheelSuspensionDistance;
			wheel.forceAppPointDistance = this.wheelForceAppPointDistance;
			wheel.transform.localPosition = new Vector3(wheel.transform.localPosition.x, -this.wheelWellDistance, wheel.transform.localPosition.z);
			wheel.suspensionSpring = new JointSpring
			{
				spring = this.wheelSuspensionSpringSpring,
				damper = this.wheelSuspensionSpringDamper,
				targetPosition = this.wheelSuspensionSpringTargetPosition
			};
			wheel.forwardFriction = new WheelFrictionCurve
			{
				extremumSlip = this.forwardFrictionExtremumSlip,
				extremumValue = this.forwardFrictionValue,
				asymptoteSlip = this.forwardFrictionAsymptoticSlip,
				asymptoteValue = this.forwardFrictionAsymptoticValue,
				stiffness = this.forwardFrictionStiffness
			};
			wheel.sidewaysFriction = new WheelFrictionCurve
			{
				extremumSlip = this.sidewaysFrictionExtremumSlip,
				extremumValue = this.sidewaysFrictionValue,
				asymptoteSlip = this.sidewaysFrictionAsymptoticSlip,
				asymptoteValue = this.sidewaysFrictionAsymptoticValue,
				stiffness = this.sidewaysFrictionStiffness
			};
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000DB7E0 File Offset: 0x000D99E0
		private void UpdateAllWheelParameters()
		{
			foreach (WheelCollider wheel in this.driveWheels)
			{
				this.UpdateWheelParameter(wheel);
			}
			foreach (WheelCollider wheel2 in this.steerWheels)
			{
				this.UpdateWheelParameter(wheel2);
			}
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000DB830 File Offset: 0x000D9A30
		private void FixedUpdate()
		{
			this.UpdateAllWheelParameters();
			if (this.inputBank)
			{
				this.moveVector = this.inputBank.moveVector;
				float f = 0f;
				if (this.moveVector.sqrMagnitude > 0f)
				{
					f = Util.AngleSigned(base.transform.forward, this.moveVector, Vector3.up);
				}
				WheelCollider[] array = this.steerWheels;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].steerAngle = Mathf.Min(this.maxSteerAngle, Mathf.Abs(f)) * Mathf.Sign(f);
				}
				array = this.driveWheels;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].motorTorque = this.moveVector.magnitude * this.motorTorque;
				}
			}
		}

		// Token: 0x040034FF RID: 13567
		[HideInInspector]
		public Vector3 moveVector;

		// Token: 0x04003500 RID: 13568
		public WheelCollider[] driveWheels;

		// Token: 0x04003501 RID: 13569
		public WheelCollider[] steerWheels;

		// Token: 0x04003502 RID: 13570
		public float motorTorque;

		// Token: 0x04003503 RID: 13571
		public float maxSteerAngle;

		// Token: 0x04003504 RID: 13572
		public float wheelMass = 20f;

		// Token: 0x04003505 RID: 13573
		public float wheelRadius = 0.5f;

		// Token: 0x04003506 RID: 13574
		public float wheelWellDistance = 2.7f;

		// Token: 0x04003507 RID: 13575
		public float wheelSuspensionDistance = 0.3f;

		// Token: 0x04003508 RID: 13576
		public float wheelForceAppPointDistance;

		// Token: 0x04003509 RID: 13577
		public float wheelSuspensionSpringSpring = 35000f;

		// Token: 0x0400350A RID: 13578
		public float wheelSuspensionSpringDamper = 4500f;

		// Token: 0x0400350B RID: 13579
		public float wheelSuspensionSpringTargetPosition = 0.5f;

		// Token: 0x0400350C RID: 13580
		public float forwardFrictionExtremumSlip = 0.4f;

		// Token: 0x0400350D RID: 13581
		public float forwardFrictionValue = 1f;

		// Token: 0x0400350E RID: 13582
		public float forwardFrictionAsymptoticSlip = 0.8f;

		// Token: 0x0400350F RID: 13583
		public float forwardFrictionAsymptoticValue = 0.5f;

		// Token: 0x04003510 RID: 13584
		public float forwardFrictionStiffness = 1f;

		// Token: 0x04003511 RID: 13585
		public float sidewaysFrictionExtremumSlip = 0.2f;

		// Token: 0x04003512 RID: 13586
		public float sidewaysFrictionValue = 1f;

		// Token: 0x04003513 RID: 13587
		public float sidewaysFrictionAsymptoticSlip = 0.5f;

		// Token: 0x04003514 RID: 13588
		public float sidewaysFrictionAsymptoticValue = 0.75f;

		// Token: 0x04003515 RID: 13589
		public float sidewaysFrictionStiffness = 1f;

		// Token: 0x04003516 RID: 13590
		private InputBankTest inputBank;
	}
}
