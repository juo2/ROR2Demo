using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CD0 RID: 3280
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(LayoutElement))]
	public class ColumnLayoutGroupElement : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x06004AC9 RID: 19145 RVA: 0x001334BE File Offset: 0x001316BE
		private void Awake()
		{
			this.rectTransform = (RectTransform)base.transform;
			this.layoutElement = base.GetComponent<LayoutElement>();
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x001334E0 File Offset: 0x001316E0
		public void OnBeginDrag(PointerEventData eventData)
		{
			ColumnLayoutGroupElement.ClickLocation clickLocation = ColumnLayoutGroupElement.ClickLocation.None;
			Vector2 vector;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, eventData.position, eventData.pressEventCamera, out vector))
			{
				Rect rect = this.rectTransform.rect;
				float width = rect.width;
				Vector2 vector2 = new Vector2(vector.x * rect.width, vector.y * rect.height);
				clickLocation = ColumnLayoutGroupElement.ClickLocation.Middle;
				if (vector2.x < this.handleWidth)
				{
					clickLocation = ColumnLayoutGroupElement.ClickLocation.LeftHandle;
				}
				if (vector2.x > width - this.handleWidth)
				{
					clickLocation = ColumnLayoutGroupElement.ClickLocation.RightHandle;
				}
			}
			this.lastClickLocation = clickLocation;
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0013356C File Offset: 0x0013176C
		public void OnDrag(PointerEventData eventData)
		{
			Transform parent = this.rectTransform.parent;
			int siblingIndex = this.rectTransform.GetSiblingIndex();
			if (this.lastClickLocation == ColumnLayoutGroupElement.ClickLocation.LeftHandle && siblingIndex != 0)
			{
				Transform child = parent.GetChild(siblingIndex - 1);
				this.<OnDrag>g__AdjustWidth|8_0((child != null) ? child.GetComponent<LayoutElement>() : null, this.layoutElement, eventData.delta.x);
			}
			if (this.lastClickLocation == ColumnLayoutGroupElement.ClickLocation.RightHandle && siblingIndex != parent.childCount - 1)
			{
				LayoutElement lhs = this.layoutElement;
				Transform child2 = parent.GetChild(siblingIndex + 1);
				this.<OnDrag>g__AdjustWidth|8_0(lhs, (child2 != null) ? child2.GetComponent<LayoutElement>() : null, eventData.delta.x);
			}
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x0013361C File Offset: 0x0013181C
		[CompilerGenerated]
		private void <OnDrag>g__AdjustWidth|8_0(LayoutElement lhs, LayoutElement rhs, float change)
		{
			if (!lhs || !rhs)
			{
				return;
			}
			if (lhs.preferredWidth + change < lhs.minWidth)
			{
				change = lhs.minWidth - lhs.preferredWidth;
			}
			if (rhs.preferredWidth - change < rhs.minWidth)
			{
				change = rhs.preferredWidth - rhs.minWidth;
			}
			if (change == 0f)
			{
				return;
			}
			lhs.preferredWidth += change;
			rhs.preferredWidth -= change;
			if (this.rectTransformToLayoutInvalidate)
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransformToLayoutInvalidate);
			}
		}

		// Token: 0x04004792 RID: 18322
		private RectTransform rectTransform;

		// Token: 0x04004793 RID: 18323
		private LayoutElement layoutElement;

		// Token: 0x04004794 RID: 18324
		public RectTransform rectTransformToLayoutInvalidate;

		// Token: 0x04004795 RID: 18325
		private float handleWidth = 4f;

		// Token: 0x04004796 RID: 18326
		private ColumnLayoutGroupElement.ClickLocation lastClickLocation;

		// Token: 0x02000CD1 RID: 3281
		private enum ClickLocation
		{
			// Token: 0x04004798 RID: 18328
			None,
			// Token: 0x04004799 RID: 18329
			Middle,
			// Token: 0x0400479A RID: 18330
			RightHandle,
			// Token: 0x0400479B RID: 18331
			LeftHandle
		}
	}
}
