using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200054A RID: 1354
	[Serializable]
	public struct ItemDisplayRule : IEquatable<ItemDisplayRule>
	{
		// Token: 0x060018A5 RID: 6309 RVA: 0x0006B678 File Offset: 0x00069878
		public bool Equals(ItemDisplayRule other)
		{
			return this.ruleType == other.ruleType && object.Equals(this.followerPrefab, other.followerPrefab) && string.Equals(this.childName, other.childName) && this.localPos.Equals(other.localPos) && this.localAngles.Equals(other.localAngles) && this.localScale.Equals(other.localScale) && this.limbMask == other.limbMask;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0006B704 File Offset: 0x00069904
		public override bool Equals(object obj)
		{
			if (obj is ItemDisplayRule)
			{
				ItemDisplayRule other = (ItemDisplayRule)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0006B72C File Offset: 0x0006992C
		public override int GetHashCode()
		{
			return (int)((((((this.ruleType * (ItemDisplayRuleType)397 ^ (ItemDisplayRuleType)((this.followerPrefab != null) ? this.followerPrefab.GetHashCode() : 0)) * (ItemDisplayRuleType)397 ^ (ItemDisplayRuleType)((this.childName != null) ? this.childName.GetHashCode() : 0)) * (ItemDisplayRuleType)397 ^ (ItemDisplayRuleType)this.localPos.GetHashCode()) * (ItemDisplayRuleType)397 ^ (ItemDisplayRuleType)this.localAngles.GetHashCode()) * (ItemDisplayRuleType)397 ^ (ItemDisplayRuleType)this.localScale.GetHashCode()) * (ItemDisplayRuleType)397 ^ (ItemDisplayRuleType)this.limbMask);
		}

		// Token: 0x04001E3E RID: 7742
		public ItemDisplayRuleType ruleType;

		// Token: 0x04001E3F RID: 7743
		public GameObject followerPrefab;

		// Token: 0x04001E40 RID: 7744
		public string childName;

		// Token: 0x04001E41 RID: 7745
		public Vector3 localPos;

		// Token: 0x04001E42 RID: 7746
		public Vector3 localAngles;

		// Token: 0x04001E43 RID: 7747
		public Vector3 localScale;

		// Token: 0x04001E44 RID: 7748
		public LimbFlags limbMask;
	}
}
