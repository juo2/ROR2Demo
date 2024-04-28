using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006E2 RID: 1762
	public class ForcedCamera : MonoBehaviour, ICameraStateProvider
	{
		// Token: 0x060022D4 RID: 8916 RVA: 0x00096838 File Offset: 0x00094A38
		private void Update()
		{
			ReadOnlyCollection<CameraRigController> readOnlyInstancesList = CameraRigController.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				CameraRigController cameraRigController = readOnlyInstancesList[i];
				if (!cameraRigController.hasOverride)
				{
					cameraRigController.SetOverrideCam(this, this.entryLerpDuration);
				}
			}
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0009687C File Offset: 0x00094A7C
		private void OnDisable()
		{
			ReadOnlyCollection<CameraRigController> readOnlyInstancesList = CameraRigController.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				CameraRigController cameraRigController = readOnlyInstancesList[i];
				if (cameraRigController.IsOverrideCam(this))
				{
					cameraRigController.SetOverrideCam(null, this.exitLerpDuration);
				}
			}
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000968BE File Offset: 0x00094ABE
		public void GetCameraState(CameraRigController cameraRigController, ref CameraState cameraState)
		{
			cameraState.position = base.transform.position;
			cameraState.rotation = base.transform.rotation;
			if (this.fovOverride > 0f)
			{
				cameraState.fov = this.fovOverride;
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000968FB File Offset: 0x00094AFB
		public bool IsUserLookAllowed(CameraRigController cameraRigController)
		{
			return this.allowUserLook;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool IsUserControlAllowed(CameraRigController cameraRigController)
		{
			return false;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00096903 File Offset: 0x00094B03
		public bool IsHudAllowed(CameraRigController cameraRigController)
		{
			return this.allowUserHud;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0009690C File Offset: 0x00094B0C
		private void OnDrawGizmosSelected()
		{
			Color color = Gizmos.color;
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.color = Color.yellow;
			Matrix4x4 identity = Matrix4x4.identity;
			identity.SetTRS(base.transform.position, base.transform.rotation, Vector3.one);
			Gizmos.matrix = identity;
			Gizmos.DrawFrustum(Vector3.zero, (this.fovOverride > 0f) ? this.fovOverride : 60f, 10f, 0.1f, 1.7777778f);
			Gizmos.matrix = matrix;
			Gizmos.color = color;
		}

		// Token: 0x040027EF RID: 10223
		public float entryLerpDuration = 1f;

		// Token: 0x040027F0 RID: 10224
		public float exitLerpDuration = 1f;

		// Token: 0x040027F1 RID: 10225
		public float fovOverride;

		// Token: 0x040027F2 RID: 10226
		public bool allowUserLook;

		// Token: 0x040027F3 RID: 10227
		public bool allowUserHud;
	}
}
