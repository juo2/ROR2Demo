using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x02000417 RID: 1047
	public class HealZoneMainState : BaseMainState
	{
		// Token: 0x060012D5 RID: 4821 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override Interactability GetInteractability(Interactor activator)
		{
			return Interactability.Disabled;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00054004 File Offset: 0x00052204
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.healZoneInstance = UnityEngine.Object.Instantiate<GameObject>(HealZoneMainState.healZonePrefab, base.transform.position, base.transform.rotation);
				this.healZoneInstance.GetComponent<TeamFilter>().teamIndex = this.teamFilter.teamIndex;
				NetworkServer.Spawn(this.healZoneInstance);
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0005406A File Offset: 0x0005226A
		public override void OnExit()
		{
			if (this.healZoneInstance)
			{
				EntityState.Destroy(this.healZoneInstance);
			}
			base.OnExit();
		}

		// Token: 0x04001839 RID: 6201
		public static GameObject healZonePrefab;

		// Token: 0x0400183A RID: 6202
		private GameObject healZoneInstance;
	}
}
