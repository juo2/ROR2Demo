using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClayGrenadier
{
	// Token: 0x020003FD RID: 1021
	public class SpawnState : BaseState
	{
		// Token: 0x06001257 RID: 4695 RVA: 0x00051E6C File Offset: 0x0005006C
		public override void OnEnter()
		{
			base.OnEnter();
			EffectManager.SimpleMuzzleFlash(this.spawnEffectPrefab, base.gameObject, this.spawnEffectChildString, false);
			Util.PlaySound(this.spawnSoundString, base.gameObject);
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.playbackRateParam, this.duration);
			PrintController printController = base.GetModelTransform().gameObject.AddComponent<PrintController>();
			printController.printTime = this.printDuration;
			printController.enabled = true;
			printController.startingPrintHeight = this.startingPrintHeight;
			printController.maxPrintHeight = this.maxPrintHeight;
			printController.startingPrintBias = this.startingPrintBias;
			printController.maxPrintBias = this.maxPrintBias;
			printController.disableWhenFinished = true;
			printController.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00051F3F File Offset: 0x0005013F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001793 RID: 6035
		[SerializeField]
		public float duration;

		// Token: 0x04001794 RID: 6036
		[SerializeField]
		public string spawnSoundString;

		// Token: 0x04001795 RID: 6037
		[SerializeField]
		public GameObject spawnEffectPrefab;

		// Token: 0x04001796 RID: 6038
		[SerializeField]
		public string spawnEffectChildString;

		// Token: 0x04001797 RID: 6039
		[SerializeField]
		public float startingPrintBias;

		// Token: 0x04001798 RID: 6040
		[SerializeField]
		public float maxPrintBias;

		// Token: 0x04001799 RID: 6041
		[SerializeField]
		public float printDuration;

		// Token: 0x0400179A RID: 6042
		[SerializeField]
		public float startingPrintHeight = 0.3f;

		// Token: 0x0400179B RID: 6043
		[SerializeField]
		public float maxPrintHeight = 0.3f;

		// Token: 0x0400179C RID: 6044
		[SerializeField]
		public string animationLayerName = "Body";

		// Token: 0x0400179D RID: 6045
		[SerializeField]
		public string animationStateName = "Spawn";

		// Token: 0x0400179E RID: 6046
		[SerializeField]
		public string playbackRateParam = "Spawn.playbackRate";
	}
}
