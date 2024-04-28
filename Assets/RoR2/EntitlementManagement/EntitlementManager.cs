using System;
using System.Collections.Generic;
using System.Text;
using HG;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C91 RID: 3217
	public static class EntitlementManager
	{
		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06004994 RID: 18836 RVA: 0x0012E964 File Offset: 0x0012CB64
		// (remove) Token: 0x06004995 RID: 18837 RVA: 0x0012E998 File Offset: 0x0012CB98
		private static event Action<Action<IUserEntitlementResolver<LocalUser>>> _collectLocalUserEntitlementResolvers;

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06004996 RID: 18838 RVA: 0x0012E9CB File Offset: 0x0012CBCB
		// (remove) Token: 0x06004997 RID: 18839 RVA: 0x0012E9E5 File Offset: 0x0012CBE5
		public static event Action<Action<IUserEntitlementResolver<LocalUser>>> collectLocalUserEntitlementResolvers
		{
			add
			{
				if (EntitlementManager.collectLocalUserEntitlementResolversLocked)
				{
					throw new InvalidOperationException("collectLocalUserEntitlementResolvers has already been invoked. It is too late to add additional subscribers.");
				}
				EntitlementManager._collectLocalUserEntitlementResolvers += value;
			}
			remove
			{
				EntitlementManager._collectLocalUserEntitlementResolvers -= value;
			}
		}

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06004998 RID: 18840 RVA: 0x0012E9F0 File Offset: 0x0012CBF0
		// (remove) Token: 0x06004999 RID: 18841 RVA: 0x0012EA24 File Offset: 0x0012CC24
		private static event Action<Action<IUserEntitlementResolver<NetworkUser>>> _collectNetworkUserEntitlementResolvers;

		// Token: 0x140000FE RID: 254
		// (add) Token: 0x0600499A RID: 18842 RVA: 0x0012EA57 File Offset: 0x0012CC57
		// (remove) Token: 0x0600499B RID: 18843 RVA: 0x0012EA71 File Offset: 0x0012CC71
		public static event Action<Action<IUserEntitlementResolver<NetworkUser>>> collectNetworkUserEntitlementResolvers
		{
			add
			{
				if (EntitlementManager.collectNetworkUserEntitlementResolversLocked)
				{
					throw new InvalidOperationException("collectNetworkUserEntitlementResolvers has already been invoked. It is too late to add additional subscribers.");
				}
				EntitlementManager._collectNetworkUserEntitlementResolvers += value;
			}
			remove
			{
				EntitlementManager._collectNetworkUserEntitlementResolvers -= value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600499C RID: 18844 RVA: 0x0012EA79 File Offset: 0x0012CC79
		// (set) Token: 0x0600499D RID: 18845 RVA: 0x0012EA80 File Offset: 0x0012CC80
		public static LocalUserEntitlementTracker localUserEntitlementTracker { get; private set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600499E RID: 18846 RVA: 0x0012EA88 File Offset: 0x0012CC88
		// (set) Token: 0x0600499F RID: 18847 RVA: 0x0012EA8F File Offset: 0x0012CC8F
		public static NetworkUserServerEntitlementTracker networkUserEntitlementTracker { get; private set; }

		// Token: 0x060049A0 RID: 18848 RVA: 0x0012EA98 File Offset: 0x0012CC98
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			List<IUserEntitlementResolver<LocalUser>> list = new List<IUserEntitlementResolver<LocalUser>>();
			EntitlementManager.collectLocalUserEntitlementResolversLocked = true;
			Action<Action<IUserEntitlementResolver<LocalUser>>> collectLocalUserEntitlementResolvers = EntitlementManager._collectLocalUserEntitlementResolvers;
			if (collectLocalUserEntitlementResolvers != null)
			{
				collectLocalUserEntitlementResolvers(new Action<IUserEntitlementResolver<LocalUser>>(list.Add));
			}
			EntitlementManager._collectLocalUserEntitlementResolvers = null;
			List<IUserEntitlementResolver<NetworkUser>> list2 = new List<IUserEntitlementResolver<NetworkUser>>();
			EntitlementManager.collectNetworkUserEntitlementResolversLocked = true;
			Action<Action<IUserEntitlementResolver<NetworkUser>>> collectNetworkUserEntitlementResolvers = EntitlementManager._collectNetworkUserEntitlementResolvers;
			if (collectNetworkUserEntitlementResolvers != null)
			{
				collectNetworkUserEntitlementResolvers(new Action<IUserEntitlementResolver<NetworkUser>>(list2.Add));
			}
			EntitlementManager._collectNetworkUserEntitlementResolvers = null;
			EntitlementManager.localUserEntitlementResolvers = list.ToArray();
			EntitlementManager.networkUserEntitlementResolvers = list2.ToArray();
			EntitlementManager.localUserEntitlementTracker = new LocalUserEntitlementTracker(EntitlementManager.localUserEntitlementResolvers);
			EntitlementManager.networkUserEntitlementTracker = new NetworkUserServerEntitlementTracker(EntitlementManager.networkUserEntitlementResolvers);
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x0012EB38 File Offset: 0x0012CD38
		[ConCommand(commandName = "entitlement_check_local", flags = ConVarFlags.None, helpText = "Displays the availability of all entitlements for the sender.")]
		private static void CCEntitlementCheckLocal(ConCommandArgs args)
		{
			LocalUser senderLocalUser = args.GetSenderLocalUser();
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			foreach (EntitlementDef entitlementDef in EntitlementCatalog.entitlementDefs)
			{
				stringBuilder.Append(entitlementDef.name).Append("=").Append(EntitlementManager.localUserEntitlementTracker.UserHasEntitlement(senderLocalUser, entitlementDef)).AppendLine();
			}
			args.Log(stringBuilder.ToString());
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x0012EBD8 File Offset: 0x0012CDD8
		[ConCommand(commandName = "entitlement_force_refresh", flags = ConVarFlags.None, helpText = "Forces the entitlement trackers to refresh.")]
		private static void CCEntitlementForceRefresh(ConCommandArgs args)
		{
			EntitlementManager.localUserEntitlementTracker.UpdateAllUserEntitlements();
			EntitlementManager.networkUserEntitlementTracker.UpdateAllUserEntitlements();
		}

		// Token: 0x04004649 RID: 17993
		private static IUserEntitlementResolver<LocalUser>[] localUserEntitlementResolvers = Array.Empty<IUserEntitlementResolver<LocalUser>>();

		// Token: 0x0400464A RID: 17994
		private static IUserEntitlementResolver<NetworkUser>[] networkUserEntitlementResolvers = Array.Empty<IUserEntitlementResolver<NetworkUser>>();

		// Token: 0x0400464B RID: 17995
		private static bool isLoaded = true;

		// Token: 0x0400464C RID: 17996
		private static bool collectLocalUserEntitlementResolversLocked = false;

		// Token: 0x0400464E RID: 17998
		private static bool collectNetworkUserEntitlementResolversLocked;
	}
}
