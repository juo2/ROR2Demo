using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000849 RID: 2121
	[RequireComponent(typeof(QuaternionPID))]
	[RequireComponent(typeof(VectorPID))]
	public class RigidbodyDirection : MonoBehaviour
	{
		// Token: 0x06002E35 RID: 11829 RVA: 0x000C4F00 File Offset: 0x000C3100
		private void Start()
		{
			this.inputBank = base.GetComponent<InputBankTest>();
			this.modelLocator = base.GetComponent<ModelLocator>();
			if (this.modelLocator)
			{
				Transform modelTransform = this.modelLocator.modelTransform;
				if (modelTransform)
				{
					this.animator = modelTransform.GetComponent<Animator>();
				}
			}
			this.aimDirection = base.transform.forward;
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000C4F64 File Offset: 0x000C3164
		private void Update()
		{
			if (this.animator)
			{
				if (this.animatorXCycle.Length > 0)
				{
					this.animator.SetFloat(this.animatorXCycle, Mathf.Clamp(0.5f + this.targetTorque.x * 0.5f * this.animatorTorqueScale, -1f, 1f), 0.1f, Time.deltaTime);
				}
				if (this.animatorYCycle.Length > 0)
				{
					this.animator.SetFloat(this.animatorYCycle, Mathf.Clamp(0.5f + this.targetTorque.y * 0.5f * this.animatorTorqueScale, -1f, 1f), 0.1f, Time.deltaTime);
				}
				if (this.animatorZCycle.Length > 0)
				{
					this.animator.SetFloat(this.animatorZCycle, Mathf.Clamp(0.5f + this.targetTorque.z * 0.5f * this.animatorTorqueScale, -1f, 1f), 0.1f, Time.deltaTime);
				}
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000C5084 File Offset: 0x000C3284
		private void FixedUpdate()
		{
			if (this.inputBank && this.rigid && this.angularVelocityPID && this.torquePID)
			{
				this.angularVelocityPID.inputQuat = this.rigid.rotation;
				Quaternion targetQuat = Util.QuaternionSafeLookRotation(this.aimDirection);
				if (this.freezeXRotation)
				{
					targetQuat.x = 0f;
				}
				if (this.freezeYRotation)
				{
					targetQuat.y = 0f;
				}
				if (this.freezeZRotation)
				{
					targetQuat.z = 0f;
				}
				this.angularVelocityPID.targetQuat = targetQuat;
				Vector3 targetVector = this.angularVelocityPID.UpdatePID();
				this.torquePID.inputVector = this.rigid.angularVelocity;
				this.torquePID.targetVector = targetVector;
				Vector3 torque = this.torquePID.UpdatePID();
				this.rigid.AddTorque(torque, ForceMode.Acceleration);
			}
		}

		// Token: 0x04003038 RID: 12344
		public Vector3 aimDirection = Vector3.one;

		// Token: 0x04003039 RID: 12345
		public Rigidbody rigid;

		// Token: 0x0400303A RID: 12346
		public QuaternionPID angularVelocityPID;

		// Token: 0x0400303B RID: 12347
		public VectorPID torquePID;

		// Token: 0x0400303C RID: 12348
		public bool freezeXRotation;

		// Token: 0x0400303D RID: 12349
		public bool freezeYRotation;

		// Token: 0x0400303E RID: 12350
		public bool freezeZRotation;

		// Token: 0x0400303F RID: 12351
		private ModelLocator modelLocator;

		// Token: 0x04003040 RID: 12352
		private Animator animator;

		// Token: 0x04003041 RID: 12353
		public string animatorXCycle;

		// Token: 0x04003042 RID: 12354
		public string animatorYCycle;

		// Token: 0x04003043 RID: 12355
		public string animatorZCycle;

		// Token: 0x04003044 RID: 12356
		public float animatorTorqueScale;

		// Token: 0x04003045 RID: 12357
		private InputBankTest inputBank;

		// Token: 0x04003046 RID: 12358
		private Vector3 targetTorque;
	}
}
