using System;
using RoR2;
using RoR2.CharacterAI;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.AI.Walker
{
	// Token: 0x020004A7 RID: 1191
	public class Combat : BaseAIState
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x0005F264 File Offset: 0x0005D464
		public override void OnEnter()
		{
			base.OnEnter();
			this.activeSoundTimer = UnityEngine.Random.Range(3f, 8f);
			if (base.ai)
			{
				this.lastPathUpdate = base.ai.broadNavigationAgent.output.lastPathUpdate;
				base.ai.broadNavigationAgent.InvalidatePath();
			}
			this.fallbackNodeStartAge = float.NegativeInfinity;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0005F2D8 File Offset: 0x0005D4D8
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0005F2E0 File Offset: 0x0005D4E0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.ai && base.body)
			{
				this.aiUpdateTimer -= Time.fixedDeltaTime;
				this.strafeTimer -= Time.fixedDeltaTime;
				this.UpdateFootPosition();
				if (this.aiUpdateTimer <= 0f)
				{
					this.aiUpdateTimer = BaseAIState.cvAIUpdateInterval.value;
					this.UpdateAI(BaseAIState.cvAIUpdateInterval.value);
					if (!this.dominantSkillDriver)
					{
						this.outer.SetNextState(new LookBusy());
					}
				}
				this.UpdateBark();
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0005F38C File Offset: 0x0005D58C
		protected void UpdateFootPosition()
		{
			this.myBodyFootPosition = base.body.footPosition;
			base.ai.broadNavigationAgent.currentPosition = new Vector3?(this.myBodyFootPosition);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0005F3C8 File Offset: 0x0005D5C8
		protected void UpdateAI(float deltaTime)
		{
			BaseAI.SkillDriverEvaluation skillDriverEvaluation = base.ai.skillDriverEvaluation;
			this.dominantSkillDriver = skillDriverEvaluation.dominantSkillDriver;
			this.currentSkillSlot = SkillSlot.None;
			this.currentSkillMeetsActivationConditions = false;
			this.bodyInputs.moveVector = Vector3.zero;
			AISkillDriver.MovementType movementType = AISkillDriver.MovementType.Stop;
			float d = 1f;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (!base.body || !base.bodyInputBank)
			{
				return;
			}
			if (this.dominantSkillDriver)
			{
				movementType = this.dominantSkillDriver.movementType;
				this.currentSkillSlot = this.dominantSkillDriver.skillSlot;
				flag = this.dominantSkillDriver.activationRequiresTargetLoS;
				flag2 = this.dominantSkillDriver.activationRequiresAimTargetLoS;
				flag3 = this.dominantSkillDriver.activationRequiresAimConfirmation;
				d = this.dominantSkillDriver.moveInputScale;
			}
			Vector3 position = base.bodyTransform.position;
			Vector3 aimOrigin = base.bodyInputBank.aimOrigin;
			BroadNavigationSystem.Agent broadNavigationAgent = base.ai.broadNavigationAgent;
			BroadNavigationSystem.AgentOutput output = broadNavigationAgent.output;
			BaseAI.Target target = skillDriverEvaluation.target;
			if ((target != null) ? target.gameObject : null)
			{
				Vector3 vector;
				target.GetBullseyePosition(out vector);
				Vector3 vector2 = vector;
				if (this.fallbackNodeStartAge + this.fallbackNodeDuration < base.fixedAge)
				{
					base.ai.SetGoalPosition(target);
				}
				Vector3 vector3 = position;
				bool allowWalkOffCliff = true;
				Vector3 a = (this.dominantSkillDriver && this.dominantSkillDriver.ignoreNodeGraph) ? ((!base.body.isFlying) ? vector2 : vector) : (output.nextPosition ?? this.myBodyFootPosition);
				Vector3 vector4 = (a - this.myBodyFootPosition).normalized * 10f;
				Vector3 a2 = Vector3.Cross(Vector3.up, vector4);
				if (movementType == AISkillDriver.MovementType.ChaseMoveTarget)
				{
					vector3 = a + (position - this.myBodyFootPosition);
				}
				else if (movementType == AISkillDriver.MovementType.FleeMoveTarget)
				{
					vector3 -= vector4;
				}
				else if (movementType == AISkillDriver.MovementType.StrafeMovetarget)
				{
					if (this.strafeTimer <= 0f)
					{
						if (this.strafeDirection == 0f)
						{
							this.strafeDirection = ((UnityEngine.Random.Range(0, 1) == 0) ? -1f : 1f);
						}
						this.strafeTimer = 0.25f;
					}
					vector3 += a2 * this.strafeDirection;
					allowWalkOffCliff = false;
				}
				base.ai.localNavigator.targetPosition = vector3;
				base.ai.localNavigator.allowWalkOffCliff = allowWalkOffCliff;
				base.ai.localNavigator.Update(deltaTime);
				if (base.ai.localNavigator.wasObstructedLastUpdate)
				{
					this.strafeDirection *= -1f;
				}
				this.bodyInputs.moveVector = base.ai.localNavigator.moveVector;
				this.bodyInputs.moveVector = this.bodyInputs.moveVector * d;
				if (!flag3 || base.ai.hasAimConfirmation)
				{
					bool flag4 = true;
					if (skillDriverEvaluation.target == skillDriverEvaluation.aimTarget && (flag && flag2))
					{
						flag2 = false;
					}
					if (flag4 && flag)
					{
						flag4 = skillDriverEvaluation.target.TestLOSNow();
					}
					if (flag4 && flag2)
					{
						flag4 = skillDriverEvaluation.aimTarget.TestLOSNow();
					}
					if (flag4)
					{
						this.currentSkillMeetsActivationConditions = true;
					}
				}
			}
			if (output.lastPathUpdate > this.lastPathUpdate && !output.targetReachable && this.fallbackNodeStartAge + this.fallbackNodeDuration < base.fixedAge)
			{
				broadNavigationAgent.goalPosition = base.PickRandomNearbyReachablePosition();
				broadNavigationAgent.InvalidatePath();
			}
			this.lastPathUpdate = output.lastPathUpdate;
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0005F764 File Offset: 0x0005D964
		public override BaseAI.BodyInputs GenerateBodyInputs(in BaseAI.BodyInputs previousBodyInputs)
		{
			bool pressSkill = false;
			bool pressSkill2 = false;
			bool pressSkill3 = false;
			bool pressSkill4 = false;
			if (base.bodyInputBank)
			{
				AISkillDriver.ButtonPressType buttonPressType = AISkillDriver.ButtonPressType.Abstain;
				if (this.dominantSkillDriver)
				{
					buttonPressType = this.dominantSkillDriver.buttonPressType;
				}
				bool flag = false;
				switch (this.currentSkillSlot)
				{
				case SkillSlot.Primary:
					flag = previousBodyInputs.pressSkill1;
					break;
				case SkillSlot.Secondary:
					flag = previousBodyInputs.pressSkill2;
					break;
				case SkillSlot.Utility:
					flag = previousBodyInputs.pressSkill3;
					break;
				case SkillSlot.Special:
					flag = previousBodyInputs.pressSkill4;
					break;
				}
				bool flag2 = this.currentSkillMeetsActivationConditions;
				switch (buttonPressType)
				{
				case AISkillDriver.ButtonPressType.Abstain:
					flag2 = false;
					break;
				case AISkillDriver.ButtonPressType.TapContinuous:
					flag2 &= !flag;
					break;
				}
				switch (this.currentSkillSlot)
				{
				case SkillSlot.Primary:
					pressSkill = flag2;
					break;
				case SkillSlot.Secondary:
					pressSkill2 = flag2;
					break;
				case SkillSlot.Utility:
					pressSkill3 = flag2;
					break;
				case SkillSlot.Special:
					pressSkill4 = flag2;
					break;
				}
			}
			this.bodyInputs.pressSkill1 = pressSkill;
			this.bodyInputs.pressSkill2 = pressSkill2;
			this.bodyInputs.pressSkill3 = pressSkill3;
			this.bodyInputs.pressSkill4 = pressSkill4;
			this.bodyInputs.pressSprint = false;
			this.bodyInputs.pressActivateEquipment = false;
			this.bodyInputs.desiredAimDirection = Vector3.zero;
			if (this.dominantSkillDriver)
			{
				this.bodyInputs.pressSprint = this.dominantSkillDriver.shouldSprint;
				this.bodyInputs.pressActivateEquipment = (this.dominantSkillDriver.shouldFireEquipment && !previousBodyInputs.pressActivateEquipment);
				int aimType = (int)this.dominantSkillDriver.aimType;
				BaseAI.Target aimTarget = base.ai.skillDriverEvaluation.aimTarget;
				if (aimType == 4)
				{
					base.AimInDirection(ref this.bodyInputs, this.bodyInputs.moveVector);
				}
				if (aimTarget != null)
				{
					base.AimAt(ref this.bodyInputs, aimTarget);
				}
			}
			base.ModifyInputsForJumpIfNeccessary(ref this.bodyInputs);
			return this.bodyInputs;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0005F958 File Offset: 0x0005DB58
		protected void UpdateBark()
		{
			this.activeSoundTimer -= Time.fixedDeltaTime;
			if (this.activeSoundTimer <= 0f)
			{
				this.activeSoundTimer = UnityEngine.Random.Range(3f, 8f);
				base.body.CallRpcBark();
			}
		}

		// Token: 0x04001B4D RID: 6989
		private float strafeDirection;

		// Token: 0x04001B4E RID: 6990
		private const float strafeDuration = 0.25f;

		// Token: 0x04001B4F RID: 6991
		private float strafeTimer;

		// Token: 0x04001B50 RID: 6992
		private float activeSoundTimer;

		// Token: 0x04001B51 RID: 6993
		private float aiUpdateTimer;

		// Token: 0x04001B52 RID: 6994
		private const float minUpdateInterval = 0.16666667f;

		// Token: 0x04001B53 RID: 6995
		private const float maxUpdateInterval = 0.2f;

		// Token: 0x04001B54 RID: 6996
		private AISkillDriver dominantSkillDriver;

		// Token: 0x04001B55 RID: 6997
		protected bool currentSkillMeetsActivationConditions;

		// Token: 0x04001B56 RID: 6998
		protected SkillSlot currentSkillSlot = SkillSlot.None;

		// Token: 0x04001B57 RID: 6999
		protected Vector3 myBodyFootPosition;

		// Token: 0x04001B58 RID: 7000
		private float lastPathUpdate;

		// Token: 0x04001B59 RID: 7001
		private float fallbackNodeStartAge;

		// Token: 0x04001B5A RID: 7002
		private readonly float fallbackNodeDuration = 4f;
	}
}
