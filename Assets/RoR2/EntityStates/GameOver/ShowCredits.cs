using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x0200037D RID: 893
	public class ShowCredits : BaseGameOverControllerState
	{
		// Token: 0x06001005 RID: 4101 RVA: 0x00046DF4 File Offset: 0x00044FF4
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.creditsControllerInstance = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/CreditsController"));
				NetworkServer.Spawn(this.creditsControllerInstance);
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00046E23 File Offset: 0x00045023
		public override void OnExit()
		{
			if (NetworkServer.active && this.creditsControllerInstance)
			{
				EntityState.Destroy(this.creditsControllerInstance);
			}
			base.OnExit();
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00046E4A File Offset: 0x0004504A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && !this.creditsControllerInstance)
			{
				this.outer.SetNextState(new ShowReport());
			}
		}

		// Token: 0x04001489 RID: 5257
		private GameObject creditsControllerInstance;
	}
}
