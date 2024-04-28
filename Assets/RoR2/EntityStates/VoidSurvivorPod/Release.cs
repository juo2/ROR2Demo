using System;
using EntityStates.SurvivorPod;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivorPod
{
	// Token: 0x020000E8 RID: 232
	public class Release : SurvivorPodBaseState
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00011374 File Offset: 0x0000F574
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation(this.animationLayerName, this.animationStateName);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			if (!base.survivorPodController)
			{
				return;
			}
			if (NetworkServer.active && base.vehicleSeat && base.vehicleSeat.currentPassengerBody)
			{
				CharacterBody currentPassengerBody = base.vehicleSeat.currentPassengerBody;
				base.vehicleSeat.EjectPassenger(currentPassengerBody.gameObject);
				HealthComponent component = currentPassengerBody.GetComponent<HealthComponent>();
				if (component)
				{
					component.TakeDamageForce(new DamageInfo
					{
						attacker = base.gameObject,
						force = Vector3.up * this.exitForceAmount,
						position = currentPassengerBody.corePosition
					}, true, false);
				}
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001144B File Offset: 0x0000F64B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && (!base.vehicleSeat || !base.vehicleSeat.currentPassengerBody))
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04000433 RID: 1075
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000434 RID: 1076
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000435 RID: 1077
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000436 RID: 1078
		[SerializeField]
		public float exitForceAmount;
	}
}
