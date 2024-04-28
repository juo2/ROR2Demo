using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200099F RID: 2463
	[DefaultExecutionOrder(-5)]
	public class EOSLinkAccountButtonVisibilitycontroller : MonoBehaviour
	{
		// Token: 0x060037EB RID: 14315 RVA: 0x000EAE4C File Offset: 0x000E904C
		private void OnEnable()
		{
			if (this.egsLinkAccountButton)
			{
				if (PlatformSystems.ShouldUseEpicOnlineSystems && EOSLoginManager.loggedInAuthId == null)
				{
					this.egsLinkAccountButton.SetActive(true);
					return;
				}
				this.egsLinkAccountButton.SetActive(false);
			}
		}

		// Token: 0x04003809 RID: 14345
		public GameObject egsLinkAccountButton;
	}
}
