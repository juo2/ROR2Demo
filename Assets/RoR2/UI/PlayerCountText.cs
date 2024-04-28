using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D68 RID: 3432
	public class PlayerCountText : MonoBehaviour
	{
		// Token: 0x06004EB0 RID: 20144 RVA: 0x001451A8 File Offset: 0x001433A8
		private void Update()
		{
			if (this.targetText)
			{
				this.targetText.text = string.Format("{0}/{1}", NetworkUser.readOnlyInstancesList.Count, NetworkManager.singleton.maxConnections);
			}
		}

		// Token: 0x04004B63 RID: 19299
		public Text targetText;
	}
}
