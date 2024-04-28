using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.EntityLogic
{
	// Token: 0x02000C85 RID: 3205
	public class TeleporterEventRelay : MonoBehaviour
	{
		// Token: 0x0600494F RID: 18767 RVA: 0x0012DE84 File Offset: 0x0012C084
		public void OnEnable()
		{
			TeleporterInteraction.onTeleporterBeginChargingGlobal += this.CheckTeleporterBeginCharging;
			TeleporterInteraction.onTeleporterChargedGlobal += this.CheckTeleporterCharged;
			TeleporterInteraction.onTeleporterFinishGlobal += this.CheckTeleporterFinish;
			this.CheckTeleporterBeginCharging(TeleporterInteraction.instance);
			this.CheckTeleporterCharged(TeleporterInteraction.instance);
			this.CheckTeleporterFinish(TeleporterInteraction.instance);
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x0012DEE5 File Offset: 0x0012C0E5
		public void OnDisable()
		{
			TeleporterInteraction.onTeleporterBeginChargingGlobal -= this.CheckTeleporterBeginCharging;
			TeleporterInteraction.onTeleporterChargedGlobal -= this.CheckTeleporterCharged;
			TeleporterInteraction.onTeleporterFinishGlobal -= this.CheckTeleporterFinish;
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x0012DF1A File Offset: 0x0012C11A
		private void CheckTeleporterBeginCharging(TeleporterInteraction teleporter)
		{
			if (teleporter && teleporter.activationState >= TeleporterInteraction.ActivationState.Charging && this.recordedActivationState < TeleporterInteraction.ActivationState.Charging)
			{
				UnityEvent unityEvent = this.onTeleporterBeginCharging;
				if (unityEvent != null)
				{
					unityEvent.Invoke();
				}
				this.recordedActivationState = TeleporterInteraction.ActivationState.Charging;
			}
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x0012DF4E File Offset: 0x0012C14E
		private void CheckTeleporterCharged(TeleporterInteraction teleporter)
		{
			if (teleporter && teleporter.activationState >= TeleporterInteraction.ActivationState.Charged && this.recordedActivationState < TeleporterInteraction.ActivationState.Charged)
			{
				UnityEvent unityEvent = this.onTeleporterCharged;
				if (unityEvent != null)
				{
					unityEvent.Invoke();
				}
				this.recordedActivationState = TeleporterInteraction.ActivationState.Charged;
			}
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x0012DF82 File Offset: 0x0012C182
		private void CheckTeleporterFinish(TeleporterInteraction teleporter)
		{
			if (teleporter && teleporter.activationState >= TeleporterInteraction.ActivationState.Finished && this.recordedActivationState < TeleporterInteraction.ActivationState.Finished)
			{
				UnityEvent unityEvent = this.onTeleporterFinish;
				if (unityEvent != null)
				{
					unityEvent.Invoke();
				}
				this.recordedActivationState = TeleporterInteraction.ActivationState.Finished;
			}
		}

		// Token: 0x04004621 RID: 17953
		public UnityEvent onTeleporterBeginCharging;

		// Token: 0x04004622 RID: 17954
		public UnityEvent onTeleporterCharged;

		// Token: 0x04004623 RID: 17955
		public UnityEvent onTeleporterFinish;

		// Token: 0x04004624 RID: 17956
		private TeleporterInteraction.ActivationState recordedActivationState;
	}
}
