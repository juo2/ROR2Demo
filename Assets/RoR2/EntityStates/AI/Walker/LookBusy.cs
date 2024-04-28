using System;
using RoR2;
using RoR2.CharacterAI;
using UnityEngine;

namespace EntityStates.AI.Walker
{
	// Token: 0x020004A8 RID: 1192
	public class LookBusy : BaseAIState
	{
		// Token: 0x0600157A RID: 5498 RVA: 0x0005F9C0 File Offset: 0x0005DBC0
		protected virtual void PickNewTargetLookDirection()
		{
			if (base.bodyInputBank)
			{
				float num = 0f;
				Vector3 aimOrigin = base.bodyInputBank.aimOrigin;
				for (int i = 0; i < 4; i++)
				{
					Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
					float num2 = 25f;
					RaycastHit raycastHit;
					if (Physics.Raycast(new Ray(aimOrigin, onUnitSphere), out raycastHit, 25f, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
					{
						num2 = raycastHit.distance;
					}
					if (num2 > num)
					{
						num = num2;
						this.bodyInputs.desiredAimDirection = onUnitSphere;
					}
				}
			}
			this.lookTimer = UnityEngine.Random.Range(0.5f, 4f);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0005FA64 File Offset: 0x0005DC64
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = UnityEngine.Random.Range(2f, 7f);
			base.bodyInputBank.moveVector = Vector3.zero;
			base.bodyInputBank.jump.PushState(false);
			this.PickNewTargetLookDirection();
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0005F2D8 File Offset: 0x0005D4D8
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0005FAB4 File Offset: 0x0005DCB4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.ai && base.body)
			{
				if (base.ai.skillDriverEvaluation.dominantSkillDriver)
				{
					this.outer.SetNextState(new Combat());
				}
				if (base.ai.hasAimConfirmation)
				{
					this.lookTimer -= Time.fixedDeltaTime;
					if (this.lookTimer <= 0f)
					{
						this.PickNewTargetLookDirection();
					}
				}
				if (base.fixedAge >= this.duration)
				{
					this.outer.SetNextState(new Wander());
					return;
				}
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0005EEF9 File Offset: 0x0005D0F9
		public override BaseAI.BodyInputs GenerateBodyInputs(in BaseAI.BodyInputs previousBodyInputs)
		{
			return this.bodyInputs;
		}

		// Token: 0x04001B5B RID: 7003
		private const float minDuration = 2f;

		// Token: 0x04001B5C RID: 7004
		private const float maxDuration = 7f;

		// Token: 0x04001B5D RID: 7005
		private Vector3 targetPosition;

		// Token: 0x04001B5E RID: 7006
		private float duration;

		// Token: 0x04001B5F RID: 7007
		private float lookTimer;

		// Token: 0x04001B60 RID: 7008
		private const float minLookDuration = 0.5f;

		// Token: 0x04001B61 RID: 7009
		private const float maxLookDuration = 4f;

		// Token: 0x04001B62 RID: 7010
		private const int lookTries = 4;

		// Token: 0x04001B63 RID: 7011
		private const float lookRaycastLength = 25f;
	}
}
