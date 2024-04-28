using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CEC RID: 3308
	public class CursorIndicatorController : MonoBehaviour
	{
		// Token: 0x06004B5B RID: 19291 RVA: 0x001357C8 File Offset: 0x001339C8
		public void SetCursor(CursorIndicatorController.CursorSet cursorSet, CursorIndicatorController.CursorImage cursorImage, Color color)
		{
			GameObject gameObject = cursorSet.GetGameObject(cursorImage);
			bool flag = color != this.cachedIndicatorColor;
			if (gameObject != this.currentChildIndicator)
			{
				if (this.currentChildIndicator)
				{
					this.currentChildIndicator.SetActive(false);
				}
				this.currentChildIndicator = gameObject;
				if (this.currentChildIndicator)
				{
					this.currentChildIndicator.SetActive(true);
				}
				flag = true;
			}
			if (flag)
			{
				this.cachedIndicatorColor = color;
				this.ApplyIndicatorColor();
			}
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00135844 File Offset: 0x00133A44
		private void ApplyIndicatorColor()
		{
			if (!this.currentChildIndicator)
			{
				return;
			}
			Image[] componentsInChildren = this.currentChildIndicator.GetComponentsInChildren<Image>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].color = this.cachedIndicatorColor;
			}
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x00135887 File Offset: 0x00133A87
		public void SetPosition(Vector2 position)
		{
			this.containerTransform.position = position;
		}

		// Token: 0x04004802 RID: 18434
		[NonSerialized]
		public CursorIndicatorController.CursorSet noneCursorSet;

		// Token: 0x04004803 RID: 18435
		public CursorIndicatorController.CursorSet mouseCursorSet;

		// Token: 0x04004804 RID: 18436
		public CursorIndicatorController.CursorSet gamepadCursorSet;

		// Token: 0x04004805 RID: 18437
		private GameObject currentChildIndicator;

		// Token: 0x04004806 RID: 18438
		public RectTransform containerTransform;

		// Token: 0x04004807 RID: 18439
		private Color cachedIndicatorColor = Color.clear;

		// Token: 0x02000CED RID: 3309
		public enum CursorImage
		{
			// Token: 0x04004809 RID: 18441
			None,
			// Token: 0x0400480A RID: 18442
			Pointer,
			// Token: 0x0400480B RID: 18443
			Hover
		}

		// Token: 0x02000CEE RID: 3310
		[Serializable]
		public struct CursorSet
		{
			// Token: 0x06004B5F RID: 19295 RVA: 0x001358AD File Offset: 0x00133AAD
			public GameObject GetGameObject(CursorIndicatorController.CursorImage cursorImage)
			{
				switch (cursorImage)
				{
				case CursorIndicatorController.CursorImage.None:
					return null;
				case CursorIndicatorController.CursorImage.Pointer:
					return this.pointerObject;
				case CursorIndicatorController.CursorImage.Hover:
					return this.hoverObject;
				default:
					return null;
				}
			}

			// Token: 0x0400480C RID: 18444
			public GameObject pointerObject;

			// Token: 0x0400480D RID: 18445
			public GameObject hoverObject;
		}
	}
}
