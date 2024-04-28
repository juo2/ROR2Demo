using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
[ExecuteInEditMode]
public class RenderDepth : MonoBehaviour
{
	// Token: 0x0600016A RID: 362 RVA: 0x00007C2E File Offset: 0x00005E2E
	private void OnEnable()
	{
		base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
	}
}
