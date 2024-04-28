using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000BD RID: 189
	public class FlyState : BaseState
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000D554 File Offset: 0x0000B754
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.PlayAnimation("Body", "Idle");
			if (this.modelAnimator)
			{
				this.hasPivotPitchLayer = (this.modelAnimator.GetLayerIndex("PivotPitch") != -1);
				this.hasPivotYawLayer = (this.modelAnimator.GetLayerIndex("PivotYaw") != -1);
				this.hasPivotRollLayer = (this.modelAnimator.GetLayerIndex("PivotRoll") != -1);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.rigidbodyDirection)
			{
				Quaternion rotation = base.transform.rotation;
				Quaternion rhs = Util.QuaternionSafeLookRotation(base.rigidbodyDirection.aimDirection);
				Quaternion quaternion = Quaternion.Inverse(rotation) * rhs;
				if (this.modelAnimator)
				{
					if (this.hasPivotPitchLayer)
					{
						this.modelAnimator.SetFloat(FlyState.pivotPitchCycle, Mathf.Clamp01(Util.Remap(quaternion.x * Mathf.Sign(quaternion.w), -1f, 1f, 0f, 1f)), 1f, Time.fixedDeltaTime);
					}
					if (this.hasPivotYawLayer)
					{
						this.modelAnimator.SetFloat(FlyState.pivotYawCycle, Mathf.Clamp01(Util.Remap(quaternion.y * Mathf.Sign(quaternion.w), -1f, 1f, 0f, 1f)), 1f, Time.fixedDeltaTime);
					}
					if (this.hasPivotRollLayer)
					{
						this.modelAnimator.SetFloat(FlyState.pivotRollCycle, Mathf.Clamp01(Util.Remap(quaternion.z * Mathf.Sign(quaternion.w), -1f, 1f, 0f, 1f)), 1f, Time.fixedDeltaTime);
					}
				}
			}
			this.PerformInputs();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool CanExecuteSkill(GenericSkill skillSlot)
		{
			return true;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000D744 File Offset: 0x0000B944
		protected virtual void PerformInputs()
		{
			if (base.isAuthority)
			{
				if (base.inputBank)
				{
					if (base.rigidbodyMotor)
					{
						base.rigidbodyMotor.moveVector = base.inputBank.moveVector * base.characterBody.moveSpeed;
						if (this.modelAnimator)
						{
							this.modelAnimator.SetFloat(FlyState.flyRate, Vector3.Magnitude(base.rigidbodyMotor.rigid.velocity));
						}
					}
					if (base.rigidbodyDirection)
					{
						base.rigidbodyDirection.aimDirection = base.GetAimRay().direction;
					}
					this.skill1InputReceived = base.inputBank.skill1.down;
					this.skill2InputReceived = base.inputBank.skill2.down;
					this.skill3InputReceived = base.inputBank.skill3.down;
					this.skill4InputReceived = base.inputBank.skill4.down;
				}
				if (base.skillLocator)
				{
					if (this.skill1InputReceived && base.skillLocator.primary && this.CanExecuteSkill(base.skillLocator.primary))
					{
						base.skillLocator.primary.ExecuteIfReady();
					}
					if (this.skill2InputReceived && base.skillLocator.secondary && this.CanExecuteSkill(base.skillLocator.secondary))
					{
						base.skillLocator.secondary.ExecuteIfReady();
					}
					if (this.skill3InputReceived && base.skillLocator.utility && this.CanExecuteSkill(base.skillLocator.utility))
					{
						base.skillLocator.utility.ExecuteIfReady();
					}
					if (this.skill4InputReceived && base.skillLocator.special && this.CanExecuteSkill(base.skillLocator.special))
					{
						base.skillLocator.special.ExecuteIfReady();
					}
				}
			}
		}

		// Token: 0x0400035A RID: 858
		private Animator modelAnimator;

		// Token: 0x0400035B RID: 859
		private bool skill1InputReceived;

		// Token: 0x0400035C RID: 860
		private bool skill2InputReceived;

		// Token: 0x0400035D RID: 861
		private bool skill3InputReceived;

		// Token: 0x0400035E RID: 862
		private bool skill4InputReceived;

		// Token: 0x0400035F RID: 863
		private bool hasPivotPitchLayer;

		// Token: 0x04000360 RID: 864
		private bool hasPivotYawLayer;

		// Token: 0x04000361 RID: 865
		private bool hasPivotRollLayer;

		// Token: 0x04000362 RID: 866
		private static readonly int pivotPitchCycle = Animator.StringToHash("pivotPitchCycle");

		// Token: 0x04000363 RID: 867
		private static readonly int pivotYawCycle = Animator.StringToHash("pivotYawCycle");

		// Token: 0x04000364 RID: 868
		private static readonly int pivotRollCycle = Animator.StringToHash("pivotRollCycle");

		// Token: 0x04000365 RID: 869
		private static readonly int flyRate = Animator.StringToHash("fly.rate");
	}
}
