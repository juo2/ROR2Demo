using System;
using EntityStates.VoidSurvivor.CorruptMode;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor
{
	// Token: 0x020000EA RID: 234
	public class EnterCorruptionTransition : CorruptionTransitionBase
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x000116FA File Offset: 0x0000F8FA
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.voidSurvivorController && NetworkServer.active)
			{
				this.voidSurvivorController.AddCorruption(100f);
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00011726 File Offset: 0x0000F926
		public override void OnFinishAuthority()
		{
			base.OnFinishAuthority();
			if (this.voidSurvivorController)
			{
				this.voidSurvivorController.corruptionModeStateMachine.SetNextState(new CorruptMode());
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00011750 File Offset: 0x0000F950
		public override void OnExit()
		{
			base.OnExit();
			if (NetworkServer.active)
			{
				base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, this.lingeringInvincibilityDuration);
			}
		}

		// Token: 0x04000446 RID: 1094
		[SerializeField]
		public float lingeringInvincibilityDuration;
	}
}
