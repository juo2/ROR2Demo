using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class ElectricEffect : MonoBehaviour
{
	// Token: 0x0600000D RID: 13 RVA: 0x0000222C File Offset: 0x0000042C
	private void Start()
	{
		this.lineRendererMat = this.lightningLineRenderer.material;
		this.lightningLineRenderer.startWidth = this.lineWidth;
		this.lightningLineRenderer.endWidth = this.lineWidth;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002264 File Offset: 0x00000464
	private void Update()
	{
		if (Time.timeScale > 0f)
		{
			if (this.counter >= this.interval)
			{
				if (this.textureNum == this.lightningTextures.Count)
				{
					this.textureNum = 0;
				}
				this.lineRendererMat.SetTexture("_MainTex", this.lightningTextures[this.textureNum]);
				this.textureNum++;
				this.counter = 0;
			}
			this.counter++;
		}
	}

	// Token: 0x04000005 RID: 5
	public LineRenderer lightningLineRenderer;

	// Token: 0x04000006 RID: 6
	public List<Texture> lightningTextures = new List<Texture>();

	// Token: 0x04000007 RID: 7
	private Material lineRendererMat;

	// Token: 0x04000008 RID: 8
	private int counter;

	// Token: 0x04000009 RID: 9
	private int textureNum;

	// Token: 0x0400000A RID: 10
	public int interval = 3;

	// Token: 0x0400000B RID: 11
	public float lineWidth = 1f;
}
