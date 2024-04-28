using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D1C RID: 3356
	public class HudObjectiveTargetSetter : MonoBehaviour
	{
		// Token: 0x06004C7B RID: 19579 RVA: 0x0013BD84 File Offset: 0x00139F84
		private void OnEnable()
		{
			this.hud = base.GetComponentInParent<HUD>();
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0013BD92 File Offset: 0x00139F92
		private void Update()
		{
			if (this.hud && this.objectivePanelController)
			{
				this.objectivePanelController.SetCurrentMaster(this.hud.targetMaster);
			}
		}

		// Token: 0x04004981 RID: 18817
		public ObjectivePanelController objectivePanelController;

		// Token: 0x04004982 RID: 18818
		private HUD hud;
	}
}
