using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000886 RID: 2182
	public class SetDontDestroyOnLoad : MonoBehaviour
	{
		// Token: 0x06002FD7 RID: 12247 RVA: 0x000CBAB2 File Offset: 0x000C9CB2
		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}
}
