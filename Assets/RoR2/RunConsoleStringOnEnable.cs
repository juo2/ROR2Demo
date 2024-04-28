using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000865 RID: 2149
	public class RunConsoleStringOnEnable : MonoBehaviour
	{
		// Token: 0x06002F15 RID: 12053 RVA: 0x000C8DE1 File Offset: 0x000C6FE1
		private void OnEnable()
		{
			Console.instance.SubmitCmd(null, this.consoleString, false);
		}

		// Token: 0x040030FE RID: 12542
		public string consoleString;
	}
}
