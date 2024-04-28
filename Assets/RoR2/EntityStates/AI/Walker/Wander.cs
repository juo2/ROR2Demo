using System;
using RoR2;
using RoR2.CharacterAI;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.AI.Walker
{
	// Token: 0x020004AA RID: 1194
	public class Wander : BaseAIState
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x0005FB80 File Offset: 0x0005DD80
		private void PickNewTargetLookPosition()
		{
			if (base.bodyInputBank)
			{
				float num = 0f;
				Vector3 aimOrigin = base.bodyInputBank.aimOrigin;
				Vector3 vector = base.bodyInputBank.moveVector;
				if (vector == Vector3.zero)
				{
					vector = UnityEngine.Random.onUnitSphere;
				}
				for (int i = 0; i < 1; i++)
				{
					Vector3 direction = Util.ApplySpread(vector, 0f, 60f, 0f, 0f, 0f, 0f);
					float num2 = 25f;
					Ray ray = new Ray(aimOrigin, direction);
					RaycastHit raycastHit;
					if (Physics.Raycast(ray, out raycastHit, 25f, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
					{
						num2 = raycastHit.distance;
					}
					if (num2 > num)
					{
						num = num2;
						this.targetLookPosition = ray.GetPoint(num2);
					}
				}
			}
			this.lookTimer = UnityEngine.Random.Range(0.5f, 4f);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0005FC74 File Offset: 0x0005DE74
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.ai && base.body)
			{
				BroadNavigationSystem.Agent broadNavigationAgent = base.ai.broadNavigationAgent;
				this.targetPosition = base.PickRandomNearbyReachablePosition();
				if (this.targetPosition != null)
				{
					broadNavigationAgent.goalPosition = new Vector3?(this.targetPosition.Value);
					broadNavigationAgent.InvalidatePath();
				}
				this.PickNewTargetLookPosition();
				this.aiUpdateTimer = 0.5f;
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0005F2D8 File Offset: 0x0005D4D8
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0005FCF8 File Offset: 0x0005DEF8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.aiUpdateTimer -= Time.fixedDeltaTime;
			if (base.ai && base.body)
			{
				if (base.ai.skillDriverEvaluation.dominantSkillDriver)
				{
					this.outer.SetNextState(new Combat());
				}
				base.ai.SetGoalPosition(this.targetPosition);
				Vector3 position = base.bodyTransform.position;
				BroadNavigationSystem.Agent broadNavigationAgent = base.ai.broadNavigationAgent;
				if (this.aiUpdateTimer <= 0f)
				{
					this.aiUpdateTimer = BaseAIState.cvAIUpdateInterval.value;
					base.ai.localNavigator.targetPosition = (broadNavigationAgent.output.nextPosition ?? base.ai.localNavigator.targetPosition);
					base.ai.localNavigator.Update(BaseAIState.cvAIUpdateInterval.value);
					if (base.bodyInputBank)
					{
						this.bodyInputs.moveVector = base.ai.localNavigator.moveVector * 0.25f;
						this.bodyInputs.desiredAimDirection = (this.targetLookPosition - base.bodyInputBank.aimOrigin).normalized;
					}
					this.lookTimer -= Time.fixedDeltaTime;
					if (this.lookTimer <= 0f)
					{
						this.PickNewTargetLookPosition();
					}
					bool flag = false;
					if (this.targetPosition != null)
					{
						float sqrMagnitude = (base.body.footPosition - this.targetPosition.Value).sqrMagnitude;
						float num = base.body.radius * base.body.radius * 4f;
						flag = (sqrMagnitude > num);
					}
					if (!flag)
					{
						this.outer.SetNextState(new LookBusy());
						return;
					}
				}
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0005FEF5 File Offset: 0x0005E0F5
		public override BaseAI.BodyInputs GenerateBodyInputs(in BaseAI.BodyInputs previousBodyInputs)
		{
			base.ModifyInputsForJumpIfNeccessary(ref this.bodyInputs);
			return this.bodyInputs;
		}

		// Token: 0x04001B64 RID: 7012
		private Vector3? targetPosition;

		// Token: 0x04001B65 RID: 7013
		private float lookTimer;

		// Token: 0x04001B66 RID: 7014
		private const float minLookDuration = 0.5f;

		// Token: 0x04001B67 RID: 7015
		private const float maxLookDuration = 4f;

		// Token: 0x04001B68 RID: 7016
		private const int lookTries = 1;

		// Token: 0x04001B69 RID: 7017
		private const float lookRaycastLength = 25f;

		// Token: 0x04001B6A RID: 7018
		private Vector3 targetLookPosition;

		// Token: 0x04001B6B RID: 7019
		private float aiUpdateTimer;
	}
}
