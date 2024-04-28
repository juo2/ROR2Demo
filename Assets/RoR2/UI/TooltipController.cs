using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D9D RID: 3485
	public class TooltipController : MonoBehaviour
	{
		// Token: 0x06004FCE RID: 20430 RVA: 0x0014A23C File Offset: 0x0014843C
		private void SetTooltipProvider(TooltipProvider provider)
		{
			this.titleLabel.text = provider.titleText;
			this.titleLabel.richText = !provider.disableTitleRichText;
			this.bodyLabel.text = provider.bodyText;
			this.bodyLabel.richText = !provider.disableBodyRichText;
			this.colorHighlightImage.color = provider.titleColor;
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x0014A2A4 File Offset: 0x001484A4
		private static UICamera FindUICamera(MPEventSystem mpEventSystem)
		{
			foreach (UICamera uicamera in UICamera.readOnlyInstancesList)
			{
				if (uicamera.GetAssociatedEventSystem() as MPEventSystem == mpEventSystem)
				{
					return uicamera;
				}
			}
			return null;
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0014A304 File Offset: 0x00148504
		private void Awake()
		{
			TooltipController.instancesList.Add(this);
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0014A311 File Offset: 0x00148511
		private void OnDestroy()
		{
			TooltipController.instancesList.Remove(this);
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0014A320 File Offset: 0x00148520
		private void LateUpdate()
		{
			Vector2 v;
			if (this.owner && this.owner.GetCursorPosition(out v))
			{
				this.tooltipCenterTransform.position = v;
			}
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0014A35C File Offset: 0x0014855C
		public static void RemoveTooltip(TooltipProvider tooltipProvider)
		{
			if (tooltipProvider.userCount > 0)
			{
				foreach (MPEventSystem eventSystem in MPEventSystem.readOnlyInstancesList)
				{
					TooltipController.RemoveTooltip(eventSystem, tooltipProvider);
				}
			}
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x0014A3B0 File Offset: 0x001485B0
		public static void RemoveTooltip(MPEventSystem eventSystem, TooltipProvider tooltipProvider)
		{
			if (eventSystem.currentTooltipProvider == tooltipProvider)
			{
				TooltipController.SetTooltip(eventSystem, null, Vector3.zero);
			}
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x0014A3D4 File Offset: 0x001485D4
		public static void SetTooltip(MPEventSystem eventSystem, TooltipProvider newTooltipProvider, Vector2 tooltipPosition)
		{
			if (eventSystem.currentTooltipProvider != newTooltipProvider)
			{
				if (eventSystem.currentTooltip)
				{
					UnityEngine.Object.Destroy(eventSystem.currentTooltip.gameObject);
					eventSystem.currentTooltip = null;
				}
				if (eventSystem.currentTooltipProvider)
				{
					eventSystem.currentTooltipProvider.userCount--;
				}
				eventSystem.currentTooltipProvider = newTooltipProvider;
				if (newTooltipProvider)
				{
					newTooltipProvider.userCount++;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Tooltip"));
					eventSystem.currentTooltip = gameObject.GetComponent<TooltipController>();
					eventSystem.currentTooltip.owner = eventSystem;
					eventSystem.currentTooltip.uiCamera = TooltipController.FindUICamera(eventSystem);
					eventSystem.currentTooltip.SetTooltipProvider(eventSystem.currentTooltipProvider);
					Canvas component = gameObject.GetComponent<Canvas>();
					UICamera uicamera = eventSystem.currentTooltip.uiCamera;
					component.worldCamera = ((uicamera != null) ? uicamera.camera : null);
				}
			}
			if (eventSystem.currentTooltip)
			{
				Vector2 zero = Vector2.zero;
				UICamera uicamera2 = eventSystem.currentTooltip.uiCamera;
				Camera camera = Camera.main;
				if (uicamera2)
				{
					camera = uicamera2.camera;
				}
				if (camera)
				{
					Vector3 vector = camera.ScreenToViewportPoint(new Vector3(tooltipPosition.x, tooltipPosition.y, 0f));
					zero = new Vector2(vector.x, vector.y);
				}
				Vector2 vector2 = new Vector2(0f, 0f);
				vector2.x = ((zero.x > 0.5f) ? 1f : 0f);
				vector2.y = ((zero.y > 0.5f) ? 1f : 0f);
				eventSystem.currentTooltip.tooltipFlipTransform.anchorMin = vector2;
				eventSystem.currentTooltip.tooltipFlipTransform.anchorMax = vector2;
				eventSystem.currentTooltip.tooltipFlipTransform.pivot = vector2;
			}
		}

		// Token: 0x04004C67 RID: 19559
		private static readonly List<TooltipController> instancesList = new List<TooltipController>();

		// Token: 0x04004C68 RID: 19560
		[NonSerialized]
		public MPEventSystem owner;

		// Token: 0x04004C69 RID: 19561
		public RectTransform tooltipCenterTransform;

		// Token: 0x04004C6A RID: 19562
		public RectTransform tooltipFlipTransform;

		// Token: 0x04004C6B RID: 19563
		public Image colorHighlightImage;

		// Token: 0x04004C6C RID: 19564
		public TextMeshProUGUI titleLabel;

		// Token: 0x04004C6D RID: 19565
		public TextMeshProUGUI bodyLabel;

		// Token: 0x04004C6E RID: 19566
		private UICamera uiCamera;
	}
}
