using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005F3 RID: 1523
	public class Billboard : MonoBehaviour
	{
		// Token: 0x06001BCD RID: 7117 RVA: 0x000765ED File Offset: 0x000747ED
		static Billboard()
		{
			SceneCamera.onSceneCameraPreCull += Billboard.OnSceneCameraPreCull;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0007660C File Offset: 0x0007480C
		private static void OnSceneCameraPreCull(SceneCamera sceneCamera)
		{
			Quaternion rotation = sceneCamera.transform.rotation;
			for (int i = 0; i < Billboard.instanceTransformsList.Count; i++)
			{
				Billboard.instanceTransformsList[i].rotation = rotation;
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0007664B File Offset: 0x0007484B
		private void OnEnable()
		{
			Billboard.instanceTransformsList.Add(base.transform);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0007665D File Offset: 0x0007485D
		private void OnDisable()
		{
			Billboard.instanceTransformsList.Remove(base.transform);
		}

		// Token: 0x0400219A RID: 8602
		private static List<Transform> instanceTransformsList = new List<Transform>();
	}
}
