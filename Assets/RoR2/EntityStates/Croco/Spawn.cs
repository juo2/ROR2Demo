using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Croco
{
	// Token: 0x020003E0 RID: 992
	public class Spawn : BaseState
	{
		// Token: 0x060011C4 RID: 4548 RVA: 0x0004E4D0 File Offset: 0x0004C6D0
		public override void OnEnter()
		{
			base.OnEnter();
			base.modelLocator.normalizeToFloor = true;
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				this.modelAnimator.SetFloat(AnimationParameters.aimWeight, 0f);
			}
			this.PlayAnimation("Body", "SleepLoop");
			EffectManager.SpawnEffect(Spawn.spawnEffectPrefab, new EffectData
			{
				origin = base.characterBody.footPosition
			}, false);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004E54E File Offset: 0x0004C74E
		public override void OnExit()
		{
			if (this.modelAnimator)
			{
				this.modelAnimator.SetFloat(AnimationParameters.aimWeight, 1f);
			}
			base.OnExit();
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0004E578 File Offset: 0x0004C778
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= Spawn.minimumSleepDuration && (base.inputBank.moveVector.sqrMagnitude >= Mathf.Epsilon || base.inputBank.CheckAnyButtonDown()))
			{
				this.outer.SetNextState(new WakeUp());
			}
		}

		// Token: 0x0400168D RID: 5773
		public static float minimumSleepDuration;

		// Token: 0x0400168E RID: 5774
		public static GameObject spawnEffectPrefab;

		// Token: 0x0400168F RID: 5775
		private Animator modelAnimator;
	}
}
