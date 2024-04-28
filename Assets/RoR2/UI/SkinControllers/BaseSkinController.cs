using System;
using UnityEngine;

namespace RoR2.UI.SkinControllers
{
	// Token: 0x02000DBF RID: 3519
	[ExecuteAlways]
	public abstract class BaseSkinController : MonoBehaviour
	{
		// Token: 0x0600508B RID: 20619
		protected abstract void OnSkinUI();

		// Token: 0x0600508C RID: 20620 RVA: 0x0014D207 File Offset: 0x0014B407
		protected void Awake()
		{
			if (this.skinData)
			{
				this.DoSkinUI();
			}
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x0014D21C File Offset: 0x0014B41C
		private void DoSkinUI()
		{
			if (this.skinData)
			{
				this.OnSkinUI();
			}
		}

		// Token: 0x04004D20 RID: 19744
		public UISkinData skinData;
	}
}
