using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000660 RID: 1632
	public class CloverEffect : MonoBehaviour
	{
		// Token: 0x06001FA2 RID: 8098 RVA: 0x00087B04 File Offset: 0x00085D04
		private void Start()
		{
			CharacterBody body = base.GetComponentInParent<CharacterModel>().body;
			this.characterBody = body.GetComponent<CharacterBody>();
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00087B2C File Offset: 0x00085D2C
		private void FixedUpdate()
		{
			if (this.characterBody && this.characterBody.wasLucky)
			{
				this.characterBody.wasLucky = false;
				EffectData effectData = new EffectData();
				effectData.origin = base.transform.position;
				effectData.rotation = base.transform.rotation;
				EffectManager.SpawnEffect(this.triggerEffect, effectData, true);
			}
		}

		// Token: 0x04002520 RID: 9504
		public GameObject triggerEffect;

		// Token: 0x04002521 RID: 9505
		private CharacterBody characterBody;

		// Token: 0x04002522 RID: 9506
		private GameObject triggerEffectInstance;

		// Token: 0x04002523 RID: 9507
		private bool trigger;
	}
}
