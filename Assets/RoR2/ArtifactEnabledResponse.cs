using System;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x020005D5 RID: 1493
	public class ArtifactEnabledResponse : MonoBehaviour
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000744FF File Offset: 0x000726FF
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x00074507 File Offset: 0x00072707
		private bool active
		{
			get
			{
				return this._active;
			}
			set
			{
				if (this._active == value)
				{
					return;
				}
				this._active = value;
				if (this.active)
				{
					UnityEvent unityEvent = this.onDiscoveredArtifactEnabled;
					if (unityEvent == null)
					{
						return;
					}
					unityEvent.Invoke();
					return;
				}
				else
				{
					UnityEvent unityEvent2 = this.onLostArtifactEnabled;
					if (unityEvent2 == null)
					{
						return;
					}
					unityEvent2.Invoke();
					return;
				}
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00074543 File Offset: 0x00072743
		private void OnEnable()
		{
			RunArtifactManager.onArtifactEnabledGlobal += this.OnArtifactEnabledGlobal;
			RunArtifactManager.onArtifactDisabledGlobal += this.OnArtifactDisabledGlobal;
			this.active = RunArtifactManager.instance.IsArtifactEnabled(this.artifact);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0007457D File Offset: 0x0007277D
		private void OnDisable()
		{
			this.active = false;
			RunArtifactManager.onArtifactDisabledGlobal -= this.OnArtifactDisabledGlobal;
			RunArtifactManager.onArtifactEnabledGlobal -= this.OnArtifactEnabledGlobal;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000745A8 File Offset: 0x000727A8
		private void OnArtifactEnabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != this.artifact)
			{
				return;
			}
			this.active = true;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000745C0 File Offset: 0x000727C0
		private void OnArtifactDisabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != this.artifact)
			{
				return;
			}
			this.active = false;
		}

		// Token: 0x0400213A RID: 8506
		public ArtifactDef artifact;

		// Token: 0x0400213B RID: 8507
		public UnityEvent onDiscoveredArtifactEnabled;

		// Token: 0x0400213C RID: 8508
		public UnityEvent onLostArtifactEnabled;

		// Token: 0x0400213D RID: 8509
		private bool _active;
	}
}
