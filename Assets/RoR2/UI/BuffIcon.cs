using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CC2 RID: 3266
	[RequireComponent(typeof(RectTransform))]
	public class BuffIcon : MonoBehaviour
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06004A74 RID: 19060 RVA: 0x00131959 File Offset: 0x0012FB59
		// (set) Token: 0x06004A75 RID: 19061 RVA: 0x00131961 File Offset: 0x0012FB61
		public RectTransform rectTransform { get; private set; }

		// Token: 0x06004A76 RID: 19062 RVA: 0x0013196A File Offset: 0x0012FB6A
		private void Awake()
		{
			this.rectTransform = base.GetComponent<RectTransform>();
			this.UpdateIcon();
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x0013197E File Offset: 0x0012FB7E
		public void Flash()
		{
			this.iconImage.color = Color.white;
			this.iconImage.CrossFadeColor(this.buffDef.buffColor, 0.25f, true, false);
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x001319B0 File Offset: 0x0012FBB0
		public void UpdateIcon()
		{
			if (!this.buffDef)
			{
				this.iconImage.sprite = null;
				return;
			}
			this.iconImage.sprite = this.buffDef.iconSprite;
			this.iconImage.color = this.buffDef.buffColor;
			if (this.buffDef.canStack)
			{
				BuffIcon.sharedStringBuilder.Clear();
				BuffIcon.sharedStringBuilder.Append("x");
				BuffIcon.sharedStringBuilder.AppendInt(this.buffCount, 1U, uint.MaxValue);
				this.stackCount.enabled = true;
				this.stackCount.SetText(BuffIcon.sharedStringBuilder);
				return;
			}
			this.stackCount.enabled = false;
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00131A6A File Offset: 0x0012FC6A
		private void Update()
		{
			if (this.lastBuffDef != this.buffDef)
			{
				this.lastBuffDef = this.buffDef;
			}
		}

		// Token: 0x04004725 RID: 18213
		private BuffDef lastBuffDef;

		// Token: 0x04004726 RID: 18214
		public BuffDef buffDef;

		// Token: 0x04004727 RID: 18215
		public Image iconImage;

		// Token: 0x04004728 RID: 18216
		public TextMeshProUGUI stackCount;

		// Token: 0x04004729 RID: 18217
		public int buffCount;

		// Token: 0x0400472A RID: 18218
		private float stopwatch;

		// Token: 0x0400472B RID: 18219
		private const float flashDuration = 0.25f;

		// Token: 0x0400472C RID: 18220
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();
	}
}
