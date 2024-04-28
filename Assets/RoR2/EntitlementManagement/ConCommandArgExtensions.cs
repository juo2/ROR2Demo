using System;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C8F RID: 3215
	public static class ConCommandArgExtensions
	{
		// Token: 0x06004990 RID: 18832 RVA: 0x0012E8C4 File Offset: 0x0012CAC4
		public static EntitlementIndex? TryGetArgEntitlementIndex(this ConCommandArgs args, int index)
		{
			string text = args.TryGetArgString(index);
			if (text != null)
			{
				EntitlementIndex entitlementIndex = EntitlementCatalog.FindEntitlementIndex(text);
				if (entitlementIndex != EntitlementIndex.None || text.Equals("None", StringComparison.Ordinal))
				{
					return new EntitlementIndex?(entitlementIndex);
				}
			}
			return null;
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x0012E908 File Offset: 0x0012CB08
		public static EntitlementIndex GetArgEntitlementIndex(this ConCommandArgs args, int index)
		{
			EntitlementIndex? entitlementIndex = args.TryGetArgEntitlementIndex(index);
			if (entitlementIndex == null)
			{
				throw new ConCommandException("No EntitlementIndex is defined for an entitlement named '" + args.TryGetArgString(index) + "'. Use the \"entitlement_list\" command to get a list of all valid entitlements.");
			}
			return entitlementIndex.GetValueOrDefault();
		}
	}
}
