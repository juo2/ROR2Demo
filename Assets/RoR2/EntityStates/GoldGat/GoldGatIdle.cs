using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GoldGat
{
	// Token: 0x0200036F RID: 879
	public class GoldGatIdle : BaseGoldGatState
	{
		// Token: 0x06000FCF RID: 4047 RVA: 0x000460A9 File Offset: 0x000442A9
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(GoldGatIdle.windDownSoundString, base.gameObject);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000460C4 File Offset: 0x000442C4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.gunAnimator)
			{
				this.gunAnimator.SetFloat("Crank.playbackRate", 0f, 1f, Time.fixedDeltaTime);
			}
			if (base.isAuthority && this.shouldFire && this.bodyMaster.money > 0U && this.bodyEquipmentSlot.stock > 0)
			{
				this.outer.SetNextState(new GoldGatFire
				{
					shouldFire = this.shouldFire
				});
				return;
			}
		}

		// Token: 0x0400143C RID: 5180
		public static string windDownSoundString;
	}
}
