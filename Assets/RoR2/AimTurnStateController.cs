using System;
using EntityStates;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005C3 RID: 1475
	public class AimTurnStateController : MonoBehaviour
	{
		// Token: 0x06001AB8 RID: 6840 RVA: 0x00072B2C File Offset: 0x00070D2C
		private void FixedUpdate()
		{
			if (Run.instance.fixedTime - this.lastTriggerTime > this.retriggerDelaySeconds)
			{
				Vector3 aimDirection = this.inputBank.aimDirection;
				aimDirection.Scale(this.aimScale);
				aimDirection.Normalize();
				Vector3 forward = this.characterDirection.forward;
				forward.Scale(this.aimScale);
				forward.Normalize();
				if (Vector3.Angle(aimDirection, forward) > this.minTriggerDegrees)
				{
					this.lastTriggerTime = Run.instance.fixedTime;
					if (this.targetStateMachine)
					{
						EntityState newNextState = EntityStateCatalog.InstantiateState(this.turnStateType);
						this.targetStateMachine.SetInterruptState(newNextState, this.interruptPriority);
					}
				}
			}
		}

		// Token: 0x040020D4 RID: 8404
		[Tooltip("The component we use to determine the current orientation")]
		[SerializeField]
		private CharacterDirection characterDirection;

		// Token: 0x040020D5 RID: 8405
		[Tooltip("The component we use to determine the current aim")]
		[SerializeField]
		private InputBankTest inputBank;

		// Token: 0x040020D6 RID: 8406
		[Tooltip("The state machine we should modify")]
		[SerializeField]
		private EntityStateMachine targetStateMachine;

		// Token: 0x040020D7 RID: 8407
		[Tooltip("The state we should push")]
		[SerializeField]
		private SerializableEntityStateType turnStateType;

		// Token: 0x040020D8 RID: 8408
		[SerializeField]
		[Tooltip("The priority of the new state")]
		private InterruptPriority interruptPriority;

		// Token: 0x040020D9 RID: 8409
		[Tooltip("The minimum difference between the current orientation and the aim before we should push the state")]
		[SerializeField]
		private float minTriggerDegrees;

		// Token: 0x040020DA RID: 8410
		[Tooltip("The minimum time before we should push the state again")]
		[SerializeField]
		private float retriggerDelaySeconds;

		// Token: 0x040020DB RID: 8411
		[Tooltip("The aim/direction vectors are multiplied by this vector and normalized before comparison.  This can be used to exclude a dimension from the calculation.")]
		[SerializeField]
		private Vector3 aimScale = new Vector3(1f, 1f, 1f);

		// Token: 0x040020DC RID: 8412
		private float lastTriggerTime;
	}
}
