using System;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.SurvivorPod
{
	// Token: 0x020001B9 RID: 441
	public class Release : SurvivorPodBaseState
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x00021C50 File Offset: 0x0001FE50
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "Release");
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				component.FindChild("Door").gameObject.SetActive(false);
				component.FindChild("ReleaseExhaustFX").gameObject.SetActive(true);
			}
			if (!base.survivorPodController)
			{
				return;
			}
			if (NetworkServer.active && base.vehicleSeat && base.vehicleSeat.currentPassengerBody)
			{
				base.vehicleSeat.EjectPassenger(base.vehicleSeat.currentPassengerBody.gameObject);
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00021D04 File Offset: 0x0001FF04
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && (!base.vehicleSeat || !base.vehicleSeat.currentPassengerBody))
			{
				this.outer.SetNextState(new ReleaseFinished());
			}
		}

		// Token: 0x0400096E RID: 2414
		public static float ejectionSpeed = 20f;
	}
}
