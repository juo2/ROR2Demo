using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
[ExecuteInEditMode]
public class GlobalShaderTextures : MonoBehaviour
{
	// Token: 0x06000130 RID: 304 RVA: 0x00007065 File Offset: 0x00005265
	private void OnValidate()
	{
		Shader.SetGlobalTexture(this.warpRampShaderVariableName, this.warpRampTexture);
		Shader.SetGlobalTexture(this.eliteRampShaderVariableName, this.eliteRampTexture);
		Shader.SetGlobalTexture(this.snowMicrofacetNoiseVariableName, this.snowMicrofacetTexture);
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00007065 File Offset: 0x00005265
	private void Start()
	{
		Shader.SetGlobalTexture(this.warpRampShaderVariableName, this.warpRampTexture);
		Shader.SetGlobalTexture(this.eliteRampShaderVariableName, this.eliteRampTexture);
		Shader.SetGlobalTexture(this.snowMicrofacetNoiseVariableName, this.snowMicrofacetTexture);
	}

	// Token: 0x04000143 RID: 323
	public Texture warpRampTexture;

	// Token: 0x04000144 RID: 324
	public string warpRampShaderVariableName;

	// Token: 0x04000145 RID: 325
	public Texture eliteRampTexture;

	// Token: 0x04000146 RID: 326
	public string eliteRampShaderVariableName;

	// Token: 0x04000147 RID: 327
	public Texture snowMicrofacetTexture;

	// Token: 0x04000148 RID: 328
	public string snowMicrofacetNoiseVariableName;
}
