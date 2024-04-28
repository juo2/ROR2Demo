using System;
using System.Collections.Generic;
using RoR2.HudOverlay;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000219 RID: 537
	public class Offline : BaseBackpack
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00026FE4 File Offset: 0x000251E4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			if (this.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
			OverlayCreationParams overlayCreationParams = new OverlayCreationParams
			{
				prefab = this.overlayPrefab,
				childLocatorEntry = this.overlayChildLocatorEntry
			};
			this.overlayController = HudOverlayManager.AddOverlay(base.gameObject, overlayCreationParams);
			this.overlayController.onInstanceAdded += this.OnOverlayInstanceAdded;
			this.overlayController.onInstanceRemove += this.OnOverlayInstanceRemoved;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x000270B4 File Offset: 0x000252B4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new Reboot());
			}
			if (this.duration > 0f)
			{
				foreach (ImageFillController imageFillController in this.fillUiList)
				{
					imageFillController.SetTValue(base.fixedAge / this.duration);
				}
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00027144 File Offset: 0x00025344
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

		// Token: 0x06000974 RID: 2420 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000271B4 File Offset: 0x000253B4
		private void OnOverlayInstanceAdded(OverlayController controller, GameObject instance)
		{
			this.fillUiList.Add(instance.GetComponent<ImageFillController>());
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000271C7 File Offset: 0x000253C7
		private void OnOverlayInstanceRemoved(OverlayController controller, GameObject instance)
		{
			this.fillUiList.Remove(instance.GetComponent<ImageFillController>());
		}

		// Token: 0x04000AFC RID: 2812
		[SerializeField]
		public float baseDuration;

		// Token: 0x04000AFD RID: 2813
		[SerializeField]
		public GameObject crosshairOverridePrefab;

		// Token: 0x04000AFE RID: 2814
		[SerializeField]
		public GameObject overlayPrefab;

		// Token: 0x04000AFF RID: 2815
		[SerializeField]
		public string overlayChildLocatorEntry;

		// Token: 0x04000B00 RID: 2816
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000B01 RID: 2817
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000B02 RID: 2818
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000B03 RID: 2819
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000B04 RID: 2820
		private OverlayController overlayController;

		// Token: 0x04000B05 RID: 2821
		private List<ImageFillController> fillUiList = new List<ImageFillController>();

		// Token: 0x04000B06 RID: 2822
		private float duration;
	}
}
