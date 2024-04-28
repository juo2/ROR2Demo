using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MiniMushroom
{
	// Token: 0x0200026F RID: 623
	public class InPlant : BaseState
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002C61C File Offset: 0x0002A81C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = InPlant.baseDuration / this.attackSpeedStat;
			Util.PlaySound(InPlant.burrowInSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(InPlant.burrowPrefab, base.gameObject, "BurrowCenter", false);
			base.PlayAnimation("Plant", "PlantStart", "PlantStart.playbackRate", this.duration);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002C683 File Offset: 0x0002A883
		public override void OnExit()
		{
			this.PlayAnimation("Plant", "Empty");
			base.OnExit();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002C69B File Offset: 0x0002A89B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new Plant());
				return;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000C67 RID: 3175
		public static GameObject burrowPrefab;

		// Token: 0x04000C68 RID: 3176
		public static float baseDuration;

		// Token: 0x04000C69 RID: 3177
		public static string burrowInSoundString;

		// Token: 0x04000C6A RID: 3178
		private float duration;
	}
}
