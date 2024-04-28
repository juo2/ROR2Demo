using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D0A RID: 3338
	public class HGTextMeshProUGUI : TextMeshProUGUI
	{
		// Token: 0x06004C0C RID: 19468 RVA: 0x00139598 File Offset: 0x00137798
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			Language.onCurrentLanguageChanged += HGTextMeshProUGUI.OnCurrentLanguageChanged;
			HGTextMeshProUGUI.OnCurrentLanguageChanged();
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x001395B0 File Offset: 0x001377B0
		private static void OnCurrentLanguageChanged()
		{
			HGTextMeshProUGUI.defaultLanguageFont = LegacyResourcesAPI.Load<TMP_FontAsset>(Language.GetString("DEFAULT_FONT"));
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x001395C6 File Offset: 0x001377C6
		protected override void Awake()
		{
			base.Awake();
			if (this.useLanguageDefaultFont)
			{
				base.font = HGTextMeshProUGUI.defaultLanguageFont;
				base.UpdateFontAsset();
			}
		}

		// Token: 0x040048E8 RID: 18664
		public bool useLanguageDefaultFont = true;

		// Token: 0x040048E9 RID: 18665
		public static TMP_FontAsset defaultLanguageFont;
	}
}
