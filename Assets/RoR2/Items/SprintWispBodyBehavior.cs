using System;
using RoR2.Orbs;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BE9 RID: 3049
	public class SprintWispBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x06004528 RID: 17704 RVA: 0x0011FE10 File Offset: 0x0011E010
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.SprintWisp;
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x0011FE18 File Offset: 0x0011E018
		private void FixedUpdate()
		{
			if (base.body.isSprinting)
			{
				this.fireTimer -= Time.fixedDeltaTime;
				if (this.fireTimer <= 0f && base.body.moveSpeed > 0f)
				{
					this.fireTimer += 1f / (SprintWispBodyBehavior.fireRate * base.body.moveSpeed);
					this.Fire();
				}
			}
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x0011FE90 File Offset: 0x0011E090
		private void Fire()
		{
			DevilOrb devilOrb = new DevilOrb
			{
				origin = base.body.corePosition,
				damageValue = base.body.damage * SprintWispBodyBehavior.damageCoefficient * (float)this.stack,
				teamIndex = base.body.teamComponent.teamIndex,
				attacker = base.gameObject,
				damageColorIndex = DamageColorIndex.Item,
				scale = 1f,
				effectType = DevilOrb.EffectType.Wisp,
				procCoefficient = 1f
			};
			if (devilOrb.target = devilOrb.PickNextTarget(devilOrb.origin, SprintWispBodyBehavior.searchRadius))
			{
				devilOrb.isCrit = Util.CheckRoll(base.body.crit, base.body.master);
				OrbManager.instance.AddOrb(devilOrb);
			}
		}

		// Token: 0x04004386 RID: 17286
		private static readonly float fireRate = 0.08571429f;

		// Token: 0x04004387 RID: 17287
		private static readonly float searchRadius = 40f;

		// Token: 0x04004388 RID: 17288
		private static readonly float damageCoefficient = 3f;

		// Token: 0x04004389 RID: 17289
		private float fireTimer;
	}
}
