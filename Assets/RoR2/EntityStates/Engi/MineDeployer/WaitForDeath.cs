using System;
using UnityEngine.Networking;

namespace EntityStates.Engi.MineDeployer
{
	// Token: 0x02000397 RID: 919
	public class WaitForDeath : BaseMineDeployerState
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x0004870C File Offset: 0x0004690C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && WaitForDeath.duration <= base.fixedAge)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x040014DF RID: 5343
		public static float duration;
	}
}
