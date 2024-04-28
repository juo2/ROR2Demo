using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D92 RID: 3474
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class SteamBuildIdLabel : MonoBehaviour
	{
		// Token: 0x06004F87 RID: 20359 RVA: 0x001490C4 File Offset: 0x001472C4
		private void Start()
		{
			string text = "ver. " + RoR2Application.GetBuildId();
			if (!string.IsNullOrEmpty(""))
			{
				text += ".";
			}
			base.GetComponent<TextMeshProUGUI>().text = text;
		}
	}
}
