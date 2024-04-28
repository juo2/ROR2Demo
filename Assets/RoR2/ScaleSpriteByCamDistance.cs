using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000869 RID: 2153
	public class ScaleSpriteByCamDistance : MonoBehaviour
	{
		// Token: 0x06002F21 RID: 12065 RVA: 0x000C8F94 File Offset: 0x000C7194
		static ScaleSpriteByCamDistance()
		{
			SceneCamera.onSceneCameraPreCull += ScaleSpriteByCamDistance.OnSceneCameraPreCull;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000C8FB1 File Offset: 0x000C71B1
		private void Awake()
		{
			this.transform = base.transform;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000C8FBF File Offset: 0x000C71BF
		private void OnEnable()
		{
			ScaleSpriteByCamDistance.instancesList.Add(this);
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000C8FCC File Offset: 0x000C71CC
		private void OnDisable()
		{
			ScaleSpriteByCamDistance.instancesList.Remove(this);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000C8FDC File Offset: 0x000C71DC
		private static void OnSceneCameraPreCull(SceneCamera sceneCamera)
		{
			Vector3 position = sceneCamera.transform.position;
			for (int i = 0; i < ScaleSpriteByCamDistance.instancesList.Count; i++)
			{
				ScaleSpriteByCamDistance scaleSpriteByCamDistance = ScaleSpriteByCamDistance.instancesList[i];
				Transform transform = scaleSpriteByCamDistance.transform;
				float num = 1f;
				float num2 = Vector3.Distance(position, transform.position);
				switch (scaleSpriteByCamDistance.scalingMode)
				{
				case ScaleSpriteByCamDistance.ScalingMode.Direct:
					num = num2;
					break;
				case ScaleSpriteByCamDistance.ScalingMode.Square:
					num = num2 * num2;
					break;
				case ScaleSpriteByCamDistance.ScalingMode.Sqrt:
					num = Mathf.Sqrt(num2);
					break;
				case ScaleSpriteByCamDistance.ScalingMode.Cube:
					num = num2 * num2 * num2;
					break;
				case ScaleSpriteByCamDistance.ScalingMode.CubeRoot:
					num = Mathf.Pow(num2, 0.33333334f);
					break;
				}
				num *= scaleSpriteByCamDistance.scaleFactor;
				transform.localScale = new Vector3(num, num, num);
			}
		}

		// Token: 0x04003108 RID: 12552
		private static List<ScaleSpriteByCamDistance> instancesList = new List<ScaleSpriteByCamDistance>();

		// Token: 0x04003109 RID: 12553
		private new Transform transform;

		// Token: 0x0400310A RID: 12554
		[Tooltip("The amount by which to scale.")]
		public float scaleFactor = 1f;

		// Token: 0x0400310B RID: 12555
		public ScaleSpriteByCamDistance.ScalingMode scalingMode;

		// Token: 0x0200086A RID: 2154
		public enum ScalingMode
		{
			// Token: 0x0400310D RID: 12557
			Direct,
			// Token: 0x0400310E RID: 12558
			Square,
			// Token: 0x0400310F RID: 12559
			Sqrt,
			// Token: 0x04003110 RID: 12560
			Cube,
			// Token: 0x04003111 RID: 12561
			CubeRoot
		}
	}
}
