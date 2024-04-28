using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008C5 RID: 2245
	public class TeslaCoilAnimator : MonoBehaviour
	{
		// Token: 0x06003246 RID: 12870 RVA: 0x000D45A0 File Offset: 0x000D27A0
		private void Start()
		{
			CharacterModel componentInParent = base.GetComponentInParent<CharacterModel>();
			if (componentInParent)
			{
				this.characterBody = componentInParent.body;
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000D45C8 File Offset: 0x000D27C8
		private void FixedUpdate()
		{
			if (this.characterBody)
			{
				this.activeEffectParent.SetActive(this.characterBody.HasBuff(RoR2Content.Buffs.TeslaField));
			}
		}

		// Token: 0x0400336D RID: 13165
		public GameObject activeEffectParent;

		// Token: 0x0400336E RID: 13166
		private CharacterBody characterBody;
	}
}
