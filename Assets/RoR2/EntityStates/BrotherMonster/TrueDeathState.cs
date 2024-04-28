using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000434 RID: 1076
	public class TrueDeathState : GenericCharacterDeath
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00055D89 File Offset: 0x00053F89
		protected override bool shouldAutoDestroy
		{
			get
			{
				return base.fixedAge > TrueDeathState.durationBeforeDissolving + TrueDeathState.dissolveDuration + 1f;
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00055DA4 File Offset: 0x00053FA4
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				Util.CleanseBody(base.characterBody, true, true, true, true, false, false);
				ReturnStolenItemsOnGettingHit component = base.GetComponent<ReturnStolenItemsOnGettingHit>();
				if (component && component.itemStealController)
				{
					EntityState.Destroy(component.itemStealController.gameObject);
				}
			}
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00055DFB File Offset: 0x00053FFB
		protected override void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			this.PlayAnimation("FullBody Override", "TrueDeath");
			base.characterDirection.moveVector = base.characterDirection.forward;
			EffectManager.SimpleMuzzleFlash(TrueDeathState.deathEffectPrefab, base.gameObject, "MuzzleCenter", false);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00055E39 File Offset: 0x00054039
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= TrueDeathState.durationBeforeDissolving)
			{
				this.Dissolve();
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00055E54 File Offset: 0x00054054
		private void Dissolve()
		{
			if (this.dissolving)
			{
				return;
			}
			this.dissolving = true;
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				modelTransform.gameObject.GetComponent<CharacterModel>();
				PrintController component = base.modelLocator.modelTransform.gameObject.GetComponent<PrintController>();
				component.enabled = false;
				component.printTime = TrueDeathState.dissolveDuration;
				component.enabled = true;
			}
			Transform transform = base.FindModelChild("TrueDeathEffect");
			if (transform)
			{
				transform.gameObject.SetActive(true);
				transform.GetComponent<ScaleParticleSystemDuration>().newDuration = TrueDeathState.dissolveDuration;
			}
		}

		// Token: 0x040018BB RID: 6331
		public static float durationBeforeDissolving;

		// Token: 0x040018BC RID: 6332
		public static float dissolveDuration;

		// Token: 0x040018BD RID: 6333
		public static GameObject deathEffectPrefab;

		// Token: 0x040018BE RID: 6334
		private bool dissolving;
	}
}
