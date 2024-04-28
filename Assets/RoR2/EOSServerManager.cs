using System;
using System.Collections.Generic;

namespace RoR2
{
	// Token: 0x020009AD RID: 2477
	internal sealed class EOSServerManager : ServerManagerBase<EOSServerManager>, IDisposable
	{
		// Token: 0x060038A8 RID: 14504 RVA: 0x000EDA48 File Offset: 0x000EBC48
		public EOSServerManager()
		{
			Run.onServerRunSetRuleBookGlobal += base.OnServerRunSetRuleBookGlobal;
			PreGameController.onPreGameControllerSetRuleBookServerGlobal += base.OnPreGameControllerSetRuleBookServerGlobal;
			this.ruleBookKvHelper = new KeyValueSplitter("ruleBook", 2048, 2048, new Action<string, string>(this.SetKey));
			this.modListKvHelper = new KeyValueSplitter(NetworkModCompatibilityHelper.steamworksGameserverRulesBaseName, 2048, 2048, new Action<string, string>(this.SetKey));
			this.modListKvHelper.SetValue(NetworkModCompatibilityHelper.steamworksGameserverGameRulesValue);
			base.UpdateServerRuleBook();
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000EDAEA File Offset: 0x000EBCEA
		public void SetKey(string Key, string Value)
		{
			this.KeyValue[Key] = Value;
		}

		// Token: 0x0400386B RID: 14443
		private Dictionary<string, string> KeyValue = new Dictionary<string, string>();
	}
}
