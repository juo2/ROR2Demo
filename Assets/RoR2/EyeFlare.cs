using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006D5 RID: 1749
	public class EyeFlare : MonoBehaviour
	{
		// Token: 0x0600227C RID: 8828 RVA: 0x00094CA2 File Offset: 0x00092EA2
		static EyeFlare()
		{
			SceneCamera.onSceneCameraPreCull += EyeFlare.OnSceneCameraPreCull;
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00094CBF File Offset: 0x00092EBF
		private void OnEnable()
		{
			EyeFlare.instancesList.Add(this);
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00094CCC File Offset: 0x00092ECC
		private void OnDisable()
		{
			EyeFlare.instancesList.Remove(this);
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00094CDC File Offset: 0x00092EDC
		private static void OnSceneCameraPreCull(SceneCamera sceneCamera)
		{
			Transform transform = Camera.current.transform;
			Quaternion rotation = transform.rotation;
			Vector3 forward = transform.forward;
			for (int i = 0; i < EyeFlare.instancesList.Count; i++)
			{
				EyeFlare eyeFlare = EyeFlare.instancesList[i];
				float num = eyeFlare.localScale;
				if (eyeFlare.directionSource)
				{
					float num2 = Vector3.Dot(forward, eyeFlare.directionSource.forward) * -0.5f + 0.5f;
					num *= num2 * num2;
				}
				eyeFlare.transform.localScale = new Vector3(num, num, num);
				eyeFlare.transform.rotation = rotation;
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x00094D81 File Offset: 0x00092F81
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x0400277D RID: 10109
		[Tooltip("The transform whose forward vector will be tested against the camera angle to determine scaling. This is usually the parent, and never this object since billboarding will affect the direction.")]
		public Transform directionSource;

		// Token: 0x0400277E RID: 10110
		public float localScale = 1f;

		// Token: 0x0400277F RID: 10111
		private static List<EyeFlare> instancesList = new List<EyeFlare>();

		// Token: 0x04002780 RID: 10112
		private new Transform transform;
	}
}
