using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000901 RID: 2305
	public class WeatherParticles : MonoBehaviour
	{
		// Token: 0x06003415 RID: 13333 RVA: 0x000DB57D File Offset: 0x000D977D
		static WeatherParticles()
		{
			SceneCamera.onSceneCameraPreRender += WeatherParticles.OnSceneCameraPreRender;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000DB59C File Offset: 0x000D979C
		private void UpdateForCamera(CameraRigController cameraRigController, bool lockPosition, bool lockRotation)
		{
			Transform transform = cameraRigController.transform;
			base.transform.SetPositionAndRotation(lockPosition ? transform.position : base.transform.position, lockRotation ? transform.rotation : base.transform.rotation);
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000DB5E8 File Offset: 0x000D97E8
		private static void OnSceneCameraPreRender(SceneCamera sceneCamera)
		{
			if (sceneCamera.cameraRigController)
			{
				for (int i = 0; i < WeatherParticles.instancesList.Count; i++)
				{
					WeatherParticles weatherParticles = WeatherParticles.instancesList[i];
					weatherParticles.UpdateForCamera(sceneCamera.cameraRigController, weatherParticles.lockPosition, weatherParticles.lockRotation);
				}
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000DB63B File Offset: 0x000D983B
		private void OnEnable()
		{
			WeatherParticles.instancesList.Add(this);
			if (this.resetPositionToZero)
			{
				base.transform.position = Vector3.zero;
			}
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000DB660 File Offset: 0x000D9860
		private void OnDisable()
		{
			WeatherParticles.instancesList.Remove(this);
		}

		// Token: 0x040034FB RID: 13563
		public bool resetPositionToZero;

		// Token: 0x040034FC RID: 13564
		public bool lockPosition = true;

		// Token: 0x040034FD RID: 13565
		public bool lockRotation = true;

		// Token: 0x040034FE RID: 13566
		private static List<WeatherParticles> instancesList = new List<WeatherParticles>();
	}
}
