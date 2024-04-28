using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Assassin2
{
	// Token: 0x0200048B RID: 1163
	public class ThrowShuriken : GenericProjectileBaseState
	{
		// Token: 0x060014CD RID: 5325 RVA: 0x0005C6C0 File Offset: 0x0005A8C0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.PlayAnimation("Gesture", "ThrowShuriken");
			Util.PlaySound(ThrowShuriken.attackString, base.gameObject);
			base.GetAimRay();
			string text = "ShurikenTag";
			this.muzzleTransform = base.FindModelChild(text);
			if (this.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, text, false);
			}
			if (base.isAuthority)
			{
				this.FireProjectile();
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0005C74F File Offset: 0x0005A94F
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0005C757 File Offset: 0x0005A957
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001AA0 RID: 6816
		public static string attackString;

		// Token: 0x04001AA1 RID: 6817
		private Transform muzzleTransform;
	}
}
