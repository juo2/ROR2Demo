using System;
using RoR2.Orbs;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BDC RID: 3036
	public static class MultiShopCardUtils
	{
		// Token: 0x060044ED RID: 17645 RVA: 0x0011EFE8 File Offset: 0x0011D1E8
		public static void OnNonMoneyPurchase(CostTypeDef.PayCostContext context)
		{
			MultiShopCardUtils.OnPurchase(context, 0);
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x0011EFF1 File Offset: 0x0011D1F1
		public static void OnMoneyPurchase(CostTypeDef.PayCostContext context)
		{
			MultiShopCardUtils.OnPurchase(context, context.cost);
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x0011F000 File Offset: 0x0011D200
		private static void OnPurchase(CostTypeDef.PayCostContext context, int moneyCost)
		{
			CharacterMaster activatorMaster = context.activatorMaster;
			if (activatorMaster && activatorMaster.hasBody && activatorMaster.inventory && activatorMaster.inventory.currentEquipmentIndex == DLC1Content.Equipment.MultiShopCard.equipmentIndex)
			{
				CharacterBody body = activatorMaster.GetBody();
				if (body.equipmentSlot.stock > 0)
				{
					bool flag = false;
					if (moneyCost > 0)
					{
						flag = true;
						GoldOrb goldOrb = new GoldOrb();
						Orb orb = goldOrb;
						GameObject purchasedObject = context.purchasedObject;
						Vector3? vector;
						if (purchasedObject == null)
						{
							vector = null;
						}
						else
						{
							Transform transform = purchasedObject.transform;
							vector = ((transform != null) ? new Vector3?(transform.position) : null);
						}
						orb.origin = (vector ?? body.corePosition);
						goldOrb.target = body.mainHurtBox;
						goldOrb.goldAmount = (uint)(0.1f * (float)moneyCost);
						OrbManager.instance.AddOrb(goldOrb);
					}
					GameObject purchasedObject2 = context.purchasedObject;
					ShopTerminalBehavior shopTerminalBehavior = (purchasedObject2 != null) ? purchasedObject2.GetComponent<ShopTerminalBehavior>() : null;
					if (shopTerminalBehavior && shopTerminalBehavior.serverMultiShopController)
					{
						flag = true;
						shopTerminalBehavior.serverMultiShopController.SetCloseOnTerminalPurchase(context.purchasedObject.GetComponent<PurchaseInteraction>(), false);
					}
					if (flag)
					{
						if (body.hasAuthority)
						{
							body.equipmentSlot.OnEquipmentExecuted();
							return;
						}
						body.equipmentSlot.CallCmdOnEquipmentExecuted();
					}
				}
			}
		}

		// Token: 0x0400435E RID: 17246
		private const float refundPercentage = 0.1f;
	}
}
