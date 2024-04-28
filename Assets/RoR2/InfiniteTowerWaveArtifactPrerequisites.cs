using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200053C RID: 1340
	[CreateAssetMenu(menuName = "RoR2/Infinite Tower/Infinite Tower Wave Artifact Prerequisites")]
	public class InfiniteTowerWaveArtifactPrerequisites : InfiniteTowerWavePrerequisites
	{
		// Token: 0x06001871 RID: 6257 RVA: 0x0006AADB File Offset: 0x00068CDB
		public override bool AreMet(InfiniteTowerRun run)
		{
			return !RunArtifactManager.instance.IsArtifactEnabled(this.bannedArtifact);
		}

		// Token: 0x04001E04 RID: 7684
		[Tooltip("This wave cannot be selected while this artifact is enabled.")]
		[SerializeField]
		private ArtifactDef bannedArtifact;
	}
}
