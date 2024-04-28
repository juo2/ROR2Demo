using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ParentPod
{
	// Token: 0x02000222 RID: 546
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x000279CF File Offset: 0x00025BCF
		public override void OnEnter()
		{
			base.OnEnter();
			this.mDeathAnimTimer = DeathState.deathAnimTimer;
			EffectManager.SimpleEffect(DeathState.deathEffect, base.gameObject.transform.position, base.transform.rotation, false);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00027A08 File Offset: 0x00025C08
		public override void FixedUpdate()
		{
			this.mDeathAnimTimer -= Time.deltaTime;
			if (this.mDeathAnimTimer <= 0f && !this.printingStarted)
			{
				this.printingStarted = true;
				PrintController printController = base.GetComponent<ModelLocator>().modelTransform.gameObject.AddComponent<PrintController>();
				printController.enabled = false;
				printController.printTime = 1f;
				printController.startingPrintHeight = 99999f;
				printController.maxPrintHeight = 99999f;
				printController.startingPrintBias = 0.95f;
				printController.maxPrintBias = 1.95f;
				printController.animateFlowmapPower = true;
				printController.startingFlowmapPower = 1.14f;
				printController.maxFlowmapPower = 30f;
				printController.disableWhenFinished = false;
				printController.printCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
				printController.enabled = true;
			}
			if (this.printingStarted)
			{
				base.FixedUpdate();
			}
		}

		// Token: 0x04000B2B RID: 2859
		public static float deathAnimTimer;

		// Token: 0x04000B2C RID: 2860
		private float mDeathAnimTimer;

		// Token: 0x04000B2D RID: 2861
		public static GameObject deathEffect;

		// Token: 0x04000B2E RID: 2862
		private bool printingStarted;
	}
}
