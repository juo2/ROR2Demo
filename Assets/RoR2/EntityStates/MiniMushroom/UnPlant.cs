using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MiniMushroom
{
	// Token: 0x02000273 RID: 627
	public class UnPlant : BaseState
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x0002CCFC File Offset: 0x0002AEFC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = UnPlant.baseDuration / this.attackSpeedStat;
			EffectManager.SimpleMuzzleFlash(UnPlant.plantEffectPrefab, base.gameObject, "BurrowCenter", false);
			Util.PlaySound(UnPlant.UnplantOutSoundString, base.gameObject);
			base.PlayAnimation("Plant", "PlantEnd", "PlantEnd.playbackRate", this.duration);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002CD63 File Offset: 0x0002AF63
		public override void OnExit()
		{
			this.PlayAnimation("Plant, Additive", "Empty");
			base.OnExit();
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002CD7B File Offset: 0x0002AF7B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000C8D RID: 3213
		public static GameObject plantEffectPrefab;

		// Token: 0x04000C8E RID: 3214
		public static float baseDuration;

		// Token: 0x04000C8F RID: 3215
		public static string UnplantOutSoundString;

		// Token: 0x04000C90 RID: 3216
		private float stopwatch;

		// Token: 0x04000C91 RID: 3217
		private Transform modelTransform;

		// Token: 0x04000C92 RID: 3218
		private ChildLocator childLocator;

		// Token: 0x04000C93 RID: 3219
		private float duration;
	}
}
