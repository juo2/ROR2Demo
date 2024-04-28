using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200067F RID: 1663
	public class ConstrainToScreen : MonoBehaviour
	{
		// Token: 0x0600207D RID: 8317 RVA: 0x0008BBBA File Offset: 0x00089DBA
		static ConstrainToScreen()
		{
			SceneCamera.onSceneCameraPreCull += ConstrainToScreen.OnSceneCameraPreCull;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0008BBE4 File Offset: 0x00089DE4
		private static void OnSceneCameraPreCull(SceneCamera sceneCamera)
		{
			Camera camera = sceneCamera.camera;
			for (int i = 0; i < ConstrainToScreen.instanceTransformsList.Count; i++)
			{
				Transform transform = ConstrainToScreen.instanceTransformsList[i];
				Vector3 vector = camera.WorldToViewportPoint(transform.position);
				vector.x = Mathf.Clamp(vector.x, ConstrainToScreen.boundaryUVSize, 1f - ConstrainToScreen.boundaryUVSize);
				vector.y = Mathf.Clamp(vector.y, ConstrainToScreen.boundaryUVSize, 1f - ConstrainToScreen.boundaryUVSize);
				transform.position = camera.ViewportToWorldPoint(vector);
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0008BC77 File Offset: 0x00089E77
		private void OnEnable()
		{
			ConstrainToScreen.instanceTransformsList.Add(base.transform);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0008BC89 File Offset: 0x00089E89
		private void OnDisable()
		{
			ConstrainToScreen.instanceTransformsList.Remove(base.transform);
		}

		// Token: 0x040025BF RID: 9663
		private static float boundaryUVSize = 0.05f;

		// Token: 0x040025C0 RID: 9664
		private static List<Transform> instanceTransformsList = new List<Transform>();
	}
}
