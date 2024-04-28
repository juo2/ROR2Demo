using System;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B39 RID: 2873
	public class GateStateSetter : MonoBehaviour
	{
		// Token: 0x06004175 RID: 16757 RVA: 0x0010EDBB File Offset: 0x0010CFBB
		private void OnEnable()
		{
			this.UpdateGates(true);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x0010EDC4 File Offset: 0x0010CFC4
		private void OnDisable()
		{
			this.UpdateGates(false);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x0010EDD0 File Offset: 0x0010CFD0
		private void UpdateGates(bool enabledState)
		{
			if (!SceneInfo.instance)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.gateToEnableWhenEnabled))
			{
				SceneInfo.instance.SetGateState(this.gateToEnableWhenEnabled, enabledState);
			}
			if (!string.IsNullOrEmpty(this.gateToDisableWhenEnabled))
			{
				SceneInfo.instance.SetGateState(this.gateToDisableWhenEnabled, !enabledState);
			}
		}

		// Token: 0x04003FC6 RID: 16326
		public string gateToEnableWhenEnabled;

		// Token: 0x04003FC7 RID: 16327
		public string gateToDisableWhenEnabled;
	}
}
