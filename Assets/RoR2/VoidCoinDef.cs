using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200056B RID: 1387
	[CreateAssetMenu(menuName = "RoR2/MiscPickupDefs/VoidCoinDef")]
	public class VoidCoinDef : MiscPickupDef
	{
		// Token: 0x06001918 RID: 6424 RVA: 0x0006C84C File Offset: 0x0006AA4C
		public override void GrantPickup(ref PickupDef.GrantContext context)
		{
			if (context.body.master)
			{
				context.body.master.GiveVoidCoins(this.coinValue);
				context.shouldDestroy = true;
				context.shouldNotify = true;
				return;
			}
			context.shouldNotify = false;
			context.shouldDestroy = false;
		}
	}
}
