using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000794 RID: 1940
	public class LightScaleFromParent : MonoBehaviour
	{
		// Token: 0x060028FD RID: 10493 RVA: 0x000B1C08 File Offset: 0x000AFE08
		private void Start()
		{
			Light component = base.GetComponent<Light>();
			if (component)
			{
				float range = component.range;
				Vector3 lossyScale = base.transform.lossyScale;
				component.range = range * Mathf.Max(new float[]
				{
					lossyScale.x,
					lossyScale.y,
					lossyScale.z
				});
			}
		}
	}
}
