using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200057D RID: 1405
	public static class DamageColor
	{
		// Token: 0x0600193A RID: 6458 RVA: 0x0006CD64 File Offset: 0x0006AF64
		static DamageColor()
		{
			DamageColor.colors[0] = Color.white;
			DamageColor.colors[1] = new Color(0.32941177f, 0.9882353f, 0.1764706f);
			DamageColor.colors[2] = new Color(0.79607844f, 0.1882353f, 0.1882353f);
			DamageColor.colors[8] = new Color(0.9372549f, 0.09411765f, 0.09411765f);
			DamageColor.colors[9] = new Color32(237, 127, 205, byte.MaxValue);
			DamageColor.colors[3] = new Color(0.827451f, 0.7490196f, 0.3137255f);
			DamageColor.colors[4] = new Color(0.76862746f, 0.96862745f, 0.34901962f);
			DamageColor.colors[5] = new Color(0.9372549f, 0.5176471f, 0.20392157f);
			DamageColor.colors[7] = new Color(0.6392157f, 0.2f, 0.20784314f);
			DamageColor.colors[10] = new Color(0.92156863f, 0.4509804f, 0.827451f);
			DamageColor.colors[11] = new Color(1f, 0.92156863f, 0.627451f);
			DamageColor.colors[12] = new Color(1f, 0.53333336f, 0.54509807f);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0006CEED File Offset: 0x0006B0ED
		public static Color FindColor(DamageColorIndex colorIndex)
		{
			if (colorIndex < DamageColorIndex.Default || colorIndex >= DamageColorIndex.Count)
			{
				return Color.white;
			}
			return DamageColor.colors[(int)colorIndex];
		}

		// Token: 0x04001F86 RID: 8070
		private static Color[] colors = new Color[13];
	}
}
