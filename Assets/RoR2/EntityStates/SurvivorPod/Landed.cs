using System;
using RoR2;
using UnityEngine;

namespace EntityStates.SurvivorPod
{
	// Token: 0x020001B7 RID: 439
	public class Landed : SurvivorPodBaseState
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x00021AD4 File Offset: 0x0001FCD4
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "Idle");
			Util.PlaySound("Play_UI_podSteamLoop", base.gameObject);
			base.survivorPodController.exitAllowed = true;
			base.vehicleSeat.handleVehicleExitRequestServer.AddCallback(new CallbackCheck<bool, GameObject>.CallbackDelegate(this.HandleVehicleExitRequest));
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00021B30 File Offset: 0x0001FD30
		private void HandleVehicleExitRequest(GameObject gameObject, ref bool? result)
		{
			base.survivorPodController.exitAllowed = false;
			this.outer.SetNextState(new PreRelease());
			result = new bool?(true);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00021B5A File Offset: 0x0001FD5A
		public override void OnExit()
		{
			base.vehicleSeat.handleVehicleExitRequestServer.RemoveCallback(new CallbackCheck<bool, GameObject>.CallbackDelegate(this.HandleVehicleExitRequest));
			base.survivorPodController.exitAllowed = false;
			Util.PlaySound("Stop_UI_podSteamLoop", base.gameObject);
		}
	}
}
