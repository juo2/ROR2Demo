using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005D6 RID: 1494
	public class ArtifactEnabler : MonoBehaviour
	{
		// Token: 0x06001B19 RID: 6937 RVA: 0x000745D8 File Offset: 0x000727D8
		private void OnEnable()
		{
			if (NetworkServer.active && this.artifactDef)
			{
				this.artifactWasEnabled = RunArtifactManager.instance.IsArtifactEnabled(this.artifactDef);
				RunArtifactManager.instance.SetArtifactEnabledServer(this.artifactDef, true);
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00074615 File Offset: 0x00072815
		private void OnDisable()
		{
			if (NetworkServer.active && this.artifactDef && RunArtifactManager.instance)
			{
				RunArtifactManager.instance.SetArtifactEnabledServer(this.artifactDef, this.artifactWasEnabled);
			}
		}

		// Token: 0x0400213E RID: 8510
		[SerializeField]
		private ArtifactDef artifactDef;

		// Token: 0x0400213F RID: 8511
		private bool artifactWasEnabled;
	}
}
