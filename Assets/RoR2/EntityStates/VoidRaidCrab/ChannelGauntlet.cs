using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2;
using RoR2.HudOverlay;
using UnityEngine;
using UnityEngine.UI;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000113 RID: 275
	public class ChannelGauntlet : BaseState
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x00014B18 File Offset: 0x00012D18
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackRateParam, this.duration);
			Util.PlaySound(this.enterSoundString, base.gameObject);
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Player);
			this.overlayControllers = new List<OverlayController>();
			this.overlayFillImages = new HashSet<Image>();
			foreach (TeamComponent teamComponent in teamMembers)
			{
				OverlayController overlayController = HudOverlayManager.AddOverlay(teamComponent.gameObject, new OverlayCreationParams
				{
					prefab = this.overlayPrefab,
					childLocatorEntry = this.overlayChildLocatorEntryName
				});
				overlayController.onInstanceAdded += this.OnOverlayInstanceAdded;
				overlayController.onInstanceRemove += this.OnOverlayInstanceRemoved;
				this.overlayControllers.Add(overlayController);
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00014C08 File Offset: 0x00012E08
		private void OnOverlayInstanceAdded(OverlayController controller, GameObject instance)
		{
			if (instance)
			{
				ChildLocator component = instance.GetComponent<ChildLocator>();
				if (component)
				{
					Image image = component.FindChildComponent<Image>(this.fillImageChildLocatorEntryName);
					if (image != null)
					{
						this.overlayFillImages.Add(image);
					}
				}
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00014C50 File Offset: 0x00012E50
		private void OnOverlayInstanceRemoved(OverlayController controller, GameObject instance)
		{
			if (instance)
			{
				ChildLocator component = instance.GetComponent<ChildLocator>();
				if (component)
				{
					Image image = component.FindChildComponent<Image>(this.overlayChildLocatorEntryName);
					if (image != null)
					{
						this.overlayFillImages.Remove(image);
					}
				}
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00014C98 File Offset: 0x00012E98
		public override void OnExit()
		{
			this.overlayFillImages.Clear();
			foreach (OverlayController overlayController in this.overlayControllers)
			{
				HudOverlayManager.RemoveOverlay(overlayController);
				overlayController.onInstanceAdded -= this.OnOverlayInstanceAdded;
				overlayController.onInstanceRemove -= this.OnOverlayInstanceRemoved;
			}
			this.overlayControllers.Clear();
			base.OnExit();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00014D28 File Offset: 0x00012F28
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.duration > 0f)
			{
				float fillAmount = base.fixedAge / this.duration;
				foreach (Image image in this.overlayFillImages)
				{
					image.fillAmount = fillAmount;
				}
			}
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				VoidRaidGauntletController.instance;
				this.outer.SetNextState(new CloseGauntlet());
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x0400057C RID: 1404
		[SerializeField]
		public float duration;

		// Token: 0x0400057D RID: 1405
		[SerializeField]
		public string animationLayerName;

		// Token: 0x0400057E RID: 1406
		[SerializeField]
		public string animationStateName;

		// Token: 0x0400057F RID: 1407
		[SerializeField]
		public string animationPlaybackRateParam;

		// Token: 0x04000580 RID: 1408
		[SerializeField]
		public string enterSoundString;

		// Token: 0x04000581 RID: 1409
		[SerializeField]
		public GameObject overlayPrefab;

		// Token: 0x04000582 RID: 1410
		[SerializeField]
		public string overlayChildLocatorEntryName;

		// Token: 0x04000583 RID: 1411
		[SerializeField]
		public string fillImageChildLocatorEntryName;

		// Token: 0x04000584 RID: 1412
		private List<OverlayController> overlayControllers;

		// Token: 0x04000585 RID: 1413
		private HashSet<Image> overlayFillImages;
	}
}
