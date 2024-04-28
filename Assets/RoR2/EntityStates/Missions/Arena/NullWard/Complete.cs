using System;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.Arena.NullWard
{
	// Token: 0x02000266 RID: 614
	public class Complete : NullWardBaseState
	{
		// Token: 0x06000ACB RID: 2763 RVA: 0x0002C170 File Offset: 0x0002A370
		public override void OnEnter()
		{
			base.OnEnter();
			this.sphereZone.Networkradius = NullWardBaseState.wardRadiusOn;
			this.purchaseInteraction.SetAvailable(false);
			this.childLocator.FindChild("CompleteEffect").gameObject.SetActive(true);
			if (NetworkServer.active)
			{
				base.arenaMissionController.EndRound();
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002C1CC File Offset: 0x0002A3CC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.sphereZone.Networkradius = Mathf.Lerp(NullWardBaseState.wardRadiusOn, NullWardBaseState.wardRadiusOff, base.fixedAge / Complete.duration);
			if (base.fixedAge >= Complete.duration && base.isAuthority)
			{
				this.outer.SetNextState(new Off());
			}
		}

		// Token: 0x04000C4E RID: 3150
		public static float duration;

		// Token: 0x04000C4F RID: 3151
		public static string soundEntryEvent;
	}
}
