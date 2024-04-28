using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LunarWisp
{
	// Token: 0x020002AF RID: 687
	public class SpawnState : BaseState
	{
		// Token: 0x06000C30 RID: 3120 RVA: 0x000336AC File Offset: 0x000318AC
		public override void OnEnter()
		{
			base.OnEnter();
			this.FXController = base.characterBody.GetComponent<LunarWispFXController>();
			this.FXController.TurnOffFX();
			this.mPrintDuration = SpawnState.printDuration;
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			this.PlayAnimation("Body", "Spawn");
			PrintController component = base.GetModelTransform().gameObject.GetComponent<PrintController>();
			component.enabled = false;
			component.printTime = this.mPrintDuration;
			component.startingPrintHeight = 25f;
			component.maxPrintHeight = 3.15f;
			component.startingPrintBias = 4f;
			component.maxPrintBias = 1.4f;
			component.disableWhenFinished = true;
			component.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			component.enabled = true;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00033784 File Offset: 0x00031984
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.spawnEffectsDelay && !this.spawnEffectsTriggered)
			{
				this.spawnEffectsTriggered = true;
				EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, SpawnState.spawnEffectMuzzleName, false);
				if (this.FXController)
				{
					this.FXController.TurnOnFX();
				}
			}
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000EDE RID: 3806
		public static float duration;

		// Token: 0x04000EDF RID: 3807
		public static string spawnSoundString;

		// Token: 0x04000EE0 RID: 3808
		public static float spawnEffectsDelay;

		// Token: 0x04000EE1 RID: 3809
		private bool spawnEffectsTriggered;

		// Token: 0x04000EE2 RID: 3810
		public static GameObject spawnEffectPrefab;

		// Token: 0x04000EE3 RID: 3811
		public static string spawnEffectMuzzleName;

		// Token: 0x04000EE4 RID: 3812
		public static float printDuration;

		// Token: 0x04000EE5 RID: 3813
		private float mPrintDuration;

		// Token: 0x04000EE6 RID: 3814
		private LunarWispFXController FXController;
	}
}
