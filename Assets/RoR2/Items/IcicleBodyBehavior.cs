using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Items
{
	// Token: 0x02000BD9 RID: 3033
	public class IcicleBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x060044DF RID: 17631 RVA: 0x0011EC37 File Offset: 0x0011CE37
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.Icicle;
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x0011EC3E File Offset: 0x0011CE3E
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			IcicleBodyBehavior.icicleAuraPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/IcicleAura");
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x0011EC50 File Offset: 0x0011CE50
		private void OnEnable()
		{
			GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(IcicleBodyBehavior.icicleAuraPrefab, base.transform.position, Quaternion.identity);
			this.icicleAura = gameObject.GetComponent<IcicleAuraController>();
			this.icicleAura.Networkowner = base.gameObject;
			NetworkServer.Spawn(gameObject);
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x0011ECAC File Offset: 0x0011CEAC
		private void OnDisable()
		{
			GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
			if (this.icicleAura)
			{
				UnityEngine.Object.Destroy(this.icicleAura);
				this.icicleAura = null;
			}
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x0011ECDE File Offset: 0x0011CEDE
		private void OnCharacterDeathGlobal(DamageReport damageReport)
		{
			if (damageReport.attackerBody != base.body)
			{
				return;
			}
			if (this.icicleAura)
			{
				this.icicleAura.OnOwnerKillOther();
			}
		}

		// Token: 0x04004354 RID: 17236
		private static GameObject icicleAuraPrefab;

		// Token: 0x04004355 RID: 17237
		private IcicleAuraController icicleAura;
	}
}
