using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CF9 RID: 3321
	[RequireComponent(typeof(RectTransform))]
	public class DragResize : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x06004B9B RID: 19355 RVA: 0x00136B9F File Offset: 0x00134D9F
		private void Awake()
		{
			this.rectTransform = (RectTransform)base.transform;
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x00136BB2 File Offset: 0x00134DB2
		public void OnDrag(PointerEventData eventData)
		{
			this.UpdateDrag(eventData);
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x00136BBB File Offset: 0x00134DBB
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.targetTransform)
			{
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.targetTransform, eventData.position, eventData.pressEventCamera, out this.grabPoint);
			}
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x00136BE8 File Offset: 0x00134DE8
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.targetTransform)
			{
				return;
			}
			Vector2 a;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.targetTransform, eventData.position, eventData.pressEventCamera, out a);
			Vector2 vector = a - this.grabPoint;
			this.grabPoint = a;
			vector.y = -vector.y;
			Vector2 rhs = new Vector2(LayoutUtility.GetMinSize(this.targetTransform, 0), LayoutUtility.GetMinSize(this.targetTransform, 1));
			this.minSize = Vector2.Max(this.minSize, rhs);
			this.targetTransform.sizeDelta = Vector2.Max(this.targetTransform.sizeDelta + vector, this.minSize);
		}

		// Token: 0x04004857 RID: 18519
		public RectTransform targetTransform;

		// Token: 0x04004858 RID: 18520
		public Vector2 minSize;

		// Token: 0x04004859 RID: 18521
		private Vector2 grabPoint;

		// Token: 0x0400485A RID: 18522
		private RectTransform rectTransform;
	}
}
