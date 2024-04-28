using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Treebot
{
	// Token: 0x02000177 RID: 375
	public class BurrowOut : GenericCharacterMain
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x0001C420 File Offset: 0x0001A620
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = BurrowOut.baseDuration / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			Util.PlaySound(BurrowOut.burrowOutSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(BurrowOut.burrowPrefab, base.gameObject, "BurrowCenter", false);
			base.characterMotor.velocity = new Vector3(0f, BurrowOut.jumpVelocity, 0f);
			base.characterMotor.Motor.ForceUnground();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001C4B8 File Offset: 0x0001A6B8
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001C4C0 File Offset: 0x0001A6C0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000805 RID: 2053
		public static GameObject burrowPrefab;

		// Token: 0x04000806 RID: 2054
		public static float baseDuration;

		// Token: 0x04000807 RID: 2055
		public static string burrowOutSoundString;

		// Token: 0x04000808 RID: 2056
		public static float jumpVelocity;

		// Token: 0x04000809 RID: 2057
		private float stopwatch;

		// Token: 0x0400080A RID: 2058
		private Transform modelTransform;

		// Token: 0x0400080B RID: 2059
		private ChildLocator childLocator;

		// Token: 0x0400080C RID: 2060
		private float duration;
	}
}
