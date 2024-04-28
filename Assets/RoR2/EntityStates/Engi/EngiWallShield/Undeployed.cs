using System;
using EntityStates.Engi.EngiBubbleShield;
using UnityEngine;

namespace EntityStates.Engi.EngiWallShield
{
	// Token: 0x020003B9 RID: 953
	public class Undeployed : Undeployed
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x0004AED6 File Offset: 0x000490D6
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0004AEE0 File Offset: 0x000490E0
		protected override void SetNextState()
		{
			Vector3 forward = base.transform.forward;
			Vector3 forward2 = new Vector3(forward.x, 0f, forward.z);
			base.transform.forward = forward2;
			this.outer.SetNextState(new Deployed());
		}
	}
}
