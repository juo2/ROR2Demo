using System;
using System.Collections.Generic;
using RoR2.HudOverlay;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x0200020F RID: 527
	public abstract class BaseCharging : BaseBackpack
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x00026BB0 File Offset: 0x00024DB0
		public override void OnEnter()
		{
			this.isSoundScaledByAttackSpeed = true;
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
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

		// Token: 0x06000953 RID: 2387 RVA: 0x00026C84 File Offset: 0x00024E84
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
			base.OnExit();
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00026CF4 File Offset: 0x00024EF4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.duration > 0f)
			{
				foreach (ImageFillController imageFillController in this.fillUiList)
				{
					imageFillController.SetTValue(base.fixedAge / this.duration);
				}
			}
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(this.InstantiateChargedState());
			}
		}

		// Token: 0x06000955 RID: 2389
		protected abstract EntityState InstantiateChargedState();

		// Token: 0x06000956 RID: 2390 RVA: 0x00026D84 File Offset: 0x00024F84
		private void OnOverlayInstanceAdded(OverlayController controller, GameObject instance)
		{
			this.fillUiList.Add(instance.GetComponent<ImageFillController>());
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00026D97 File Offset: 0x00024F97
		private void OnOverlayInstanceRemoved(OverlayController controller, GameObject instance)
		{
			this.fillUiList.Remove(instance.GetComponent<ImageFillController>());
		}

		// Token: 0x04000AE3 RID: 2787
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000AE4 RID: 2788
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x04000AE5 RID: 2789
		[SerializeField]
		public GameObject overlayPrefab;

		// Token: 0x04000AE6 RID: 2790
		[SerializeField]
		public string overlayChildLocatorEntry;

		// Token: 0x04000AE7 RID: 2791
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000AE8 RID: 2792
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000AE9 RID: 2793
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000AEA RID: 2794
		private OverlayController overlayController;

		// Token: 0x04000AEB RID: 2795
		private List<ImageFillController> fillUiList = new List<ImageFillController>();

		// Token: 0x04000AEC RID: 2796
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000AED RID: 2797
		private float duration;
	}
}
