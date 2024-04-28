using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006BB RID: 1723
	public class EnableWithPhase : MonoBehaviour
	{
		// Token: 0x0600218A RID: 8586 RVA: 0x000907D0 File Offset: 0x0008E9D0
		private void FixedUpdate()
		{
			if (PhaseCounter.instance)
			{
				int phase = PhaseCounter.instance.phase;
				for (int i = 0; i < this.phaseInfos.Length; i++)
				{
					bool active = true;
					EnableWithPhase.PhaseInfo phaseInfo = this.phaseInfos[i];
					if (phaseInfo.minimumPhaseThreshold > phase || phaseInfo.maximumPhaseThreshold < phase)
					{
						active = false;
					}
					GameObject[] gameObjectsToEnable = phaseInfo.gameObjectsToEnable;
					for (int j = 0; j < gameObjectsToEnable.Length; j++)
					{
						gameObjectsToEnable[j].SetActive(active);
					}
				}
			}
		}

		// Token: 0x040026F6 RID: 9974
		public EnableWithPhase.PhaseInfo[] phaseInfos;

		// Token: 0x020006BC RID: 1724
		[Serializable]
		public struct PhaseInfo
		{
			// Token: 0x040026F7 RID: 9975
			public int minimumPhaseThreshold;

			// Token: 0x040026F8 RID: 9976
			public int maximumPhaseThreshold;

			// Token: 0x040026F9 RID: 9977
			public GameObject[] gameObjectsToEnable;
		}
	}
}
