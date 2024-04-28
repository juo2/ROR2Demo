using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000799 RID: 1945
	public class LocalCameraEffect : MonoBehaviour
	{
		// Token: 0x06002918 RID: 10520 RVA: 0x000B2109 File Offset: 0x000B0309
		static LocalCameraEffect()
		{
			UICamera.onUICameraPreCull += LocalCameraEffect.OnUICameraPreCull;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000B2128 File Offset: 0x000B0328
		private static void OnUICameraPreCull(UICamera uiCamera)
		{
			for (int i = 0; i < LocalCameraEffect.instancesList.Count; i++)
			{
				GameObject target = uiCamera.cameraRigController.target;
				LocalCameraEffect localCameraEffect = LocalCameraEffect.instancesList[i];
				if (localCameraEffect.targetCharacter == target)
				{
					localCameraEffect.effectRoot.SetActive(true);
				}
				else
				{
					localCameraEffect.effectRoot.SetActive(false);
				}
			}
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000B218A File Offset: 0x000B038A
		private void Start()
		{
			LocalCameraEffect.instancesList.Add(this);
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000B2197 File Offset: 0x000B0397
		private void OnDestroy()
		{
			LocalCameraEffect.instancesList.Remove(this);
		}

		// Token: 0x04002C87 RID: 11399
		public GameObject targetCharacter;

		// Token: 0x04002C88 RID: 11400
		public GameObject effectRoot;

		// Token: 0x04002C89 RID: 11401
		private static List<LocalCameraEffect> instancesList = new List<LocalCameraEffect>();
	}
}
