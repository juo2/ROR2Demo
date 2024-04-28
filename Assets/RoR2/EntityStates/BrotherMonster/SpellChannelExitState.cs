using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000446 RID: 1094
	public class SpellChannelExitState : SpellBaseState
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool DisplayWeapon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00057368 File Offset: 0x00055568
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "SpellChannelExit", "SpellChannelExit.playbackRate", SpellChannelExitState.duration);
			if (NetworkServer.active && this.itemStealController)
			{
				this.itemStealController.stealInterval = SpellChannelExitState.lendInterval;
				this.itemStealController.LendImmediately(base.characterBody.inventory);
			}
			if (SpellChannelExitState.channelFinishEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(SpellChannelExitState.channelFinishEffectPrefab, base.gameObject, "SpellChannel", false);
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000573F1 File Offset: 0x000555F1
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > SpellChannelExitState.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00057414 File Offset: 0x00055614
		public override void OnExit()
		{
			SetStateOnHurt component = base.GetComponent<SetStateOnHurt>();
			if (component)
			{
				component.canBeFrozen = true;
			}
			base.OnExit();
		}

		// Token: 0x04001906 RID: 6406
		public static float lendInterval;

		// Token: 0x04001907 RID: 6407
		public static float duration;

		// Token: 0x04001908 RID: 6408
		public static GameObject channelFinishEffectPrefab;
	}
}
