using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClayBruiserMonster
{
	// Token: 0x02000404 RID: 1028
	public class SpawnState : BaseState
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x00052818 File Offset: 0x00050A18
		public override void OnEnter()
		{
			base.OnEnter();
			EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, SpawnState.spawnEffectChildString, false);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			PrintController printController = base.GetModelTransform().gameObject.AddComponent<PrintController>();
			printController.printTime = SpawnState.printDuration;
			printController.enabled = true;
			printController.startingPrintHeight = 0.3f;
			printController.maxPrintHeight = 0.3f;
			printController.startingPrintBias = SpawnState.startingPrintBias;
			printController.maxPrintBias = SpawnState.maxPrintBias;
			printController.disableWhenFinished = true;
			printController.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000528DF File Offset: 0x00050ADF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040017CB RID: 6091
		public static float duration;

		// Token: 0x040017CC RID: 6092
		public static string spawnSoundString;

		// Token: 0x040017CD RID: 6093
		public static GameObject spawnEffectPrefab;

		// Token: 0x040017CE RID: 6094
		public static string spawnEffectChildString;

		// Token: 0x040017CF RID: 6095
		public static float startingPrintBias;

		// Token: 0x040017D0 RID: 6096
		public static float maxPrintBias;

		// Token: 0x040017D1 RID: 6097
		public static float printDuration;
	}
}
