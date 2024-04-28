using System;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000D5 RID: 213
	public class TestState1 : EntityState
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x00010061 File Offset: 0x0000E261
		public override void OnEnter()
		{
			Debug.LogFormat("{0} Entering TestState1.", new object[]
			{
				base.gameObject
			});
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001007C File Offset: 0x0000E27C
		public override void OnExit()
		{
			Debug.LogFormat("{0} Exiting TestState1.", new object[]
			{
				base.gameObject
			});
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010097 File Offset: 0x0000E297
		public override void FixedUpdate()
		{
			if (base.isAuthority && Input.GetButton("Fire1"))
			{
				this.outer.SetNextState(new TestState2());
			}
		}
	}
}
