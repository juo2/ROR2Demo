using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000782 RID: 1922
	public class MushroomVoidBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x0600283D RID: 10301 RVA: 0x0007FEC8 File Offset: 0x0007E0C8
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000AEB34 File Offset: 0x000ACD34
		private void OnEnable()
		{
			if (this.body)
			{
				this.wasSprinting = (this.body.GetBuffCount(DLC1Content.Buffs.MushroomVoidActive) > 0);
				this.healthComponent = this.body.GetComponent<HealthComponent>();
			}
			this.healTimer = 0f;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000AEB83 File Offset: 0x000ACD83
		private void OnDisable()
		{
			if (this.body && this.wasSprinting)
			{
				this.body.RemoveBuff(DLC1Content.Buffs.MushroomVoidActive);
			}
			this.healthComponent = null;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000AEBB4 File Offset: 0x000ACDB4
		private void FixedUpdate()
		{
			if (this.body)
			{
				if (this.body.isSprinting)
				{
					this.healTimer += Time.fixedDeltaTime;
					if (!this.wasSprinting)
					{
						this.wasSprinting = true;
						this.body.AddBuff(DLC1Content.Buffs.MushroomVoidActive);
					}
				}
				else if (this.wasSprinting)
				{
					this.body.RemoveBuff(DLC1Content.Buffs.MushroomVoidActive);
					this.wasSprinting = false;
				}
			}
			while (this.healTimer > 0.5f)
			{
				this.healthComponent.HealFraction(0.01f * (float)this.stack, default(ProcChainMask));
				this.healTimer -= 0.5f;
			}
		}

		// Token: 0x04002BE7 RID: 11239
		private const float healPercentagePerStack = 0.01f;

		// Token: 0x04002BE8 RID: 11240
		private const float healPeriodSeconds = 0.5f;

		// Token: 0x04002BE9 RID: 11241
		private bool wasSprinting;

		// Token: 0x04002BEA RID: 11242
		private HealthComponent healthComponent;

		// Token: 0x04002BEB RID: 11243
		private float healTimer;
	}
}
