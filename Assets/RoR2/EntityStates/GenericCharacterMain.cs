using System;
using System.Runtime.CompilerServices;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000C1 RID: 193
	public class GenericCharacterMain : BaseCharacterMain
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.aimAnimator = modelTransform.GetComponent<AimAnimator>();
				if (this.aimAnimator)
				{
					this.aimAnimator.enabled = true;
				}
			}
			this.hasAimAnimator = this.aimAnimator;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E434 File Offset: 0x0000C634
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
			if (base.isAuthority)
			{
				if (base.characterMotor)
				{
					base.characterMotor.moveDirection = Vector3.zero;
				}
				if (base.railMotor)
				{
					base.railMotor.inputMoveVector = Vector3.zero;
				}
			}
			base.OnExit();
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000E4AE File Offset: 0x0000C6AE
		public override void Update()
		{
			base.Update();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000E4B6 File Offset: 0x0000C6B6
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.GatherInputs();
			this.HandleMovements();
			this.PerformInputs();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		public virtual void HandleMovements()
		{
			if (this.useRootMotion)
			{
				if (this.hasCharacterMotor)
				{
					base.characterMotor.moveDirection = Vector3.zero;
				}
				if (this.hasRailMotor)
				{
					base.railMotor.inputMoveVector = this.moveVector;
				}
			}
			else
			{
				if (this.hasCharacterMotor)
				{
					base.characterMotor.moveDirection = this.moveVector;
				}
				if (this.hasRailMotor)
				{
					base.railMotor.inputMoveVector = this.moveVector;
				}
			}
			bool isGrounded = base.isGrounded;
			if (!this.hasRailMotor && this.hasCharacterDirection && this.hasCharacterBody)
			{
				if (this.hasAimAnimator && this.aimAnimator.aimType == AimAnimator.AimType.Smart)
				{
					Vector3 vector = (this.moveVector == Vector3.zero) ? base.characterDirection.forward : this.moveVector;
					float num = Vector3.Angle(this.aimDirection, vector);
					float num2 = Mathf.Max(this.aimAnimator.pitchRangeMax + this.aimAnimator.pitchGiveupRange, this.aimAnimator.yawRangeMax + this.aimAnimator.yawGiveupRange);
					base.characterDirection.moveVector = ((base.characterBody && base.characterBody.shouldAim && num > num2) ? this.aimDirection : vector);
				}
				else
				{
					base.characterDirection.moveVector = ((base.characterBody && base.characterBody.shouldAim) ? this.aimDirection : this.moveVector);
				}
			}
			if (base.isAuthority)
			{
				this.ProcessJump();
				if (this.hasCharacterBody)
				{
					bool isSprinting = this.sprintInputReceived;
					if (this.moveVector.magnitude <= 0.5f)
					{
						isSprinting = false;
					}
					base.characterBody.isSprinting = isSprinting;
				}
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000E69C File Offset: 0x0000C89C
		public static void ApplyJumpVelocity(CharacterMotor characterMotor, CharacterBody characterBody, float horizontalBonus, float verticalBonus, bool vault = false)
		{
			Vector3 vector = characterMotor.moveDirection;
			if (vault)
			{
				characterMotor.velocity = vector;
			}
			else
			{
				vector.y = 0f;
				float magnitude = vector.magnitude;
				if (magnitude > 0f)
				{
					vector /= magnitude;
				}
				Vector3 velocity = vector * characterBody.moveSpeed * horizontalBonus;
				velocity.y = characterBody.jumpPower * verticalBonus;
				characterMotor.velocity = velocity;
			}
			characterMotor.Motor.ForceUnground();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000E718 File Offset: 0x0000C918
		public virtual void ProcessJump()
		{
			if (this.hasCharacterMotor)
			{
				bool flag = false;
				bool flag2 = false;
				if (this.jumpInputReceived && base.characterBody && base.characterMotor.jumpCount < base.characterBody.maxJumpCount)
				{
					int itemCount = base.characterBody.inventory.GetItemCount(RoR2Content.Items.JumpBoost);
					float horizontalBonus = 1f;
					float verticalBonus = 1f;
					if (base.characterMotor.jumpCount >= base.characterBody.baseJumpCount)
					{
						flag = true;
						horizontalBonus = 1.5f;
						verticalBonus = 1.5f;
					}
					else if ((float)itemCount > 0f && base.characterBody.isSprinting)
					{
						float num = base.characterBody.acceleration * base.characterMotor.airControl;
						if (base.characterBody.moveSpeed > 0f && num > 0f)
						{
							flag2 = true;
							float num2 = Mathf.Sqrt(10f * (float)itemCount / num);
							float num3 = base.characterBody.moveSpeed / num;
							horizontalBonus = (num2 + num3) / num3;
						}
					}
					GenericCharacterMain.ApplyJumpVelocity(base.characterMotor, base.characterBody, horizontalBonus, verticalBonus, false);
					if (this.hasModelAnimator)
					{
						int layerIndex = base.modelAnimator.GetLayerIndex("Body");
						if (layerIndex >= 0)
						{
							if (base.characterMotor.jumpCount == 0 || base.characterBody.baseJumpCount == 1)
							{
								base.modelAnimator.CrossFadeInFixedTime("Jump", this.smoothingParameters.intoJumpTransitionTime, layerIndex);
							}
							else
							{
								base.modelAnimator.CrossFadeInFixedTime("BonusJump", this.smoothingParameters.intoJumpTransitionTime, layerIndex);
							}
						}
					}
					if (flag)
					{
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/FeatherEffect"), new EffectData
						{
							origin = base.characterBody.footPosition
						}, true);
					}
					else if (base.characterMotor.jumpCount > 0)
					{
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/CharacterLandImpact"), new EffectData
						{
							origin = base.characterBody.footPosition,
							scale = base.characterBody.radius
						}, true);
					}
					if (flag2)
					{
						EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BoostJumpEffect"), new EffectData
						{
							origin = base.characterBody.footPosition,
							rotation = Util.QuaternionSafeLookRotation(base.characterMotor.velocity)
						}, true);
					}
					base.characterMotor.jumpCount++;
				}
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool CanExecuteSkill(GenericSkill skillSlot)
		{
			return true;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000E978 File Offset: 0x0000CB78
		protected void PerformInputs()
		{
			if (base.isAuthority)
			{
				if (this.hasSkillLocator)
				{
					this.<PerformInputs>g__HandleSkill|15_0(base.skillLocator.primary, ref base.inputBank.skill1);
					this.<PerformInputs>g__HandleSkill|15_0(base.skillLocator.secondary, ref base.inputBank.skill2);
					this.<PerformInputs>g__HandleSkill|15_0(base.skillLocator.utility, ref base.inputBank.skill3);
					this.<PerformInputs>g__HandleSkill|15_0(base.skillLocator.special, ref base.inputBank.skill4);
				}
				this.jumpInputReceived = false;
				this.sprintInputReceived = false;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000EA18 File Offset: 0x0000CC18
		protected void GatherInputs()
		{
			if (this.hasInputBank)
			{
				this.moveVector = base.inputBank.moveVector;
				this.aimDirection = base.inputBank.aimDirection;
				this.emoteRequest = base.inputBank.emoteRequest;
				base.inputBank.emoteRequest = -1;
				this.jumpInputReceived = base.inputBank.jump.justPressed;
				this.sprintInputReceived |= base.inputBank.sprint.down;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		[CompilerGenerated]
		private void <PerformInputs>g__HandleSkill|15_0(GenericSkill skillSlot, ref InputBankTest.ButtonState buttonState)
		{
			if (!buttonState.down || !skillSlot)
			{
				return;
			}
			if (skillSlot.mustKeyPress && buttonState.hasPressBeenClaimed)
			{
				return;
			}
			if (this.CanExecuteSkill(skillSlot) && skillSlot.ExecuteIfReady())
			{
				buttonState.hasPressBeenClaimed = true;
			}
		}

		// Token: 0x0400038F RID: 911
		private AimAnimator aimAnimator;

		// Token: 0x04000390 RID: 912
		protected bool jumpInputReceived;

		// Token: 0x04000391 RID: 913
		protected bool sprintInputReceived;

		// Token: 0x04000392 RID: 914
		private Vector3 moveVector = Vector3.zero;

		// Token: 0x04000393 RID: 915
		private Vector3 aimDirection = Vector3.forward;

		// Token: 0x04000394 RID: 916
		private int emoteRequest = -1;

		// Token: 0x04000395 RID: 917
		private bool hasAimAnimator;
	}
}
