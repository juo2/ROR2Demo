using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D8F RID: 3471
	public class SpectatorLabel : MonoBehaviour
	{
		// Token: 0x06004F7D RID: 20349 RVA: 0x00148E4B File Offset: 0x0014704B
		private void Awake()
		{
			this.labelRoot.SetActive(false);
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00148E59 File Offset: 0x00147059
		private void Update()
		{
			this.UpdateLabel();
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x00148E64 File Offset: 0x00147064
		private void UpdateLabel()
		{
			CameraRigController cameraRigController = this.hud.cameraRigController;
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			if (cameraRigController)
			{
				gameObject = cameraRigController.target;
				gameObject2 = cameraRigController.localUserViewer.cachedBodyObject;
			}
			if (gameObject == gameObject2 || !(gameObject != null))
			{
				this.labelRoot.SetActive(false);
				this.cachedTarget = null;
				return;
			}
			this.labelRoot.SetActive(true);
			if (this.cachedTarget == gameObject)
			{
				return;
			}
			string text = gameObject ? Util.GetBestBodyName(gameObject) : "";
			this.label.SetText(Language.GetStringFormatted("SPECTATING_NAME_FORMAT", new object[]
			{
				text
			}), true);
			this.cachedTarget = gameObject;
		}

		// Token: 0x04004C29 RID: 19497
		public HUD hud;

		// Token: 0x04004C2A RID: 19498
		public HGTextMeshProUGUI label;

		// Token: 0x04004C2B RID: 19499
		public GameObject labelRoot;

		// Token: 0x04004C2C RID: 19500
		private GameObject cachedTarget;

		// Token: 0x04004C2D RID: 19501
		private HudElement hudElement;
	}
}
