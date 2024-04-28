using System;
using RoR2;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class DestroyOnKill : MonoBehaviour, IOnKilledServerReceiver
{
	// Token: 0x060000FF RID: 255 RVA: 0x00005754 File Offset: 0x00003954
	public void OnKilledServer(DamageReport damageReport)
	{
		UnityEngine.Object.Instantiate<GameObject>(this.effectPrefab, base.transform.position, base.transform.rotation);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040000F0 RID: 240
	public GameObject effectPrefab;
}
