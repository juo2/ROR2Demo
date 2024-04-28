using System;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000D6 RID: 214
	public class TestState2 : EntityState
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x000100BD File Offset: 0x0000E2BD
		public override void OnEnter()
		{
			Debug.LogFormat("{0} Entering TestState2.", new object[]
			{
				base.gameObject
			});
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000100D8 File Offset: 0x0000E2D8
		public override void OnExit()
		{
			Debug.LogFormat("{0} Exiting TestState2.", new object[]
			{
				base.gameObject
			});
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000100F3 File Offset: 0x0000E2F3
		public override void FixedUpdate()
		{
			if (base.isAuthority && Input.GetButton("Fire2"))
			{
				this.outer.SetNextState(new TestState1());
			}
		}
	}
}
