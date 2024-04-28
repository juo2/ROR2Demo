using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006AB RID: 1707
	public class DisableIfGameModded : MonoBehaviour
	{
		// Token: 0x06002139 RID: 8505 RVA: 0x0008EC3E File Offset: 0x0008CE3E
		public void OnEnable()
		{
			if (RoR2Application.isModded)
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
