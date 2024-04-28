using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006AD RID: 1709
	public class DitherModel : MonoBehaviour
	{
		// Token: 0x0600213D RID: 8509 RVA: 0x0008EC85 File Offset: 0x0008CE85
		static DitherModel()
		{
			SceneCamera.onSceneCameraPreRender += DitherModel.OnSceneCameraPreRender;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0008ECA2 File Offset: 0x0008CEA2
		private static void OnSceneCameraPreRender(SceneCamera sceneCamera)
		{
			if (sceneCamera.cameraRigController)
			{
				DitherModel.RefreshObstructorsForCamera(sceneCamera.cameraRigController);
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0008ECBC File Offset: 0x0008CEBC
		private static void RefreshObstructorsForCamera(CameraRigController cameraRigController)
		{
			Vector3 position = cameraRigController.transform.position;
			for (int i = 0; i < DitherModel.instancesList.Count; i++)
			{
				DitherModel ditherModel = DitherModel.instancesList[i];
				if (ditherModel.bounds)
				{
					Vector3 a = ditherModel.bounds.ClosestPointOnBounds(position);
					if (cameraRigController.enableFading)
					{
						ditherModel.fade = Mathf.Clamp01(Util.Remap(Vector3.Distance(a, position), cameraRigController.fadeStartDistance, cameraRigController.fadeEndDistance, 0f, 1f));
					}
					else
					{
						ditherModel.fade = 1f;
					}
					ditherModel.UpdateDither();
				}
				else
				{
					Debug.LogFormat("{0} has missing collider for dither model", new object[]
					{
						ditherModel.gameObject
					});
				}
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0008ED7C File Offset: 0x0008CF7C
		private void UpdateDither()
		{
			for (int i = this.renderers.Length - 1; i >= 0; i--)
			{
				Renderer renderer = this.renderers[i];
				renderer.GetPropertyBlock(this.propertyStorage);
				this.propertyStorage.SetFloat("_Fade", this.fade);
				renderer.SetPropertyBlock(this.propertyStorage);
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0008EDD3 File Offset: 0x0008CFD3
		private void Awake()
		{
			this.propertyStorage = new MaterialPropertyBlock();
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0008EDE0 File Offset: 0x0008CFE0
		private void OnEnable()
		{
			DitherModel.instancesList.Add(this);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0008EDED File Offset: 0x0008CFED
		private void OnDisable()
		{
			DitherModel.instancesList.Remove(this);
		}

		// Token: 0x0400269C RID: 9884
		[HideInInspector]
		public float fade;

		// Token: 0x0400269D RID: 9885
		public Collider bounds;

		// Token: 0x0400269E RID: 9886
		public Renderer[] renderers;

		// Token: 0x0400269F RID: 9887
		private MaterialPropertyBlock propertyStorage;

		// Token: 0x040026A0 RID: 9888
		private static List<DitherModel> instancesList = new List<DitherModel>();
	}
}
