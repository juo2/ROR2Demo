using System;
using EntityStates.SurvivorPod;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.RoboCratePod
{
	// Token: 0x020001B5 RID: 437
	public class Descent : Descent
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x0002191A File Offset: 0x0001FB1A
		protected override void TransitionIntoNextState()
		{
			base.TransitionIntoNextState();
			EffectManager.SimpleMuzzleFlash(Descent.effectPrefab, base.gameObject, "Base", true);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00021938 File Offset: 0x0001FB38
		public override void OnExit()
		{
			VehicleSeat component = base.GetComponent<VehicleSeat>();
			if (component)
			{
				component.EjectPassenger();
			}
			if (NetworkServer.active)
			{
				EntityState.Destroy(base.gameObject);
			}
			base.OnExit();
		}

		// Token: 0x0400096C RID: 2412
		public static GameObject effectPrefab;
	}
}
