using System;
using UnityEngine;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001DC RID: 476
	public class Sit : BaseSitState
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x00023FBE File Offset: 0x000221BE
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Body", "SitLoop", 0.1f);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00023FDB File Offset: 0x000221DB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.inputBank.moveVector.sqrMagnitude >= Mathf.Epsilon && base.fixedAge >= Sit.minimumDuration)
			{
				this.outer.SetNextState(new ExitSit());
			}
		}

		// Token: 0x040009FD RID: 2557
		public static string soundString;

		// Token: 0x040009FE RID: 2558
		public static float minimumDuration;
	}
}
