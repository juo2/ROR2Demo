using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D09 RID: 3337
	[RequireComponent(typeof(ScrollRect))]
	[RequireComponent(typeof(MPEventSystemLocator))]
	public class HGScrollRectHelper : MonoBehaviour
	{
		// Token: 0x06004C04 RID: 19460 RVA: 0x00139279 File Offset: 0x00137479
		private void Start()
		{
			this.Initialize();
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00139284 File Offset: 0x00137484
		private void Initialize()
		{
			if (this.hasInitialized)
			{
				return;
			}
			this.hasInitialized = true;
			this.scrollRect = base.GetComponent<ScrollRect>();
			this.contentRectTransform = this.scrollRect.content.GetComponent<RectTransform>();
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.idealVerticalNormalizedPosition = this.scrollRect.verticalNormalizedPosition;
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x001392E0 File Offset: 0x001374E0
		private bool GamepadIsCurrentInputSource()
		{
			return this.hasInitialized && this.eventSystemLocator.eventSystem && this.eventSystemLocator.eventSystem.currentInputSource == MPEventSystem.InputSource.Gamepad;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x00139313 File Offset: 0x00137513
		private bool CanAcceptInput()
		{
			return !this.requiredTopLayer || this.requiredTopLayer.representsTopLayer;
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x00139330 File Offset: 0x00137530
		private void Update()
		{
			this.Initialize();
			if (!this.GamepadIsCurrentInputSource())
			{
				return;
			}
			if (this.eventSystemLocator && this.eventSystemLocator.eventSystem && this.CanAcceptInput())
			{
				float height = this.scrollRect.content.rect.height;
				float axis = this.eventSystemLocator.eventSystem.player.GetAxis(13);
				this.eventSystemLocator.eventSystem.player.GetAxis(12);
				if (this.allowVerticalScrollingWithGamepadSticks)
				{
					this.idealVerticalNormalizedPosition = Mathf.Clamp01(this.idealVerticalNormalizedPosition + axis * this.stickScale * Time.unscaledDeltaTime / height);
					this.scrollRect.verticalNormalizedPosition = this.idealVerticalNormalizedPosition;
				}
			}
			this.scrollRect.verticalNormalizedPosition = Mathf.SmoothDamp(this.scrollRect.verticalNormalizedPosition, this.idealVerticalNormalizedPosition, ref this.scrollbarVelocity, 0.1f, float.PositiveInfinity, Time.unscaledDeltaTime);
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x00139438 File Offset: 0x00137638
		public void ScrollToShowMe(MPButton mpButton)
		{
			this.Initialize();
			if (!this.GamepadIsCurrentInputSource() || !this.CanAcceptInput())
			{
				return;
			}
			Canvas.ForceUpdateCanvases();
			RectTransform content = this.scrollRect.content;
			RectTransform component = mpButton.GetComponent<RectTransform>();
			float num = -this.scrollRect.viewport.rect.size.y / 2f;
			float num2 = -this.scrollRect.content.rect.size.y;
			float num3 = 0f;
			if (component.anchoredPosition.y - this.spacingPerElement > num3 + num)
			{
				this.idealVerticalNormalizedPosition = 1f;
				return;
			}
			if (component.anchoredPosition.y - this.spacingPerElement > num)
			{
				return;
			}
			if (component.anchoredPosition.y - this.spacingPerElement < num2 - num)
			{
				this.idealVerticalNormalizedPosition = 0f;
				return;
			}
			float num4 = Mathf.InverseLerp(num, num2 - num, component.anchoredPosition.y - this.spacingPerElement);
			float num5 = 1f - num4;
			this.idealVerticalNormalizedPosition = num5;
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0013954C File Offset: 0x0013774C
		private static Rect GetWorldspaceRect(Vector3 pos, Rect rect, Vector2 offset)
		{
			float x = pos.x + rect.xMin - offset.x;
			float y = pos.y + rect.yMin - offset.y;
			return new Rect(new Vector2(x, y), rect.size);
		}

		// Token: 0x040048DD RID: 18653
		public float spacingPerElement;

		// Token: 0x040048DE RID: 18654
		public bool allowVerticalScrollingWithGamepadSticks;

		// Token: 0x040048DF RID: 18655
		public float stickScale;

		// Token: 0x040048E0 RID: 18656
		public UILayerKey requiredTopLayer;

		// Token: 0x040048E1 RID: 18657
		private ScrollRect scrollRect;

		// Token: 0x040048E2 RID: 18658
		private RectTransform contentRectTransform;

		// Token: 0x040048E3 RID: 18659
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x040048E4 RID: 18660
		private float idealVerticalNormalizedPosition;

		// Token: 0x040048E5 RID: 18661
		private float scrollbarVelocity;

		// Token: 0x040048E6 RID: 18662
		private const float scrollbarSmoothdampTime = 0.1f;

		// Token: 0x040048E7 RID: 18663
		private bool hasInitialized;
	}
}
