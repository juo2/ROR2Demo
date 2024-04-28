using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class DisableOnStart : MonoBehaviour
{
	// Token: 0x06000107 RID: 263 RVA: 0x000057E0 File Offset: 0x000039E0
	private void Start()
	{
		base.gameObject.SetActive(false);
	}
}
