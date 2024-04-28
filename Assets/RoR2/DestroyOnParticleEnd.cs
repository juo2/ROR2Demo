using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class DestroyOnParticleEnd : MonoBehaviour
{
	// Token: 0x06000101 RID: 257 RVA: 0x00005783 File Offset: 0x00003983
	public void Awake()
	{
		this.ps = base.GetComponentInChildren<ParticleSystem>();
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00005791 File Offset: 0x00003991
	public void Update()
	{
		if (this.ps && !this.ps.IsAlive())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040000F1 RID: 241
	private ParticleSystem ps;
}
