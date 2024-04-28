using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ParentMonster
{
	// Token: 0x0200022B RID: 555
	public class SpawnState : EntityState
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x000285A0 File Offset: 0x000267A0
		public override void OnEnter()
		{
			base.OnEnter();
			base.GetModelAnimator();
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			if (SpawnState.spawnEffectPrefab)
			{
				EffectData effectData = new EffectData
				{
					origin = base.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild("SpawnEffectOrigin").position
				};
				EffectManager.SpawnEffect(SpawnState.spawnEffectPrefab, effectData, true);
			}
			PrintController component = base.modelLocator.modelTransform.gameObject.GetComponent<PrintController>();
			component.enabled = false;
			component.printTime = SpawnState.duration;
			component.startingPrintHeight = 4f;
			component.maxPrintHeight = -1f;
			component.startingPrintBias = 2f;
			component.maxPrintBias = 0.95f;
			component.disableWhenFinished = true;
			component.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			component.enabled = true;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000286A7 File Offset: 0x000268A7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000B52 RID: 2898
		public static float duration = 2f;

		// Token: 0x04000B53 RID: 2899
		public static string spawnSoundString;

		// Token: 0x04000B54 RID: 2900
		public static GameObject spawnEffectPrefab;

		// Token: 0x04000B55 RID: 2901
		private ParentEnergyFXController FXController;
	}
}
