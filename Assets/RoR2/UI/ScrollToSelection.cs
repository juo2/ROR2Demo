using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D7D RID: 3453
	[RequireComponent(typeof(MPEventSystemLocator))]
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollToSelection : MonoBehaviour
	{
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06004F2D RID: 20269 RVA: 0x00147B1A File Offset: 0x00145D1A
		private EventSystem eventSystem
		{
			get
			{
				return this.eventSystemLocator.eventSystem;
			}
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x00147B27 File Offset: 0x00145D27
		private void Awake()
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x00147B44 File Offset: 0x00145D44
		private void Update()
		{
			GameObject gameObject = this.eventSystem ? this.eventSystem.currentSelectedGameObject : null;
			if (this.lastSelectedObject != gameObject)
			{
				if (gameObject && gameObject.transform.IsChildOf(base.transform))
				{
					this.ScrollToRect((RectTransform)gameObject.transform);
				}
				this.lastSelectedObject = gameObject;
			}
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x00147BB0 File Offset: 0x00145DB0
		private void ScrollToRect(RectTransform targetRectTransform)
		{
			targetRectTransform.GetWorldCorners(this.targetWorldCorners);
			((RectTransform)base.transform).GetWorldCorners(this.viewPortWorldCorners);
			if (this.scrollRect.vertical && this.scrollRect.verticalScrollbar)
			{
				float y = this.targetWorldCorners[1].y;
				float y2 = this.targetWorldCorners[0].y;
				float y3 = this.viewPortWorldCorners[1].y;
				float y4 = this.viewPortWorldCorners[0].y;
				float num = y - y3;
				float num2 = y2 - y4;
				float num3 = y3 - y4;
				if (num > 0f)
				{
					this.scrollRect.verticalScrollbar.value += num / num3;
				}
				if (num2 < 0f)
				{
					this.scrollRect.verticalScrollbar.value += num2 / num3;
				}
			}
			if (this.scrollRect.horizontal && this.scrollRect.horizontalScrollbar)
			{
				float y5 = this.targetWorldCorners[2].y;
				float y6 = this.targetWorldCorners[0].y;
				float y7 = this.viewPortWorldCorners[2].y;
				float y8 = this.viewPortWorldCorners[0].y;
				float num4 = y5 - y7;
				float num5 = y6 - y8;
				float num6 = y7 - y8;
				if (num4 > 0f)
				{
					this.scrollRect.horizontalScrollbar.value += num4 / num6;
				}
				if (num5 < 0f)
				{
					this.scrollRect.horizontalScrollbar.value += num5 / num6;
				}
			}
		}

		// Token: 0x04004BE3 RID: 19427
		private ScrollRect scrollRect;

		// Token: 0x04004BE4 RID: 19428
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004BE5 RID: 19429
		private Vector3[] targetWorldCorners = new Vector3[4];

		// Token: 0x04004BE6 RID: 19430
		private Vector3[] viewPortWorldCorners = new Vector3[4];

		// Token: 0x04004BE7 RID: 19431
		private GameObject lastSelectedObject;
	}
}
