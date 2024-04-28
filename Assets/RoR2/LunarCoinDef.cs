using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000550 RID: 1360
	[CreateAssetMenu(menuName = "RoR2/MiscPickupDefs/LunarCoinDef")]
	public class LunarCoinDef : MiscPickupDef
	{
		// Token: 0x060018B3 RID: 6323 RVA: 0x0006B910 File Offset: 0x00069B10
		public override string GetInternalName()
		{
			if (!string.IsNullOrEmpty(this.internalNameOverride))
			{
				return this.internalNameOverride;
			}
			return "MiscPickupIndex." + base.name;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0006B938 File Offset: 0x00069B38
		public override void GrantPickup(ref PickupDef.GrantContext context)
		{
			NetworkUser networkUser = Util.LookUpBodyNetworkUser(context.body);
			if (networkUser)
			{
				networkUser.AwardLunarCoins(this.coinValue);
				context.shouldDestroy = true;
				context.shouldNotify = true;
				return;
			}
			context.shouldNotify = false;
			context.shouldDestroy = false;
		}

		// Token: 0x04001E57 RID: 7767
		[SerializeField]
		public string internalNameOverride;
	}
}
