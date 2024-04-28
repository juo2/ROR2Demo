using System;
using RoR2;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000443 RID: 1091
	public class SpellChannelEnterState : SpellBaseState
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool DisplayWeapon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00056FA8 File Offset: 0x000551A8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "SpellChannelEnter", "SpellChannelEnter.playbackRate", SpellChannelEnterState.duration);
			Util.PlaySound("Play_moonBrother_phase4_transition", base.gameObject);
			this.trueDeathEffect = base.FindModelChild("TrueDeathEffect");
			if (this.trueDeathEffect)
			{
				this.trueDeathEffect.gameObject.SetActive(true);
				this.trueDeathEffect.GetComponent<ScaleParticleSystemDuration>().newDuration = 10f;
			}
			HurtBoxGroup component = base.GetModelTransform().GetComponent<HurtBoxGroup>();
			if (component)
			{
				HurtBoxGroup hurtBoxGroup = component;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0005704E File Offset: 0x0005524E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > SpellChannelEnterState.duration)
			{
				this.outer.SetNextState(new SpellChannelState());
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0005707C File Offset: 0x0005527C
		public override void OnExit()
		{
			HurtBoxGroup component = base.GetModelTransform().GetComponent<HurtBoxGroup>();
			if (component)
			{
				HurtBoxGroup hurtBoxGroup = component;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
			if (SpellChannelEnterState.channelBeginEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(SpellChannelEnterState.channelBeginEffectPrefab, base.gameObject, "SpellChannel", false);
			}
			if (this.trueDeathEffect)
			{
				this.trueDeathEffect.gameObject.SetActive(false);
			}
			base.OnExit();
		}

		// Token: 0x040018FA RID: 6394
		public static GameObject channelBeginEffectPrefab;

		// Token: 0x040018FB RID: 6395
		public static float duration;

		// Token: 0x040018FC RID: 6396
		private Transform trueDeathEffect;
	}
}
