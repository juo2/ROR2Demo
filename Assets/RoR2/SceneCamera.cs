using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace RoR2
{
	// Token: 0x0200086E RID: 2158
	[RequireComponent(typeof(Camera))]
	public class SceneCamera : MonoBehaviour
	{
		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06002F38 RID: 12088 RVA: 0x000C948C File Offset: 0x000C768C
		// (remove) Token: 0x06002F39 RID: 12089 RVA: 0x000C94C0 File Offset: 0x000C76C0
		public static event SceneCamera.SceneCameraDelegate onSceneCameraPreCull;

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06002F3A RID: 12090 RVA: 0x000C94F4 File Offset: 0x000C76F4
		// (remove) Token: 0x06002F3B RID: 12091 RVA: 0x000C9528 File Offset: 0x000C7728
		public static event SceneCamera.SceneCameraDelegate onSceneCameraPreRender;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06002F3C RID: 12092 RVA: 0x000C955C File Offset: 0x000C775C
		// (remove) Token: 0x06002F3D RID: 12093 RVA: 0x000C9590 File Offset: 0x000C7790
		public static event SceneCamera.SceneCameraDelegate onSceneCameraPostRender;

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000C95C3 File Offset: 0x000C77C3
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000C95CB File Offset: 0x000C77CB
		public Camera camera { get; private set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000C95D4 File Offset: 0x000C77D4
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x000C95DC File Offset: 0x000C77DC
		public CameraRigController cameraRigController { get; private set; }

		// Token: 0x06002F42 RID: 12098 RVA: 0x000C95E5 File Offset: 0x000C77E5
		private void Awake()
		{
			this.camera = base.GetComponent<Camera>();
			this.cameraRigController = base.GetComponentInParent<CameraRigController>();
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000C95FF File Offset: 0x000C77FF
		private void OnPreCull()
		{
			if (SceneCamera.onSceneCameraPreCull != null)
			{
				SceneCamera.onSceneCameraPreCull(this);
			}
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000C9613 File Offset: 0x000C7813
		private void OnPreRender()
		{
			if (SceneCamera.onSceneCameraPreRender != null)
			{
				this.camera.opaqueSortMode = this.sortMode;
				SceneCamera.onSceneCameraPreRender(this);
			}
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000C9638 File Offset: 0x000C7838
		private void OnPostRender()
		{
			if (SceneCamera.onSceneCameraPostRender != null)
			{
				SceneCamera.onSceneCameraPostRender(this);
			}
		}

		// Token: 0x04003126 RID: 12582
		public OpaqueSortMode sortMode = OpaqueSortMode.NoDistanceSort;

		// Token: 0x0200086F RID: 2159
		// (Invoke) Token: 0x06002F48 RID: 12104
		public delegate void SceneCameraDelegate(SceneCamera sceneCamera);
	}
}
