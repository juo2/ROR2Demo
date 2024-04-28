using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006B9 RID: 1721
	public class ElementalRingController : MonoBehaviour
	{
		// Token: 0x06002185 RID: 8581 RVA: 0x000906CC File Offset: 0x0008E8CC
		private void Start()
		{
			CharacterModel componentInParent = base.GetComponentInParent<CharacterModel>();
			if (componentInParent)
			{
				this.characterBody = componentInParent.body;
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000906F4 File Offset: 0x0008E8F4
		private void FixedUpdate()
		{
			if (!this.characterBody)
			{
				return;
			}
			bool flag = this.characterBody.HasBuff(RoR2Content.Buffs.ElementalRingsReady);
			if (flag != this.elementalRingAvailableObject.activeSelf)
			{
				this.elementalRingAvailableObject.SetActive(flag);
			}
		}

		// Token: 0x040026ED RID: 9965
		public GameObject elementalRingAvailableObject;

		// Token: 0x040026EE RID: 9966
		private CharacterBody characterBody;
	}
}
