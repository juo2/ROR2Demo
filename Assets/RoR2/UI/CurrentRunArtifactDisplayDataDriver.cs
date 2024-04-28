using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CEB RID: 3307
	public class CurrentRunArtifactDisplayDataDriver : MonoBehaviour
	{
		// Token: 0x06004B54 RID: 19284 RVA: 0x0013571E File Offset: 0x0013391E
		private void OnEnable()
		{
			RunArtifactManager.onArtifactEnabledGlobal += this.OnArtifactEnabledGlobal;
			RunArtifactManager.onArtifactEnabledGlobal += this.OnArtifactDisabledGlobal;
			this.MarkDirty();
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x00135748 File Offset: 0x00133948
		private void OnDisable()
		{
			RunArtifactManager.onArtifactEnabledGlobal -= this.OnArtifactDisabledGlobal;
			RunArtifactManager.onArtifactEnabledGlobal -= this.OnArtifactEnabledGlobal;
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x0013576C File Offset: 0x0013396C
		private void OnArtifactEnabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			this.MarkDirty();
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x0013576C File Offset: 0x0013396C
		private void OnArtifactDisabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			this.MarkDirty();
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x00135774 File Offset: 0x00133974
		private void MarkDirty()
		{
			if (this.dirty)
			{
				return;
			}
			this.dirty = true;
			RoR2Application.onLateUpdate += this.Refresh;
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x00135798 File Offset: 0x00133998
		private void Refresh()
		{
			RunArtifactManager.RunEnabledArtifacts enumerator = RunArtifactManager.enabledArtifactsEnumerable.GetEnumerator();
			this.artifactDisplayPanelController.SetDisplayData<RunArtifactManager.RunEnabledArtifacts>(ref enumerator);
			this.dirty = false;
		}

		// Token: 0x04004800 RID: 18432
		public ArtifactDisplayPanelController artifactDisplayPanelController;

		// Token: 0x04004801 RID: 18433
		private bool dirty;
	}
}
