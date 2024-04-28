using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CB1 RID: 3249
	[RequireComponent(typeof(LayoutElement))]
	[RequireComponent(typeof(RectTransform))]
	public class AllyCardController : MonoBehaviour
	{
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x001305D7 File Offset: 0x0012E7D7
		// (set) Token: 0x06004A1F RID: 18975 RVA: 0x001305DF File Offset: 0x0012E7DF
		public bool shouldIndent { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06004A20 RID: 18976 RVA: 0x001305E8 File Offset: 0x0012E7E8
		// (set) Token: 0x06004A21 RID: 18977 RVA: 0x001305F0 File Offset: 0x0012E7F0
		public CharacterMaster sourceMaster { get; set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06004A22 RID: 18978 RVA: 0x001305F9 File Offset: 0x0012E7F9
		// (set) Token: 0x06004A23 RID: 18979 RVA: 0x00130601 File Offset: 0x0012E801
		public RectTransform rectTransform { get; private set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06004A24 RID: 18980 RVA: 0x0013060A File Offset: 0x0012E80A
		// (set) Token: 0x06004A25 RID: 18981 RVA: 0x00130612 File Offset: 0x0012E812
		public LayoutElement layoutElement { get; private set; }

		// Token: 0x06004A26 RID: 18982 RVA: 0x0013061B File Offset: 0x0012E81B
		private void Awake()
		{
			this.rectTransform = (RectTransform)base.transform;
			this.layoutElement = base.GetComponent<LayoutElement>();
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x0013063A File Offset: 0x0012E83A
		private void LateUpdate()
		{
			this.UpdateInfo();
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x00130644 File Offset: 0x0012E844
		private void UpdateInfo()
		{
			HealthComponent source = null;
			string text = "";
			Texture texture = null;
			if (this.sourceMaster)
			{
				CharacterBody body = this.sourceMaster.GetBody();
				if (body)
				{
					texture = body.portraitIcon;
					source = body.healthComponent;
					text = Util.GetBestBodyName(body.gameObject);
				}
				else
				{
					text = Util.GetBestMasterName(this.sourceMaster);
				}
			}
			this.healthBar.source = source;
			this.nameLabel.text = text;
			this.portraitIconImage.texture = texture;
			this.portraitIconImage.enabled = (texture != null);
		}

		// Token: 0x040046DB RID: 18139
		public HealthBar healthBar;

		// Token: 0x040046DC RID: 18140
		public TextMeshProUGUI nameLabel;

		// Token: 0x040046DD RID: 18141
		public RawImage portraitIconImage;
	}
}
