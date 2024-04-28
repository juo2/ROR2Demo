using System;
using System.Collections.Generic;
using RoR2;
using RoR2.CharacterAI;
using RoR2.ConVar;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.AI
{
	// Token: 0x020004A6 RID: 1190
	public abstract class BaseAIState : EntityState
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0005EDE9 File Offset: 0x0005CFE9
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x0005EDF1 File Offset: 0x0005CFF1
		private protected CharacterMaster characterMaster { protected get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0005EDFA File Offset: 0x0005CFFA
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x0005EE02 File Offset: 0x0005D002
		private protected BaseAI ai { protected get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0005EE0B File Offset: 0x0005D00B
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x0005EE13 File Offset: 0x0005D013
		private protected CharacterBody body { protected get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0005EE1C File Offset: 0x0005D01C
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x0005EE24 File Offset: 0x0005D024
		private protected Transform bodyTransform { protected get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0005EE2D File Offset: 0x0005D02D
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x0005EE35 File Offset: 0x0005D035
		private protected InputBankTest bodyInputBank { protected get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0005EE3E File Offset: 0x0005D03E
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x0005EE46 File Offset: 0x0005D046
		private protected CharacterMotor bodyCharacterMotor { protected get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0005EE4F File Offset: 0x0005D04F
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x0005EE57 File Offset: 0x0005D057
		private protected SkillLocator bodySkillLocator { protected get; private set; }

		// Token: 0x06001568 RID: 5480 RVA: 0x0005EE60 File Offset: 0x0005D060
		public override void OnEnter()
		{
			base.OnEnter();
			this.characterMaster = base.GetComponent<CharacterMaster>();
			this.ai = base.GetComponent<BaseAI>();
			if (this.ai)
			{
				this.body = this.ai.body;
				this.bodyTransform = this.ai.bodyTransform;
				this.bodyInputBank = this.ai.bodyInputBank;
				this.bodyCharacterMotor = this.ai.bodyCharacterMotor;
				this.bodySkillLocator = this.ai.bodySkillLocator;
			}
			this.bodyInputs = default(BaseAI.BodyInputs);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0000F997 File Offset: 0x0000DB97
		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0005EEF9 File Offset: 0x0005D0F9
		public virtual BaseAI.BodyInputs GenerateBodyInputs(in BaseAI.BodyInputs previousBodyInputs)
		{
			return this.bodyInputs;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0005EF04 File Offset: 0x0005D104
		protected void ModifyInputsForJumpIfNeccessary(ref BaseAI.BodyInputs bodyInputs)
		{
			if (!this.ai)
			{
				return;
			}
			BroadNavigationSystem.AgentOutput output = this.ai.broadNavigationAgent.output;
			bodyInputs.pressJump = false;
			if (this.bodyCharacterMotor)
			{
				bool isGrounded = this.bodyCharacterMotor.isGrounded;
				bool flag = isGrounded || this.bodyCharacterMotor.isFlying || !this.bodyCharacterMotor.useGravity;
				if (this.isInJump && flag)
				{
					this.isInJump = false;
					this.jumpLockedMoveVector = null;
				}
				if (isGrounded)
				{
					float num = Mathf.Max(output.desiredJumpVelocity, this.ai.localNavigator.jumpSpeed);
					if (num > 0f && this.body.jumpPower > 0f)
					{
						bool flag2 = output.desiredJumpVelocity >= this.ai.localNavigator.jumpSpeed;
						num = this.body.jumpPower;
						bodyInputs.pressJump = true;
						if (flag2 && output.nextPosition != null)
						{
							Vector3 vector = output.nextPosition.Value - this.bodyTransform.position;
							Vector3 a = vector;
							a.y = 0f;
							float num2 = Trajectory.CalculateFlightDuration(0f, vector.y, num);
							float walkSpeed = this.bodyCharacterMotor.walkSpeed;
							if (num2 > 0f && walkSpeed > 0f)
							{
								float magnitude = a.magnitude;
								float num3 = Mathf.Max(magnitude / num2 / this.bodyCharacterMotor.walkSpeed, 0f);
								this.jumpLockedMoveVector = new Vector3?(a * (num3 / magnitude));
								this.bodyCharacterMotor.moveDirection = this.jumpLockedMoveVector.Value;
							}
						}
						this.isInJump = true;
					}
				}
				if (this.jumpLockedMoveVector != null)
				{
					bodyInputs.moveVector = this.jumpLockedMoveVector.Value;
				}
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0005F0FC File Offset: 0x0005D2FC
		protected Vector3? PickRandomNearbyReachablePosition()
		{
			if (!this.ai || !this.body)
			{
				return null;
			}
			NodeGraph nodeGraph = SceneInfo.instance.GetNodeGraph(this.body.isFlying ? MapNodeGroup.GraphType.Air : MapNodeGroup.GraphType.Ground);
			NodeGraphSpider nodeGraphSpider = new NodeGraphSpider(nodeGraph, (HullMask)(1 << (int)this.body.hullClassification));
			NodeGraph.NodeIndex nodeIndex = nodeGraph.FindClosestNode(this.bodyTransform.position, this.body.hullClassification, 50f);
			nodeGraphSpider.AddNodeForNextStep(nodeIndex);
			for (int i = 0; i < 6; i++)
			{
				nodeGraphSpider.PerformStep();
			}
			List<NodeGraphSpider.StepInfo> collectedSteps = nodeGraphSpider.collectedSteps;
			if (collectedSteps.Count == 0)
			{
				return null;
			}
			int index = UnityEngine.Random.Range(0, collectedSteps.Count);
			NodeGraph.NodeIndex node = collectedSteps[index].node;
			Vector3 value;
			if (nodeGraph.GetNodePosition(node, out value))
			{
				return new Vector3?(value);
			}
			return null;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0005F1F8 File Offset: 0x0005D3F8
		protected void AimAt(ref BaseAI.BodyInputs dest, BaseAI.Target aimTarget)
		{
			if (aimTarget == null)
			{
				return;
			}
			Vector3 a;
			if (aimTarget.GetBullseyePosition(out a))
			{
				dest.desiredAimDirection = (a - this.bodyInputBank.aimOrigin).normalized;
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0005F232 File Offset: 0x0005D432
		protected void AimInDirection(ref BaseAI.BodyInputs dest, Vector3 aimDirection)
		{
			if (aimDirection != Vector3.zero)
			{
				dest.desiredAimDirection = aimDirection;
			}
		}

		// Token: 0x04001B42 RID: 6978
		protected static FloatConVar cvAIUpdateInterval = new FloatConVar("ai_update_interval", ConVarFlags.Cheat, "0.2", "Frequency that the local navigator refreshes.");

		// Token: 0x04001B4A RID: 6986
		protected BaseAI.BodyInputs bodyInputs;

		// Token: 0x04001B4B RID: 6987
		protected bool isInJump;

		// Token: 0x04001B4C RID: 6988
		protected Vector3? jumpLockedMoveVector;
	}
}
