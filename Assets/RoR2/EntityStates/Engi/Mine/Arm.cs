using System;
using UnityEngine.Networking;

namespace EntityStates.Engi.Mine
{
	// Token: 0x0200039F RID: 927
	public class Arm : BaseMineState
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldStick
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00048952 File Offset: 0x00046B52
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && Arm.duration <= base.fixedAge)
			{
				this.outer.SetNextState(new WaitForTarget());
			}
		}

		// Token: 0x040014ED RID: 5357
		public static float duration;
	}
}
