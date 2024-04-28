using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2.UI
{
	// Token: 0x02000D06 RID: 3334
	public class HGHeaderNavigationController : MonoBehaviour
	{
		// Token: 0x06004BF7 RID: 19447 RVA: 0x00138F54 File Offset: 0x00137154
		private void Start()
		{
			this.RebuildHeaders();
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x00138F5C File Offset: 0x0013715C
		private void LateUpdate()
		{
			for (int i = 0; i < this.headers.Length; i++)
			{
				HGHeaderNavigationController.Header header = this.headers[i];
				if (i == this.currentHeaderIndex)
				{
					header.tmpHeaderText.color = this.selectedTextColor;
				}
				else
				{
					header.tmpHeaderText.color = this.unselectedTextColor;
				}
			}
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x00138FB8 File Offset: 0x001371B8
		public void ChooseHeader(string headerName)
		{
			for (int i = 0; i < this.headers.Length; i++)
			{
				if (this.headers[i].headerName == headerName)
				{
					this.currentHeaderIndex = i;
					this.RebuildHeaders();
					return;
				}
			}
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x00139000 File Offset: 0x00137200
		public void ChooseHeaderByButton(MPButton mpButton)
		{
			for (int i = 0; i < this.headers.Length; i++)
			{
				if (this.headers[i].headerButton == mpButton)
				{
					this.currentHeaderIndex = i;
					this.RebuildHeaders();
					return;
				}
			}
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x00139047 File Offset: 0x00137247
		public void MoveHeaderLeft()
		{
			this.currentHeaderIndex = Mathf.Max(0, this.currentHeaderIndex - 1);
			Util.PlaySound("Play_UI_menuClick", RoR2Application.instance.gameObject);
			this.RebuildHeaders();
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x00139078 File Offset: 0x00137278
		public void MoveHeaderRight()
		{
			this.currentHeaderIndex = Mathf.Min(this.headers.Length - 1, this.currentHeaderIndex + 1);
			Util.PlaySound("Play_UI_menuClick", RoR2Application.instance.gameObject);
			this.RebuildHeaders();
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x001390B4 File Offset: 0x001372B4
		private void RebuildHeaders()
		{
			for (int i = 0; i < this.headers.Length; i++)
			{
				HGHeaderNavigationController.Header header = this.headers[i];
				if (i == this.currentHeaderIndex)
				{
					if (header.headerRoot)
					{
						header.headerRoot.SetActive(true);
					}
					if (header.headerButton)
					{
						if (this.makeSelectedHeaderButtonNoninteractable)
						{
							header.headerButton.interactable = false;
						}
						if (this.headerHighlightObject)
						{
							this.headerHighlightObject.transform.SetParent(header.headerButton.transform, false);
							this.headerHighlightObject.SetActive(false);
							this.headerHighlightObject.SetActive(true);
							RectTransform component = this.headerHighlightObject.GetComponent<RectTransform>();
							component.offsetMin = Vector2.zero;
							component.offsetMax = Vector2.zero;
							component.localScale = Vector3.one;
						}
					}
				}
				else
				{
					if (header.headerButton && this.makeSelectedHeaderButtonNoninteractable)
					{
						header.headerButton.interactable = true;
					}
					if (header.headerRoot)
					{
						header.headerRoot.SetActive(false);
					}
				}
			}
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x001391DB File Offset: 0x001373DB
		private HGHeaderNavigationController.Header GetCurrentHeader()
		{
			return this.headers[this.currentHeaderIndex];
		}

		// Token: 0x040048CD RID: 18637
		[FormerlySerializedAs("buttonSelectionRoot")]
		[Header("Header Parameters")]
		public GameObject headerHighlightObject;

		// Token: 0x040048CE RID: 18638
		public int currentHeaderIndex;

		// Token: 0x040048CF RID: 18639
		public Color selectedTextColor = Color.white;

		// Token: 0x040048D0 RID: 18640
		public Color unselectedTextColor = Color.gray;

		// Token: 0x040048D1 RID: 18641
		public bool makeSelectedHeaderButtonNoninteractable;

		// Token: 0x040048D2 RID: 18642
		[Header("Header Infos")]
		public HGHeaderNavigationController.Header[] headers;

		// Token: 0x02000D07 RID: 3335
		[Serializable]
		public struct Header
		{
			// Token: 0x040048D3 RID: 18643
			public MPButton headerButton;

			// Token: 0x040048D4 RID: 18644
			public string headerName;

			// Token: 0x040048D5 RID: 18645
			public TextMeshProUGUI tmpHeaderText;

			// Token: 0x040048D6 RID: 18646
			public GameObject headerRoot;
		}
	}
}
