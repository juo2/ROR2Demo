using System;
using System.Collections.Generic;
using RoR2;
using RoR2.HudOverlay;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x0200020E RID: 526
	public abstract class BaseCharged : BaseBackpack
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x00026940 File Offset: 0x00024B40
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			SkillLocator component = base.gameObject.GetComponent<SkillLocator>();
			if (component && component.primary)
			{
				this.primarySkill = component.primary;
				this.primarySkill.SetSkillOverride(this, this.primaryOverride, GenericSkill.SkillOverridePriority.Contextual);
			}
			OverlayCreationParams overlayCreationParams = new OverlayCreationParams
			{
				prefab = this.overlayPrefab,
				childLocatorEntry = this.overlayChildLocatorEntry
			};
			this.overlayController = HudOverlayManager.AddOverlay(base.gameObject, overlayCreationParams);
			this.overlayController.onInstanceAdded += this.OnOverlayInstanceAdded;
			this.overlayController.onInstanceRemove += this.OnOverlayInstanceRemoved;
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.PrioritySkill);
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00026A3C File Offset: 0x00024C3C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.duration > 0f)
			{
				foreach (ImageFillController imageFillController in this.fillUiList)
				{
					imageFillController.SetTValue(1f - base.fixedAge / this.duration);
				}
			}
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(this.InstantiateExpiredState());
			}
			base.characterBody.SetAimTimer(3f);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00026AE4 File Offset: 0x00024CE4
		public override void OnExit()
		{
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			if (this.overlayController != null)
			{
				this.overlayController.onInstanceAdded -= this.OnOverlayInstanceAdded;
				this.overlayController.onInstanceRemove -= this.OnOverlayInstanceRemoved;
				this.fillUiList.Clear();
				HudOverlayManager.RemoveOverlay(this.overlayController);
			}
			if (base.skillLocator)
			{
				this.primarySkill.UnsetSkillOverride(this, this.primaryOverride, GenericSkill.SkillOverridePriority.Contextual);
			}
			base.OnExit();
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x0600094E RID: 2382
		protected abstract EntityState InstantiateExpiredState();

		// Token: 0x0600094F RID: 2383 RVA: 0x00026B74 File Offset: 0x00024D74
		private void OnOverlayInstanceAdded(OverlayController controller, GameObject instance)
		{
			this.fillUiList.Add(instance.GetComponent<ImageFillController>());
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00026B87 File Offset: 0x00024D87
		private void OnOverlayInstanceRemoved(OverlayController controller, GameObject instance)
		{
			this.fillUiList.Remove(instance.GetComponent<ImageFillController>());
		}

		// Token: 0x04000AD7 RID: 2775
		[SerializeField]
		public float duration;

		// Token: 0x04000AD8 RID: 2776
		[SerializeField]
		public SkillDef primaryOverride;

		// Token: 0x04000AD9 RID: 2777
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x04000ADA RID: 2778
		[SerializeField]
		public GameObject overlayPrefab;

		// Token: 0x04000ADB RID: 2779
		[SerializeField]
		public string overlayChildLocatorEntry;

		// Token: 0x04000ADC RID: 2780
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000ADD RID: 2781
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000ADE RID: 2782
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000ADF RID: 2783
		private OverlayController overlayController;

		// Token: 0x04000AE0 RID: 2784
		private GenericSkill primarySkill;

		// Token: 0x04000AE1 RID: 2785
		private List<ImageFillController> fillUiList = new List<ImageFillController>();

		// Token: 0x04000AE2 RID: 2786
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
	}
}
