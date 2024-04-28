using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CF7 RID: 3319
	[RequireComponent(typeof(PositionIndicator))]
	public class DistanceIndicator : MonoBehaviour
	{
		// Token: 0x06004B91 RID: 19345 RVA: 0x001369DD File Offset: 0x00134BDD
		private void OnEnable()
		{
			DistanceIndicator.instancesList.Add(this);
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x001369EA File Offset: 0x00134BEA
		private void OnDisable()
		{
			DistanceIndicator.instancesList.Remove(this);
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x001369F8 File Offset: 0x00134BF8
		static DistanceIndicator()
		{
			UICamera.onUICameraPreCull += DistanceIndicator.UpdateText;
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x00136A18 File Offset: 0x00134C18
		private static void UpdateText(UICamera uiCamera)
		{
			CameraRigController cameraRigController = uiCamera.cameraRigController;
			Transform transform = null;
			if (cameraRigController && cameraRigController.target)
			{
				CharacterBody component = cameraRigController.target.GetComponent<CharacterBody>();
				if (component)
				{
					transform = component.coreTransform;
				}
				else
				{
					transform = cameraRigController.target.transform;
				}
			}
			if (transform)
			{
				for (int i = 0; i < DistanceIndicator.instancesList.Count; i++)
				{
					DistanceIndicator distanceIndicator = DistanceIndicator.instancesList[i];
					string text = (distanceIndicator.positionIndicator.targetTransform.position - transform.position).magnitude.ToString("0.0") + "m";
					distanceIndicator.tmp.text = text;
				}
			}
		}

		// Token: 0x04004851 RID: 18513
		public PositionIndicator positionIndicator;

		// Token: 0x04004852 RID: 18514
		public TextMeshPro tmp;

		// Token: 0x04004853 RID: 18515
		private static readonly List<DistanceIndicator> instancesList = new List<DistanceIndicator>();
	}
}
