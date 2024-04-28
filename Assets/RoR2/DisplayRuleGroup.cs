using System;
using HG;

namespace RoR2
{
	// Token: 0x0200054B RID: 1355
	[Serializable]
	public struct DisplayRuleGroup : IEquatable<DisplayRuleGroup>
	{
		// Token: 0x060018A8 RID: 6312 RVA: 0x0006B7D4 File Offset: 0x000699D4
		public void AddDisplayRule(ItemDisplayRule itemDisplayRule)
		{
			if (this.rules == null)
			{
				this.rules = Array.Empty<ItemDisplayRule>();
			}
			ArrayUtils.ArrayAppend<ItemDisplayRule>(ref this.rules, itemDisplayRule);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0006B7F6 File Offset: 0x000699F6
		public bool Equals(DisplayRuleGroup other)
		{
			return ArrayUtils.SequenceEquals<ItemDisplayRule>(this.rules, other.rules);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0006B80C File Offset: 0x00069A0C
		public override bool Equals(object obj)
		{
			if (obj is DisplayRuleGroup)
			{
				DisplayRuleGroup other = (DisplayRuleGroup)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0006B833 File Offset: 0x00069A33
		public override int GetHashCode()
		{
			if (this.rules == null)
			{
				return 0;
			}
			return this.rules.GetHashCode();
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0006B84A File Offset: 0x00069A4A
		public bool isEmpty
		{
			get
			{
				return this.rules == null || this.rules.Length == 0;
			}
		}

		// Token: 0x04001E45 RID: 7749
		public static readonly DisplayRuleGroup empty = new DisplayRuleGroup
		{
			rules = null
		};

		// Token: 0x04001E46 RID: 7750
		public ItemDisplayRule[] rules;
	}
}
