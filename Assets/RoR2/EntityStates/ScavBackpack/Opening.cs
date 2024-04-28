using System;
using EntityStates.Barrel;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ScavBackpack
{
	// Token: 0x020001D2 RID: 466
	public class Opening : EntityState
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x00023564 File Offset: 0x00021764
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Body", "Opening");
			this.timeBetweenDrops = Opening.duration / (float)Opening.maxItemDropCount;
			this.chestBehavior = base.GetComponent<ChestBehavior>();
			if (base.sfxLocator)
			{
				Util.PlaySound(base.sfxLocator.openSound, base.gameObject);
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000235CC File Offset: 0x000217CC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.itemDropAge += Time.fixedDeltaTime;
				if (this.itemDropCount < (float)Opening.maxItemDropCount && this.itemDropAge > this.timeBetweenDrops)
				{
					this.itemDropCount += 1f;
					this.itemDropAge -= this.timeBetweenDrops;
					this.chestBehavior.RollItem();
					this.chestBehavior.ItemDrop();
				}
				if (base.fixedAge >= Opening.duration)
				{
					this.outer.SetNextState(new Opened());
					return;
				}
			}
		}

		// Token: 0x040009CB RID: 2507
		public static float duration = 1f;

		// Token: 0x040009CC RID: 2508
		public static int maxItemDropCount;

		// Token: 0x040009CD RID: 2509
		private ChestBehavior chestBehavior;

		// Token: 0x040009CE RID: 2510
		private float itemDropCount;

		// Token: 0x040009CF RID: 2511
		private float timeBetweenDrops;

		// Token: 0x040009D0 RID: 2512
		private float itemDropAge;
	}
}
