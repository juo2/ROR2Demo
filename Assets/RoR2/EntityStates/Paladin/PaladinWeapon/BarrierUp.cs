using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Paladin.PaladinWeapon
{
	// Token: 0x0200022F RID: 559
	public class BarrierUp : BaseState
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x00028F8C File Offset: 0x0002718C
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(BarrierUp.soundEffectString, base.gameObject);
			this.stopwatch = 0f;
			this.paladinBarrierController = base.GetComponent<PaladinBarrierController>();
			if (this.paladinBarrierController)
			{
				this.paladinBarrierController.EnableBarrier();
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00028FDF File Offset: 0x000271DF
		public override void OnExit()
		{
			base.OnExit();
			if (this.paladinBarrierController)
			{
				this.paladinBarrierController.DisableBarrier();
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00029000 File Offset: 0x00027200
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= 0.1f && !base.inputBank.skill2.down && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000B7D RID: 2941
		public static float duration = 5f;

		// Token: 0x04000B7E RID: 2942
		public static string soundEffectString;

		// Token: 0x04000B7F RID: 2943
		private float stopwatch;

		// Token: 0x04000B80 RID: 2944
		private PaladinBarrierController paladinBarrierController;
	}
}
