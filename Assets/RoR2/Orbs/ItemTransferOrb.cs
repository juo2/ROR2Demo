using System;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Orbs
{
	// Token: 0x02000B15 RID: 2837
	public class ItemTransferOrb : Orb
	{
		// Token: 0x060040BD RID: 16573 RVA: 0x0010BD8C File Offset: 0x00109F8C
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			ItemTransferOrb.orbEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/ItemTransferOrbEffect");
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x0010BDA0 File Offset: 0x00109FA0
		public override void Begin()
		{
			base.duration = this.travelDuration;
			if (this.target || this.orbEffectTargetObjectOverride)
			{
				EffectData effectData = new EffectData
				{
					origin = this.origin,
					genericFloat = base.duration,
					genericUInt = Util.IntToUintPlusOne((int)this.itemIndex)
				};
				if (this.orbEffectTargetObjectOverride)
				{
					effectData.SetNetworkedObjectReference(this.orbEffectTargetObjectOverride.gameObject);
				}
				else
				{
					effectData.SetHurtBoxReference(this.target);
				}
				EffectManager.SpawnEffect(ItemTransferOrb.orbEffectPrefab, effectData, true);
			}
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0010BE3A File Offset: 0x0010A03A
		public override void OnArrival()
		{
			Action<ItemTransferOrb> action = this.onArrival;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x0010BE4D File Offset: 0x0010A04D
		public static void DefaultOnArrivalBehavior(ItemTransferOrb orb)
		{
			if (orb.inventoryToGrantTo)
			{
				orb.inventoryToGrantTo.GiveItem(orb.itemIndex, orb.stack);
			}
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x0010BE74 File Offset: 0x0010A074
		public static ItemTransferOrb DispatchItemTransferOrb(Vector3 origin, Inventory inventoryToGrantTo, ItemIndex itemIndex, int itemStack, Action<ItemTransferOrb> onArrivalBehavior = null, Either<NetworkIdentity, HurtBox> orbDestinationOverride = default(Either<NetworkIdentity, HurtBox>))
		{
			if (onArrivalBehavior == null)
			{
				onArrivalBehavior = new Action<ItemTransferOrb>(ItemTransferOrb.DefaultOnArrivalBehavior);
			}
			ItemTransferOrb itemTransferOrb = new ItemTransferOrb();
			itemTransferOrb.origin = origin;
			itemTransferOrb.inventoryToGrantTo = inventoryToGrantTo;
			itemTransferOrb.itemIndex = itemIndex;
			itemTransferOrb.stack = itemStack;
			itemTransferOrb.onArrival = onArrivalBehavior;
			NetworkIdentity a = orbDestinationOverride.a;
			HurtBox b = orbDestinationOverride.b;
			if (!b)
			{
				if (inventoryToGrantTo)
				{
					CharacterMaster component = inventoryToGrantTo.GetComponent<CharacterMaster>();
					if (component)
					{
						CharacterBody body = component.GetBody();
						if (body)
						{
							itemTransferOrb.target = body.mainHurtBox;
						}
					}
				}
				if (a)
				{
					itemTransferOrb.orbEffectTargetObjectOverride = a;
				}
			}
			else
			{
				itemTransferOrb.target = b;
			}
			OrbManager.instance.AddOrb(itemTransferOrb);
			return itemTransferOrb;
		}

		// Token: 0x04003F1C RID: 16156
		public ItemIndex itemIndex;

		// Token: 0x04003F1D RID: 16157
		public int stack;

		// Token: 0x04003F1E RID: 16158
		public Inventory inventoryToGrantTo;

		// Token: 0x04003F1F RID: 16159
		public Action<ItemTransferOrb> onArrival = new Action<ItemTransferOrb>(ItemTransferOrb.DefaultOnArrivalBehavior);

		// Token: 0x04003F20 RID: 16160
		public NetworkIdentity orbEffectTargetObjectOverride;

		// Token: 0x04003F21 RID: 16161
		public float travelDuration = 1f;

		// Token: 0x04003F22 RID: 16162
		private static GameObject orbEffectPrefab;
	}
}
