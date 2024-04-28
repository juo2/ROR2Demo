using System;
using System.Collections.Generic;
using RoR2.Orbs;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BE7 RID: 3047
	public class ShockNearbyBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x0600451B RID: 17691 RVA: 0x0011FB14 File Offset: 0x0011DD14
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.ShockNearby;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x0011FB1B File Offset: 0x0011DD1B
		private BuffDef grantedBuff
		{
			get
			{
				return RoR2Content.Buffs.TeslaField;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x0011FB22 File Offset: 0x0011DD22
		// (set) Token: 0x0600451E RID: 17694 RVA: 0x0011FB2A File Offset: 0x0011DD2A
		private bool grantingBuff
		{
			get
			{
				return this._grantingBuff;
			}
			set
			{
				if (this._grantingBuff == value)
				{
					return;
				}
				this._grantingBuff = value;
				if (this._grantingBuff)
				{
					base.body.AddBuff(this.grantedBuff);
					return;
				}
				base.body.RemoveBuff(this.grantedBuff);
			}
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x0011FB68 File Offset: 0x0011DD68
		private void FixedUpdate()
		{
			this.teslaBuffRollTimer += Time.fixedDeltaTime;
			if (this.teslaBuffRollTimer >= 10f)
			{
				this.teslaBuffRollTimer = 0f;
				this.teslaCrit = base.body.RollCrit();
				this.grantingBuff = !this.grantingBuff;
			}
			if (this.grantingBuff)
			{
				this.teslaFireTimer += Time.fixedDeltaTime;
				this.teslaResetListTimer += Time.fixedDeltaTime;
				if (this.teslaFireTimer >= 0.083333336f)
				{
					this.teslaFireTimer = 0f;
					LightningOrb lightningOrb = new LightningOrb
					{
						origin = base.body.corePosition,
						damageValue = base.body.damage * 2f,
						isCrit = this.teslaCrit,
						bouncesRemaining = 2 * this.stack,
						teamIndex = base.body.teamComponent.teamIndex,
						attacker = base.gameObject,
						procCoefficient = 0.3f,
						bouncedObjects = this.previousTeslaTargetList,
						lightningType = LightningOrb.LightningType.Tesla,
						damageColorIndex = DamageColorIndex.Item,
						range = 35f
					};
					HurtBox hurtBox = lightningOrb.PickNextTarget(base.transform.position);
					if (hurtBox)
					{
						this.previousTeslaTargetList.Add(hurtBox.healthComponent);
						lightningOrb.target = hurtBox;
						OrbManager.instance.AddOrb(lightningOrb);
					}
				}
				if (this.teslaResetListTimer >= this.teslaResetListInterval)
				{
					this.teslaResetListTimer -= this.teslaResetListInterval;
					this.previousTeslaTargetList.Clear();
				}
			}
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x0011FD0C File Offset: 0x0011DF0C
		private void OnDisable()
		{
			if (base.body && base.body.HasBuff(this.grantedBuff))
			{
				base.body.RemoveBuff(this.grantedBuff);
			}
		}

		// Token: 0x0400437A RID: 17274
		private float teslaBuffRollTimer;

		// Token: 0x0400437B RID: 17275
		private const float teslaRollInterval = 10f;

		// Token: 0x0400437C RID: 17276
		private float teslaFireTimer;

		// Token: 0x0400437D RID: 17277
		private float teslaResetListTimer;

		// Token: 0x0400437E RID: 17278
		private float teslaResetListInterval = 0.5f;

		// Token: 0x0400437F RID: 17279
		private const float teslaFireInterval = 0.083333336f;

		// Token: 0x04004380 RID: 17280
		private bool teslaCrit;

		// Token: 0x04004381 RID: 17281
		private bool teslaIsOn;

		// Token: 0x04004382 RID: 17282
		private List<HealthComponent> previousTeslaTargetList = new List<HealthComponent>();

		// Token: 0x04004383 RID: 17283
		private bool _grantingBuff;
	}
}
