using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200053F RID: 1343
	[CreateAssetMenu(menuName = "RoR2/Infinite Tower/Infinite Tower Wave Count Prerequisites")]
	public class InfiniteTowerWaveCountPrerequisites : InfiniteTowerWavePrerequisites
	{
		// Token: 0x06001877 RID: 6263 RVA: 0x0006AC98 File Offset: 0x00068E98
		public override bool AreMet(InfiniteTowerRun run)
		{
			return run.waveIndex >= this.minimumWaveCount;
		}

		// Token: 0x04001E0C RID: 7692
		[SerializeField]
		private int minimumWaveCount;
	}
}
