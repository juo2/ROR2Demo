using System;
using RoR2;
using RoR2.HudOverlay;
using UnityEngine;

namespace EntityStates.Railgunner.Reload
{
	// Token: 0x0200020A RID: 522
	public class Boosted : BaseState
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x00026048 File Offset: 0x00024248
		public float GetBonusDamage()
		{
			return this.bonusDamageCoefficient * this.damageStat;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00026057 File Offset: 0x00024257
		public void ConsumeBoost(bool queueReload)
		{
			Util.PlaySound(this.boostConsumeSoundString, base.gameObject);
			this.outer.SetNextState(new Waiting(queueReload));
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0002607C File Offset: 0x0002427C
		public override void OnEnter()
		{
			base.OnEnter();
			OverlayCreationParams overlayCreationParams = new OverlayCreationParams
			{
				prefab = this.overlayPrefab,
				childLocatorEntry = this.overlayChildLocatorEntry
			};
			this.overlayController = HudOverlayManager.AddOverlay(base.gameObject, overlayCreationParams);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000260C5 File Offset: 0x000242C5
		public override void OnExit()
		{
			HudOverlayManager.RemoveOverlay(this.overlayController);
			base.OnExit();
		}

		// Token: 0x04000AB2 RID: 2738
		[SerializeField]
		public float bonusDamageCoefficient;

		// Token: 0x04000AB3 RID: 2739
		[SerializeField]
		public string boostConsumeSoundString;

		// Token: 0x04000AB4 RID: 2740
		[SerializeField]
		public GameObject overlayPrefab;

		// Token: 0x04000AB5 RID: 2741
		[SerializeField]
		public string overlayChildLocatorEntry;

		// Token: 0x04000AB6 RID: 2742
		private OverlayController overlayController;
	}
}
