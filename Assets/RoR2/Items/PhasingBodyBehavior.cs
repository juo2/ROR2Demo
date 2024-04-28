using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BE0 RID: 3040
	public class PhasingBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x06004500 RID: 17664 RVA: 0x0011F3E1 File Offset: 0x0011D5E1
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.Phasing;
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x0011F3E8 File Offset: 0x0011D5E8
		private void Start()
		{
			this.rechargeStopwatch = this.baseRechargeSeconds;
			this.effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ProcStealthkit");
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x0011F408 File Offset: 0x0011D608
		private void FixedUpdate()
		{
			if (!base.body.healthComponent.alive)
			{
				return;
			}
			this.rechargeStopwatch += Time.fixedDeltaTime;
			if (base.body.healthComponent.isHealthLow && !base.body.hasCloakBuff && this.rechargeStopwatch >= this.buffDuration + this.baseRechargeSeconds * Mathf.Pow(this.rechargeReductionMultiplierPerStack, (float)(this.stack - 1)))
			{
				base.body.AddTimedBuff(RoR2Content.Buffs.Cloak, this.buffDuration);
				base.body.AddTimedBuff(RoR2Content.Buffs.CloakSpeed, this.buffDuration);
				EffectManager.SpawnEffect(this.effectPrefab, new EffectData
				{
					origin = base.transform.position,
					rotation = Quaternion.identity
				}, true);
				this.rechargeStopwatch = 0f;
			}
		}

		// Token: 0x04004367 RID: 17255
		private readonly float baseRechargeSeconds = 30f;

		// Token: 0x04004368 RID: 17256
		private readonly float rechargeReductionMultiplierPerStack = 0.5f;

		// Token: 0x04004369 RID: 17257
		private readonly float buffDuration = 5f;

		// Token: 0x0400436A RID: 17258
		private float rechargeStopwatch;

		// Token: 0x0400436B RID: 17259
		private GameObject effectPrefab;
	}
}
