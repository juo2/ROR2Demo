using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RoR2.UI
{
	// Token: 0x02000D9F RID: 3487
	public class TooltipProvider : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06004FDF RID: 20447 RVA: 0x0014A7DD File Offset: 0x001489DD
		private bool tooltipIsAvailable
		{
			get
			{
				return this.titleColor != Color.clear;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06004FE0 RID: 20448 RVA: 0x0014A7EF File Offset: 0x001489EF
		public string titleText
		{
			get
			{
				if (!string.IsNullOrEmpty(this.overrideTitleText))
				{
					return this.overrideTitleText;
				}
				if (this.titleToken == null)
				{
					return null;
				}
				return Language.GetString(this.titleToken);
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06004FE1 RID: 20449 RVA: 0x0014A81A File Offset: 0x00148A1A
		public string bodyText
		{
			get
			{
				if (!string.IsNullOrEmpty(this.overrideBodyText))
				{
					return this.overrideBodyText;
				}
				if (this.bodyToken == null)
				{
					return null;
				}
				return Language.GetString(this.bodyToken);
			}
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0014A848 File Offset: 0x00148A48
		public void SetContent(TooltipContent tooltipContent)
		{
			this.titleToken = tooltipContent.titleToken;
			this.overrideTitleText = tooltipContent.overrideTitleText;
			this.titleColor = tooltipContent.titleColor;
			this.bodyToken = tooltipContent.bodyToken;
			this.overrideBodyText = tooltipContent.overrideBodyText;
			this.bodyColor = tooltipContent.bodyColor;
			this.disableTitleRichText = tooltipContent.disableTitleRichText;
			this.disableBodyRichText = tooltipContent.disableBodyRichText;
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0014A8B5 File Offset: 0x00148AB5
		private void OnDisable()
		{
			TooltipController.RemoveTooltip(this);
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0014A8C0 File Offset: 0x00148AC0
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			MPEventSystem mpeventSystem = EventSystem.current as MPEventSystem;
			if (mpeventSystem != null && this.tooltipIsAvailable)
			{
				TooltipController.SetTooltip(mpeventSystem, this, eventData.position);
			}
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0014A8F8 File Offset: 0x00148AF8
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			MPEventSystem mpeventSystem = EventSystem.current as MPEventSystem;
			if (mpeventSystem != null && this.tooltipIsAvailable)
			{
				TooltipController.RemoveTooltip(mpeventSystem, this);
			}
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0014A928 File Offset: 0x00148B28
		public static TooltipContent GetPlayerNameTooltipContent(string userName)
		{
			string stringFormatted = Language.GetStringFormatted("PLAYER_NAME_TOOLTIP_FORMAT", new object[]
			{
				userName
			});
			return new TooltipContent
			{
				overrideTitleText = stringFormatted,
				disableTitleRichText = true,
				titleColor = TooltipProvider.playerColor
			};
		}

		// Token: 0x04004C77 RID: 19575
		public string titleToken = "";

		// Token: 0x04004C78 RID: 19576
		public Color titleColor = Color.clear;

		// Token: 0x04004C79 RID: 19577
		public string bodyToken = "";

		// Token: 0x04004C7A RID: 19578
		public Color bodyColor;

		// Token: 0x04004C7B RID: 19579
		public string overrideTitleText = "";

		// Token: 0x04004C7C RID: 19580
		public string overrideBodyText = "";

		// Token: 0x04004C7D RID: 19581
		public bool disableTitleRichText;

		// Token: 0x04004C7E RID: 19582
		public bool disableBodyRichText;

		// Token: 0x04004C7F RID: 19583
		[NonSerialized]
		public int userCount;

		// Token: 0x04004C80 RID: 19584
		private static readonly Color playerColor = new Color32(242, 65, 65, byte.MaxValue);
	}
}
