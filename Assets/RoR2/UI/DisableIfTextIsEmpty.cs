using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CF5 RID: 3317
	public class DisableIfTextIsEmpty : MonoBehaviour
	{
		// Token: 0x06004B8C RID: 19340 RVA: 0x001368A0 File Offset: 0x00134AA0
		private void Update()
		{
			bool flag = !string.IsNullOrEmpty(this.tmpUGUI.text);
			if (flag != this.isActive)
			{
				this.isActive = flag;
				GameObject[] array = this.gameObjects;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(this.isActive);
				}
			}
		}

		// Token: 0x04004846 RID: 18502
		public GameObject[] gameObjects;

		// Token: 0x04004847 RID: 18503
		public TextMeshProUGUI tmpUGUI;

		// Token: 0x04004848 RID: 18504
		private bool isActive;
	}
}
