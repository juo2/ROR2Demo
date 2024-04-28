using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D9E RID: 3486
	[Serializable]
	public struct TooltipContent : IEquatable<TooltipContent>
	{
		// Token: 0x06004FD8 RID: 20440 RVA: 0x0014A5C4 File Offset: 0x001487C4
		public bool Equals(TooltipContent other)
		{
			return string.Equals(this.titleToken, other.titleToken) && string.Equals(this.overrideTitleText, other.overrideTitleText) && this.titleColor.Equals(other.titleColor) && string.Equals(this.bodyToken, other.bodyToken) && string.Equals(this.overrideBodyText, other.overrideBodyText) && this.bodyColor.Equals(other.bodyColor) && this.disableTitleRichText == other.disableTitleRichText && this.disableBodyRichText == other.disableBodyRichText;
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0014A664 File Offset: 0x00148864
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is TooltipContent)
			{
				TooltipContent other = (TooltipContent)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0014A690 File Offset: 0x00148890
		public override int GetHashCode()
		{
			return ((((((((this.titleToken != null) ? this.titleToken.GetHashCode() : 0) * 397 ^ ((this.overrideTitleText != null) ? this.overrideTitleText.GetHashCode() : 0)) * 397 ^ this.titleColor.GetHashCode()) * 397 ^ ((this.bodyToken != null) ? this.bodyToken.GetHashCode() : 0)) * 397 ^ ((this.overrideBodyText != null) ? this.overrideBodyText.GetHashCode() : 0)) * 397 ^ this.bodyColor.GetHashCode()) * 397 ^ this.disableTitleRichText.GetHashCode()) * 397 ^ this.disableBodyRichText.GetHashCode();
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0014A75E File Offset: 0x0014895E
		public static bool operator ==(TooltipContent left, TooltipContent right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0014A768 File Offset: 0x00148968
		public static bool operator !=(TooltipContent left, TooltipContent right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0014A775 File Offset: 0x00148975
		[NotNull]
		public string GetTitleText()
		{
			if (!string.IsNullOrEmpty(this.overrideTitleText))
			{
				return this.overrideTitleText;
			}
			if (!string.IsNullOrEmpty(this.titleToken))
			{
				return Language.GetString(this.titleToken);
			}
			return string.Empty;
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0014A7A9 File Offset: 0x001489A9
		[NotNull]
		public string GetBodyText()
		{
			if (!string.IsNullOrEmpty(this.overrideBodyText))
			{
				return this.overrideBodyText;
			}
			if (!string.IsNullOrEmpty(this.bodyToken))
			{
				return Language.GetString(this.bodyToken);
			}
			return string.Empty;
		}

		// Token: 0x04004C6F RID: 19567
		public string titleToken;

		// Token: 0x04004C70 RID: 19568
		public string overrideTitleText;

		// Token: 0x04004C71 RID: 19569
		public Color titleColor;

		// Token: 0x04004C72 RID: 19570
		public string bodyToken;

		// Token: 0x04004C73 RID: 19571
		public string overrideBodyText;

		// Token: 0x04004C74 RID: 19572
		public Color bodyColor;

		// Token: 0x04004C75 RID: 19573
		public bool disableTitleRichText;

		// Token: 0x04004C76 RID: 19574
		public bool disableBodyRichText;
	}
}
