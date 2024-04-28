using System;
using System.Collections.Generic;
using System.Text;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C8E RID: 3214
	public static class EntitlementCatalog
	{
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x0012E79F File Offset: 0x0012C99F
		public static ReadOnlyArray<EntitlementDef> entitlementDefs
		{
			get
			{
				return EntitlementCatalog._entitlementDefs;
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x0012E7AB File Offset: 0x0012C9AB
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			EntitlementCatalog.SetEntitlementDefs(ContentManager.entitlementDefs);
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x0012E7B8 File Offset: 0x0012C9B8
		private static void SetEntitlementDefs(EntitlementDef[] newEntitlementDefs)
		{
			EntitlementCatalog._entitlementDefs = ArrayUtils.Clone<EntitlementDef>(newEntitlementDefs);
			EntitlementCatalog.nameToIndex.Clear();
			Array.Resize<string>(ref EntitlementCatalog.indexToName, newEntitlementDefs.Length);
			for (int i = 0; i < EntitlementCatalog.entitlementDefs.Length; i++)
			{
				EntitlementCatalog._entitlementDefs[i].entitlementIndex = (EntitlementIndex)i;
				string name = EntitlementCatalog._entitlementDefs[i].name;
				EntitlementCatalog.nameToIndex[name] = (EntitlementIndex)i;
				EntitlementCatalog.indexToName[i] = name;
			}
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x0012E830 File Offset: 0x0012CA30
		public static EntitlementIndex FindEntitlementIndex([NotNull] string entitlementName)
		{
			EntitlementIndex result;
			if (EntitlementCatalog.nameToIndex.TryGetValue(entitlementName, out result))
			{
				return result;
			}
			return EntitlementIndex.None;
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x0012E84F File Offset: 0x0012CA4F
		public static EntitlementDef GetEntitlementDef(EntitlementIndex entitlementIndex)
		{
			return ArrayUtils.GetSafe<EntitlementDef>(EntitlementCatalog._entitlementDefs, (int)entitlementIndex);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x0012E85C File Offset: 0x0012CA5C
		[ConCommand(commandName = "entitlements_list", flags = ConVarFlags.None, helpText = "Displays all registered entitlements.")]
		private static void CCEntitlementsList(ConCommandArgs args)
		{
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			for (int i = 0; i < EntitlementCatalog.indexToName.Length; i++)
			{
				stringBuilder.AppendLine(EntitlementCatalog.indexToName[i]);
			}
			args.Log(stringBuilder.ToString());
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
		}

		// Token: 0x04004642 RID: 17986
		private static EntitlementDef[] _entitlementDefs = Array.Empty<EntitlementDef>();

		// Token: 0x04004643 RID: 17987
		private static Dictionary<string, EntitlementIndex> nameToIndex = new Dictionary<string, EntitlementIndex>();

		// Token: 0x04004644 RID: 17988
		private static string[] indexToName = Array.Empty<string>();
	}
}
