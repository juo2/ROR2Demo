using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008D3 RID: 2259
	public class ConsoleFunctions : MonoBehaviour
	{
		// Token: 0x06003297 RID: 12951 RVA: 0x000D5974 File Offset: 0x000D3B74
		public void SubmitCmd(string cmd)
		{
			Console.instance.SubmitCmd(null, cmd, false);
		}
	}
}
