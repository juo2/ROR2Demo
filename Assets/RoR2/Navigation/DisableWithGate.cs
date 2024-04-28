using System;
using UnityEngine;

namespace RoR2.Navigation
{
	// Token: 0x02000B38 RID: 2872
	public class DisableWithGate : MonoBehaviour
	{
		// Token: 0x06004173 RID: 16755 RVA: 0x0010ED84 File Offset: 0x0010CF84
		private void Start()
		{
			if (SceneInfo.instance && SceneInfo.instance.groundNodes.IsGateOpen(this.gateToMatch) == this.invert)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x04003FC4 RID: 16324
		public string gateToMatch;

		// Token: 0x04003FC5 RID: 16325
		public bool invert;
	}
}
