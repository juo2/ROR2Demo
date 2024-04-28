using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000900 RID: 2304
	[Obsolete]
	public class WarCryOnCombatDisplayController : MonoBehaviour
	{
		// Token: 0x06003410 RID: 13328 RVA: 0x000DB500 File Offset: 0x000D9700
		public void Start()
		{
			CharacterModel component = base.transform.root.gameObject.GetComponent<CharacterModel>();
			if (component)
			{
				this.body = component.body;
			}
			this.UpdateReadyIndicator();
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000DB53D File Offset: 0x000D973D
		public void FixedUpdate()
		{
			this.UpdateReadyIndicator();
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000DB548 File Offset: 0x000D9748
		private void UpdateReadyIndicator()
		{
			bool active = this.body && WarCryOnCombatDisplayController.IsBodyWarCryReady(this.body);
			this.readyIndicator.SetActive(active);
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x0000CF8A File Offset: 0x0000B18A
		private static bool IsBodyWarCryReady(CharacterBody body)
		{
			return false;
		}

		// Token: 0x040034F9 RID: 13561
		private CharacterBody body;

		// Token: 0x040034FA RID: 13562
		[Tooltip("The child gameobject to enable when the warcry is ready.")]
		public GameObject readyIndicator;
	}
}
