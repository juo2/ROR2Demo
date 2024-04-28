using System;

namespace RoR2
{
	// Token: 0x0200092C RID: 2348
	public struct HullDef
	{
		// Token: 0x0600350C RID: 13580 RVA: 0x000E097C File Offset: 0x000DEB7C
		static HullDef()
		{
			HullDef.hullDefs[0] = new HullDef
			{
				height = 2f,
				radius = 0.5f
			};
			HullDef.hullDefs[1] = new HullDef
			{
				height = 8f,
				radius = 1.8f
			};
			HullDef.hullDefs[2] = new HullDef
			{
				height = 20f,
				radius = 5f
			};
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000E0A18 File Offset: 0x000DEC18
		public static HullDef Find(HullClassification hullClassification)
		{
			return HullDef.hullDefs[(int)hullClassification];
		}

		// Token: 0x040035FA RID: 13818
		public float height;

		// Token: 0x040035FB RID: 13819
		public float radius;

		// Token: 0x040035FC RID: 13820
		private static HullDef[] hullDefs = new HullDef[3];
	}
}
