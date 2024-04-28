using System;
using RoR2;
using UnityEngine;

namespace EntityStates.SurvivorPod.BatteryPanel
{
	// Token: 0x020001BC RID: 444
	public class BaseBatteryPanelState : BaseState
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x00021DEF File Offset: 0x0001FFEF
		public override void OnEnter()
		{
			base.OnEnter();
			VehicleSeat componentInParent = base.gameObject.GetComponentInParent<VehicleSeat>();
			this.SetPodObject((componentInParent != null) ? componentInParent.gameObject : null);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00021E14 File Offset: 0x00020014
		private void SetPodObject(GameObject podObject)
		{
			this.podInfo = default(BaseBatteryPanelState.PodInfo);
			if (!podObject)
			{
				return;
			}
			this.podInfo.podObject = podObject;
			ModelLocator component = podObject.GetComponent<ModelLocator>();
			if (component)
			{
				Transform modelTransform = component.modelTransform;
				if (modelTransform)
				{
					this.podInfo.podAnimator = modelTransform.GetComponent<Animator>();
				}
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00021E71 File Offset: 0x00020071
		protected void PlayPodAnimation(string layerName, string animationStateName, string playbackRateParam, float duration)
		{
			if (this.podInfo.podAnimator)
			{
				EntityState.PlayAnimationOnAnimator(this.podInfo.podAnimator, layerName, animationStateName, playbackRateParam, duration);
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00021E9A File Offset: 0x0002009A
		protected void PlayPodAnimation(string layerName, string animationStateName)
		{
			if (this.podInfo.podAnimator)
			{
				EntityState.PlayAnimationOnAnimator(this.podInfo.podAnimator, layerName, animationStateName);
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00021EC0 File Offset: 0x000200C0
		protected void EnablePickup()
		{
			ChildLocator component = this.podInfo.podAnimator.GetComponent<ChildLocator>();
			if (!component)
			{
				Debug.Log("Could not find pod child locator.");
				return;
			}
			Transform transform = component.FindChild("BatteryAttachmentPoint");
			if (!transform)
			{
				Debug.Log("Could not find battery attachment point.");
				return;
			}
			Transform transform2 = transform.Find("QuestVolatileBatteryWorldPickup(Clone)");
			if (!transform2)
			{
				Debug.Log("Could not find battery transform");
				return;
			}
			GenericPickupController component2 = transform2.GetComponent<GenericPickupController>();
			if (component2)
			{
				component2.enabled = true;
				return;
			}
			Debug.Log("Could not find pickup controller.");
		}

		// Token: 0x04000971 RID: 2417
		protected BaseBatteryPanelState.PodInfo podInfo;

		// Token: 0x020001BD RID: 445
		protected struct PodInfo
		{
			// Token: 0x04000972 RID: 2418
			public GameObject podObject;

			// Token: 0x04000973 RID: 2419
			public Animator podAnimator;
		}
	}
}
