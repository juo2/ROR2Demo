using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Commando
{
	// Token: 0x020003E5 RID: 997
	public class MainState : BaseState
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x0004EEC0 File Offset: 0x0004D0C0
		public override void OnEnter()
		{
			base.OnEnter();
			GenericSkill[] components = base.gameObject.GetComponents<GenericSkill>();
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i].skillName == "FirePistol")
				{
					this.skill1 = components[i];
				}
				else if (components[i].skillName == "FireFMJ")
				{
					this.skill2 = components[i];
				}
				else if (components[i].skillName == "Roll")
				{
					this.skill3 = components[i];
				}
				else if (components[i].skillName == "FireBarrage")
				{
					this.skill4 = components[i];
				}
			}
			this.modelAnimator = base.GetModelAnimator();
			this.previousPosition = base.transform.position;
			if (this.modelAnimator)
			{
				int layerIndex = this.modelAnimator.GetLayerIndex("Body");
				this.modelAnimator.CrossFadeInFixedTime("Walk", 0.1f, layerIndex);
				this.modelAnimator.Update(0f);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				AimAnimator component = modelTransform.GetComponent<AimAnimator>();
				if (component)
				{
					component.enabled = true;
				}
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004EFF4 File Offset: 0x0004D1F4
		public override void OnExit()
		{
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				AimAnimator component = modelTransform.GetComponent<AimAnimator>();
				if (component)
				{
					component.enabled = false;
				}
			}
			if (base.isAuthority && base.characterMotor)
			{
				base.characterMotor.moveDirection = Vector3.zero;
			}
			base.OnExit();
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004F054 File Offset: 0x0004D254
		public override void Update()
		{
			base.Update();
			if (base.inputBank.skill1.down)
			{
				this.skill1InputRecieved = true;
			}
			if (base.inputBank.skill2.down)
			{
				this.skill2InputRecieved = true;
			}
			if (base.inputBank.skill3.down)
			{
				this.skill3InputRecieved = true;
			}
			if (base.inputBank.skill4.down)
			{
				this.skill4InputRecieved = true;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0004F0CC File Offset: 0x0004D2CC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			Vector3 position = base.transform.position;
			if (Time.fixedDeltaTime != 0f)
			{
				this.estimatedVelocity = (position - this.previousPosition) / Time.fixedDeltaTime;
			}
			if (base.isAuthority)
			{
				Vector3 moveVector = base.inputBank.moveVector;
				if (base.characterMotor)
				{
					base.characterMotor.moveDirection = moveVector;
					if (this.skill3InputRecieved)
					{
						if (this.skill3)
						{
							this.skill3.ExecuteIfReady();
						}
						this.skill3InputRecieved = false;
					}
				}
				if (base.characterDirection)
				{
					if (base.characterBody && base.characterBody.shouldAim)
					{
						base.characterDirection.moveVector = base.inputBank.aimDirection;
					}
					else
					{
						base.characterDirection.moveVector = moveVector;
					}
				}
				if (this.skill1InputRecieved)
				{
					if (this.skill1)
					{
						this.skill1.ExecuteIfReady();
					}
					this.skill1InputRecieved = false;
				}
				if (this.skill2InputRecieved)
				{
					if (this.skill2)
					{
						this.skill2.ExecuteIfReady();
					}
					this.skill2InputRecieved = false;
				}
				if (this.skill4InputRecieved)
				{
					if (this.skill4)
					{
						this.skill4.ExecuteIfReady();
					}
					this.skill4InputRecieved = false;
				}
			}
			if (this.modelAnimator && base.characterDirection)
			{
				Vector3 lhs = this.estimatedVelocity;
				lhs.y = 0f;
				Vector3 forward = base.characterDirection.forward;
				Vector3 rhs = Vector3.Cross(Vector3.up, forward);
				float magnitude = lhs.magnitude;
				float value = Vector3.Dot(lhs, forward);
				float value2 = Vector3.Dot(lhs, rhs);
				this.modelAnimator.SetBool("isMoving", magnitude != 0f);
				this.modelAnimator.SetFloat("walkSpeed", magnitude);
				this.modelAnimator.SetFloat("forwardSpeed", value, 0.2f, Time.fixedDeltaTime);
				this.modelAnimator.SetFloat("rightSpeed", value2, 0.2f, Time.fixedDeltaTime);
			}
			this.previousPosition = position;
		}

		// Token: 0x040016AE RID: 5806
		private Animator modelAnimator;

		// Token: 0x040016AF RID: 5807
		private GenericSkill skill1;

		// Token: 0x040016B0 RID: 5808
		private GenericSkill skill2;

		// Token: 0x040016B1 RID: 5809
		private GenericSkill skill3;

		// Token: 0x040016B2 RID: 5810
		private GenericSkill skill4;

		// Token: 0x040016B3 RID: 5811
		private bool skill1InputRecieved;

		// Token: 0x040016B4 RID: 5812
		private bool skill2InputRecieved;

		// Token: 0x040016B5 RID: 5813
		private bool skill3InputRecieved;

		// Token: 0x040016B6 RID: 5814
		private bool skill4InputRecieved;

		// Token: 0x040016B7 RID: 5815
		private Vector3 previousPosition;

		// Token: 0x040016B8 RID: 5816
		private Vector3 estimatedVelocity;
	}
}
