using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000846 RID: 2118
	public class RenderSettingsController : MonoBehaviour
	{
		// Token: 0x06002E25 RID: 11813 RVA: 0x000C4B3E File Offset: 0x000C2D3E
		[ContextMenu("Copy from current render settings")]
		public void FromCurrent()
		{
			this.renderSettingsState = RenderSettingsState.FromCurrent();
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000C4B4B File Offset: 0x000C2D4B
		[ContextMenu("Apply as current render settings")]
		public void Apply()
		{
			this.renderSettingsState.Apply();
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000C4B58 File Offset: 0x000C2D58
		private void LateUpdate()
		{
			this.Apply();
		}

		// Token: 0x0400302A RID: 12330
		public RenderSettingsState renderSettingsState;
	}
}
