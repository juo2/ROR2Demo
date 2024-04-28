using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200050F RID: 1295
	public class CostTypeDef
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00067B6D File Offset: 0x00065D6D
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x00067B64 File Offset: 0x00065D64
		public CostTypeDef.BuildCostStringDelegate buildCostString { private get; set; } = new CostTypeDef.BuildCostStringDelegate(CostTypeDef.BuildCostStringDefault);

		// Token: 0x06001799 RID: 6041 RVA: 0x00067B78 File Offset: 0x00065D78
		public void BuildCostString(int cost, [NotNull] StringBuilder stringBuilder)
		{
			this.buildCostString(this, new CostTypeDef.BuildCostStringContext
			{
				cost = cost,
				stringBuilder = stringBuilder
			});
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00067BAA File Offset: 0x00065DAA
		public static void BuildCostStringDefault(CostTypeDef costTypeDef, CostTypeDef.BuildCostStringContext context)
		{
			context.stringBuilder.Append(Language.GetStringFormatted(costTypeDef.costStringFormatToken, new object[]
			{
				context.cost
			}));
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00067BE0 File Offset: 0x00065DE0
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x00067BD7 File Offset: 0x00065DD7
		public CostTypeDef.GetCostColorDelegate getCostColor { private get; set; } = new CostTypeDef.GetCostColorDelegate(CostTypeDef.GetCostColorDefault);

		// Token: 0x0600179D RID: 6045 RVA: 0x00067BE8 File Offset: 0x00065DE8
		public Color32 GetCostColor(bool forWorldDisplay)
		{
			return this.getCostColor(this, new CostTypeDef.GetCostColorContext
			{
				forWorldDisplay = forWorldDisplay
			});
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00067C14 File Offset: 0x00065E14
		public static Color32 GetCostColorDefault(CostTypeDef costTypeDef, CostTypeDef.GetCostColorContext context)
		{
			Color32 color = ColorCatalog.GetColor(costTypeDef.colorIndex);
			if (context.forWorldDisplay)
			{
				float h;
				float num;
				float num2;
				Color.RGBToHSV(color, out h, out num, out num2);
				if (costTypeDef.saturateWorldStyledCostString && num > 0f)
				{
					num = 1f;
				}
				if (costTypeDef.darkenWorldStyledCostString)
				{
					num2 *= 0.5f;
				}
				color = Color.HSVToRGB(h, num, num2);
			}
			return color;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00067C84 File Offset: 0x00065E84
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x00067C7B File Offset: 0x00065E7B
		public CostTypeDef.BuildCostStringStyledDelegate buildCostStringStyled { private get; set; } = new CostTypeDef.BuildCostStringStyledDelegate(CostTypeDef.BuildCostStringStyledDefault);

		// Token: 0x060017A1 RID: 6049 RVA: 0x00067C8C File Offset: 0x00065E8C
		public void BuildCostStringStyled(int cost, [NotNull] StringBuilder stringBuilder, bool forWorldDisplay, bool includeColor = true)
		{
			this.buildCostStringStyled(this, new CostTypeDef.BuildCostStringStyledContext
			{
				cost = cost,
				forWorldDisplay = forWorldDisplay,
				stringBuilder = stringBuilder,
				includeColor = includeColor
			});
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00067CD0 File Offset: 0x00065ED0
		public static void BuildCostStringStyledDefault(CostTypeDef costTypeDef, CostTypeDef.BuildCostStringStyledContext context)
		{
			StringBuilder stringBuilder = context.stringBuilder;
			stringBuilder.Append("<nobr>");
			if (costTypeDef.costStringStyle != null)
			{
				stringBuilder.Append("<style=");
				stringBuilder.Append(costTypeDef.costStringStyle);
				stringBuilder.Append(">");
			}
			if (context.includeColor)
			{
				Color32 costColor = costTypeDef.GetCostColor(context.forWorldDisplay);
				stringBuilder.Append("<color=#");
				stringBuilder.AppendColor32RGBHexValues(costColor);
				stringBuilder.Append(">");
			}
			costTypeDef.BuildCostString(context.cost, context.stringBuilder);
			if (context.includeColor)
			{
				stringBuilder.Append("</color>");
			}
			if (costTypeDef.costStringStyle != null)
			{
				stringBuilder.Append("</style>");
			}
			stringBuilder.Append("</nobr>");
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00067DA1 File Offset: 0x00065FA1
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x00067D98 File Offset: 0x00065F98
		public CostTypeDef.IsAffordableDelegate isAffordable { private get; set; }

		// Token: 0x060017A5 RID: 6053 RVA: 0x00067DAC File Offset: 0x00065FAC
		public bool IsAffordable(int cost, Interactor activator)
		{
			return this.isAffordable(this, new CostTypeDef.IsAffordableContext
			{
				cost = cost,
				activator = activator
			});
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00067DE7 File Offset: 0x00065FE7
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x00067DDE File Offset: 0x00065FDE
		public CostTypeDef.PayCostDelegate payCost { private get; set; }

		// Token: 0x060017A8 RID: 6056 RVA: 0x00067DF0 File Offset: 0x00065FF0
		public CostTypeDef.PayCostResults PayCost(int cost, Interactor activator, GameObject purchasedObject, Xoroshiro128Plus rng, ItemIndex avoidedItemIndex)
		{
			CostTypeDef.PayCostResults payCostResults = new CostTypeDef.PayCostResults();
			CharacterBody component = activator.GetComponent<CharacterBody>();
			this.payCost(this, new CostTypeDef.PayCostContext
			{
				cost = cost,
				activator = activator,
				activatorBody = component,
				activatorMaster = (component ? component.master : null),
				purchasedObject = purchasedObject,
				results = payCostResults,
				rng = rng,
				avoidedItemIndex = avoidedItemIndex
			});
			return payCostResults;
		}

		// Token: 0x04001D4E RID: 7502
		public string name;

		// Token: 0x04001D4F RID: 7503
		public ItemTier itemTier = ItemTier.NoTier;

		// Token: 0x04001D50 RID: 7504
		public ColorCatalog.ColorIndex colorIndex = ColorCatalog.ColorIndex.Error;

		// Token: 0x04001D51 RID: 7505
		public string costStringFormatToken;

		// Token: 0x04001D52 RID: 7506
		public string costStringStyle;

		// Token: 0x04001D53 RID: 7507
		public bool saturateWorldStyledCostString = true;

		// Token: 0x04001D54 RID: 7508
		public bool darkenWorldStyledCostString = true;

		// Token: 0x02000510 RID: 1296
		// (Invoke) Token: 0x060017AB RID: 6059
		public delegate void BuildCostStringDelegate(CostTypeDef costTypeDef, CostTypeDef.BuildCostStringContext context);

		// Token: 0x02000511 RID: 1297
		public struct BuildCostStringContext
		{
			// Token: 0x04001D5A RID: 7514
			public StringBuilder stringBuilder;

			// Token: 0x04001D5B RID: 7515
			public int cost;
		}

		// Token: 0x02000512 RID: 1298
		// (Invoke) Token: 0x060017AF RID: 6063
		public delegate Color32 GetCostColorDelegate(CostTypeDef costTypeDef, CostTypeDef.GetCostColorContext context);

		// Token: 0x02000513 RID: 1299
		public struct GetCostColorContext
		{
			// Token: 0x04001D5C RID: 7516
			public bool forWorldDisplay;
		}

		// Token: 0x02000514 RID: 1300
		// (Invoke) Token: 0x060017B3 RID: 6067
		public delegate void BuildCostStringStyledDelegate(CostTypeDef costTypeDef, CostTypeDef.BuildCostStringStyledContext context);

		// Token: 0x02000515 RID: 1301
		public struct BuildCostStringStyledContext
		{
			// Token: 0x04001D5D RID: 7517
			public StringBuilder stringBuilder;

			// Token: 0x04001D5E RID: 7518
			public int cost;

			// Token: 0x04001D5F RID: 7519
			public bool forWorldDisplay;

			// Token: 0x04001D60 RID: 7520
			public bool includeColor;
		}

		// Token: 0x02000516 RID: 1302
		// (Invoke) Token: 0x060017B7 RID: 6071
		public delegate bool IsAffordableDelegate(CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context);

		// Token: 0x02000517 RID: 1303
		public struct IsAffordableContext
		{
			// Token: 0x04001D61 RID: 7521
			public int cost;

			// Token: 0x04001D62 RID: 7522
			public Interactor activator;
		}

		// Token: 0x02000518 RID: 1304
		// (Invoke) Token: 0x060017BB RID: 6075
		public delegate void PayCostDelegate(CostTypeDef costTypeDef, CostTypeDef.PayCostContext context);

		// Token: 0x02000519 RID: 1305
		public struct PayCostContext
		{
			// Token: 0x04001D63 RID: 7523
			public int cost;

			// Token: 0x04001D64 RID: 7524
			public Interactor activator;

			// Token: 0x04001D65 RID: 7525
			public CharacterBody activatorBody;

			// Token: 0x04001D66 RID: 7526
			public CharacterMaster activatorMaster;

			// Token: 0x04001D67 RID: 7527
			public GameObject purchasedObject;

			// Token: 0x04001D68 RID: 7528
			public CostTypeDef.PayCostResults results;

			// Token: 0x04001D69 RID: 7529
			public Xoroshiro128Plus rng;

			// Token: 0x04001D6A RID: 7530
			public ItemIndex avoidedItemIndex;
		}

		// Token: 0x0200051A RID: 1306
		public class PayCostResults
		{
			// Token: 0x04001D6B RID: 7531
			public List<ItemIndex> itemsTaken = new List<ItemIndex>();

			// Token: 0x04001D6C RID: 7532
			public List<EquipmentIndex> equipmentTaken = new List<EquipmentIndex>();
		}
	}
}
