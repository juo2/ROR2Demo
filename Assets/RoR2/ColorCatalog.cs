using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200050A RID: 1290
	public class ColorCatalog
	{
		// Token: 0x06001790 RID: 6032 RVA: 0x00067634 File Offset: 0x00065834
		static ColorCatalog()
		{
			ColorCatalog.indexToColor32[1] = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			ColorCatalog.indexToColor32[2] = new Color32(119, byte.MaxValue, 23, byte.MaxValue);
			ColorCatalog.indexToColor32[3] = new Color32(231, 84, 58, byte.MaxValue);
			ColorCatalog.indexToColor32[4] = new Color32(48, 127, byte.MaxValue, byte.MaxValue);
			ColorCatalog.indexToColor32[5] = new Color32(byte.MaxValue, 128, 0, byte.MaxValue);
			ColorCatalog.indexToColor32[6] = new Color32(235, 232, 122, byte.MaxValue);
			ColorCatalog.indexToColor32[7] = new Color32(231, 84, 58, byte.MaxValue);
			ColorCatalog.indexToColor32[8] = new Color32(239, 235, 26, byte.MaxValue);
			ColorCatalog.indexToColor32[9] = new Color32(206, 41, 41, byte.MaxValue);
			ColorCatalog.indexToColor32[10] = new Color32(100, 100, 100, byte.MaxValue);
			ColorCatalog.indexToColor32[11] = Color32.Lerp(new Color32(142, 56, 206, byte.MaxValue), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), 0.575f);
			ColorCatalog.indexToColor32[12] = new Color32(173, 189, 250, byte.MaxValue);
			ColorCatalog.indexToColor32[13] = Color.yellow;
			ColorCatalog.indexToColor32[14] = new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue);
			ColorCatalog.indexToColor32[15] = new Color32(106, 170, 95, byte.MaxValue);
			ColorCatalog.indexToColor32[16] = new Color32(173, 117, 80, byte.MaxValue);
			ColorCatalog.indexToColor32[17] = new Color32(142, 49, 49, byte.MaxValue);
			ColorCatalog.indexToColor32[18] = new Color32(193, 193, 193, byte.MaxValue);
			ColorCatalog.indexToColor32[19] = new Color32(88, 149, 88, byte.MaxValue);
			ColorCatalog.indexToColor32[20] = new Color32(142, 49, 49, byte.MaxValue);
			ColorCatalog.indexToColor32[21] = new Color32(76, 84, 144, byte.MaxValue);
			ColorCatalog.indexToColor32[22] = new Color32(189, 180, 60, byte.MaxValue);
			ColorCatalog.indexToColor32[23] = new Color32(200, 80, 0, byte.MaxValue);
			ColorCatalog.indexToColor32[24] = new Color32(140, 114, 219, byte.MaxValue);
			ColorCatalog.indexToColor32[25] = new Color32(237, 127, 205, byte.MaxValue);
			ColorCatalog.indexToColor32[26] = new Color32(163, 77, 132, byte.MaxValue);
			ColorCatalog.indexToColor32[27] = new Color32(244, 173, 250, byte.MaxValue);
			for (ColorCatalog.ColorIndex colorIndex = ColorCatalog.ColorIndex.None; colorIndex < ColorCatalog.ColorIndex.Count; colorIndex++)
			{
				ColorCatalog.indexToHexString[(int)colorIndex] = Util.RGBToHex(ColorCatalog.indexToColor32[(int)colorIndex]);
			}
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00067A8E File Offset: 0x00065C8E
		public static Color32 GetColor(ColorCatalog.ColorIndex colorIndex)
		{
			if (colorIndex < ColorCatalog.ColorIndex.None || colorIndex >= ColorCatalog.ColorIndex.Count)
			{
				colorIndex = ColorCatalog.ColorIndex.Error;
			}
			return ColorCatalog.indexToColor32[(int)colorIndex];
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00067AA8 File Offset: 0x00065CA8
		public static string GetColorHexString(ColorCatalog.ColorIndex colorIndex)
		{
			if (colorIndex < ColorCatalog.ColorIndex.None || colorIndex >= ColorCatalog.ColorIndex.Count)
			{
				colorIndex = ColorCatalog.ColorIndex.Error;
			}
			return ColorCatalog.indexToHexString[(int)colorIndex];
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00067ABE File Offset: 0x00065CBE
		public static Color GetMultiplayerColor(int playerSlot)
		{
			if (playerSlot >= 0 && playerSlot < ColorCatalog.multiplayerColors.Length)
			{
				return ColorCatalog.multiplayerColors[playerSlot];
			}
			return Color.black;
		}

		// Token: 0x04001D13 RID: 7443
		private static readonly Color32[] indexToColor32 = new Color32[28];

		// Token: 0x04001D14 RID: 7444
		private static readonly string[] indexToHexString = new string[28];

		// Token: 0x04001D15 RID: 7445
		private static readonly Color[] multiplayerColors = new Color[]
		{
			new Color32(252, 62, 62, byte.MaxValue),
			new Color32(62, 109, 252, byte.MaxValue),
			new Color32(129, 252, 62, byte.MaxValue),
			new Color32(252, 241, 62, byte.MaxValue)
		};

		// Token: 0x0200050B RID: 1291
		public enum ColorIndex
		{
			// Token: 0x04001D17 RID: 7447
			None,
			// Token: 0x04001D18 RID: 7448
			Tier1Item,
			// Token: 0x04001D19 RID: 7449
			Tier2Item,
			// Token: 0x04001D1A RID: 7450
			Tier3Item,
			// Token: 0x04001D1B RID: 7451
			LunarItem,
			// Token: 0x04001D1C RID: 7452
			Equipment,
			// Token: 0x04001D1D RID: 7453
			Interactable,
			// Token: 0x04001D1E RID: 7454
			Teleporter,
			// Token: 0x04001D1F RID: 7455
			Money,
			// Token: 0x04001D20 RID: 7456
			Blood,
			// Token: 0x04001D21 RID: 7457
			Unaffordable,
			// Token: 0x04001D22 RID: 7458
			Unlockable,
			// Token: 0x04001D23 RID: 7459
			LunarCoin,
			// Token: 0x04001D24 RID: 7460
			BossItem,
			// Token: 0x04001D25 RID: 7461
			Error,
			// Token: 0x04001D26 RID: 7462
			EasyDifficulty,
			// Token: 0x04001D27 RID: 7463
			NormalDifficulty,
			// Token: 0x04001D28 RID: 7464
			HardDifficulty,
			// Token: 0x04001D29 RID: 7465
			Tier1ItemDark,
			// Token: 0x04001D2A RID: 7466
			Tier2ItemDark,
			// Token: 0x04001D2B RID: 7467
			Tier3ItemDark,
			// Token: 0x04001D2C RID: 7468
			LunarItemDark,
			// Token: 0x04001D2D RID: 7469
			BossItemDark,
			// Token: 0x04001D2E RID: 7470
			WIP,
			// Token: 0x04001D2F RID: 7471
			Artifact,
			// Token: 0x04001D30 RID: 7472
			VoidItem,
			// Token: 0x04001D31 RID: 7473
			VoidItemDark,
			// Token: 0x04001D32 RID: 7474
			VoidCoin,
			// Token: 0x04001D33 RID: 7475
			Count
		}
	}
}
