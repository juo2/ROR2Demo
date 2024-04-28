using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006F8 RID: 1784
	public class GenericDisplayNameProvider : MonoBehaviour, IDisplayNameProvider
	{
		// Token: 0x06002458 RID: 9304 RVA: 0x0009B962 File Offset: 0x00099B62
		public string GetDisplayName()
		{
			return Language.GetString(this.displayToken);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0009B96F File Offset: 0x00099B6F
		public void SetDisplayToken(string newDisplayToken)
		{
			this.displayToken = newDisplayToken;
		}

		// Token: 0x040028A3 RID: 10403
		public string displayToken;
	}
}
