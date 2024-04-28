using System;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Engi.Mine
{
	// Token: 0x020003A1 RID: 929
	public class PreDetonate : BaseMineState
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldStick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldRevertToWaitForStickOnSurfaceLost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00048A80 File Offset: 0x00046C80
		public override void OnEnter()
		{
			base.OnEnter();
			base.transform.Find(PreDetonate.pathToPrepForExplosionChildEffect).gameObject.SetActive(true);
			base.rigidbody.AddForce(base.transform.forward * PreDetonate.detachForce);
			base.rigidbody.AddTorque(UnityEngine.Random.onUnitSphere * 200f);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00048AE8 File Offset: 0x00046CE8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && PreDetonate.duration <= base.fixedAge)
			{
				this.outer.SetNextState(new Detonate());
			}
		}

		// Token: 0x040014F1 RID: 5361
		public static float duration;

		// Token: 0x040014F2 RID: 5362
		public static string pathToPrepForExplosionChildEffect;

		// Token: 0x040014F3 RID: 5363
		public static float detachForce;
	}
}
