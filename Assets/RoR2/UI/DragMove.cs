using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RoR2.UI
{
	// Token: 0x02000CF8 RID: 3320
	[RequireComponent(typeof(RectTransform))]
	public class DragMove : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler
	{
		// Token: 0x06004B96 RID: 19350 RVA: 0x00136ADE File Offset: 0x00134CDE
		private void OnAwake()
		{
			this.rectTransform = (RectTransform)base.transform;
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x00136AF1 File Offset: 0x00134CF1
		public void OnDrag(PointerEventData eventData)
		{
			this.UpdateDrag(eventData);
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x00136AFA File Offset: 0x00134CFA
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.targetTransform)
			{
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.targetTransform, eventData.position, eventData.pressEventCamera, out this.grabPoint);
			}
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x00136B28 File Offset: 0x00134D28
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
			this.targetTransform.localPosition += new Vector3(vector.x, vector.y, 0f);
		}

		// Token: 0x04004854 RID: 18516
		public RectTransform targetTransform;

		// Token: 0x04004855 RID: 18517
		private Vector2 grabPoint;

		// Token: 0x04004856 RID: 18518
		private RectTransform rectTransform;
	}
}
