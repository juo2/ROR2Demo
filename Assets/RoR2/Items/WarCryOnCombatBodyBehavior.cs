using System;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BEE RID: 3054
	public class WarCryOnCombatBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x06004545 RID: 17733 RVA: 0x001205F8 File Offset: 0x0011E7F8
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return JunkContent.Items.WarCryOnCombat;
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x001205FF File Offset: 0x0011E7FF
		private void OnEnable()
		{
			this.warCryTimer = WarCryOnCombatBodyBehavior.warCryChargeDuration;
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x0012060C File Offset: 0x0011E80C
		private void FixedUpdate()
		{
			this.warCryTimer -= Time.fixedDeltaTime;
			if (this.warCryTimer <= 0f && !base.body.outOfCombat && this.wasOutOfCombat)
			{
				this.warCryTimer = WarCryOnCombatBodyBehavior.warCryChargeDuration;
				this.ActivateWarCryAura(this.stack);
			}
			this.wasOutOfCombat = base.body.outOfCombat;
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x00120678 File Offset: 0x0011E878
		private void ActivateWarCryAura(int stacks)
		{
			if (this.warCryAuraController)
			{
				UnityEngine.Object.Destroy(this.warCryAuraController);
			}
			this.warCryAuraController = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/WarCryAura"), base.transform.position, base.transform.rotation, base.transform);
			this.warCryAuraController.GetComponent<TeamFilter>().teamIndex = base.body.teamComponent.teamIndex;
			BuffWard component = this.warCryAuraController.GetComponent<BuffWard>();
			component.expireDuration = 2f + 4f * (float)stacks;
			component.Networkradius = 8f + 4f * (float)stacks;
			this.warCryAuraController.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(base.gameObject, null);
		}

		// Token: 0x04004391 RID: 17297
		private static readonly float warCryChargeDuration = 30f;

		// Token: 0x04004392 RID: 17298
		private float warCryTimer;

		// Token: 0x04004393 RID: 17299
		private GameObject warCryAuraController;

		// Token: 0x04004394 RID: 17300
		private bool wasOutOfCombat;
	}
}
