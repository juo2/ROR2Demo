using System;
using UnityEngine;

namespace EntityStates.Huntress
{
	// Token: 0x0200031C RID: 796
	public class MiniBlinkState : BlinkState
	{
		// Token: 0x06000E3B RID: 3643 RVA: 0x0003D248 File Offset: 0x0003B448
		protected override Vector3 GetBlinkVector()
		{
			return ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
		}
	}
}
